namespace SQLLibrary.Structure
{
    public interface IColumnOption<TNext, TComplete> : IBase<TComplete>
        where TNext : IBase<TComplete>
    {
        TNext Nullable();
        TNext NotNullable();
        TNext WithDefault(object defaultValue);
        TNext WithoutDefault();
    }
}
