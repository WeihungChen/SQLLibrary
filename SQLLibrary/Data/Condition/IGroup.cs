namespace SQLLibrary.Data.Condition
{
    public interface IGroup<TComplete> : IBase<TComplete>
    {
        IGroupStartOptions<TComplete> GroupStart();
        IGroupEndOptions<TComplete> GroupEnd();
    }

    public interface IGroupStartOptions<TComplete> : IConditionColumn<TComplete>, IGroup<TComplete> { }

    public interface IGroupEndOptions<TComplete> : IGroup<TComplete>, IConjunction<TComplete> { }
}
