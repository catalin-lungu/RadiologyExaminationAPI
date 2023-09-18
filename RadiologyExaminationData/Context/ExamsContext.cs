using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RadiologyExaminationData.Entities;

public partial class ExamsContext : DbContext
{
    public ExamsContext()
    {
    }

    public ExamsContext(DbContextOptions<ExamsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamPatient> ExamPatients { get; set; }

    public virtual DbSet<ExamPatientProtocol> ExamPatientProtocols { get; set; }

    public virtual DbSet<Protocol> Protocols { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ExamsDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__exam__3213E83FC62FBFC9");

            entity.ToTable("exam", "exams", tb =>
                {
                    tb.HasTrigger("exam_dss_delete_trigger");
                    tb.HasTrigger("exam_dss_insert_trigger");
                    tb.HasTrigger("exam_dss_update_trigger");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ExamPatient>(entity =>
        {
            entity.ToTable("exam_patient", "exams", tb =>
                {
                    tb.HasTrigger("exam_patient_dss_delete_trigger");
                    tb.HasTrigger("exam_patient_dss_insert_trigger");
                    tb.HasTrigger("exam_patient_dss_update_trigger");
                });
        });

        modelBuilder.Entity<ExamPatientProtocol>(entity =>
        {
            entity.ToTable("exam_patient_protocol", "exams", tb =>
                {
                    tb.HasTrigger("exam_patient_protocol_dss_delete_trigger");
                    tb.HasTrigger("exam_patient_protocol_dss_insert_trigger");
                    tb.HasTrigger("exam_patient_protocol_dss_update_trigger");
                });

            entity.HasOne(d => d.ExamPatient).WithMany(p => p.ExamPatientProtocols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_exam_patient_protocol_exam_patient");

            entity.HasOne(d => d.Protocol).WithMany(p => p.ExamPatientProtocols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_exam_patient_protocol_protocol");
        });

        modelBuilder.Entity<Protocol>(entity =>
        {
            entity.ToTable("protocol", "exams", tb =>
                {
                    tb.HasTrigger("protocol_dss_delete_trigger");
                    tb.HasTrigger("protocol_dss_insert_trigger");
                    tb.HasTrigger("protocol_dss_update_trigger");
                });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
