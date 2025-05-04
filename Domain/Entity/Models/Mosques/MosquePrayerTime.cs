using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Mosques;
[Table("mosque_prayer_times",Schema = "prayerful")]
public class MosquePrayerTime : AuditableModelBase<long>
{
    [Column("adham_fajr")] public TimeOnly AdhamFajr { get; set; }
    [Column("adham_dhuhr")]public TimeOnly AdhamDhuhr { get; set; }
    [Column("adham_asr")]public TimeOnly AdhamAsr { get; set; }
    [Column("adham_maghrib")]public TimeOnly AdhamMaghrib { get; set; }
    [Column("adham_isha")]public TimeOnly AdhamIsha { get; set; }
    [Column("iqamah_fajr")]public TimeOnly IqamahFajr { get; set; }
    [Column("iqamah_dhuhr")]public TimeOnly IqamahDhuhr { get; set; }
    [Column("iqamah_asr")]public TimeOnly IqamahAsr { get; set; }
    [Column("iqamah_maghrib")]public TimeOnly IqamahMaghrib { get; set; }
    [Column("iqamah_isha")]public TimeOnly IqamahIsha { get; set; }
    [Column("mosque_id"),ForeignKey(nameof(Mosque))] public long MosqueId { get; set; }
    public virtual Mosque Mosque { get; set; }
}