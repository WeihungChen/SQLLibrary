namespace SQLLibrary.Structure.Create
{
    internal class CreateExpression : TableColumnExpression<ICreate_ColumnOptions<IExecute>, ICreate_ColumnOptions_Constraint<IExecute>, IExecute>,
                                      ICreate_ColumnOptions<IExecute>, 
                                      ICreate_ColumnOptions_Constraint<IExecute>,
                                      IExecute
    {
        public CreateExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public IColumnType<ICreate_ColumnOptions<IExecute>, IExecute> WithColumn(string columnName)
        {
            _currentColumn = new ColumnDefinition(columnName);
            ColumnList.Add(_currentColumn);
            return (IColumnType<ICreate_ColumnOptions<IExecute>, IExecute>)(object)this;
        }

        public override bool Execute()
        {
            if (ColumnList.Count < 1)
                return false;

            return _sql.Execute(_sql.GetCreateCommand(this));
        }
    }
}
