using Entity.Enums;

namespace Entity.DataTransferObjects.QuranCourses;

public record PetitionInfosForQuranCourseDto(
    long Id,
    string FullName,
    string PhoneNumber,
    QuranCoursePetitionStatus Status,
    DateTime BirthDay,
    TimeSpan Time);