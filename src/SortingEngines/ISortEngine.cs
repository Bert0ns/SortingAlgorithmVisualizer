using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SortingAlgorithmVisualizer
{
    interface ISortEngine
    {
        void NextStep();
        bool IsSorted();
        void ReDraw();
    }
}
