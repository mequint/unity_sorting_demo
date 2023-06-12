using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sorting.Sorts
{
    public class RadixSort : AbstractSort
    {
        public override string Name => "Radix Sort";

        private float _waitTime = 0.0f;
        private List<SortUnit> _units;

        private SortUnit _max = null;

        public override IEnumerator Execute(List<SortUnit> units, float time = 0)
        {
            _waitTime = time;
            _units = units;
            SetSorted(_units, false);

            yield return FindLargestUnit();

            for (int exp = 1; (_max?.Value ?? 0) / exp > 0; exp *= 10)
            {
                var isLastPass = ((_max?.Value ?? 0) / (exp * 10) == 0);
                yield return CountSort(exp, isLastPass);
            }

            SortComplete?.Invoke();
        }

        private IEnumerator CountSort(int exponent, bool isLastPass)
        {
            SetCompared(_units, false);

            var output = new SortUnit[_units.Count];
            var count = new int[10];

            // This maybe redundant...
            for (int i = 0; i < 10; ++i)
            {
                count[i] = 0;
            }

            // Store count of occurences in count-ing array
            for (int i = 0; i < _units.Count; ++i)
            {
                count[(_units[i].Value / exponent) % 10]++;
            }

            for (int i = 1; i < 10; ++i)
            {
                count[i] += count[i - 1];
            }

            for (int i = _units.Count - 1; i >= 0; --i)
            {
                output[count[(_units[i].Value / exponent) % 10] - 1] = _units[i];
                count[(_units[i].Value / exponent) % 10]--;
            }

            for (int i = 0; i < _units.Count; ++i)
            {
                _units[i] = output[i];
                IncreaseAssignmentCount?.Invoke(1);
                FixVisualization?.Invoke(i);

                if (isLastPass) 
                {
                    _units[i].Sorted = true;
                }
                else
                {
                    // Not a compare but an intermeidate move in this case...
                    _units[i].Compared = true;
                }

                yield return new WaitForSeconds(_waitTime);
            }
        }

        // This assumes an integral value...
        private IEnumerator FindLargestUnit()
        {
            var max = _units.First();
            foreach (var unit in _units)
            {
                if (Compare(unit, max))
                {
                    max = unit;
                    yield return new WaitForSeconds(_waitTime);
                }
            }

            _max = max;
        }
    }
}