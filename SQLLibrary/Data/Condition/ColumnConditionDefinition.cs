namespace SQLLibrary.Data.Condition
{
    internal class ColumnConditionDefinition : ConditionDefinition
    {
        public string ColumnName { get; private set; }
        public Relations Relation { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }

        public ColumnConditionDefinition(string columnName)
        {
            ColumnName = columnName;
            Condition = Conditions.ColumnCondition;
        }
    }
}
