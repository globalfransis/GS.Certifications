using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.ConceptosGastosTipos
{
    public class ConceptoGastoTipoConfiguration : BaseWithInt32IdEntityConfiguration<ConceptoGastoTipo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ConceptoGastoTipo> builder)
        {
            builder.ToTable("com_ConceptosGastosTipos");
            builder.Property(i => i.Nombre).HasMaxLength(100);
            builder.Property(i => i.Descripcion).HasMaxLength(255);
            builder.Property(i => i.ConceptoContableNombre).HasMaxLength(255);
            builder.Property(i => i.ConceptoContableValor).HasMaxLength(255);
        }
    }
}
