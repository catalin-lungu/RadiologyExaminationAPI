using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RadiologyExaminationData.Entities;

[Table("exam", Schema = "exams")]
public partial class Exam
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("path")]
    [StringLength(500)]
    public string? Path { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string? Name { get; set; }

    [Column("cnp")]
    [StringLength(20)]
    public string? Cnp { get; set; }
}
