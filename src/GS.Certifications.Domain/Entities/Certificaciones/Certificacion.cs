using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.GSF.Extensions;
using GSF.Domain.Common;
using GSF.Domain.Entities.Companies;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.Certificaciones;

public class Certificacion : BaseIntEntity
{
    public long CompanyId { get; set; } = GSCompany.GLOBALSIS;
    public Company Company { get; set; }
    public short TipoEmpresaPortalId { get; set; }
    public TipoEmpresaPortal TipoEmpresaPortal { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Activa { get; set; }
    public short CantidadAprobaciones { get; set; } = 0;
    public List<SolicitudCertificacion> Solicitudes { get; set; }
    public List<DocumentoRequerido> DocumentosRequeridos { get; set; }
}
