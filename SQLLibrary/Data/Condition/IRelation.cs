using SQLLibrary.Data.Select;

namespace SQLLibrary.Data.Condition
{
    public interface IRelation<TComplete> : IBase<TComplete>
    {
        IRelationOptions<TComplete> EqualsValue(object value);
        IRelationOptions<TComplete> NotEqualValue(object value);
        IRelationOptions<TComplete> BiggerThan(object value);
        IRelationOptions<TComplete> BiggerEquals(object value);
        IRelationOptions<TComplete> SmallerThan(object value);
        IRelationOptions<TComplete> SmallerEquals(object value);
        IRelationOptions<TComplete> Between(object value1, object value2);
        IRelationOptions<TComplete> IsNull();
        IRelationOptions<TComplete> IsNotNull();

        ISelectColumn<IWhere<IConjunction<TComplete>>, IConjunction<TComplete>> In(string tableName);
    }

    public interface IRelationOptions<TComplete> : IConjunction<TComplete>, IGroup<TComplete> { }
}
