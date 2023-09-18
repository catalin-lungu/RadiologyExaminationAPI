using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RadiologyExaminationData.Entities;

[Table("exam_patient_protocol", Schema = "exams")]
public partial class ExamPatientProtocol
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("exam_patient_id")]
    public int ExamPatientId { get; set; }

    [Column("protocol_id")]
    public int ProtocolId { get; set; }

    [ForeignKey("ExamPatientId")]
    [InverseProperty("ExamPatientProtocols")]
    public virtual ExamPatient ExamPatient { get; set; } = null!;

    [ForeignKey("ProtocolId")]
    [InverseProperty("ExamPatientProtocols")]
    public virtual Protocol Protocol { get; set; } = null!;
}
