using System;
using System.Collections.Generic;
using System.Text;

namespace SQLLibrary.Structure.Create
{
    public interface ICreate_ColumnOptions_Constraint<TComplete> : ICreate_ColumnOptions<TComplete>, 
                                                        IConstraintOption<ICreate_ColumnOptions_Constraint<TComplete>, TComplete> { }
}
