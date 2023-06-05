using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorts
{
    public class MergeSort : AbstractSort
    {
        public override string Name => "Merge Sort";

        private List<SortUnit> _units;

        private float _waitTime;

        public override IEnumerator Execute(List<SortUnit> units, float time = 0)
        {
            _units = units;
            _waitTime = time;
            yield return Sort(0, units.Count - 1);
            
            SortComplete?.Invoke();
        }

        private IEnumerator Merge(int left, int middle, int right, int level = 0)
        {
            ClearCompares(_units, left, right);

            var (leftArray, rightArray) = CreateSubArrays(left, middle, right);

            // Merge the temporary arrays
            var i = 0;
            var j = 0;
            var k = left;

            while (i < leftArray.Length && j < rightArray.Length)
            {
                IncreaseAssignmentCount?.Invoke(1);

                if (Compare(leftArray[i], rightArray[j]))
                {
                    _units[k] = leftArray[i];
                    i++;
                }
                else
                {
                    _units[k] = rightArray[j];
                    j++;
                }
            
                UpdateVisualization(k, level);
                k++;

                yield return new WaitForSeconds(_waitTime);
            }

            while (i < leftArray.Length)
            {
                IncreaseAssignmentCount?.Invoke(1);

                _units[k] = leftArray[i];

                UpdateVisualization(k, level);
                
                i++;
                k++;


                yield return new WaitForSeconds(_waitTime);
            }
            
            while (j < rightArray.Length)
            {
                IncreaseAssignmentCount?.Invoke(1);

                _units[k] = rightArray[j];

                UpdateVisualization(k, level);
                
                j++;
                k++;

                yield return new WaitForSeconds(_waitTime);
            }
        }

        private IEnumerator Sort(int left, int right, int level = 0)
        {
            if (left < right)
            {
                var middle = left + (right - left) / 2;

                yield return Sort(left, middle, level + 1);
                yield return Sort(middle + 1, right, level + 1);

                yield return Merge(left, middle, right, level);
            }
        }

        protected override bool Compare(SortUnit lhs, SortUnit rhs)
        {
            InrceaseComparisonCount?.Invoke();
            lhs.Compared = true;
            rhs.Compared = true;

            return lhs.Value <= rhs.Value;
        }

        private (SortUnit[] leftArray, SortUnit[] rightArray) CreateSubArrays(int left, int middle, int right)
        {
            // Create temporary arrays
            var n1 = middle - left + 1;
            var n2 = right - middle;

            var leftArray = new SortUnit[n1];
            var rightArray = new SortUnit[n2];

            for (int i = 0; i < n1; ++i)
            {
                leftArray[i] = _units[left + i];
            }

            for (int j = 0; j < n2; ++j)
            {
                rightArray[j] = _units[middle + 1 + j];
            }

            return (leftArray, rightArray);
        }

        private void UpdateVisualization(int index, int level)
        {
            if (level == 0)
            {
                _units[index].Sorted = true;
            }

            FixVisualization?.Invoke(index);
        }
    }
}