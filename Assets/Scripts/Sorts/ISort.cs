using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorts
{
    public interface ISort
    {
        IEnumerator Execute(List<SortUnit> units, float time = 0.0f);
    }
}