using Entity.Models.QuranCourses;

namespace Entity.DataTransferObjects.QuranCourses;

public record TrainingCenterDto(
    long Id,
    string Name,
    string Description,
    string Address,
    string Landmark,
    string PhoneNumber,
    string[] PhotosLink,
    WorkingHour WorkingHours,
    double Latitude,
    double Longitude,
    long DistrictId) : BaseDto<long>(Id);