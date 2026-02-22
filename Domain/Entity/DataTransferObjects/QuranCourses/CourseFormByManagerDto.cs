using Entity.Enums;
using Entity.Models.QuranCourses;

namespace Entity.DataTransferObjects.QuranCourses;

public record CourseFormByManagerDto(
    long Id,
    string MarkerId,
    string Name,
    string Description,
    int AdmissionQuota,
    int Duration,
    Gender? ForGender,
    WorkingHour TransitionTime,
    int NewPetitionCount) : BaseDto<long>(Id);