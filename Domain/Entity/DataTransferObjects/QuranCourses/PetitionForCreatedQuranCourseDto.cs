using Entity.Enums;

namespace Entity.DataTransferObjects.QuranCourses;

public record PetitionForCreatedQuranCourseDto(
    long CourseFormId,
    string FullName,
    string Pinfl,
    DateTime PassportExpireDate,
    string Passport,
    string PassportPhotoUrl,
    string PhoneNumber,
    DateTime BirthDay,
    Gender Gender);