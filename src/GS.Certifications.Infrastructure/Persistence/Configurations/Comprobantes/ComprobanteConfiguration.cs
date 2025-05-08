using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Comprobantes;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Comprobantes;

public class ComprobanteConfiguration : BaseWithInt32IdEntityConfiguration<Comprobante>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Comprobante> builder)
    {
        builder.ToTable("com_Comprobantes");
        builder.Property(i => i.NroIdentificacionFiscalPro).HasMaxLength(250);
        builder.Property(i => i.DomicilioPro).HasMaxLength(1000);
        builder.Property(i => i.NroIdentificacionFiscalCompany).HasMaxLength(250);
        builder.Property(i => i.Letra).HasMaxLength(2);
        builder.Property(i => i.CodigoAutorizacion).HasMaxLength(60);
        builder.Property(i => i.ImporteNeto).HasColumnType("money");
        builder.Property(i => i.ImporteTotal).HasColumnType("money");
        builder.Property(i => i.ImporteIVA).HasColumnType("money");
        builder.Property(i => i.ImporteBonificacion).HasColumnType("money");
        builder.Property(i => i.ImportePercepcionIVA).HasColumnType("money");
        builder.Property(i => i.ImportePercepcionIIBB).HasColumnType("money");
        builder.Property(i => i.ImportePercepcionMunicipal).HasColumnType("money");
        builder.Property(i => i.ImporteImpuestosInternos).HasColumnType("money");
        builder.Property(i => i.ImporteOtrosTributosProv).HasColumnType("money");
        builder.Property(i => i.CodigoHES).HasMaxLength(100);
        builder.Property(i => i.Comentarios).HasMaxLength(500);
        builder.Property(i => i.UsuarioRechazo).HasMaxLength(500);
        builder.Property(i => i.MotivoRechazo).HasMaxLength(500);
        builder.Property(i => i.ObservacionesARCA).HasMaxLength(1000);
        builder.Property(i => i.ObservacionesARCAQR).HasMaxLength(1000);
        builder.Property(i => i.NombreArchivo).HasMaxLength(1000);

        builder.Property(i => i.NroRemito).HasMaxLength(500);
        builder.Property(i => i.NroOrdenCompra).HasMaxLength(500);

        builder.HasOne(c => c.CategoriaTipoEmisor)
               .WithMany() //  CategoriaTipo no tiene una propiedad de navegación a Comprobante
               .HasForeignKey(c => c.CategoriaTipoEmisorId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.CategoriaTipoReceptor)
               .WithMany() //  CategoriaTipo no tiene una propiedad de navegación a Comprobante
               .HasForeignKey(c => c.CategoriaTipoReceptorId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}

public class ComprobanteDetalleConfiguration : BaseWithInt32IdEntityConfiguration<ComprobanteDetalle>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ComprobanteDetalle> builder)
    {
        builder.ToTable("com_ComprobanteDetalles");
        builder.Property(i => i.PrecioUnitario).HasColumnType("money");
        builder.Property(i => i.ImporteBonificacion).HasColumnType("money");
        builder.Property(i => i.Subtotal).HasColumnType("money");
        builder.Property(i => i.Detalle).HasMaxLength(400);

    }
}