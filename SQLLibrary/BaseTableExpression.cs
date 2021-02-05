using System.Data;

namespace SQLLibrary
{
    internal abstract class BaseTableExpression<TComplete>
    {
        protected BaseSQL _sql;

        internal string TableName { get; private set; }

        internal BaseTableExpression(string tableName, BaseSQL sql)
        {
            TableName = tableName;
            _sql = sql;
        }

        public virtual TComplete Complete()
        {
            return (TComplete)(object)this;
        }

        public virtual bool Execute()
        {
            return true;
        }

        public virtual bool Execute(out DataTable dataTable)
        {
            dataTable = null;
            return true;
        }
    }
}
