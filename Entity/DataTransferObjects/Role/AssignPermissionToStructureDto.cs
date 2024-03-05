namespace Entity.DataTransferObjects.Role;

public record AssignPermissionToStructureDto(
    long StructureId,
    long[] PermissionIds);