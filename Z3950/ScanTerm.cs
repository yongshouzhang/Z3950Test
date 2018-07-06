
namespace Z3950
{
    public class ScanTerm  
    {
        internal ScanTerm(string term, int occurences)
        {
            Term = term;
            Occurences = occurences;
        }

        public string Term { get; }

        public int Occurences { get; }
    }
}