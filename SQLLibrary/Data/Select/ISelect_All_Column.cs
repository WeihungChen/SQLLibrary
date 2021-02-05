namespace SQLLibrary.Data.Select
{
    public interface ISelect_All_Column<TComplete> : ISelectAll<TComplete>, ISelectColumn<ISelect_Column_Join_Where<TComplete>, TComplete> { }
}
