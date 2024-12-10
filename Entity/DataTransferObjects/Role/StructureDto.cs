using Entity.Models;
using Entity.Models.Common;

namespace Entity.DataTransferObjects.Role;

public record StructureDto(
    long Id,
    MultiLanguageField Name);