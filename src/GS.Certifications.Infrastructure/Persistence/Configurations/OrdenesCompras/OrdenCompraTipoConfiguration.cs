using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.OrdenesCompras
{
    public class OrdenCompraTipoConfiguration : BaseWithInt32IdEntityConfiguration<OrdenCompraTipo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<OrdenCompraTipo> builder)
        {
            builder.ToTable("orc_OrdenesComprasTipos");
            builder.Property(i => i.Nombre).HasMaxLength(100);
            builder.Property(i => i.Descripcion).HasMaxLength(255);
        }
    }
}
