using System.Data;
using SQLLibrary.Data.Select;
using SQLLibrary.Data.Condition;

namespace SQLLibrary.Data
{
    internal abstract class ConditionExpression<TComplete> : BaseTableExpression<TComplete>,
                                    IWhere<TComplete>,
                                    IWhereOptions<TComplete>,
                                    IRelation<TComplete>,
                                    IRelationOptions<TComplete>,
                                    IConjunctionOptions<TComplete>,
                                    IGroupStartOptions<TComplete>,
                                    IGroupEndOptions<TComplete>
    {
        public GroupDefinition ConditionBracket { get; set; }
        public GroupDefinition CurrentBracket { get; set; }
        public ColumnConditionDefinition CurrentColumnCondition { get; set; }
        public SelectConditionDefinition<IConjunction<TComplete>> CurrentSelectCondition { get; set; }

        public ConditionExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public override bool Execute()
        {
            return true;
        }

        public override bool Execute(out DataTable dataTable)
        {
            dataTable = null;
            return true;
        }

        public IWhereOptions<TComplete> Where()
        {
            CurrentBracket = new GroupDefinition();
            ConditionBracket = CurrentBracket;
            return (IWhereOptions<TComplete>)(object)this;
        }

        public IRelation<TComplete> Column(string columnName)
        {
            CurrentColumnCondition = new ColumnConditionDefinition(columnName);
            CurrentSelectCondition = new SelectConditionDefinition<IConjunction<TComplete>>(columnName);
            return (IRelation<TComplete>)(object)this;
        }

        public IGroupStartOptions<TComplete> GroupStart()
        {
            GroupDefinition bracketStruct = new GroupDefinition();
            bracketStruct.PreviousBracket = CurrentBracket;
            CurrentBracket.SeriesConnection.Add(bracketStruct);
            CurrentBracket = bracketStruct;
            return (IGroupStartOptions<TComplete>)(object)this;
        }

        public IGroupEndOptions<TComplete> GroupEnd()
        {
            CurrentBracket = CurrentBracket.PreviousBracket;
            return (IGroupEndOptions<TComplete>)(object)this;
        }

        #region Conditions
        public IRelationOptions<TComplete> EqualsValue(object value)
        {
            CurrentColumnCondition.Relation = Relations.Equals;
            CurrentColumnCondition.Value1 = value;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> NotEqualValue(object value)
        {
            CurrentColumnCondition.Relation = Relations.NotEqual;
            CurrentColumnCondition.Value1 = value;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> BiggerThan(object value)
        {
            CurrentColumnCondition.Relation = Relations.BiggerThan;
            CurrentColumnCondition.Value1 = value;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> BiggerEquals(object value)
        {
            CurrentColumnCondition.Relation = Relations.BiggerEquals;
            CurrentColumnCondition.Value1 = value;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> SmallerThan(object value)
        {
            CurrentColumnCondition.Relation = Relations.SmallerThan;
            CurrentColumnCondition.Value1 = value;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> SmallerEquals(object value)
        {
            CurrentColumnCondition.Relation = Relations.SmallerEquals;
            CurrentColumnCondition.Value1 = value;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> Between(object value1, object value2)
        {
            CurrentColumnCondition.Relation = Relations.Between;
            CurrentColumnCondition.Value1 = value1;
            CurrentColumnCondition.Value2 = value2;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> IsNull()
        {
            CurrentColumnCondition.Relation = Relations.IsNull;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public IRelationOptions<TComplete> IsNotNull()
        {
            CurrentColumnCondition.Relation = Relations.IsNotNull;
            CurrentBracket.SeriesConnection.Add(CurrentColumnCondition);
            return (IRelationOptions<TComplete>)(object)this;
        }

        public ISelectColumn<IWhere<IConjunction<TComplete>>, IConjunction<TComplete>> In(string tableName)
        {
            CurrentBracket.SeriesConnection.Add(CurrentSelectCondition);
            return CurrentSelectCondition.CreateExpression(tableName, this);
        }
        #endregion

        #region Conjunction
        public IConjunctionOptions<TComplete> And()
        {
            CurrentBracket.SeriesConnection.Add(new AndConjunctionDefinition());
            return (IConjunctionOptions<TComplete>)(object)this;
        }

        public IConjunctionOptions<TComplete> Or()
        {
            CurrentBracket.SeriesConnection.Add(new OrConjunctionDefinition());
            return (IConjunctionOptions<TComplete>)(object)this;
        }
        #endregion
    }
}
