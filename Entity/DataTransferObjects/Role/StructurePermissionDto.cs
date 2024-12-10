namespace Entity.DataTransferObjects.Role;

public record StructurePermissionDto(
    long Id,
    long StructureId,
    long PermissionId);