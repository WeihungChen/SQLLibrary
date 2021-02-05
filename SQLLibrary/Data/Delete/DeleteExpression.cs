namespace SQLLibrary.Data.Delete
{
    internal class DeleteExpression : ConditionExpression<IExecute>,
                                      IDeleteOptions<IExecute>,
                                      IExecute
    {
        public DeleteExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public override bool Execute()
        {
            return _sql.Execute(_sql.GetDeleteCommand(this));
        }
    }
}
