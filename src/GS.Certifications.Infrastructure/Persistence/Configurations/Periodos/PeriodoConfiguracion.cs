using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Periodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Periodos
{
    public class PeriodoConfiguracion : BaseWithInt32IdEntityConfiguration<Periodo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Periodo> builder)
        {
            builder.ToTable("prd_Periodos");
        }
    }

    public class EstadoPeriodoConfiguracion : BaseFixedEntityInt16Configuration<EstadoPeriodo>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EstadoPeriodo> builder)
        {
            builder.ToTable("prd_EstadoPeriodos");
            builder.Property(i => i.Descripcion).HasMaxLength(50);
        }

        protected override void LoadSeedingData()
        {
            SeedingData.AddRange(
                new EstadoPeriodo { Idm = EstadoPeriodo.CERRADO_IDM, Descripcion = EstadoPeriodo.CERRADO_DESCRIPCION },
                new EstadoPeriodo { Idm = EstadoPeriodo.ABIERTO_IDM, Descripcion = EstadoPeriodo.ABIERTO_DESCRIPCION },
                new EstadoPeriodo { Idm = EstadoPeriodo.PRESENTADO_IDM, Descripcion = EstadoPeriodo.PRESENTADO_DESCRIPCION },
                new EstadoPeriodo { Idm = EstadoPeriodo.NO_VIGENTE_IDM, Descripcion = EstadoPeriodo.NO_VIGENTE_DESCRIPCION }
            );
        }
    }
}