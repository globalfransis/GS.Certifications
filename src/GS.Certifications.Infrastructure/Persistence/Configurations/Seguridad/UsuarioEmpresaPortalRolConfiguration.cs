using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Seguridad;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Seguridad;

public class UsuarioEmpresaPortalRolConfiguration : BaseWithIdEntityConfiguration<UsuarioEmpresaPortalRol>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UsuarioEmpresaPortalRol> builder)
    {
        builder.ToTable("sec_UsuarioEmpresaPortalRoles");
    }
}
