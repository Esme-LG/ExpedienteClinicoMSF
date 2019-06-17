using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpedienteClinicoMSF.Models
{
    public partial class expedienteContext : DbContext
    {
        public expedienteContext()
        {
        }

        public expedienteContext(DbContextOptions<expedienteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Antecedentes> Antecedentes { get; set; }
        public virtual DbSet<Camillas> Camillas { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cirugias> Cirugias { get; set; }
        public virtual DbSet<CirugiasPacientes> CirugiasPacientes { get; set; }
        public virtual DbSet<CodigosCie10> CodigosCie10 { get; set; }
        public virtual DbSet<ConsultasMedicas> ConsultasMedicas { get; set; }
        public virtual DbSet<Diagnosticos> Diagnosticos { get; set; }
        public virtual DbSet<Direcciones> Direcciones { get; set; }
        public virtual DbSet<Especialidades> Especialidades { get; set; }
        public virtual DbSet<EstadosCiviles> EstadosCiviles { get; set; }
        public virtual DbSet<Examenes> Examenes { get; set; }
        public virtual DbSet<ExamenesMultimedias> ExamenesMultimedias { get; set; }
        public virtual DbSet<ExamenesPacientes> ExamenesPacientes { get; set; }
        public virtual DbSet<ExamenesResultados> ExamenesResultados { get; set; }
        public virtual DbSet<Expedientes> Expedientes { get; set; }
        public virtual DbSet<Familiares> Familiares { get; set; }
        public virtual DbSet<Generos> Generos { get; set; }
        public virtual DbSet<Hospitales> Hospitales { get; set; }
        public virtual DbSet<Hospitalizaciones> Hospitalizaciones { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Medicamentos> Medicamentos { get; set; }
        public virtual DbSet<Medicos> Medicos { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Multimedias> Multimedias { get; set; }
        public virtual DbSet<Pacientes> Pacientes { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Parentescos> Parentescos { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Recetas> Recetas { get; set; }
        public virtual DbSet<Regiones> Regiones { get; set; }
        public virtual DbSet<Responsables> Responsables { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesMenus> RolesMenus { get; set; }
        public virtual DbSet<RolesPermisos> RolesPermisos { get; set; }
        public virtual DbSet<Salas> Salas { get; set; }
        public virtual DbSet<SignosVitales> SignosVitales { get; set; }
        public virtual DbSet<Telefonos> Telefonos { get; set; }
        public virtual DbSet<TiposMultimedia> TiposMultimedia { get; set; }
        public virtual DbSet<Tratamientos> Tratamientos { get; set; }
        public virtual DbSet<Ubicaciones> Ubicaciones { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-INN4EME\\SQLEXPRESS;Initial Catalog=expediente;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Antecedentes>(entity =>
            {
                entity.HasKey(e => e.AntecedenteId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ANTECEDENTES");

                entity.HasIndex(e => e.ExpedienteId)
                    .HasName("CONTIENE_FK");

                entity.HasIndex(e => e.FamiliarId)
                    .HasName("PADECE_FK");

                entity.Property(e => e.AntecedenteId).HasColumnName("ANTECEDENTE_ID");

                entity.Property(e => e.Enfermedad)
                    .IsRequired()
                    .HasColumnName("ENFERMEDAD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoEnfermedad)
                    .IsRequired()
                    .HasColumnName("ESTADO_ENFERMEDAD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpedienteId).HasColumnName("EXPEDIENTE_ID");

                entity.Property(e => e.FamiliarId).HasColumnName("FAMILIAR_ID");

                entity.Property(e => e.FechaDiagnostico)
                    .HasColumnName("FECHA_DIAGNOSTICO")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Expediente)
                    .WithMany(p => p.Antecedentes)
                    .HasForeignKey(d => d.ExpedienteId)
                    .HasConstraintName("FK_ANTECEDE_CONTIENE_EXPEDIEN");

                entity.HasOne(d => d.Familiar)
                    .WithMany(p => p.Antecedentes)
                    .HasForeignKey(d => d.FamiliarId)
                    .HasConstraintName("FK_ANTECEDE_PADECE_FAMILIAR");
            });

            modelBuilder.Entity<Camillas>(entity =>
            {
                entity.HasKey(e => e.CamillaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CAMILLAS");

                entity.HasIndex(e => e.SalaId)
                    .HasName("CUENTA_CON_FK");

                entity.Property(e => e.CamillaId).HasColumnName("CAMILLA_ID");

                entity.Property(e => e.CorrelativoCamilla)
                    .IsRequired()
                    .HasColumnName("CORRELATIVO_CAMILLA")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCamilla).HasColumnName("ESTADO_CAMILLA");

                entity.Property(e => e.SalaId).HasColumnName("SALA_ID");

                entity.HasOne(d => d.Sala)
                    .WithMany(p => p.Camillas)
                    .HasForeignKey(d => d.SalaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CAMILLAS_CUENTA_CO_SALAS");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.CategoriaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.CategoriaId).HasColumnName("CATEGORIA_ID");

                entity.Property(e => e.Categoria1)
                    .IsRequired()
                    .HasColumnName("CATEGORIA")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.CategoriaDescripcion)
                    .IsRequired()
                    .HasColumnName("CATEGORIA_DESCRIPCION")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cirugias>(entity =>
            {
                entity.HasKey(e => e.CirugiaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CIRUGIAS");

                entity.HasIndex(e => e.EspecialidadId)
                    .HasName("PUEDE_REALIZAR_FK");

                entity.Property(e => e.CirugiaId).HasColumnName("CIRUGIA_ID");

                entity.Property(e => e.Cirugia)
                    .IsRequired()
                    .HasColumnName("CIRUGIA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EspecialidadId).HasColumnName("ESPECIALIDAD_ID");

                entity.HasOne(d => d.Especialidad)
                    .WithMany(p => p.Cirugias)
                    .HasForeignKey(d => d.EspecialidadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CIRUGIAS_PUEDE_REA_ESPECIAL");
            });

            modelBuilder.Entity<CirugiasPacientes>(entity =>
            {
                entity.HasKey(e => e.CirugiaPacienteId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CIRUGIAS_PACIENTES");

                entity.HasIndex(e => e.CirugiaId)
                    .HasName("DE_TIPO_FK");

                entity.HasIndex(e => e.MedicoId)
                    .HasName("REALIZA__FK");

                entity.HasIndex(e => e.PacienteId)
                    .HasName("SE_SOMETE_A_FK");

                entity.Property(e => e.CirugiaPacienteId).HasColumnName("CIRUGIA_PACIENTE_ID");

                entity.Property(e => e.CirugiaId).HasColumnName("CIRUGIA_ID");

                entity.Property(e => e.FechaCirugia)
                    .HasColumnName("FECHA_CIRUGIA")
                    .HasColumnType("datetime");

                entity.Property(e => e.MedicoId).HasColumnName("MEDICO_ID");

                entity.Property(e => e.PacienteId).HasColumnName("PACIENTE_ID");

                entity.HasOne(d => d.Cirugia)
                    .WithMany(p => p.CirugiasPacientes)
                    .HasForeignKey(d => d.CirugiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CIRUGIAS_DE_TIPO_CIRUGIAS");

                entity.HasOne(d => d.Medico)
                    .WithMany(p => p.CirugiasPacientes)
                    .HasForeignKey(d => d.MedicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CIRUGIAS_REALIZA__MEDICOS");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.CirugiasPacientes)
                    .HasForeignKey(d => d.PacienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CIRUGIAS_SE_SOMETE_PACIENTE");
            });

            modelBuilder.Entity<CodigosCie10>(entity =>
            {
                entity.HasKey(e => e.CodigoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CODIGOS_CIE10");

                entity.Property(e => e.CodigoId).HasColumnName("CODIGO_ID");

                entity.Property(e => e.Cie10)
                    .IsRequired()
                    .HasColumnName("CIE10")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.NomEnfermedad)
                    .IsRequired()
                    .HasColumnName("NOM_ENFERMEDAD")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConsultasMedicas>(entity =>
            {
                entity.HasKey(e => e.ConsultaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("CONSULTAS_MEDICAS");

                entity.HasIndex(e => e.MedicoId)
                    .HasName("REALIZA_FK");

                entity.HasIndex(e => e.PacienteId)
                    .HasName("PASA_FK");

                entity.HasIndex(e => e.SignoVitalId)
                    .HasName("TOMA_FK");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.FechaConsulta)
                    .HasColumnName("FECHA_CONSULTA")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaReserva)
                    .HasColumnName("FECHA_RESERVA")
                    .HasColumnType("datetime");

                entity.Property(e => e.MedicoId).HasColumnName("MEDICO_ID");

                entity.Property(e => e.PacienteId).HasColumnName("PACIENTE_ID");

                entity.Property(e => e.SignoVitalId).HasColumnName("SIGNO_VITAL_ID");

                entity.Property(e => e.Sintomas)
                    .IsRequired()
                    .HasColumnName("SINTOMAS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TipoReserva)
                    .IsRequired()
                    .HasColumnName("TIPO_RESERVA")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Medico)
                    .WithMany(p => p.ConsultasMedicas)
                    .HasForeignKey(d => d.MedicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSULTA_REALIZA_MEDICOS");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.ConsultasMedicas)
                    .HasForeignKey(d => d.PacienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSULTA_PASA_PACIENTE");

                entity.HasOne(d => d.SignoVital)
                    .WithMany(p => p.ConsultasMedicas)
                    .HasForeignKey(d => d.SignoVitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSULTA_TOMA_SIGNOS_V");
            });

            modelBuilder.Entity<Diagnosticos>(entity =>
            {
                entity.HasKey(e => e.DiagnosticoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("DIAGNOSTICOS");

                entity.HasIndex(e => e.CodigoId)
                    .HasName("BASADO_EN_FK");

                entity.HasIndex(e => e.ConsultaId)
                    .HasName("GENERA_FK");

                entity.Property(e => e.DiagnosticoId).HasColumnName("DIAGNOSTICO_ID");

                entity.Property(e => e.CodigoId).HasColumnName("CODIGO_ID");

                entity.Property(e => e.Comentario)
                    .HasColumnName("COMENTARIO")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.Diagnostico)
                    .IsRequired()
                    .HasColumnName("DIAGNOSTICO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Codigo)
                    .WithMany(p => p.Diagnosticos)
                    .HasForeignKey(d => d.CodigoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DIAGNOST_BASADO_EN_CODIGOS_");

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Diagnosticos)
                    .HasForeignKey(d => d.ConsultaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DIAGNOST_GENERA_CONSULTA");
            });

            modelBuilder.Entity<Direcciones>(entity =>
            {
                entity.HasKey(e => e.DireccionId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("DIRECCIONES");

                entity.HasIndex(e => e.PaisId)
                    .HasName("PERTENECE_FK");

                entity.Property(e => e.DireccionId).HasColumnName("DIRECCION_ID");

                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasColumnName("CALLE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasColumnName("CIUDAD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCasa).HasColumnName("NUMERO_CASA");

                entity.Property(e => e.PaisId).HasColumnName("PAIS_ID");

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Direcciones)
                    .HasForeignKey(d => d.PaisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DIRECCIO_PERTENECE_PAISES");
            });

            modelBuilder.Entity<Especialidades>(entity =>
            {
                entity.HasKey(e => e.EspecialidadId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ESPECIALIDADES");

                entity.Property(e => e.EspecialidadId).HasColumnName("ESPECIALIDAD_ID");

                entity.Property(e => e.DescripcionEsp)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION_ESP")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EsMedica).HasColumnName("ES_MEDICA");

                entity.Property(e => e.EsQuirurgica).HasColumnName("ES_QUIRURGICA");

                entity.Property(e => e.Especialidad)
                    .IsRequired()
                    .HasColumnName("ESPECIALIDAD")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadosCiviles>(entity =>
            {
                entity.HasKey(e => e.EstadoCivilId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ESTADOS_CIVILES");

                entity.Property(e => e.EstadoCivilId).HasColumnName("ESTADO_CIVIL_ID");

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasColumnName("ESTADO_CIVIL")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Examenes>(entity =>
            {
                entity.HasKey(e => e.ExamenId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EXAMENES");

                entity.HasIndex(e => e.CategoriaId)
                    .HasName("ES_DE_TIPO_FK");

                entity.Property(e => e.ExamenId).HasColumnName("EXAMEN_ID");

                entity.Property(e => e.CategoriaId).HasColumnName("CATEGORIA_ID");

                entity.Property(e => e.DescripcionExamen)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION_EXAMEN")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Examen)
                    .IsRequired()
                    .HasColumnName("EXAMEN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Examenes)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXAMENES_ES_DE_TIP_CATEGORI");
            });

            modelBuilder.Entity<ExamenesMultimedias>(entity =>
            {
                entity.HasKey(e => new { e.TipoMultimediaId, e.ExamenId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EXAMENES_MULTIMEDIAS");

                entity.HasIndex(e => e.ExamenId)
                    .HasName("ES_DE2_FK");

                entity.HasIndex(e => e.TipoMultimediaId)
                    .HasName("ES_DE_FK");

                entity.Property(e => e.TipoMultimediaId).HasColumnName("TIPO_MULTIMEDIA_ID");

                entity.Property(e => e.ExamenId).HasColumnName("EXAMEN_ID");

                entity.HasOne(d => d.Examen)
                    .WithMany(p => p.ExamenesMultimedias)
                    .HasForeignKey(d => d.ExamenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXAMENES_ES_DE2_EXAMENES");

                entity.HasOne(d => d.TipoMultimedia)
                    .WithMany(p => p.ExamenesMultimedias)
                    .HasForeignKey(d => d.TipoMultimediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXAMENES_ES_DE_TIPOS_MU");
            });

            modelBuilder.Entity<ExamenesPacientes>(entity =>
            {
                entity.HasKey(e => e.ExamenPacienteId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EXAMENES_PACIENTES");

                entity.HasIndex(e => e.ConsultaId)
                    .HasName("RECETADO_FK");

                entity.HasIndex(e => e.ExamenId)
                    .HasName("SE_SOMETE_FK");

                entity.Property(e => e.ExamenPacienteId).HasColumnName("EXAMEN_PACIENTE_ID");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.ExamenId).HasColumnName("EXAMEN_ID");

                entity.Property(e => e.FechaLectura)
                    .HasColumnName("FECHA_LECTURA")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaRealizacion)
                    .HasColumnName("FECHA_REALIZACION")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lectura)
                    .HasColumnName("LECTURA")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.ExamenesPacientes)
                    .HasForeignKey(d => d.ConsultaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXAMENES_RECETADO_CONSULTA");

                entity.HasOne(d => d.Examen)
                    .WithMany(p => p.ExamenesPacientes)
                    .HasForeignKey(d => d.ExamenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXAMENES_SE_SOMETE_EXAMENES");
            });

            modelBuilder.Entity<ExamenesResultados>(entity =>
            {
                entity.HasKey(e => e.ExamenResultadoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EXAMENES_RESULTADOS");

                entity.HasIndex(e => e.ExamenId)
                    .HasName("_TIENE_FK");

                entity.Property(e => e.ExamenResultadoId).HasColumnName("EXAMEN_RESULTADO_ID");

                entity.Property(e => e.ExamenId).HasColumnName("EXAMEN_ID");

                entity.Property(e => e.Medida)
                    .IsRequired()
                    .HasColumnName("MEDIDA")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Resultado)
                    .IsRequired()
                    .HasColumnName("RESULTADO")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ValorMax)
                    .HasColumnName("VALOR_MAX")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ValorMin)
                    .HasColumnName("VALOR_MIN")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Examen)
                    .WithMany(p => p.ExamenesResultados)
                    .HasForeignKey(d => d.ExamenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXAMENES__TIENE_EXAMENES");
            });

            modelBuilder.Entity<Expedientes>(entity =>
            {
                entity.HasKey(e => e.ExpedienteId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EXPEDIENTES");

                entity.HasIndex(e => e.DireccionId)
                    .HasName("VIVE_FK");

                entity.HasIndex(e => e.EstadoCivilId)
                    .HasName("ESTA__FK");

                entity.HasIndex(e => e.GeneroId)
                    .HasName("ES___FK");

                entity.HasIndex(e => e.PacienteId)
                    .HasName("GUARDA_DATOS_EN_FK");

                entity.Property(e => e.ExpedienteId).HasColumnName("EXPEDIENTE_ID");

                entity.Property(e => e.DireccionId).HasColumnName("DIRECCION_ID");

                entity.Property(e => e.EstadoCivilId).HasColumnName("ESTADO_CIVIL_ID");

                entity.Property(e => e.ExpEstado).HasColumnName("EXP_ESTADO");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneroId).HasColumnName("GENERO_ID");

                entity.Property(e => e.NumExpediente)
                    .IsRequired()
                    .HasColumnName("NUM_EXPEDIENTE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PacienteId).HasColumnName("PACIENTE_ID");

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Expedientes)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXPEDIEN_VIVE_DIRECCIO");

                entity.HasOne(d => d.EstadoCivil)
                    .WithMany(p => p.Expedientes)
                    .HasForeignKey(d => d.EstadoCivilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXPEDIEN_ESTA__ESTADOS_");

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Expedientes)
                    .HasForeignKey(d => d.GeneroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXPEDIEN_ES___GENEROS");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Expedientes)
                    .HasForeignKey(d => d.PacienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXPEDIEN_GUARDA_DA_PACIENTE");
            });

            modelBuilder.Entity<Familiares>(entity =>
            {
                entity.HasKey(e => e.FamiliarId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("FAMILIARES");

                entity.HasIndex(e => e.DireccionId)
                    .HasName("VIVE_EN_FK");

                entity.HasIndex(e => e.GeneroId)
                    .HasName("ES____FK");

                entity.HasIndex(e => e.ParentescoId)
                    .HasName("ES_FK");

                entity.HasIndex(e => e.PersonaId)
                    .HasName("_ES_UNA_FK");

                entity.Property(e => e.FamiliarId).HasColumnName("FAMILIAR_ID");

                entity.Property(e => e.CausaMuerte)
                    .HasColumnName("CAUSA_MUERTE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionId).HasColumnName("DIRECCION_ID");

                entity.Property(e => e.FechaMuerte)
                    .HasColumnName("FECHA_MUERTE")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneroId).HasColumnName("GENERO_ID");

                entity.Property(e => e.ParentescoId).HasColumnName("PARENTESCO_ID");

                entity.Property(e => e.PersonaId).HasColumnName("PERSONA_ID");

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Familiares)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAMILIAR_VIVE_EN_DIRECCIO");

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Familiares)
                    .HasForeignKey(d => d.GeneroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAMILIAR_ES____GENEROS");

                entity.HasOne(d => d.Parentesco)
                    .WithMany(p => p.Familiares)
                    .HasForeignKey(d => d.ParentescoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAMILIAR_ES_PARENTES");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Familiares)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAMILIAR__ES_UNA_PERSONAS");
            });

            modelBuilder.Entity<Generos>(entity =>
            {
                entity.HasKey(e => e.GeneroId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("GENEROS");

                entity.Property(e => e.GeneroId).HasColumnName("GENERO_ID");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasColumnName("GENERO")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Terminacion)
                    .IsRequired()
                    .HasColumnName("TERMINACION")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hospitales>(entity =>
            {
                entity.HasKey(e => e.HospitalId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("HOSPITALES");

                entity.HasIndex(e => e.DireccionId)
                    .HasName("LOCALIZADA_EN_FK");

                entity.Property(e => e.HospitalId).HasColumnName("HOSPITAL_ID");

                entity.Property(e => e.DireccionId).HasColumnName("DIRECCION_ID");

                entity.Property(e => e.DuracionPromConsulta).HasColumnName("DURACION_PROM_CONSULTA");

                entity.Property(e => e.HospitalNombre)
                    .IsRequired()
                    .HasColumnName("HOSPITAL_NOMBRE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Hospitales)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOSPITAL_LOCALIZAD_DIRECCIO");
            });

            modelBuilder.Entity<Hospitalizaciones>(entity =>
            {
                entity.HasKey(e => e.HospitalizacionId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("HOSPITALIZACIONES");

                entity.HasIndex(e => e.CamillaId)
                    .HasName("REPOSA_EN_FK");

                entity.HasIndex(e => e.CirugiaPacienteId)
                    .HasName("PARA_FK");

                entity.HasIndex(e => e.SalaId)
                    .HasName("SE_ALOJA_FK");

                entity.Property(e => e.HospitalizacionId).HasColumnName("HOSPITALIZACION_ID");

                entity.Property(e => e.CamillaId).HasColumnName("CAMILLA_ID");

                entity.Property(e => e.CirugiaPacienteId).HasColumnName("CIRUGIA_PACIENTE_ID");

                entity.Property(e => e.FechaAlta)
                    .HasColumnName("FECHA_ALTA")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaAltaAprox)
                    .HasColumnName("FECHA_ALTA_APROX")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("FECHA_INGRESO")
                    .HasColumnType("datetime");

                entity.Property(e => e.NumeroCamilla).HasColumnName("NUMERO_CAMILLA");

                entity.Property(e => e.Sala).HasColumnName("SALA");

                entity.Property(e => e.SalaId).HasColumnName("SALA_ID");

                entity.HasOne(d => d.Camilla)
                    .WithMany(p => p.Hospitalizaciones)
                    .HasForeignKey(d => d.CamillaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOSPITAL_REPOSA_EN_CAMILLAS");

                entity.HasOne(d => d.CirugiaPaciente)
                    .WithMany(p => p.Hospitalizaciones)
                    .HasForeignKey(d => d.CirugiaPacienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOSPITAL_PARA_CIRUGIAS");

                entity.HasOne(d => d.SalaNavigation)
                    .WithMany(p => p.Hospitalizaciones)
                    .HasForeignKey(d => d.SalaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOSPITAL_SE_ALOJA_SALAS");
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(e => new { e.PersonaId, e.UsuarioId, e.LogId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("LOGS");

                entity.HasIndex(e => new { e.PersonaId, e.UsuarioId })
                    .HasName("REGISTRA_FK");

                entity.Property(e => e.PersonaId).HasColumnName("PERSONA_ID");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.Property(e => e.LogId)
                    .HasColumnName("LOG_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasColumnName("ACCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Entidad)
                    .IsRequired()
                    .HasColumnName("ENTIDAD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAccion)
                    .HasColumnName("FECHA_ACCION")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => new { d.PersonaId, d.UsuarioId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOGS_REGISTRA_USUARIOS");
            });

            modelBuilder.Entity<Medicamentos>(entity =>
            {
                entity.HasKey(e => e.MedicamentosId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("MEDICAMENTOS");

                entity.Property(e => e.MedicamentosId).HasColumnName("MEDICAMENTOS_ID");

                entity.Property(e => e.Medicamento)
                    .IsRequired()
                    .HasColumnName("MEDICAMENTO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Stock).HasColumnName("STOCK");
            });

            modelBuilder.Entity<Medicos>(entity =>
            {
                entity.HasKey(e => e.MedicoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("MEDICOS");

                entity.HasIndex(e => e.EspecialidadId)
                    .HasName("EJERCE_FK");

                entity.HasIndex(e => e.HospitalId)
                    .HasName("TIENE__FK");

                entity.Property(e => e.MedicoId).HasColumnName("MEDICO_ID");

                entity.Property(e => e.EspecialidadId).HasColumnName("ESPECIALIDAD_ID");

                entity.Property(e => e.HospitalId).HasColumnName("HOSPITAL_ID");

                entity.Property(e => e.NumMedico)
                    .IsRequired()
                    .HasColumnName("NUM_MEDICO")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Especialidad)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.EspecialidadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MEDICOS_EJERCE_ESPECIAL");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MEDICOS_TIENE__HOSPITAL");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("MENUS");

                entity.HasIndex(e => e.MenMenuId)
                    .HasName("SE_COMPONE_DE_FK");

                entity.Property(e => e.MenuId).HasColumnName("MENU_ID");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("DIRECCION")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MenMenuId).HasColumnName("MEN_MENU_ID");

                entity.Property(e => e.Opcion)
                    .IsRequired()
                    .HasColumnName("OPCION")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.MenMenu)
                    .WithMany(p => p.InverseMenMenu)
                    .HasForeignKey(d => d.MenMenuId)
                    .HasConstraintName("FK_MENUS_SE_COMPON_MENUS");
            });

            modelBuilder.Entity<Multimedias>(entity =>
            {
                entity.HasKey(e => e.MultimediaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("MULTIMEDIAS");

                entity.HasIndex(e => e.ExamenPacienteId)
                    .HasName("REGISTRA__FK");

                entity.HasIndex(e => e.TipoMultimediaId)
                    .HasName("ES__FK");

                entity.Property(e => e.MultimediaId).HasColumnName("MULTIMEDIA_ID");

                entity.Property(e => e.Archivo)
                    .IsRequired()
                    .HasColumnName("ARCHIVO")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExamenPacienteId).HasColumnName("EXAMEN_PACIENTE_ID");

                entity.Property(e => e.Formato)
                    .IsRequired()
                    .HasColumnName("FORMATO")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMultimediaId).HasColumnName("TIPO_MULTIMEDIA_ID");

                entity.HasOne(d => d.ExamenPaciente)
                    .WithMany(p => p.Multimedias)
                    .HasForeignKey(d => d.ExamenPacienteId)
                    .HasConstraintName("FK_MULTIMED_REGISTRA__EXAMENES");

                entity.HasOne(d => d.TipoMultimedia)
                    .WithMany(p => p.Multimedias)
                    .HasForeignKey(d => d.TipoMultimediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MULTIMED_ES__TIPOS_MU");
            });

            modelBuilder.Entity<Pacientes>(entity =>
            {
                entity.HasKey(e => e.PacienteId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PACIENTES");

                entity.HasIndex(e => e.PersonaId)
                    .HasName("ES_UNA__FK");

                entity.Property(e => e.PacienteId).HasColumnName("PACIENTE_ID");

                entity.Property(e => e.PacienteEmail)
                    .IsRequired()
                    .HasColumnName("PACIENTE_EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId).HasColumnName("PERSONA_ID");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PACIENTE_ES_UNA__PERSONAS");
            });

            modelBuilder.Entity<Paises>(entity =>
            {
                entity.HasKey(e => e.PaisId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PAISES");

                entity.Property(e => e.PaisId).HasColumnName("PAIS_ID");

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasColumnName("PAIS")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parentescos>(entity =>
            {
                entity.HasKey(e => e.ParentescoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PARENTESCOS");

                entity.Property(e => e.ParentescoId).HasColumnName("PARENTESCO_ID");

                entity.Property(e => e.Parentesco)
                    .IsRequired()
                    .HasColumnName("PARENTESCO")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.HasKey(e => e.PermisoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PERMISOS");

                entity.Property(e => e.PermisoId).HasColumnName("PERMISO_ID");

                entity.Property(e => e.EstadoPermiso).HasColumnName("ESTADO_PERMISO");

                entity.Property(e => e.Permiso)
                    .IsRequired()
                    .HasColumnName("PERMISO")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasKey(e => e.PersonaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PERSONAS");

                entity.HasIndex(e => e.FamiliarId)
                    .HasName("_ES_UNA2_FK");

                entity.HasIndex(e => e.PacienteId)
                    .HasName("ES_UNA_2_FK");

                entity.HasIndex(e => e.ResponsableId)
                    .HasName("_ES_UNA_2_FK");

                entity.HasIndex(e => new { e.PersonaId, e.UsuarioId })
                    .HasName("ES_UNA2_FK");

                entity.Property(e => e.PersonaId)
                    .HasColumnName("PERSONA_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ApellidoCasada)
                    .HasColumnName("APELLIDO_CASADA")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoMaterno)
                    .IsRequired()
                    .HasColumnName("APELLIDO_MATERNO")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .IsRequired()
                    .HasColumnName("APELLIDO_PATERNO")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FamiliarId).HasColumnName("FAMILIAR_ID");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("FECHA_NACIMIENTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.PacienteId).HasColumnName("PACIENTE_ID");

                entity.Property(e => e.PrimerNombre)
                    .IsRequired()
                    .HasColumnName("PRIMER_NOMBRE")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsableId).HasColumnName("RESPONSABLE_ID");

                entity.Property(e => e.SegundoNombre)
                    .IsRequired()
                    .HasColumnName("SEGUNDO_NOMBRE")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Familiar)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.FamiliarId)
                    .HasConstraintName("FK_PERSONAS__ES_UNA2_FAMILIAR");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.PacienteId)
                    .HasConstraintName("FK_PERSONAS_ES_UNA_2_PACIENTE");

                entity.HasOne(d => d.Responsable)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.ResponsableId)
                    .HasConstraintName("FK_PERSONAS__ES_UNA_2_RESPONSA");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => new { d.PersonaId, d.UsuarioId })
                    .HasConstraintName("FK_PERSONAS_ES_UNA2_USUARIOS");
            });

            modelBuilder.Entity<Recetas>(entity =>
            {
                entity.HasKey(e => new { e.MedicamentosId, e.TratamientoId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("RECETAS");

                entity.HasIndex(e => e.MedicamentosId)
                    .HasName("CON_FK");

                entity.HasIndex(e => e.TratamientoId)
                    .HasName("CON2_FK");

                entity.Property(e => e.MedicamentosId).HasColumnName("MEDICAMENTOS_ID");

                entity.Property(e => e.TratamientoId).HasColumnName("TRATAMIENTO_ID");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.HasOne(d => d.Medicamentos)
                    .WithMany(p => p.Recetas)
                    .HasForeignKey(d => d.MedicamentosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RECETAS_CON_MEDICAME");

                entity.HasOne(d => d.Tratamiento)
                    .WithMany(p => p.Recetas)
                    .HasForeignKey(d => d.TratamientoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RECETAS_CON2_TRATAMIE");
            });

            modelBuilder.Entity<Regiones>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("REGIONES");

                entity.HasIndex(e => e.RegRegionId)
                    .HasName("UBICADA_FK");

                entity.Property(e => e.RegionId).HasColumnName("REGION_ID");

                entity.Property(e => e.RegRegionId).HasColumnName("REG_REGION_ID");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnName("REGION")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.RegRegion)
                    .WithMany(p => p.InverseRegRegion)
                    .HasForeignKey(d => d.RegRegionId)
                    .HasConstraintName("FK_REGIONES_UBICADA_REGIONES");
            });

            modelBuilder.Entity<Responsables>(entity =>
            {
                entity.HasKey(e => e.ResponsableId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("RESPONSABLES");

                entity.HasIndex(e => e.ExpedienteId)
                    .HasName("DE_FK");

                entity.HasIndex(e => e.PersonaId)
                    .HasName("_ES_UNA__FK");

                entity.Property(e => e.ResponsableId).HasColumnName("RESPONSABLE_ID");

                entity.Property(e => e.ExpedienteId).HasColumnName("EXPEDIENTE_ID");

                entity.Property(e => e.PersonaId).HasColumnName("PERSONA_ID");

                entity.Property(e => e.Relacion)
                    .HasColumnName("RELACION")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Expediente)
                    .WithMany(p => p.Responsables)
                    .HasForeignKey(d => d.ExpedienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RESPONSA_DE_EXPEDIEN");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Responsables)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RESPONSA__ES_UNA__PERSONAS");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ROLES");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.Property(e => e.DescripcionRol)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION_ROL")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnName("ROL")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolesMenus>(entity =>
            {
                entity.HasKey(e => new { e.RolId, e.MenuId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ROLES_MENUS");

                entity.HasIndex(e => e.MenuId)
                    .HasName("GESTIONA2_FK");

                entity.HasIndex(e => e.RolId)
                    .HasName("GESTIONA_FK");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.Property(e => e.MenuId).HasColumnName("MENU_ID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RolesMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLES_ME_GESTIONA2_MENUS");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolesMenus)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLES_ME_GESTIONA_ROLES");
            });

            modelBuilder.Entity<RolesPermisos>(entity =>
            {
                entity.HasKey(e => new { e.PermisoId, e.RolId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ROLES_PERMISOS");

                entity.HasIndex(e => e.PermisoId)
                    .HasName("POSEE_FK");

                entity.HasIndex(e => e.RolId)
                    .HasName("POSEE2_FK");

                entity.Property(e => e.PermisoId).HasColumnName("PERMISO_ID");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.HasOne(d => d.Permiso)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.PermisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLES_PE_POSEE_PERMISOS");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLES_PE_POSEE2_ROLES");
            });

            modelBuilder.Entity<Salas>(entity =>
            {
                entity.HasKey(e => e.SalaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("SALAS");

                entity.Property(e => e.SalaId).HasColumnName("SALA_ID");

                entity.Property(e => e.EstadoSala).HasColumnName("ESTADO_SALA");

                entity.Property(e => e.NombreSala)
                    .IsRequired()
                    .HasColumnName("NOMBRE_SALA")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSala).HasColumnName("NUMERO_SALA");
            });

            modelBuilder.Entity<SignosVitales>(entity =>
            {
                entity.HasKey(e => e.SignoVitalId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("SIGNOS_VITALES");

                entity.Property(e => e.SignoVitalId).HasColumnName("SIGNO_VITAL_ID");

                entity.Property(e => e.Estatura)
                    .HasColumnName("ESTATURA")
                    .HasColumnType("decimal(3, 2)");

                entity.Property(e => e.FrecRespiratoria).HasColumnName("FREC_RESPIRATORIA");

                entity.Property(e => e.Peso)
                    .HasColumnName("PESO")
                    .HasColumnType("decimal(4, 1)");

                entity.Property(e => e.PresionArterial)
                    .HasColumnName("PRESION_ARTERIAL")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Pulso).HasColumnName("PULSO");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("TEMPERATURA")
                    .HasColumnType("decimal(3, 1)");
            });

            modelBuilder.Entity<Telefonos>(entity =>
            {
                entity.HasKey(e => e.TelefonoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TELEFONOS");

                entity.HasIndex(e => e.ExpedienteId)
                    .HasName("ES_DUENO_DE__FK");

                entity.HasIndex(e => e.ResponsableId)
                    .HasName("_ES_DUENO_DE__FK");

                entity.HasIndex(e => new { e.PersonaId, e.UsuarioId })
                    .HasName("ES_DUENO_DE_FK");

                entity.Property(e => e.TelefonoId).HasColumnName("TELEFONO_ID");

                entity.Property(e => e.ExpedienteId).HasColumnName("EXPEDIENTE_ID");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnName("NUMERO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId).HasColumnName("PERSONA_ID");

                entity.Property(e => e.ResponsableId).HasColumnName("RESPONSABLE_ID");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Expediente)
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(d => d.ExpedienteId)
                    .HasConstraintName("FK_TELEFONO_ES_DUENO__EXPEDIEN");

                entity.HasOne(d => d.Responsable)
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(d => d.ResponsableId)
                    .HasConstraintName("FK_TELEFONO__ES_DUENO_RESPONSA");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(d => new { d.PersonaId, d.UsuarioId })
                    .HasConstraintName("FK_TELEFONO_ES_DUENO__USUARIOS");
            });

            modelBuilder.Entity<TiposMultimedia>(entity =>
            {
                entity.HasKey(e => e.TipoMultimediaId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TIPOS_MULTIMEDIA");

                entity.Property(e => e.TipoMultimediaId).HasColumnName("TIPO_MULTIMEDIA_ID");

                entity.Property(e => e.TipoMultimedia)
                    .IsRequired()
                    .HasColumnName("TIPO_MULTIMEDIA")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tratamientos>(entity =>
            {
                entity.HasKey(e => e.TratamientoId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TRATAMIENTOS");

                entity.HasIndex(e => e.ConsultaId)
                    .HasName("RECETA_FK");

                entity.Property(e => e.TratamientoId).HasColumnName("TRATAMIENTO_ID");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.Dosis)
                    .IsRequired()
                    .HasColumnName("DOSIS")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Durante)
                    .IsRequired()
                    .HasColumnName("DURANTE")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Frecuencia)
                    .IsRequired()
                    .HasColumnName("FRECUENCIA")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Tratamientos)
                    .HasForeignKey(d => d.ConsultaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRATAMIE_RECETA_CONSULTA");
            });

            modelBuilder.Entity<Ubicaciones>(entity =>
            {
                entity.HasKey(e => new { e.RegionId, e.DireccionId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("UBICACIONES");

                entity.HasIndex(e => e.DireccionId)
                    .HasName("UBICADA_EN2_FK");

                entity.HasIndex(e => e.RegionId)
                    .HasName("UBICADA_EN_FK");

                entity.Property(e => e.RegionId).HasColumnName("REGION_ID");

                entity.Property(e => e.DireccionId).HasColumnName("DIRECCION_ID");

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Ubicaciones)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UBICACIO_UBICADA_E_DIRECCIO");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Ubicaciones)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UBICACIO_UBICADA_E_REGIONES");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => new { e.PersonaId, e.UsuarioId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("USUARIOS");

                entity.HasIndex(e => e.DireccionId)
                    .HasName("VIVE_EN__FK");

                entity.HasIndex(e => e.EstadoCivilId)
                    .HasName("ESTA_FK");

                entity.HasIndex(e => e.GeneroId)
                    .HasName("_ES_FK");

                entity.HasIndex(e => e.HospitalId)
                    .HasName("PERTENECE_A_FK");

                entity.HasIndex(e => e.PersonaId)
                    .HasName("ES_UNA_FK");

                entity.HasIndex(e => e.RolId)
                    .HasName("TIENE_FK");

                entity.Property(e => e.PersonaId).HasColumnName("PERSONA_ID");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("USUARIO_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Bloqueado).HasColumnName("BLOQUEADO");

                entity.Property(e => e.DireccionId).HasColumnName("DIRECCION_ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivilId).HasColumnName("ESTADO_CIVIL_ID");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("FECHA_REGISTRO")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneroId).HasColumnName("GENERO_ID");

                entity.Property(e => e.HospitalId).HasColumnName("HOSPITAL_ID");

                entity.Property(e => e.Intentos).HasColumnName("INTENTOS");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("PASS")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS_VIVE_EN__DIRECCIO");

                entity.HasOne(d => d.EstadoCivil)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.EstadoCivilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS_ESTA_ESTADOS_");

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.GeneroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS__ES_GENEROS");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS_PERTENECE_HOSPITAL");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.UsuariosNavigation)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS_ES_UNA_PERSONAS");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIOS_TIENE_ROLES");
            });
        }
    }
}
