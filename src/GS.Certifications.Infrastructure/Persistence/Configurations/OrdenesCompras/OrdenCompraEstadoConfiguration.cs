using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.OrdenesCompras
{
    public class OrdenCompraEstadoConfiguration : BaseFixedEntityInt16Configuration<OrdenCompraEstado>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<OrdenCompraEstado> builder)
        {
            builder.ToTable("orc_OrdenesComprasEstados");
            builder.Property(i => i.Descripcion).HasMaxLength(50);
            builder.Property(i => i.Nombre).HasMaxLength(50);
        }

        protected override void LoadSeedingData()
        {
            SeedingData.Add(new OrdenCompraEstado()
            {
                Idm = OrdenCompraEstado.ESTADO_GENERADA_IDM,
                Valor = OrdenCompraEstado.ESTADO_GENERADA_VALOR,
                Descripcion = OrdenCompraEstado.ESTADO_GENERADA_DESCRIPCION,
                Nombre = OrdenCompraEstado.ESTADO_GENERADA_NOMBRE
            });
            SeedingData.Add(new OrdenCompraEstado()
            {
                Idm = OrdenCompraEstado.ESTADO_APROBADA_IDM,
                Valor = OrdenCompraEstado.ESTADO_APROBADA_VALOR,
                Descripcion = OrdenCompraEstado.ESTADO_APROBADA_DESCRIPCION,
                Nombre = OrdenCompraEstado.ESTADO_APROBADA_NOMBRE
            });
            SeedingData.Add(new OrdenCompraEstado()
            {
                Idm = OrdenCompraEstado.ESTADO_ANULADA_IDM,
                Valor = OrdenCompraEstado.ESTADO_ANULADA_VALOR,
                Descripcion = OrdenCompraEstado.ESTADO_ANULADA_DESCRIPCION,
                Nombre = OrdenCompraEstado.ESTADO_ANULADA_NOMBRE
            });
        }
    }
}
