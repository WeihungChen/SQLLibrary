using SQLLibrary.Data.Condition;

namespace SQLLibrary.Data.Select
{
    public interface ISelect_Column_Join_Where<TComplete> : ISelectColumn<ISelect_Column_Join_Where<TComplete>, TComplete>, IWhere<TComplete>, IJoins<TComplete> { }
}
