namespace SQLLibrary.Data.Condition
{
    internal class JoinDefinition
    {
        public Joins JoinType { get; set; }
        public string JoinedTable { get; set; }
        public string JoinedColumn { get; set; }
        public string Table { get; set; }
        public string Column { get; set; }
    }
}
