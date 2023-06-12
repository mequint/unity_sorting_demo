using System.Collections.Generic;
using Sorting;
using TMPro;
using UnityEngine;

public class SortScreen : MonoBehaviour
{
    [SerializeField] private Sorter _sorter;
    
    [SerializeField] private TMP_Text _comparisonsCountText;
    [SerializeField] private TMP_Text _assignmentsCountText;
    [SerializeField] private TMP_Dropdown _sortTypeDropdown;

    [SerializeField] private float _waitTime = 0.1f;

    private SortData _sortData;
    private string _sortOption;
    
    protected void Awake()
    {
        _sortData = new SortData();
        
        _sorter.IncreaseComparisonCount += IncreaseComparisonCount;
        _sorter.IncreaseAssignmentCount += IncreaseAssignmentsCount;

        _sortTypeDropdown.ClearOptions();
        
        var options = new List<string>();
        foreach (var sort in _sorter.SortTypes)
        {
            options.Add(sort.Name);
        }
        _sortTypeDropdown.AddOptions(options);

        _sortOption = _sortTypeDropdown.options[0].text;
    }

    public void IncreaseComparisonCount()
    {
         _sortData.Comparisons++;
         _comparisonsCountText.SetText($"{_sortData.Comparisons}");
    }

    public void IncreaseAssignmentsCount(int count)
    {
        _sortData.Assignments += count;
         _assignmentsCountText.SetText($"{_sortData.Assignments}");
    }

    public void OnSortPressed()
    {
        ResetData();
        _sorter.Sort(_sortOption, _waitTime);
    }

    public void OnShufflePressed()
    {
        if (_sorter.Sorting) return;
        
        OnResetPressed();
        _sorter.Shuffle();
    }

    public void OnResetPressed()
    {
        _sorter.Reset();
        ResetData();
    }

    public void OnSortDropdownValueChanged(int index)
    {
        OnResetPressed();
        _sortOption = _sortTypeDropdown.options[index].text;
    }

    private void ResetData()
    {
        _sortData.Reset();
        _comparisonsCountText.SetText($"{_sortData.Comparisons}");
        _assignmentsCountText.SetText($"{_sortData.Assignments}");
    }
}