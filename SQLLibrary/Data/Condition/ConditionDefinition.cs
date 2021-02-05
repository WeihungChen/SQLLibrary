using SQLLibrary.Data.Select;

namespace SQLLibrary.Data.Condition
{
    internal abstract class ConditionDefinition
    {
        public Conditions Condition { get; protected set; }

        public virtual SelectDataDefinition GetSelectData()
        {
            return null;
        }

        public virtual string GetColumnName()
        {
            return string.Empty;
        }
    }
}
