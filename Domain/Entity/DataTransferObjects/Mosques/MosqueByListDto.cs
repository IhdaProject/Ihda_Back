namespace Entity.DataTransferObjects.Mosques;

public record MosqueByListDto(
    long Id,
    string Name,
    double Latitude,
    double Longitude);