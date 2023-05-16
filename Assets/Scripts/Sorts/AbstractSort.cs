using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting.Sorts
{
    public abstract class AbstractSort : ISort
    {
        public Action InrceaseComparisonCount { get; set; }
        public Action IncreaseSwapCount { get; set; }
        public Action SortComplete { get; set; }
        public Action<int> FixVisualization { get; set; }

        public SortData SortData { get; set; } = new SortData();

        public abstract string Name { get; }
        public abstract IEnumerator Execute(List<SortUnit> units, float time = 0);

        // TODO: Move this into Sort Unit...
        protected bool Compare(SortUnit lhs, SortUnit rhs)
        {
            InrceaseComparisonCount?.Invoke();
            lhs.Compared = true;
            rhs.Compared = true;

            return lhs.Value > rhs.Value;
        }

        protected void Swap(List<SortUnit> units, int leftIndex, int rightIndex)
        {
            IncreaseSwapCount?.Invoke();

            var temp = units[leftIndex];
            units[leftIndex] = units[rightIndex];
            units[rightIndex] = temp;

            FixVisualization?.Invoke(leftIndex);
            FixVisualization?.Invoke(rightIndex);            
        }
    }
}