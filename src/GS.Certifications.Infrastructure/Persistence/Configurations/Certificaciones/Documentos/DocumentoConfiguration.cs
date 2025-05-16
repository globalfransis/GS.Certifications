using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Certificaciones.Documentos
{
    public class DocumentoCargadoConfiguration : BaseWithInt32IdEntityConfiguration<DocumentoCargado>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<DocumentoCargado> builder)
        {
            builder.ToTable("doc_DocumentosCargados");
            builder.Property(i => i.ArchivoURL).HasMaxLength(2000);
        }
    }
    
    public class DocumentoRequeridoConfiguration : BaseWithInt32IdEntityConfiguration<DocumentoRequerido>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<DocumentoRequerido> builder)
        {
            builder.ToTable("doc_DocumentosRequeridos");
        }
    }

    public class DocumentoEstadoConfiguration : BaseFixedEntityInt16Configuration<DocumentoEstado>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<DocumentoEstado> builder)
        {
            builder.ToTable("doc_DocumentoEstados");

            builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(500);
        }

        protected override void LoadSeedingData()
        {
            SeedingData.AddRange(
                new DocumentoEstado() { Idm = DocumentoEstado.PENDIENTE, Descripcion = "Pendiente" },
                new DocumentoEstado() { Idm = DocumentoEstado.VALIDADO, Descripcion = "Validado" },
                new DocumentoEstado() { Idm = DocumentoEstado.RECHAZADO, Descripcion = "Rechazado" },
                new DocumentoEstado() { Idm = DocumentoEstado.VENCIDO, Descripcion = "Vencido" },
                new DocumentoEstado() { Idm = DocumentoEstado.PRESENTADO, Descripcion = "Presentado" }
            );
        }
    }

    public class TipoDocumentoConfiguration : BaseFixedEntityInt16Configuration<TipoDocumento>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("doc_TipoDocumentos");

            builder.Property(i => i.Nombre).IsRequired().HasMaxLength(500);
        }

        protected override void LoadSeedingData()
        {
        }
    }
}
