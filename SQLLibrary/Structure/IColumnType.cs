namespace SQLLibrary.Structure
{
    public interface IColumnType<TNext, TComplete>
        where TNext : IBase<TComplete>
    {
        TNext AsBit();
        TNext AsChar(int size);
        TNext AsVarChar(int size);
        TNext AsBigInt();
        TNext AsInt();
        TNext AsSmallInt();
        TNext AsTinyInt();
        TNext AsFloat();
        TNext AsDouble();
        TNext AsDate();
        TNext AsDateTime();

    }
}
