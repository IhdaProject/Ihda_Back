using Entity.DataTransferObjects.PrayerTimes;

namespace Entity.DataTransferObjects.Mosques;

public record MosqueWithTimeDto(long Id,
    string Name,
    string Description,
    List<string> PhotoUrls,
    double Latitude,
    double Longitude,
    MosquePrayerTimeDto PrayerTime);