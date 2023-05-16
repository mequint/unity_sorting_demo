using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorts
{
    public class SelectionSort : AbstractSort
    {
        public override string Name => "Selection Sort";

        public override IEnumerator Execute(List<SortUnit> units, float time = 0.0f)
        {
            for (int i = 0; i < units.Count; ++i)
            {
                var minIndex = i;
                for (int j = i + 1; j < units.Count; ++j)
                {
                    if (Compare(units[minIndex], units[j]))
                    {
                        units[minIndex].Compared = false;
                        minIndex = j;
                        
                        yield return new WaitForSeconds(time);
                    }

                    units[j].Compared = false;
                }

                Swap(units, i, minIndex);
                units[i].Sorted = true;
            }

            SortComplete?.Invoke();
        }
    }
}