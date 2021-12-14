using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Attributes
{
    public class DescriptionAttribute : TableAttribute
    {
        public string Slug { get; }
        public string RuName { get; }
        public bool IsEntityPartition { get; set; }
        
        public DescriptionAttribute(
            string name,
            string slug,
            string schema,
            string ruName,
            bool isEntity = false
        ) : base(name)
        {
            this.Slug = slug;
            this.RuName = ruName;
            this.IsEntityPartition = isEntity;
            base.Schema = schema;
        }
    }
}