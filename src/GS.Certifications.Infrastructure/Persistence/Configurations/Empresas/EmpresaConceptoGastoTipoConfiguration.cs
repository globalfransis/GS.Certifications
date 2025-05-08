using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas.ConceptosGastosTipos;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas
{
    public class EmpresaConceptoGastoTipoConfiguration : BaseWithInt32IdEntityConfiguration<EmpresaConceptoGastoTipo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EmpresaConceptoGastoTipo> builder)
        {
            builder.ToTable("emp_EmpresasPortalesGastosTipos");
        }
    }
}
