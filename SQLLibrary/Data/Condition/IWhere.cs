namespace SQLLibrary.Data.Condition
{
    public interface IWhere<TComplete> : IBase<TComplete>
    {
        IWhereOptions<TComplete> Where();
    }

    public interface IWhereOptions<TComplete> : IConditionColumn<TComplete>, IGroup<TComplete> { }
}
