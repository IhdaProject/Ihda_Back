using Entity.Enums;

namespace Entity.DataTransferObjects.QuranCourses;

public record PetitionForQuranCourseDto(
    long Id,
    long CourseFormId,
    string FullName,
    string Pinfl,
    DateTime PassportExpireDate,
    string Passport,
    string PhoneNumber,
    DateTime BirthDay,
    Gender Gender) : BaseDto<long>(Id);