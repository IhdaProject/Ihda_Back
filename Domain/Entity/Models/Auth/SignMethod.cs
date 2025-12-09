using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Entity.Enums;
using Entity.Models.Common;

namespace Entity.Models.Auth;

[Table("user_sign_methods", Schema = "auth")]
public abstract class SignMethod : ModelBase<long>
{
    [Column("type")] public SignMethods Type { get; set; }

    [Column("user_id"), ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public virtual User User { get; set; }
}
public class DefaultSignMethod : SignMethod
{
    [Column("username")] public string Username { get; set; }
    [Column("salt")] public string Salt { get; set; }
    [Column("password_hash")] public string PasswordHash { get; set; }
}