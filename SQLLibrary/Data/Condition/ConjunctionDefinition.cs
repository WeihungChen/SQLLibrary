namespace SQLLibrary.Data.Condition
{
    internal class AndConjunctionDefinition : ConditionDefinition
    {
        public AndConjunctionDefinition()
        {
            Condition = Conditions.AndConjunction;
        }
    }

    internal class OrConjunctionDefinition : ConditionDefinition
    {
        public OrConjunctionDefinition()
        {
            Condition = Conditions.OrConjunction;
        }
    }
}
