using System.Collections.Generic;

namespace SQLLibrary.Data.Insert
{
    internal class InsertExpression : BaseTableExpression<IExecute>,
                                      IInsertOptions<IExecute>,
                                      IExecute
    {
        private readonly List<SettingDefinition> settingList = new List<SettingDefinition>();
        public List<SettingDefinition> SettingList { get { return settingList; } }

        public InsertExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

        public override bool Execute()
        {
            return _sql.Execute(_sql.GetInsertCommand(this));
        }

        public IInsertOptions<IExecute> InsertColumn(string columnName, object value)
        {
            settingList.Add(new SettingDefinition
            {
                ColumnName = columnName,
                Value = value
            });
            return (IInsertOptions<IExecute>)(object)this;
        }
    }
}
