namespace SQLLibrary.Data.Update
{
    public interface IUpdateSet<TComplete>
    {
        IUpdate_Set_Where<TComplete> SetColumn(string columnName, object value);
        IUpdate_Set_Where<TComplete> SetColumnPlusValue(string columnName, object value);
        IUpdate_Set_Where<TComplete> SetColumnMinusValue(string columnName, object value);
    }
}
