using System.Data;

namespace SQLLibrary.Data.Select
{
    internal class SelectExpression : BaseSelectExpression<IExecute>,
                                      ISelectOptions<IExecute>,
                                      ISelectJoin_Where<IExecute>,
                                      ISelect_Column_Join_Where<IExecute>,
                                      IExecute
    {
        public SelectExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public override bool Execute(out DataTable dataTable)
        {
            dataTable = null;
            return _sql.Execute(_sql.GetSelectCommand(GetSelectData()), out dataTable);
        }
        public ISelectJoin_Where<IExecute> SelectAll()
        {
            selectTargetList.Add("*");
            return (ISelectJoin_Where<IExecute>)(object)this;
        }

        public ISelect_Column_Join_Where<IExecute> SelectColumn(string columnName)
        {
            selectTargetList.Add(columnName);
            return (ISelect_Column_Join_Where<IExecute>)(object)this;
        }
    }
}
