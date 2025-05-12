using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;
using GSF.Domain.Entities.Security;

namespace GS.Certifications.Domain.Entities.Certificaciones;

public class SolicitudCertificacion : BaseIntEntity
{
    public int SocioId { get; set; }
    public EmpresaPortal Socio { get; set; }
    public int CertificacionId { get; set; }
    public Certificacion Certificacion { get; set; }
    public short EstadoId { get; set; }
    public SolicitudCertificacionEstado Estado { get; set; }
    public short CantidadAprobaciones { get; set; }
    public string Observaciones { get; set; }
}

public class SolicitudCertificacionEstado : BaseFixedShortEntity
{
    public const short PENDIENTE = 1;
    public const short EN_PROCESO = 2;
    public const short APROBADA = 3;
    public const short RECHAZADA = 4;

    public string Descripcion { get; set; }
}