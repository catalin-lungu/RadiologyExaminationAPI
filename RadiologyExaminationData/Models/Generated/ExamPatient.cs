using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RadiologyExaminationData.Entities;

[Table("exam_patient", Schema = "exams")]
public partial class ExamPatient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("exam_id")]
    public int ExamId { get; set; }

    [Column("type")]
    [StringLength(50)]
    public string? Type { get; set; }

    [Column("patient_id")]
    [StringLength(20)]
    public string PatientId { get; set; } = null!;

    [Column("patient_name")]
    [StringLength(200)]
    public string? PatientName { get; set; }

    [Column("date")]
    [StringLength(20)]
    public string? Date { get; set; }

    [Column("institution")]
    [StringLength(100)]
    public string? Institution { get; set; }

    [InverseProperty("ExamPatient")]
    public virtual ICollection<ExamPatientProtocol> ExamPatientProtocols { get; set; } = new List<ExamPatientProtocol>();
}
