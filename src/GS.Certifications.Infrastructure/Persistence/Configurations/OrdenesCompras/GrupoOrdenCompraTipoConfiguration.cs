using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.OrdenesCompras;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.OrdenesCompras
{
    public class GrupoOrdenCompraTipoConfiguration : BaseWithInt32IdEntityConfiguration<GrupoOrdenCompraTipo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<GrupoOrdenCompraTipo> builder)
        {
            builder.ToTable("emp_GruposDocsComprasTipos");
        }
    }
}
