using SQLLibrary.Data.Condition;

namespace SQLLibrary.Data.Update
{
    public interface IUpdate_Set_Where<TComplete> : IWhere<TComplete>, IUpdateSet<TComplete> { }
}
