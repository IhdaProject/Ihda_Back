using Entitys.Models;

namespace Entity.DataTransferObjects.Role;

public record StructureForModificationDTO(
    long id,
    MultiLanguageField name 
    );