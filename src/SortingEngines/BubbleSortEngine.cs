using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SortingAlgorithmVisualizer
{
    class BubbleSortEngine : ISortEngine
    {
        //private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public BubbleSortEngine(int[] TheArray_In, Graphics g_In, int maxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        public void NextStep()
        {
            //Bubble sort
            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1])
                {
                    Swap(i, i + 1);
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
