using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Seguridad;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Seguridad;

public class UsuarioEmpresaPortalConfiguration : BaseWithIdEntityConfiguration<UsuarioEmpresaPortal>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UsuarioEmpresaPortal> builder)
    {

    }
}

