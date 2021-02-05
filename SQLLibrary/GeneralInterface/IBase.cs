namespace SQLLibrary
{
    public interface IBase<TNext>
    {
        TNext Complete();
    }
}
