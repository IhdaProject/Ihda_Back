using System.ComponentModel.DataAnnotations.Schema;
using Entitys.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models
{
    [Table("permissions", Schema = "auth"), Index(nameof(Code), IsUnique = true)]
    public class Permission : ModelBase<long>
    {
        [Column("name")] public MultiLanguageField Name { get; set; }
        [Column("code")] public int Code { get; set; }
    }
}