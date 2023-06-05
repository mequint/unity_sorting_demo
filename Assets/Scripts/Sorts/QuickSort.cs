using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorts
{
    public class QuickSort : AbstractSort
    {
        public override string Name => "Quick Sort";

        private float _waitTime;
        private List<SortUnit> _units;

        public override IEnumerator Execute(List<SortUnit> units, float time = 0)
        {
            _waitTime = time;
            _units = units;

            yield return Sort(0, _units.Count - 1);

            SetSorted(_units, true);
            SortComplete?.Invoke();
        }

        private IEnumerator Partition(int low, int high, Action<int> onNextPivot)
        {
            SetCompared(_units, false);

            var pivot = _units[high];

            var i = (low - 1);

            for (int j = low; j <= high - 1; ++j)
            {
                InrceaseComparisonCount?.Invoke();
                if (Compare(_units[j], pivot))
                {
                    ++i;
                    Swap(i, j);

                    yield return new WaitForSeconds(_waitTime);

                    _units[i].Compared = false;
                    _units[j].Compared = false;
                }
            }

            Swap(i + 1, high);

            if (onNextPivot != null)
            {
                onNextPivot(i + 1);
            }
        }

        private IEnumerator Sort(int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = -1;
                yield return Partition(low, high, output => partitionIndex = output);

                yield return Sort(low, partitionIndex - 1);
                yield return Sort(partitionIndex + 1, high);
            }
        }

        private void Swap(int leftIndex, int rightIndex)
        {
            var temp = _units[leftIndex];
            _units[leftIndex] = _units[rightIndex];
            _units[rightIndex] = temp;

            IncreaseAssignmentCount?.Invoke(2);

            FixVisualization?.Invoke(leftIndex);
            FixVisualization?.Invoke(rightIndex);            
        }

        protected override bool Compare(SortUnit lhs, SortUnit rhs)
        {
            InrceaseComparisonCount?.Invoke();
            lhs.Compared = true;
            rhs.Compared = true;

            return lhs.Value < rhs.Value;
        }
    }
}