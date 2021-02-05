using SQLLibrary.Data.Select;

namespace SQLLibrary.Data.Condition
{
    internal class SelectConditionExpression<TComplete> : BaseSelectExpression<TComplete>,
                                               ISelectColumn<IWhere<TComplete>, TComplete>,
                                               IConjunction<TComplete>
    {
        private TComplete _parentExpression;

        public SelectConditionExpression(string tableName, TComplete parentExpression) : base (tableName, null) 
        {
            _parentExpression = parentExpression;
        }

        public IWhere<TComplete> SelectColumn(string columnName)
        {
            selectTargetList.Add(columnName);
            return (IWhere<TComplete>)(object)this;
        }

        public override TComplete Complete()
        {
            return _parentExpression;
        }
    }
}
