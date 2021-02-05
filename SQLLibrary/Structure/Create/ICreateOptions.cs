namespace SQLLibrary.Structure.Create
{
    public interface ICreateOptions<TComplete> : IBase<TComplete>
    {
        IColumnType<ICreate_ColumnOptions<TComplete>, TComplete> WithColumn(string columnName);
    }
}
