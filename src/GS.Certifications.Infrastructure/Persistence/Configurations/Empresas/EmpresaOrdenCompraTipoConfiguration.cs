using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas.OrdenesCompras;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas
{
    public class EmpresaOrdenCompraTipoConfiguration : BaseWithInt32IdEntityConfiguration<EmpresaOrdenCompraTipo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EmpresaOrdenCompraTipo> builder)
        {
            builder.ToTable("emp_EmpresasPortalesDocsComprasTipos");
        }
    }
}
