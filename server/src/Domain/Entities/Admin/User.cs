using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Admin
{
    [Description(name: "cd_users", schema: "admin", ruName: "пользователи", isEntityPartition: true)]
    [Comment("пользователи")]
    public class User : AuditedEntity
    {
        [Column("c_username")]
        [Comment("логин")]
        [Required]
        public string UserName { get; set; }

        [Column("c_lastname")]
        [Comment("фамилия")]
        [Required]
        public string LastName { get; set; }

        [Column("c_firstname")]
        [Comment("имя")]
        [Required]
        public string FirstName { get; set; }

        [Column("c_middlename")]
        [Comment("отчество")]
        public string MiddleName { get; set; }
    }
}