namespace SQLLibrary.Data.Select
{
    public interface ISelectColumn<TNext, TComplete> : IBase<TComplete>
    {
        TNext SelectColumn(string columnName);
    }
}
