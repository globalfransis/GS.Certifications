using GSF.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Certifications.Domain.Entities.ModosLecturas;

namespace GS.Certifications.Infrastructure.Persistence.Configurations.ModosLecturas
{
    public class ModoLecturaConfiguration : BaseFixedEntityInt16Configuration<ModoLectura>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ModoLectura> builder)
        {
            builder.ToTable("mlr_ModosLecturas");

            builder.Property(i => i.Descripcion).IsRequired().HasMaxLength(200);
        }
        protected override void LoadSeedingData()
        {
            SeedingData.Add(new ModoLectura()
            {
                Idm = ModoLectura.QR_IDM,
                Descripcion = ModoLectura.QR_DESCRIPCION,
                Codigo = ModoLectura.QR_CODIGO
            });
            SeedingData.Add(new ModoLectura()
            {
                Idm = ModoLectura.OCR_DETALLE_IDM,
                Descripcion = ModoLectura.OCR_DETALLE_DESCRIPCION,
                Codigo = ModoLectura.OCR_DETALLE_CODIGO
            });
            SeedingData.Add(new ModoLectura()
            {
                Idm = ModoLectura.OCR_IMPUESTOS_IDM,
                Descripcion = ModoLectura.OCR_IMPUESTOS_DESCRIPCION,
                Codigo = ModoLectura.OCR_IMPUESTOS_CODIGO
            });
            SeedingData.Add(new ModoLectura()
            {
                Idm = ModoLectura.OCR_CABECERA_IDM,
                Descripcion = ModoLectura.OCR_CABECERA_DESCRIPCION,
                Codigo = ModoLectura.OCR_CABECERA_CODIGO
            });
            SeedingData.Add(new ModoLectura()
            {
                Idm = ModoLectura.MANUAL_IDM,
                Descripcion = ModoLectura.MANUAL_DESCRIPCION,
                Codigo = ModoLectura.MANUAL_CODIGO
            });
        }
    }
}
