using System.Collections.Generic;
using SQLLibrary.Data.Condition;

namespace SQLLibrary.Data
{
    internal class GroupDefinition : ConditionDefinition
    {
        public GroupDefinition PreviousBracket { get; set; }
        public List<ConditionDefinition> SeriesConnection { get; private set; } = new List<ConditionDefinition>();

        public GroupDefinition()
        {
            Condition = Conditions.Bracket;
        }
    }
}
