using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithmVisualizer
{
    class QuickSortEngineV2 : ISortEngine
    {
        //private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public QuickSortEngineV2(int[] TheArray_In, Graphics g_In, int maxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        public void NextStep()
        {
            //Quick Sort
            Quick_Sort(TheArray, 0, TheArray.Length - 1);
        }

        private static void Quick_Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }

        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];

            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }
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
