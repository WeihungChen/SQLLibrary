namespace SQLLibrary.Structure
{
    public interface IConstraint<TNext, TComplete> : IBase<TComplete>
        where TNext : IBase<TComplete>
    {
        TNext SetPrimaryKey(params string[] keys);
        TNext PrimaryKey();
        TNext ForeignKey(string referecedTable, string referencedColumn);
        TNext Unique();
    }
}
