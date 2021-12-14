using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Attributes
{
    public class DescriptionAttribute : TableAttribute //System.Attribute
    {
        public string Slug { get; }
        public string RuName { get; }
        
        public DescriptionAttribute(
            string name,
            string slug,
            string schema,
            string ruName
        ) : base(name)
        {
            this.Slug = slug;
            this.RuName = ruName;
            base.Schema = schema;
        }
    }
}