using Entity.DataTransferObjects.Files;

namespace Entity.DataTransferObjects.Mosques;

public record MosqueDto(
    long Id,
    string Name,
    string Description,
    List<FileItemDto> PhotoUrls,
    double Latitude,
    double Longitude);