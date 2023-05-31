using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sorting.Sorts
{
    public class BubbleSort : AbstractSort
    {
        public override string Name => "Bubble Sort";
        
        public override IEnumerator Execute(List<SortUnit> units, float time = 0)
        {
            for (int i = 0; i < units.Count - 1; ++i)
            {
                for (int j = 0; j < units.Count - i - 1; ++j)
                {
                    if (Compare(units[j], units[j + 1]))
                    {
                        Swap(units, j, j + 1);

                        yield return new WaitForSeconds(time);

                        SetCompared(units, false);
                    }
                }

                units[units.Count - i - 1].Sorted = true;
            }

            units.First().Sorted = true;

            SortComplete?.Invoke();
        }

        private void Swap(List<SortUnit> units, int leftIndex, int rightIndex)
        {
            var temp = units[leftIndex];
            units[leftIndex] = units[rightIndex];
            units[rightIndex] = temp;

            IncreaseAssignmentCount?.Invoke(2);

            FixVisualization?.Invoke(leftIndex);
            FixVisualization?.Invoke(rightIndex);            
        }
    }
}