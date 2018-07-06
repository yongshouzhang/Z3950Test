namespace Z3950
{
    public class PrefixQuery :IQuery
    {
        public PrefixQuery(string query)
        {
            QueryString = query;
        }

        public string QueryString { get; set; }
    }
    public class CQLQuery :IQuery
    {
        public CQLQuery(string query)
        {
            QueryString = query;
        }
        public string QueryString { get; set; }
    }
}