using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.Alicuotas;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Percepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.Alicuotas;
public class AlicuotaConfiguration : BaseFixedEntityInt16Configuration<Alicuota>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Alicuota> builder)
    {
        builder.ToTable("ali_Alicuotas");
        builder.Property(i => i.CodigoAFIP).HasMaxLength(4);
        builder.HasIndex(i => i.CodigoAFIP).IsUnique();
    }

    protected override void LoadSeedingData()
    {
        SeedingData.AddRange(
            new Alicuota() { Idm = Alicuota.CODIGOTRES_IDM, CodigoAFIP = Alicuota.CODIGOTRES_CODIGOAFIP, Valor = Alicuota.CODIGOTRES_VALOR },
            new Alicuota() { Idm = Alicuota.CODIGOCUATRO_IDM, CodigoAFIP = Alicuota.CODIGOCUATRO_CODIGOAFIP, Valor = Alicuota.CODIGOCUATRO_VALOR },
            new Alicuota() { Idm = Alicuota.CODIGOCINCO_IDM, CodigoAFIP = Alicuota.CODIGOCINCO_CODIGOAFIP, Valor = Alicuota.CODIGOCINCO_VALOR },
            new Alicuota() { Idm = Alicuota.CODIGOSEIS_IDM, CodigoAFIP = Alicuota.CODIGOSEIS_CODIGOAFIP, Valor = Alicuota.CODIGOSEIS_VALOR },
            new Alicuota() { Idm = Alicuota.CODIGOOCHO_IDM, CodigoAFIP = Alicuota.CODIGOOCHO_CODIGOAFIP, Valor = Alicuota.CODIGOOCHO_VALOR },
            new Alicuota() { Idm = Alicuota.CODIGONUEVE_IDM, CodigoAFIP = Alicuota.CODIGONUEVE_CODIGOAFIP, Valor = Alicuota.CODIGONUEVE_VALOR }
        );
    }
}
