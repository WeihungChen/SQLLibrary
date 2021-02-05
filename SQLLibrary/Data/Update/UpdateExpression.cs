using System.Collections.Generic;

namespace SQLLibrary.Data.Update
{
    internal class UpdateExpression : ConditionExpression<IExecute>,
                                      IUpdateOptions<IExecute>,
                                      IUpdate_Set_Where<IExecute>,
                                      IExecute
    {
        private readonly List<SettingDefinition> settingList = new List<SettingDefinition>();
        public List<SettingDefinition> SettingList { get { return settingList; } }

        public UpdateExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public override bool Execute()
        {
            if (SettingList.Count == 0)
                return false;
            return _sql.Execute(_sql.GetUpdateCommand(this));
        }

        public IUpdate_Set_Where<IExecute> SetColumn(string columnName, object value)
        {
            SettingList.Add(new SettingDefinition
            {
                ColumnName = columnName,
                Value = value
            });
            return (IUpdate_Set_Where<IExecute>)(object)this;
        }

        public IUpdate_Set_Where<IExecute> SetColumnPlusValue(string columnName, object value)
        {
            SettingList.Add(new SettingDefinition
            {
                ColumnName = columnName,
                Value = $"{columnName}+{value}"
            });
            return (IUpdate_Set_Where<IExecute>)(object)this;
        }

        public IUpdate_Set_Where<IExecute> SetColumnMinusValue(string columnName, object value)
        {
            SettingList.Add(new SettingDefinition
            {
                ColumnName = columnName,
                Value = $"{columnName}-{value}"
            });
            return (IUpdate_Set_Where<IExecute>)(object)this;
        }
    }
}
