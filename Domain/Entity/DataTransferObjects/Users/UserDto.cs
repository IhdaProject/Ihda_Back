namespace Entity.DataTransferObjects.Users;

public record UserDto(
    long Id,
    string MarketId,
    string FullName,
    string AvatarUrl,
    List<long> StructuresId);