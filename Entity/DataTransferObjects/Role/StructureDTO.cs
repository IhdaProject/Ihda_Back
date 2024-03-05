using Entity.Models;
using Entitys.Models;

namespace Entity.DataTransferObjects.Role;

public record StructureDTO(
    long id,
    MultiLanguageField name,
    bool IsDefault = false);