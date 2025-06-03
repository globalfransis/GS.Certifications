using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;
using System;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.Certificaciones;

public class SolicitudCertificacion : BaseIntEntity
{
    public SolicitudCertificacion()
    {
        Guid = Guid.NewGuid();
    }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public int SocioId { get; set; }
    public EmpresaPortal Socio { get; set; }
    public int CertificacionId { get; set; }
    public Certificacion Certificacion { get; set; }
    public short EstadoId { get; set; }
    public SolicitudCertificacionEstado Estado { get; set; }
    public string Observaciones { get; set; }
    public string MotivoRechazo { get; set; }
    public List<DocumentoCargado> DocumentosCargados { get; set; }
    public DateTime? FechaSolicitud { get; set; }
    public DateTime? UltimaModificacionEstado { get; set; }
    public DateTime? VigenciaDesde { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    public short? OrigenId { get; set; }
    public Origen Origen { get; set; }
    public short? PropietarioActualId { get; set; }
    public Origen PropietarioActual { get; set; }
}

public class SolicitudCertificacionEstado : BaseFixedShortEntity
{
    public const short PENDIENTE = 1;
    public const short PRESENTADA = 2;
    public const short APROBADA = 3;
    public const short RECHAZADA = 4;
    public const short BORRADOR = 5;
    public const short REVISION = 6;

    public string Descripcion { get; set; }
}