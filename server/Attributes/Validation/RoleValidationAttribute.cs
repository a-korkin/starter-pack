using System.ComponentModel.DataAnnotations;
using System.Linq;
using server.Models.DTO.Admin;

namespace server.Attributes.Validation
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, 
            ValidationContext validationContext)
        {
            var role = (RoleInDto)validationContext.ObjectInstance;
            string errorMessage = string.Empty;
            
            // одинаковые клэймы
            var claimGroups = role.Claims
                .GroupBy(g => g.TypeId)
                .Select(s => new { Type = s.Key, Count = s.Count() });

            if (claimGroups.Any(w => w.Count > 1))
                errorMessage = "Одинаковые клэймы";

            // одинаковые пользователи
            var userGroups = role.Users
                .GroupBy(g => g.UserId)
                .Select(s => new { User = s.Key, Count = s.Count() });
            
            if (userGroups.Any(w => w.Count > 1))
                errorMessage = "Одинаковые пользователи";

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return new ValidationResult(errorMessage, 
                    new[] { nameof(RoleInDto) });
            }
                
            return ValidationResult.Success;
        }
    }
}