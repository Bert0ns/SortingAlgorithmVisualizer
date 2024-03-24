using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithmVisualizer
{
    class MergeSortEngine : ISortEngine
    {
        //private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public MergeSortEngine(int[] TheArray_In, Graphics g_In, int maxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        public void NextStep()
        {
            //Merge Sort
            MergeSort(0, TheArray.Length - 1);
        }

        void MergeSort(int l, int r)
        {
            // Main function that
            // sorts arr[l..r] using
            // merge()

            if (l < r)
            {
                // Find the middle
                // point
                int m = l + (r - l) / 2;

                // Sort first and
                // second halves
                MergeSort(l, m);
                MergeSort(m + 1, r);

                // Merge the sorted halves
                Merge(l, m, r);
            }
        }

        void Merge(int l, int m, int r)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = m - l + 1;
            int n2 = r - m;

            // Create temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
            {
                L[i] = TheArray[l + i];
            }
            for (j = 0; j < n2; ++j)
            {
                R[j] = TheArray[m + 1 + j];
            }
               
            // Merge the temp arrays

            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged
            // subarray array
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    TheArray[k] = L[i];
                    DrawBar(k, TheArray[k]);
                    i++;
                }
                else
                {
                    TheArray[k] = R[j];
                    DrawBar(k, TheArray[k]);
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                TheArray[k] = L[i];
                DrawBar(k, TheArray[k]);
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                TheArray[k] = R[j];
                DrawBar(k, TheArray[k]);
                j++;
                k++;
            }
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

