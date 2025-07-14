using Entity.Enums;

namespace Entity.DataTransferObjects.QuranCourses;

public record GetPetitionInfoDto(
    long Id,
    string Pinfl,
    DateTime BirthDay);