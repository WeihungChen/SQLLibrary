using System.Collections.Generic;

namespace SQLLibrary.Structure
{
    internal class TableColumnExpression<TNext, TNextConstraint, TComplete> : BaseTableExpression<TComplete>,
                                                                   IColumnType<TNext, TComplete>,
                                                                   IColumnOption<TNext, TComplete>,
                                                                   IConstraint<TNextConstraint, TComplete>,
                                                                   IConstraintOption<TNextConstraint, TComplete>
        where TNext : IBase<TComplete>
        where TNextConstraint : IBase<TComplete>
    {
        public List<ColumnDefinition> ColumnList = new List<ColumnDefinition>();
        protected ColumnDefinition _currentColumn { get; set; }
        public List<string> PrimaryKeyGroup { get; set; }

        public TableColumnExpression(string tableName, BaseSQL sql) : base(tableName, sql) { }

            
        #region Column Type
        public TNext AsBit()
        {
            _currentColumn.Type = DbType.Bit;
            return (TNext)(object)this;
        }

        public TNext AsChar(int size)
        {
            _currentColumn.Type = DbType.Char;
            _currentColumn.Size = size;
            return (TNext)(object)this;
        }

        public TNext AsVarChar(int size)
        {
            _currentColumn.Type = DbType.VarChar;
            _currentColumn.Size = size;
            return (TNext)(object)this;
        }

        public TNext AsBigInt()
        {
            _currentColumn.Type = DbType.BigInt;
            return (TNext)(object)this;
        }

        public TNext AsInt()
        {
            _currentColumn.Type = DbType.Int;
            return (TNext)(object)this;
        }

        public TNext AsSmallInt()
        {
            _currentColumn.Type = DbType.SmallInt;
            return (TNext)(object)this;
        }

        public TNext AsTinyInt()
        {
            _currentColumn.Type = DbType.TinyInt;
            return (TNext)(object)this;
        }

        public TNext AsFloat()
        {
            _currentColumn.Type = DbType.Float;
            return (TNext)(object)this;
        }

        public TNext AsDouble()
        {
            _currentColumn.Type = DbType.Double;
            return (TNext)(object)this;
        }

        public TNext AsDate()
        {
            _currentColumn.Type = DbType.Date;
            return (TNext)(object)this;
        }

        public TNext AsDateTime()
        {
            _currentColumn.Type = DbType.DateTime;
            return (TNext)(object)this;
        }
        #endregion

        #region Column Option
        public TNext Nullable()
        {
            _currentColumn.IsNullable = true;
            return (TNext)(object)this;
        }

        public TNext NotNullable()
        {
            _currentColumn.IsNullable = false;
            return (TNext)(object)this;
        }

        public TNext WithDefault(object defaultValue)
        {
            _currentColumn.IsWithDefault = true;
            _currentColumn.DefaultValue = defaultValue;
            return (TNext)(object)this;
        }

        public TNext WithoutDefault()
        {
            _currentColumn.IsWithDefault = false;
            return (TNext)(object)this;
        }
        #endregion

        #region Constraint
        public TNextConstraint SetPrimaryKey(params string[] keys)
        {
            if (IsPrimaryKeyNull())
            {
                PrimaryKeyGroup = new List<string>();
                foreach (string name in keys)
                {
                    if (ColumnList.Find(col => col.ColumnName == name) != null)
                        PrimaryKeyGroup.Add(name);
                }
            }
            return (TNextConstraint)(object)this;
        }

        public TNextConstraint PrimaryKey()
        {
            if(IsPrimaryKeyNull())
                _currentColumn.IsPrimaryKey = true;
            return (TNextConstraint)(object)this;
        }

        public TNextConstraint ForeignKey(string referencedTable, string referencedColumn)
        {
            _currentColumn.IsForeignKey = true;
            _currentColumn.ForeignKeyReference[0] = referencedTable;
            _currentColumn.ForeignKeyReference[1] = referencedColumn;
            return (TNextConstraint)(object)this;
        }

        public TNextConstraint Unique()
        {
            _currentColumn.IsUnique = true;
            return (TNextConstraint)(object)this;
        }
        #endregion

        #region Constraint Option
        public TNextConstraint Identity(int seed, int increment)
        {
            _currentColumn.Identity = $"Identity({seed},{increment})";
            return (TNextConstraint)(object)this;
        }
        #endregion

        private bool IsPrimaryKeyNull()
        {
            return (ColumnList.Find(col => col.IsPrimaryKey == true) == null) && (PrimaryKeyGroup == null);
        }
    }
}
