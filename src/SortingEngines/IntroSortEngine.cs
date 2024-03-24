using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithmVisualizer
{
    class IntroSortEngine : ISortEngine
    {
        //private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int maxVal;
        Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public IntroSortEngine(int[] TheArray_In, Graphics g_In, int maxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        public void NextStep()
        {
            //IntroSort
            //QuickSort(0, TheArray.Length - 1);
            sortData();
        }

        // A utility function to begin
        // the Introsort module
        private void sortData()
        {
            // Initialise the depthLimit
            // as 2*log(length(data))
            int depthLimit = (int)(2 * Math.Floor(Math.Log(TheArray.Length) / Math.Log(2)));

            this.sortDataUtil(0, TheArray.Length - 1, depthLimit);
        }

        private void maxHeap(int i, int heapN, int begin)
        {
            int temp = TheArray[begin + i - 1];
            int child;

            while (i <= heapN / 2)
            {
                child = 2 * i;

                if (child < heapN && TheArray[begin + child - 1] < TheArray[begin + child])
                    child++;

                if (temp >= TheArray[begin + child - 1])
                    break;

                TheArray[begin + i - 1] = TheArray[begin + child - 1];
                DrawBar(begin + i - 1, TheArray[begin + child - 1]);
                i = child;
            }
            TheArray[begin + i - 1] = temp;
            DrawBar(begin + i - 1, temp);
        }

        // Function to build the
        // heap (rearranging the array)
        private void heapify(int begin, int end, int heapN)
        {
            for (int i = (heapN) / 2; i >= 1; i--)
                maxHeap(i, heapN, begin);
        }

        // main function to do heapsort
        private void heapSort(int begin, int end)
        {
            int heapN = end - begin;

            // Build heap (rearrange array)
            this.heapify(begin, end, heapN);

            // One by one extract an element
            // from heap
            for (int i = heapN; i >= 1; i--)
            {
                // Move current root to end
                Swap(begin, begin + i);

                // call maxHeap() on the
                // reduced heap
                maxHeap(1, i, begin);
            }
        }

        private void insertionSort(int left, int right)
        {
            for (int i = left; i <= right; i++)
            {
                int key = TheArray[i];
                int j = i;

                // Move elements of arr[0..i-1],
                // that are greater than the key,
                // to one position ahead
                // of their current position
                while (j > left && TheArray[j - 1] > key)
                {
                    TheArray[j] = TheArray[j - 1];
                    DrawBar(j, TheArray[j -1]);
                    j--;
                }
                TheArray[j] = key;
                DrawBar(j, key);
            }
        }

        // Function for finding the median
        // of the three elements
        private int findPivot(int a1, int b1, int c1)
        {
            int max = Math.Max(
                      Math.Max(TheArray[a1],
                               TheArray[b1]), TheArray[c1]);
            int min = Math.Min(
                      Math.Min(TheArray[a1],
                               TheArray[b1]), TheArray[c1]);
            int median = max ^ min ^
                         TheArray[a1] ^ TheArray[b1] ^ TheArray[c1];
            if (median == TheArray[a1])
                return a1;
            if (median == TheArray[b1])
                return b1;
            return c1;
        }

        // This function takes the last element
        // as pivot, places the pivot element at
        // its correct position in sorted
        // array, and places all smaller
        // (smaller than pivot) to the left of
        // the pivot and greater elements to
        // the right of the pivot
        private int partition(int low, int high)
        {
            // pivot
            int pivot = TheArray[high];

            // Index of smaller element
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                // If the current element
                // is smaller than or equal
                // to the pivot
                if (TheArray[j] <= pivot)
                {
                    // increment index of
                    // smaller element
                    i++;
                    Swap(i, j);
                }
            }
            Swap(i + 1, high);
            return (i + 1);
        }

        // The main function that implements
        // Introsort low  --> Starting index,
        // high  --> Ending index, depthLimit
        // --> recursion level
        private void sortDataUtil(int begin, int end, int depthLimit)
        {
            if (end - begin > 16)
            {
                if (depthLimit == 0)
                {
                    // if the recursion limit is
                    // occurred call heap sort
                    this.heapSort(begin, end);
                    return;
                }

                depthLimit = depthLimit - 1;
                int pivot = findPivot(begin, begin + ((end - begin) / 2) + 1, end);
                                       
                Swap(pivot, end);

                // p is partitioning index,
                // arr[p] is now at right place
                int p = partition(begin, end);

                // Separately sort elements
                // before partition and after
                // partition
                sortDataUtil(begin, p - 1, depthLimit);
                sortDataUtil(p + 1, end, depthLimit);
            }

            else
            {
                // if the data set is small,
                // call insertion sort
                insertionSort(begin, end);
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

