using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorts
{
    public class HeapSort : AbstractSort
    {
        public override string Name => "Heap Sort";

        private float _waitTime;
        private List<SortUnit> _units;
        
        public override IEnumerator Execute(List<SortUnit> units, float time = 0)
        {
            _waitTime = time;
            _units = units;
            yield return Sort();
            SortComplete?.Invoke();
        }

        private IEnumerator Sort()
        {
            // Build heap (rearrange container)
            for (int i = _units.Count / 2 - 1; i >= 0; --i)
            {
                yield return Heapify(_units.Count, i);
            }

            for (int i = _units.Count - 1; i > 0; i--)
            {
                ClearCompares(_units, 0, i);
                Swap(0, i);

                _units[i].Sorted = true;
                yield return Heapify(i, 0);
            }

            _units[0].Sorted = true;
        }

        private IEnumerator Heapify(int length, int index)
        {
            var largest = index;
            var left = 2 * index + 1;
            var right = 2 * index + 2;

            if (left < length && Compare(_units[left], _units[largest]))
            { 
                largest = left;
            }

            if (right < length && Compare(_units[right], _units[largest]))
            {
                largest = right;
            }

            if (largest != index)
            {
                Swap(index, largest);

                yield return new WaitForSeconds(_waitTime);

                yield return Heapify(length, largest);
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
    }
}