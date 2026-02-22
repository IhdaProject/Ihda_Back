using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Auth;
using Entity.Models.Common;

namespace Entity.Models.QuranCourses;
/// <summary>
/// O'quv markaz Kurs formasida o'qish uchun topshirilgan arizaq
/// </summary>
[Table("petition_for_quran_course", Schema = "quran_courses")]
public class PetitionForQuranCourse : AuditableModelBase<long>
{
    /// <summary>
    /// qaysi kurs formasiga topshirilayotganligi
    /// </summary>
    [Column("course_form_id"),ForeignKey(nameof(CourseForm))] public long CourseFormId { get; set; }
    public virtual CourseForm CourseForm { get; set; }
    /// <summary>
    /// Qaysi foydalanuvchi ariza berganligi
    /// </summary>
    [Column("user_id"),ForeignKey(nameof(User))] public long? userId { get; set; }
    public virtual User? User { get; set; }
    /// <summary>
    /// JShShIR
    /// </summary>
    [Column("pinfl")]public string Pinfl { get; set; }
    /// <summary>
    /// Ariza holati
    /// </summary>
    [Column("status")] public QuranCoursePetitionStatus Status { get; set; } = QuranCoursePetitionStatus.New;
    /// <summary>
    /// Pasport seria va raqami
    /// </summary>
    [Column("passport")]public string Passport { get; set; }
    /// <summary>
    /// Pasport rasmi
    /// </summary>
    [Column("passport_photo_url")]public string PassportPhotoUrl { get; set; }
    /// <summary>
    /// Passportining amal qilish muddat
    /// </summary>
    [Column("passport_expire_date")]public DateTime PassportExpireDate { get; set; }
    /// <summary>
    /// Doimiy yashash manzili
    /// </summary>
    [Column("address")]public string Address { get; set; }
    /// <summary>
    /// Telefon raqami
    /// </summary>
    [Column("phone_number")]public string PhoneNumber { get; set; }
    /// <summary>
    /// Tug'ilgan kuni
    /// </summary>
    [Column("birthday")]public DateTime BirthDay { get; set; }
    /// <summary>
    /// To'lliq ism sharfi
    /// </summary>
    [Column("full_name")]public string FullName { get; set; }
    /// <summary>
    /// Jinsi
    /// </summary>
    [Column("duration")]public Gender Gender { get; set; }
}