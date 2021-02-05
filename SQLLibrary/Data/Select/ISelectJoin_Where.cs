using SQLLibrary.Data.Condition;

namespace SQLLibrary.Data.Select
{
    public interface ISelectJoin_Where<TComplete> : IWhere<TComplete>, IJoins<TComplete> { }
}
