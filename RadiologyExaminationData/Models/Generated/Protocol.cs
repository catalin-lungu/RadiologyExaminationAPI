using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RadiologyExaminationData.Entities;

[Table("protocol", Schema = "exams")]
public partial class Protocol
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("protocol")]
    [StringLength(200)]
    public string? Protocol1 { get; set; }

    [InverseProperty("Protocol")]
    public virtual ICollection<ExamPatientProtocol> ExamPatientProtocols { get; set; } = new List<ExamPatientProtocol>();
}
