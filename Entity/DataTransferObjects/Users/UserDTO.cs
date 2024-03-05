using Entity.Models;

namespace Entity.DataTransferObjects;

public record UserDTO(
    long id,
    string firstname,
    string midlename,
    string lastname,
    long? structureid
);