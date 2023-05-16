using UnityEngine;

public class SortUnit : MonoBehaviour
{   
    [SerializeField] private int _value;

    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _comparedColor;
    [SerializeField] private Color _sortedColor;

    private Material _material;

    private bool _sorted;
    public bool Sorted 
    { 
        get => _sorted; 
        set 
        {
            _sorted = value;
            if (value == true)
            {
                _material.color = _sortedColor;
            }
            else
            {
                _material.color = _defaultColor;
            }
        }
    }

    private bool _compared;
    public bool Compared 
    { 
        get => _compared; 
        set
        {
            _compared = value;
            if (value == true)
            {
                _material.color = _comparedColor;
            }
            else
            {
                _material.color = _defaultColor;
            }
        }
    }

    public int Value 
    { 
        get => _value; 
        set => _value = value; 
    }

    protected void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = _defaultColor;
    }
}