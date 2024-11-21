﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RIESGOS_Y_RESPUESTAS.Models;

public partial class DbgestorContext : DbContext
{
    public DbgestorContext()
    {
    }

    public DbgestorContext(DbContextOptions<DbgestorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activo> Activos { get; set; }

    public virtual DbSet<Dañosformulario> Dañosformularios { get; set; }

    public virtual DbSet<MunicipalidadIqq> MunicipalidadIqqs { get; set; }

    public virtual DbSet<RegistroDeDato> RegistroDeDatos { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    public virtual DbSet<Riesgo> Riesgos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activo>(entity =>
        {
            entity.HasKey(e => e.Idactivos).HasName("PK__activos__0432D503EC956BD8");

            entity.ToTable("activos");

            entity.Property(e => e.Idactivos).HasColumnName("idactivos");
            entity.Property(e => e.Cantidad)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("cantidad");
            entity.Property(e => e.CosteActivos)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("coste_activos");
            entity.Property(e => e.Idmunicipalidad).HasColumnName("idmunicipalidad");
            entity.Property(e => e.Implementos)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("implementos");
            entity.Property(e => e.TiposDeActivosIdtipos)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("tipos_de_activos_idtipos");

            entity.HasOne(d => d.IdmunicipalidadNavigation).WithMany(p => p.Activos)
                .HasForeignKey(d => d.Idmunicipalidad)
                .HasConstraintName("FK__activos__idmunic__398D8EEE");
        });

        modelBuilder.Entity<Dañosformulario>(entity =>
        {
            entity.HasKey(e => e.Iddañosimportantes).HasName("PK__dañosfor__10645B7A04A4FCD4");

            entity.ToTable("dañosformulario");

            entity.Property(e => e.AreasAfectadas)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("areas_afectadas");
            entity.Property(e => e.EstadoDaño)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("estado_daño");
            entity.Property(e => e.Respuesta).HasColumnName("respuesta");
            entity.Property(e => e.ValorDaños)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("valor_daños");

            entity.HasOne(d => d.ActivosNavigation).WithMany(p => p.Dañosformularios)
                .HasForeignKey(d => d.Activos)
                .HasConstraintName("FK__dañosform__Activ__3E52440B");

            entity.HasMany(d => d.RespuestaIdrespuesta).WithMany(p => p.DañosformularioIddañosimportantes)
                .UsingEntity<Dictionary<string, object>>(
                    "CombinacionDañosRespuestum",
                    r => r.HasOne<Respuestum>().WithMany()
                        .HasForeignKey("RespuestaIdrespuesta")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__combinaci__respu__3A81B327"),
                    l => l.HasOne<Dañosformulario>().WithMany()
                        .HasForeignKey("DañosformularioIddañosimportantes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_combinacion_daños_respuesta_dañosformulario"),
                    j =>
                    {
                        j.HasKey("DañosformularioIddañosimportantes", "RespuestaIdrespuesta").HasName("PK__combinac__A7AB429D7AC0A1E4");
                        j.ToTable("combinacion_daños_respuesta");
                        j.IndexerProperty<int>("DañosformularioIddañosimportantes").HasColumnName("dañosformulario_iddañosimportantes");
                        j.IndexerProperty<int>("RespuestaIdrespuesta").HasColumnName("respuesta_idrespuesta");
                    });
        });

        modelBuilder.Entity<MunicipalidadIqq>(entity =>
        {
            entity.HasKey(e => e.Idmunicipalidad).HasName("PK__municipa__7186AF2C9CCCF936");

            entity.ToTable("municipalidad_iqq");

            entity.Property(e => e.Idmunicipalidad).HasColumnName("idmunicipalidad");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre_departamento");
            entity.Property(e => e.NombreDirección)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre_dirección");
        });

        modelBuilder.Entity<RegistroDeDato>(entity =>
        {
            entity.HasKey(e => e.IdregistroDeDatos).HasName("PK__registro__DD141AD1B916DE53");

            entity.ToTable("registro_de_datos");

            entity.Property(e => e.IdregistroDeDatos)
                .ValueGeneratedNever()
                .HasColumnName("idregistro_de_datos");
            entity.Property(e => e.DatoCierre)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("dato_cierre");
            entity.Property(e => e.DatoIdusuario)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("dato_idusuario");
            entity.Property(e => e.DatoTablaRiesgos)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("dato_tabla_riesgos");
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.Idrespuesta).HasName("PK__respuest__34C520FD31BCE165");

            entity.ToTable("respuesta");

            entity.Property(e => e.Idrespuesta).HasColumnName("idrespuesta");
            entity.Property(e => e.Descripción)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("descripción");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PorcentajeMtigación)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("porcentaje_mtigación");
            entity.Property(e => e.TiempoRespuesta).HasColumnName("tiempo_respuesta");
            entity.Property(e => e.ValorMejorasMonetario).HasColumnName("valor_mejoras_monetario");
        });

        modelBuilder.Entity<Riesgo>(entity =>
        {
            entity.HasKey(e => e.Idreportes).HasName("PK__riesgos__01F86A4FD3EC8E6C");

            entity.ToTable("riesgos");

            entity.Property(e => e.Idreportes).HasColumnName("idreportes");
            entity.Property(e => e.AreaMunicipalidad).HasColumnName("area_municipalidad");
            entity.Property(e => e.Daños)
                .HasMaxLength(50)
                .HasColumnName("daños");
            entity.Property(e => e.Detalles)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("detalles");
            entity.Property(e => e.EstadoRiesgo).HasColumnName("estado_riesgo");
            entity.Property(e => e.Impacto)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("impacto");
            entity.Property(e => e.NivelAmenaza)
                .HasColumnType("decimal(5, 0)")
                .HasColumnName("nivel_amenaza");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoRiesgo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_riesgo");

            entity.HasOne(d => d.AreaMunicipalidadNavigation).WithMany(p => p.Riesgos)
                .HasForeignKey(d => d.AreaMunicipalidad)
                .HasConstraintName("FK__riesgos__area_mu__3F466844");

            entity.HasMany(d => d.DañosformularioIddañosimportantes).WithMany(p => p.RiesgosIdreportes)
                .UsingEntity<Dictionary<string, object>>(
                    "CombinacionRiesgoPerdida",
                    r => r.HasOne<Dañosformulario>().WithMany()
                        .HasForeignKey("DañosformularioIddañosimportantes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__combinaci__daños__3C69FB99"),
                    l => l.HasOne<Riesgo>().WithMany()
                        .HasForeignKey("RiesgosIdreportes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__combinaci__riesg__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("RiesgosIdreportes", "DañosformularioIddañosimportantes").HasName("PK__combinac__47D7FFD151ED80AB");
                        j.ToTable("combinacion_riesgo_perdidas");
                        j.IndexerProperty<int>("RiesgosIdreportes").HasColumnName("riesgos_idreportes");
                        j.IndexerProperty<int>("DañosformularioIddañosimportantes").HasColumnName("dañosformulario_iddañosimportantes");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__tmp_ms_x__080A974381BF9B9F");

            entity.ToTable("usuario");

            entity.Property(e => e.Idusuario)
                .ValueGeneratedNever()
                .HasColumnName("idusuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Roles)
                .IsUnicode(false)
                .HasColumnName("roles");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}