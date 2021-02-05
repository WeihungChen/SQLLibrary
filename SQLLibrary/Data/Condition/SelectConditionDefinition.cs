using SQLLibrary.Data.Select;

namespace SQLLibrary.Data.Condition
{
    internal class SelectConditionDefinition<TComplete> : ConditionDefinition
    {
        public string ColumnName { get; private set; }
        public SelectConditionExpression<TComplete> Expression { get; private set; }
        public SelectConditionDefinition(string columnName)
        {
            ColumnName = columnName;
            Condition = Conditions.Select;
        }
        
        public ISelectColumn<IWhere<TComplete>, TComplete> CreateExpression(string tableName, TComplete parent)
        {
            Expression = new SelectConditionExpression<TComplete> (tableName, parent);
            return Expression;
        }

        public override SelectDataDefinition GetSelectData()
        {
            return Expression.GetSelectData();
        }

        public override string GetColumnName()
        {
            return ColumnName;
        }
    }
}
