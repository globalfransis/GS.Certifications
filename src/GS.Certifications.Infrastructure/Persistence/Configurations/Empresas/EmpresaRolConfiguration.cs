using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas.Roles;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas;

public class EmpresaRolConfiguration : BaseWithInt32IdEntityConfiguration<EmpresaRol>
{
    protected override void ConfigureEntity(EntityTypeBuilder<EmpresaRol> builder)
    {
        builder.ToTable("emp_EmpresaRoles");
    }
}
