using System.Collections.Generic;
using Sorting;
using TMPro;
using UnityEngine;

public class SortScreen : MonoBehaviour
{
    [SerializeField] private Sorter _sorter;
    
    [SerializeField] private TMP_Text _comparisonsCountText;
    [SerializeField] private TMP_Text _swapsCountText;
    [SerializeField] private TMP_Dropdown _sortTypeDropdown;

    private float _swapTime = 0.1f;

    private SortData _sortData;

    private string _sortOption;
    
    protected void Awake()
    {
        _sortData = new SortData();
        _sorter.IncreaseComparisonCount += IncreaseComparisonCount;
        _sorter.IncreaseSwapCount += IncreaseSwapCount;

        _sortTypeDropdown.ClearOptions();
        
        var options = new List<string>();
        foreach (var sort in _sorter.SortTypes)
        {
            options.Add(sort.Name);
        }
        _sortTypeDropdown.AddOptions(options);
    }

    public void IncreaseComparisonCount()
    {
         _sortData.Comparisons++;
         _comparisonsCountText.SetText($"{_sortData.Comparisons}");
    }

    public void IncreaseSwapCount()
    {
        _sortData.Swaps++;
         _swapsCountText.SetText($"{_sortData.Swaps}");
    }

    public void OnSortPressed()
    {
        _sorter.Sort(_sortOption, _swapTime);
    }

    public void OnShufflePressed()
    {
        if (_sorter.Sorting) return;
        
        OnResetPressed();
        _sorter.Shuffle();
    }

    public void OnResetPressed()
    {
        StopAllCoroutines();
        _sortData.Reset();
        _comparisonsCountText.SetText($"{_sortData.Comparisons}");
        _swapsCountText.SetText($"{_sortData.Swaps}");
        _sorter.Reset();
    }

    public void OnSortDropdownValueChanged(int index)
    {
        OnResetPressed();
        _sortOption = _sortTypeDropdown.options[index].text;
    }
}
