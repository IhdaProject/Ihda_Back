namespace Entity.DataTransferObjects.Users;

public record UserFullDto(
    long Id,
    string FullName,
    string AvatarUrl,
    List<long> StructuresId,
    List<string> StructuresName
);