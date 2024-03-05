namespace Entity.DataTransferObjects.Authentication;

public record ChangeUserStructureDto
{
    public long UserId { get; set; }
    public long StructureId { get; set; }
}