using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Empresas;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Empresas;

public class RolTipoConfiguration : BaseFixedEntityInt16Configuration<RolTipo>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RolTipo> builder)
    {
        builder.ToTable("emp_RolTipos");

        builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(200);
        builder.Property(i => i.Codigo).IsRequired().HasMaxLength(20);
    }
    protected override void LoadSeedingData()
    {
        SeedingData.Add(new RolTipo()
        {
            Idm = RolTipo.PROV_IDM,
            Descripcion = RolTipo.PROV_DESCRIPCION,
            Codigo = RolTipo.PROV_CODIGO
        });
        SeedingData.Add(new RolTipo()
        {
            Idm = RolTipo.CONT_IDM,
            Descripcion = RolTipo.CONT_DESCRIPCION,
            Codigo = RolTipo.CONT_CODIGO
        });
        SeedingData.Add(new RolTipo()
        {
            Idm = RolTipo.CLI_IDM,
            Descripcion = RolTipo.CLI_DESCRIPCION,
            Codigo = RolTipo.CLI_CODIGO
        });
    }
}
