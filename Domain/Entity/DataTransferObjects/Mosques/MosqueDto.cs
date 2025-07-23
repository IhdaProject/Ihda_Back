namespace Entity.DataTransferObjects.Mosques;

public record MosqueDto(
    long Id,
    string Name,
    string Description,
    List<string> PhotoUrls,
    double Latitude,
    double Longitude);