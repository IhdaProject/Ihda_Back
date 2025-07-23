namespace Entity.DataTransferObjects.Authentication;

public record ChangeUserStructureDto(
    long UserId,
    long StructureId);