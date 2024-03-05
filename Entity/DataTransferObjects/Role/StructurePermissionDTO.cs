namespace Entity.DataTransferObjects.Role;

public record StructurePermissionDTO(
    long id,
    long structureId,
    long permissionId);