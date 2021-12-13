namespace Domain.Attributes
{
    public class DescriptionAttribute : System.Attribute
    {
        public string Name { get; }
        public string Slug { get; }
        public string Schema { get; }
        public string TableName { get; }
        
        public DescriptionAttribute(
            string name,
            string slug,
            string schema,
            string tableName
        )
        {
            this.Name = name;
            this.Slug = slug;
            this.Schema = schema;
            this.TableName = tableName;
        }
    }
}