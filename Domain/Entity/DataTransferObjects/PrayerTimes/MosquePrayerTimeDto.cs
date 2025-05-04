namespace Entity.DataTransferObjects.PrayerTimes;

public record MosquePrayerTimeDto(
    long Id,
    long MosqueId,
    TimeOnly AdhamFajr,
    TimeOnly AdhamDhuhr,
    TimeOnly AdhamAsr,
    TimeOnly AdhamMaghrib,
    TimeOnly AdhamIsha,
    TimeOnly IqamahFajr,
    TimeOnly IqamahDhuhr,
    TimeOnly IqamahAsr,
    TimeOnly IqamahMaghrib,
    TimeOnly IqamahIsha);