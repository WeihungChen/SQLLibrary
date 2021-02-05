namespace SQLLibrary.Data.Select
{
    public interface ISelectAll<TComplete>
    {
        ISelectJoin_Where<TComplete> SelectAll();
    }
}
