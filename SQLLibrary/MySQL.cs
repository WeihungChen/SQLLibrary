using SQLLibrary.Data;
using SQLLibrary.Data.Condition;
using SQLLibrary.Data.Select;
using SQLLibrary.Structure;
using SQLLibrary.Structure.Create;
using SQLLibrary.Structure.Drop;
using System;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using SQLLibrary.Data.Delete;
using SQLLibrary.Data.Update;
using SQLLibrary.Data.Insert;

namespace SQLLibrary
{
    public class MySQL : BaseSQL
    {
        private MySqlConnection _connection;

        public MySQL(MySQLConnectData connectData)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = connectData.Server;
            builder.Port = connectData.Port;
            builder.UserID = connectData.UserID;
            builder.Password = connectData.Password;
            builder.Database = connectData.DataBase;
            _connection = new MySqlConnection(builder.GetConnectionString(true));
        }

        public override bool Execute(string command)
        {
            bool success = false;
            mutex.WaitOne();
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();
            try
            {
                using (var cmd = new MySqlCommand(command, _connection, transaction))
                {
                    cmd.CommandTimeout = 99999;
                    success = cmd.ExecuteNonQuery() > 0;
                    transaction.Commit();
                }
            }
            catch(MySqlException e)
            {
                transaction.Rollback();
                throw new ApplicationException(command, e);
            }
            _connection.Close();
            mutex.ReleaseMutex();
            return success;
        }

        public override bool Execute(string command, out DataTable dataTable)
        {
            dataTable = null;
            bool success = false;
            mutex.WaitOne();
            _connection.Open();
            MySqlTransaction transaction = _connection.BeginTransaction();
            try
            {
                using (var cmd = new MySqlCommand(command, _connection, transaction))
                {
                    cmd.CommandTimeout = 99999;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dataTable = new DataTable();
                        dataTable.Load(reader);
                    }
                    reader.Close();
                    transaction.Commit();
                    success = true;
                }
            }
            catch(MySqlException e)
            {
                transaction.Rollback();
                throw new ApplicationException(command, e);
            }
            _connection.Close();
            mutex.ReleaseMutex();
            return success;
        }

        #region Create
        internal override string GetCreateCommand(CreateExpression expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"CREATE TABLE {expression.TableName} (");
            foreach (ColumnDefinition column in expression.ColumnList)
            {
                stringBuilder.Append(GetNewColumnCommand(column));
                stringBuilder.Append($",{GetConstraintKeyCommand(column)}");
                if (column != expression.ColumnList.Last())
                    stringBuilder.Append(",");
            }
            if (expression.PrimaryKeyGroup != null)
            {
                stringBuilder.Append(",PRIMARY KEY (");
                foreach (string key in expression.PrimaryKeyGroup)
                {
                    stringBuilder.Append(key);
                    if (key != expression.PrimaryKeyGroup.Last())
                        stringBuilder.Append(",");
                }
                stringBuilder.Append(")");
            }
            stringBuilder.Append(");");
            return stringBuilder.ToString();
        }
        #endregion

        #region Drop
        internal override string GetDropCommand(DropExpression expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"DROP TABLE {expression.TableName};");
            return stringBuilder.ToString();
        }
        #endregion

        #region Select
        internal override string GetSelectCommand(SelectDataDefinition data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            foreach (string target in data.SelectTargetList)
            {
                stringBuilder.Append(target);
                if (target != data.SelectTargetList.Last())
                    stringBuilder.Append(",");
            }
            stringBuilder.Append($" FROM {data.TableName}");

            if(data.JoinDef != null)
                stringBuilder.Append($" {GetJoinCommand(data.JoinDef)}");

            if(data.ConditionBracket != null)
                stringBuilder.Append($" {GetConditionCommand(data.ConditionBracket)}");
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

        internal override string GetSelectCommand(BaseSelectExpression<IConjunction<IExecute>> expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            foreach (string target in expression.SelectTargetList)
            {
                stringBuilder.Append(target);
                if (target != expression.SelectTargetList.Last())
                    stringBuilder.Append(",");
            }
            stringBuilder.Append($" FROM {expression.TableName}");

            if (expression.JoinDef != null)
                stringBuilder.Append($" {GetJoinCommand(expression.JoinDef)}");

            if (expression.ConditionBracket != null)
                stringBuilder.Append($" {GetConditionCommand(expression.ConditionBracket)}");
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
        #endregion

        #region Delete
        internal override string GetDeleteCommand(DeleteExpression expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"DELETE FROM {expression.TableName}");
            if (expression.ConditionBracket != null)
                stringBuilder.Append($" {GetConditionCommand(expression.ConditionBracket)}");
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
        #endregion

        #region Update
        internal override string GetUpdateCommand(UpdateExpression expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {expression.TableName} SET ");
            foreach (var setting in expression.SettingList)
            {
                stringBuilder.Append($"{setting.ColumnName}={setting.Value}");
                if (setting != expression.SettingList.Last())
                    stringBuilder.Append(", ");
            }
            if (expression.ConditionBracket != null)
                stringBuilder.Append($" {GetConditionCommand(expression.ConditionBracket)}");
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }
        #endregion

        #region Insert
        internal override string GetInsertCommand(InsertExpression expression)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder columnString = new StringBuilder();
            StringBuilder valueString = new StringBuilder();
            foreach(var setting in expression.SettingList)
            {
                columnString.Append(setting.ColumnName);
                if (setting.Value.GetType() == DateTime.Now.GetType())
                    valueString.Append($"'{((DateTime)setting.Value).ToString("yyyy-MM-dd")}'");
                else
                    valueString.Append($"'{setting.Value}'");
                if (setting != expression.SettingList.Last())
                {
                    columnString.Append(",");
                    valueString.Append(",");
                }
            }
            stringBuilder.Append($"INSERT INTO {expression.TableName} ({columnString.ToString()}) VALUES ({valueString.ToString()});");
            return stringBuilder.ToString();
        }
        #endregion

        private string GetJoinCommand(JoinDefinition joinDef)
        {
            StringBuilder stringBuilder = new StringBuilder();
            switch(joinDef.JoinType)
            {
                case Joins.InnerJoin:
                    stringBuilder.Append($"INNER JOIN {joinDef.Table} ON {joinDef.JoinedTable}.{joinDef.JoinedColumn}={joinDef.Table}.{joinDef.Column}");
                    break;
                case Joins.LeftJoin:
                    stringBuilder.Append($"LEFT JOIN {joinDef.Table} ON {joinDef.JoinedTable}.{joinDef.JoinedColumn}={joinDef.Table}.{joinDef.Column}");
                    break;
                case Joins.RightJoin:
                    stringBuilder.Append($"RIGHT JOIN {joinDef.Table} ON {joinDef.JoinedTable}.{joinDef.JoinedColumn}={joinDef.Table}.{joinDef.Column}");
                    break;
            }
            return stringBuilder.ToString();
        }

        private string GetConditionCommand(GroupDefinition bracketDefinition)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"WHERE{GetGroupConditionCommand(bracketDefinition)}");
            return stringBuilder.ToString();
        }

        private string GetGroupConditionCommand(GroupDefinition bracketDefinition)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var condition in bracketDefinition.SeriesConnection)
            {
                switch(condition.Condition)
                {
                    case Conditions.ColumnCondition:
                        stringBuilder.Append($" {GetRelationString((ColumnConditionDefinition)condition)}");
                        break;
                    case Conditions.AndConjunction:
                        stringBuilder.Append(" AND");
                        break;
                    case Conditions.OrConjunction:
                        stringBuilder.Append(" OR");
                        break;
                    case Conditions.Bracket:
                        stringBuilder.Append($" ( {GetGroupConditionCommand((GroupDefinition)condition)} )");
                        break;
                    case Conditions.Select:
                        string selectCondition = $" {condition.GetColumnName()} IN ( {GetSelectCommand(condition.GetSelectData())} )";
                        stringBuilder.Append($" ( {selectCondition.Replace(";", "")} )");
                        break;
                }
            }
            return stringBuilder.ToString();
        }

        private string GetRelationString(ColumnConditionDefinition columnCondition)
        {
            StringBuilder stringBuilder = new StringBuilder();
            switch(columnCondition.Relation)
            {
                case Relations.Equals:
                    stringBuilder.Append($"{columnCondition.ColumnName} = '{columnCondition.Value1}'");
                    break;
                case Relations.NotEqual:
                    stringBuilder.Append($"{columnCondition.ColumnName} <> '{columnCondition.Value1}'");
                    break;
                case Relations.BiggerThan:
                    stringBuilder.Append($"{columnCondition.ColumnName} > '{columnCondition.Value1}'");
                    break;
                case Relations.BiggerEquals:
                    stringBuilder.Append($"{columnCondition.ColumnName} >= '{columnCondition.Value1}'");
                    break;
                case Relations.SmallerThan:
                    stringBuilder.Append($"{columnCondition.ColumnName} < '{columnCondition.Value1}'");
                    break;
                case Relations.SmallerEquals:
                    stringBuilder.Append($"{columnCondition.ColumnName} <= '{columnCondition.Value1}'");
                    break;
                case Relations.Between:
                    stringBuilder.Append($"{columnCondition.ColumnName} BETWEEN '{columnCondition.Value1}' AND '{columnCondition.Value2}'");
                    break;
                case Relations.IsNull:
                    stringBuilder.Append($"{columnCondition.ColumnName} IS NULL");
                    break;
                case Relations.IsNotNull:
                    stringBuilder.Append($"{columnCondition.ColumnName} IS NOT NULL");
                    break;
            }
            return stringBuilder.ToString();
        }

        private string GetNewColumnCommand(ColumnDefinition column)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{column.ColumnName} {column.Type.ToString()}");
            if (column.Size != null)
                stringBuilder.Append($"({column.Size.ToString()})");
            if (column.IsWithDefault != null && (bool)column.IsWithDefault)
                stringBuilder.Append($" DEFAULT {column.DefaultValue.ToString()}");
            if (column.IsNullable != null && !(bool)column.IsNullable)
                stringBuilder.Append(" NOT NULL");
            if (column.Identity != string.Empty)
                stringBuilder.Append($" {column.Identity}");
            if (column.IsUnique != null && (bool)column.IsUnique)
                stringBuilder.Append(" UNIQUE");
            return stringBuilder.ToString();
        }

        private string GetConstraintKeyCommand(ColumnDefinition column)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (column.IsPrimaryKey != null && (bool)column.IsPrimaryKey)
                stringBuilder.Append($"PRIMARY KEY ({column.ColumnName})");
            else if (column.IsForeignKey != null && (bool)column.IsForeignKey)
                stringBuilder.Append($"FOREIGN KEY ({column.ColumnName}) REFERENCES {column.ForeignKeyReference[0]} ({column.ForeignKeyReference[1]})");

            return stringBuilder.ToString();
        }
    }

    public class MySQLConnectData
    {
        public string Server { get; set; }
        public uint Port { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string DataBase { get; set; }
    }
}
