using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.OrdenesCompras;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.OrdenesCompras
{
    public class OrdenCompraConfiguration : BaseWithInt32IdEntityConfiguration<OrdenCompra>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<OrdenCompra> builder)
        {
            builder.ToTable("orc_OrdenesCompras");
            builder.Property(i => i.NumeroOrden).HasMaxLength(50);
            builder.Property(i => i.CodigoHES).HasMaxLength(50);
            builder.Property(i => i.CodigoQAD).HasMaxLength(50);
        }
    }
}
