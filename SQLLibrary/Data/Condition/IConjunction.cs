namespace SQLLibrary.Data.Condition
{
    public interface IConjunction<TComplete> : IBase<TComplete>
    {
        IConjunctionOptions<TComplete> And();
        IConjunctionOptions<TComplete> Or();
    }

    public interface IConjunctionOptions<TComplete> : IGroup<TComplete>, IConditionColumn<TComplete> { }
}
