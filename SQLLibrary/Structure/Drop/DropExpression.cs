namespace SQLLibrary.Structure.Drop
{
    internal class DropExpression : BaseTableExpression<IExecute>,
                                    IDropOptions<IExecute>,
                                    IExecute
    {
        public DropExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public override bool Execute()
        {
            return _sql.Execute(_sql.GetDropCommand(this));
        }
    }
}
