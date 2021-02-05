namespace SQLLibrary.Structure
{
    public interface IConstraintOption<TNext, TComplete> : IBase<TComplete>
        where TNext : IBase<TComplete>
    {
        TNext Identity(int seed = 1, int increment = 1);
    }
}
