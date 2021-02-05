namespace SQLLibrary.Data.Condition
{
    public interface IJoins<TComplete>
    {
        IWhere<TComplete> LeftJoin(string joinedColumn, string table, string column);
        IWhere<TComplete> InnerJoin(string joinedColumn, string table, string column);
        IWhere<TComplete> RightJoin(string joinedColumn, string table, string column);
    }
}
