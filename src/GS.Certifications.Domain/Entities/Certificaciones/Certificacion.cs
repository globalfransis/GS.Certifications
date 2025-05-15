using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.Certificaciones;

public class Certificacion : BaseIntEntity
{
    public short TipoEmpresaPortalId { get; set; }
    public TipoEmpresaPortal TipoEmpresaPortal { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Activa { get; set; }
    public List<SolicitudCertificacion> Solicitudes { get; set; }
    public List<DocumentoRequerido> DocumentosRequeridos { get; set; }
}
