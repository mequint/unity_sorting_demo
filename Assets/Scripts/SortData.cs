public class SortData
{
    public int Comparisons { get; set; }
    public int Swaps { get; set; }

    public void Reset()
    {
        Comparisons = 0;
        Swaps = 0;
    }
}
