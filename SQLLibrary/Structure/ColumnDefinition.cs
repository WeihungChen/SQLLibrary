namespace SQLLibrary.Structure
{
    internal class ColumnDefinition
    {
        public string ColumnName { get; private set; }
        public DbType? Type { get; set; }
        public int? Size { get; set; }
        public bool? IsUnique { get; set; }
        public bool? IsNullable { get; set; }
        public bool? IsWithDefault { get; set; }
        public object DefaultValue { get; set; }

        public bool? IsPrimaryKey { get; set; }
        public bool? IsForeignKey { get; set; }
        public string[] ForeignKeyReference { get; } = new string[2];
        public string Identity { get; set; }

        public ColumnDefinition(string name)
        {
            ColumnName = name;
        }
    }
}
