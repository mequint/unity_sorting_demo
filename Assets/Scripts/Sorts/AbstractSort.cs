using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting.Sorts
{
    public abstract class AbstractSort : ISort
    {
        public Action InrceaseComparisonCount { get; set; }
        public Action<int> IncreaseAssignmentCount { get; set; }
        public Action SortComplete { get; set; }
        public Action<int> FixVisualization { get; set; }

        public abstract string Name { get; }
        public abstract IEnumerator Execute(List<SortUnit> units, float time = 0);

        protected bool Compare(SortUnit lhs, SortUnit rhs)
        {
            InrceaseComparisonCount?.Invoke();
            lhs.Compared = true;
            rhs.Compared = true;

            return lhs.Value > rhs.Value;
        }

        protected void SetCompared(List<SortUnit> units, bool compared)
        {
            foreach (var unit in units)
            {
                unit.Compared = compared;
            }
        }

        protected void SetSorted(List<SortUnit> units, bool sorted)
        {
            foreach (var unit in units)
            {
                unit.Sorted = sorted;
            }
        }
    }
}