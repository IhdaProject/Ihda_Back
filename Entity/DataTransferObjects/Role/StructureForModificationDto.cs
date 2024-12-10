using Entity.Models.Common;

namespace Entity.DataTransferObjects.Role;

public record StructureForModificationDto(
    long Id,
    MultiLanguageField Name);