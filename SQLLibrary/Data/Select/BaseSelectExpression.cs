using SQLLibrary.Data.Condition;
using System.Collections.Generic;

namespace SQLLibrary.Data.Select
{
    internal abstract class BaseSelectExpression<TComplete> : ConditionExpression<TComplete>
    {
        protected readonly List<string> selectTargetList = new List<string>();
        public List<string> SelectTargetList { get { return selectTargetList; } }
        public JoinDefinition JoinDef { get; private set; }
        public BaseSelectExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public SelectDataDefinition GetSelectData()
        {
            return new SelectDataDefinition
            {
                TableName = TableName,
                SelectTargetList = SelectTargetList,
                JoinDef = JoinDef,
                ConditionBracket = ConditionBracket
            };
        }

        public IWhere<TComplete> InnerJoin(string joinedColumn, string table, string column)
        {
            JoinDef = new JoinDefinition
            {
                JoinType = Joins.InnerJoin,
                JoinedTable = TableName,
                JoinedColumn = joinedColumn,
                Table = table,
                Column = column
            };
            return (IWhere<TComplete>)(object)this;
        }

        public IWhere<TComplete> LeftJoin(string joinedColumn, string table, string column)
        {
            JoinDef = new JoinDefinition
            {
                JoinType = Joins.LeftJoin,
                JoinedTable = TableName,
                JoinedColumn = joinedColumn,
                Table = table,
                Column = column
            };
            return (IWhere<TComplete>)(object)this;
        }

        public IWhere<TComplete> RightJoin(string joinedColumn, string table, string column)
        {
            JoinDef = new JoinDefinition
            {
                JoinType = Joins.RightJoin,
                JoinedTable = TableName,
                JoinedColumn = joinedColumn,
                Table = table,
                Column = column
            };
            return (IWhere<TComplete>)(object)this;
        }
    }
}
