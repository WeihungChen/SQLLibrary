namespace SQLLibrary.Data.Insert
{
    public interface IInsertOptions<TComplete> : IBase<TComplete>
    {
        IInsertOptions<TComplete> InsertColumn(string columnName, object value);
    }
}
