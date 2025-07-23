using Entity.Enums;

namespace Entity.DataTransferObjects.QuranCourses;

public record PetitionForQuranCourseByListDto(
    long Id,
    string FullName,
    string PhoneNumber,
    QuranCoursePetitionStatus Status,
    DateTime BirthDay,
    Gender Gender) : BaseDto<long>(Id);