using System.Collections.Generic;
using SQLLibrary.Data.Condition;

namespace SQLLibrary.Data.Select
{
    internal class SelectDataDefinition
    {
        public string TableName { get; set; }
        public List<string> SelectTargetList { get; set; }
        public JoinDefinition JoinDef { get; set; }
        public GroupDefinition ConditionBracket { get; set; }
    }
}
