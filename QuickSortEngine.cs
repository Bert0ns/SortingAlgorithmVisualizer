using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithmVisualizer
{
    class QuickSortEngine : ISortEngine
    {
        //private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public QuickSortEngine(int[] TheArray_In, Graphics g_In, int maxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        public void NextStep()
        {
            //Quick Sort
            QuickSort(0, TheArray.Length - 1);
        }

        private void QuickSort(int start, int end)
        {
            int pivot;
            if (start < end)
            {
                pivot = Partition(start, end);

                QuickSort(start, pivot - 1);
                QuickSort(pivot + 1, end);
            }
        }

        private int Partition(int start, int end)
        {
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (TheArray[j] <= TheArray[end])
                {
                    i++;
                    Swap(i , j);
                }
            }
            Swap(i + 1, end);

            return i + 1;
        }

        private void Swap(int i, int p)
        {
            int temp = TheArray[i];
            TheArray[i] = TheArray[p];
            TheArray[p] = temp;

            //Ricolora il panel
            //Set colore nero nei vecchi valori
            DrawBar(i, TheArray[i]);
            //Set colore bianco nuovi valori
            DrawBar(p, TheArray[p]);
        }

        private void DrawBar(int position, int height)
        {
            g.FillRectangle(blackBrush, position, 0, 1, maxVal);
            g.FillRectangle(whiteBrush, position, maxVal - TheArray[position], 1, maxVal);
        }

        public bool IsSorted()
        {
            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1])
                {
                    return false;
                }
            }
            Form1.isFinished = true;
            return true;
        }

        public void ReDraw()
        {
            for (int i = 0; i < TheArray.Count(); i++)
            {
                g.FillRectangle(whiteBrush, i, maxVal - TheArray[i], 1, maxVal);
            }
        }
    }
}
