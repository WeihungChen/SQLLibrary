namespace SQLLibrary.Data.Condition
{
    public interface IConditionColumn<TComplete> : IBase<TComplete>
    {
        IRelation<TComplete> Column(string columnName);
    }
}
