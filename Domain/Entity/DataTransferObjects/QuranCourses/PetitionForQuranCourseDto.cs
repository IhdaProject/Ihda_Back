using Entity.Enums;

namespace Entity.DataTransferObjects.QuranCourses;

public record PetitionForQuranCourseDto(
    long Id,
    long CourseFormId,
    string FullName,
    string Pinfl,
    string Passport,
    DateTime BirthDay,
    Gender Gender);