using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enum;

namespace Entity.Models;

[Table("tokens", Schema = "auth")]
public class TokenModel : AuditableModelBase<long>
{
    [Column("user_id")]
    [ForeignKey("User")]
    public long UserId { get; set; }
    [NotMapped] public virtual User User { get; set; }
    [Column("type")] public TokenTypes TokenType { get; set; }
    [Column("access_token")] public string AccessToken { get; set; }
    [Column("refresh_token")] public string RefreshToken { get; set; }
    [Column("expire_refresh_token")] public DateTime ExpireRefreshToken { get; set; }
}