using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Common;

namespace Entity.Models.Auth;

[Table("tokens", Schema = "auth")]
public class TokenModel : AuditableModelBase<long>
{
    [Column("user_id")]
    [ForeignKey("User")]
    public long UserId { get; set; }
    [NotMapped] public virtual User User { get; set; }
    [Column("type")] public TokenTypes TokenType { get; set; }
    [Column("access_token_id")] public string AccessTokenId { get; set; }
    [Column("expire_token")] public DateTime ExpireToken { get; set; }
    [Column("refresh_token")] public string RefreshToken { get; set; }
    [Column("expire_refresh_token")] public DateTime ExpireRefreshToken { get; set; }
    
}