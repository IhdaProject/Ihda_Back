﻿using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.ReferenceBook;
[Table("prayer_time_styles", Schema = "reference_book"), Index(nameof(Code), IsUnique = true)]
public class PrayerTimeStyle : ModelBase<long>
{
    [Column("name")] public string Name { get; set; }
    [Column("code")] public int Code { get; set; }
}