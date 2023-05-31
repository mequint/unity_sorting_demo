namespace Sorting
{
    public class SortData
    {
        public int Comparisons { get; set; }
        public int Assignments { get; set; }

        public void Reset()
        {
            Comparisons = 0;
            Assignments = 0;
        }
    }
}