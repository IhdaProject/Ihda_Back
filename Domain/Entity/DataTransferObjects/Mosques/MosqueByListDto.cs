namespace Entity.DataTransferObjects.Mosques;

public record MosqueByListDto(
    long Id,
    string MarkerId,
    string Name,
    double Latitude,
    double Longitude);