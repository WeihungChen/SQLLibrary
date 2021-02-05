namespace SQLLibrary.Structure.Create
{
    public interface ICreate_ColumnOptions<TComplete> : ICreateOptions<TComplete>,
                                             IColumnOption<ICreate_ColumnOptions<TComplete>, TComplete>,
                                             IConstraint<ICreate_ColumnOptions_Constraint<TComplete>, TComplete> { }
}
