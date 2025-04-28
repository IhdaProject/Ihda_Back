namespace Entity.DataTransferObjects.Role;

public record StructureForCreationDto(
    string Name,
    List<long> PermissionIds);