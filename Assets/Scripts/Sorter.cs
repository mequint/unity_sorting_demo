using System;
using System.Collections.Generic;
using System.Linq;
using Sorting.Sorts;
using UnityEngine;

namespace Sorting
{
    public class Sorter : MonoBehaviour
    {
        public Action IncreaseComparisonCount { get; set; }
        public Action<int> IncreaseAssignmentCount { get; set; }

        [SerializeField] private SortUnit _sortUnit;
        [SerializeField] private int _values;

        private float _scaleValue = 1.0f;
        private List<SortUnit> _units = new List<SortUnit>();

        public bool Sorting { get; private set; } = false;

        public List<AbstractSort> SortTypes { get; } = new List<AbstractSort>
        {
            new BubbleSort(),
            new SelectionSort(),
            new InsertionSort(),
            new MergeSort()
        };

        protected void Start()
        {
            _scaleValue = _sortUnit.transform.localScale.x;

            for (int i = 1; i <= _values; ++i)
            {
                var position = new Vector3(IndexPosition(i - 1), i * _scaleValue / 2.0f, 0.0f);
                var scale = new Vector3(_scaleValue, i * _scaleValue, _scaleValue);

                var instance = Instantiate(_sortUnit, position, Quaternion.identity);
                instance.transform.localScale = scale;
                instance.Value = i;

                _units.Add(instance);
            }

            foreach (var sort in SortTypes)
            {
                sort.FixVisualization += FixPosition;
                sort.InrceaseComparisonCount = IncreaseComparisonCount;
                sort.IncreaseAssignmentCount = IncreaseAssignmentCount;
                sort.SortComplete += SortComplete;
            }
        }

        public void Reset()
        {
            StopAllCoroutines();
            
            Sorting = false;

            for (int i = 1; i <= _units.Count; ++i)
            {
                var position = new Vector3(IndexPosition(i - 1), i * _scaleValue / 2.0f, 0.0f);
                var scale = new Vector3(_scaleValue, i * _scaleValue, _scaleValue); 

                var unit = _units[i - 1];
                unit.transform.localPosition = position;
                unit.transform.localScale = scale;
                unit.Value = i;
                unit.Compared = false;
                unit.Sorted = false;
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < _units.Count; ++i)
            {
                var swapIndex = UnityEngine.Random.Range(i, _units.Count);
        
                var swap = _units[swapIndex];
                _units[swapIndex] = _units[i];
                _units[i] = swap;

                FixPosition(swapIndex);
                FixPosition(i);

                _units[i].Compared = false;
                _units[i].Sorted = false;
            }
        }

        // Bubble sort (variant) from smallest to largest
        public void Sort(string sortName, float swapTime)
        {
            var sort = SortTypes.FirstOrDefault(x => x.Name == sortName);
            if (sort == null) 
            {
                Debug.LogError($"Oops - {sortName} is not registered.");
                return;
            }

            if (!Sorting)
            {   
                Sorting = true;
                StartCoroutine(sort.Execute(_units, swapTime));
            }
        }

        #region Helper and Action methods

        private void FixPosition(int index)
        {
            var position = _units[index].transform.position;
            position.x = IndexPosition(index);
            _units[index].transform.position = position;
        }

        private float IndexPosition(int index) => index * _scaleValue - _values * _scaleValue / 2.0f;

        private void SortComplete() => Sorting = false;
    
        #endregion Helper and Action methods
    }
}