using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorts
{
    public class InsertionSort : AbstractSort
    {
        public override string Name => "Insertion Sort";

        public override IEnumerator Execute(List<SortUnit> units, float time = 0)
        {
            for (int i = 1; i < units.Count; ++i)
            {
                SetCompared(units, false);

                var key = units[i];
                key.Compared = true;
                var j = i - 1;

                while (j >= 0 && Compare(units[j], key))
                {
                    units[j + 1] = units[j];
                    FixVisualization?.Invoke(j+1);

                    j = j - 1;
                    IncreaseAssignmentCount?.Invoke(1);
                    yield return new WaitForSeconds(time);
                }

                units[j + 1] = key;
                IncreaseAssignmentCount?.Invoke(1);
                FixVisualization?.Invoke(j+1);
            }

            SetSorted(units, true);

            SortComplete?.Invoke();
        }
    }
}