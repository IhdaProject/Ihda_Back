using Entity.DataTransferObjects.Files;
using Entity.Models.QuranCourses;

namespace Entity.DataTransferObjects.QuranCourses;

public record TrainingCenterDto(
    long Id,
    string Name,
    string Description,
    string Address,
    string Landmark,
    string PhoneNumber,
    List<FileItemDto>? PhotosLink,
    WorkingHour WorkingHours,
    double Latitude,
    double Longitude,
    long DistrictId,
    List<string> EncryptUserIds) : BaseDto<long>(Id);