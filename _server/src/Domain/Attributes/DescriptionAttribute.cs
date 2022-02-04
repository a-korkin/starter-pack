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
            string schema,
            string ruName,
            bool isEntity = false
        ) : base(name)
        {
            this.Slug = name.Substring(3);
            this.RuName = ruName;
            this.IsEntityPartition = isEntity;
            base.Schema = schema;
        }
    }
}