using GSF.Domain.Common;
using GSF.Domain.Entities.Security;
using System;

namespace GS.Certifications.Domain.Entities.Certificaciones.Documentos
{
    public class DocumentoCargado : BaseIntEntity
    {
        public int SolicitudId { get; set; }
        public SolicitudCertificacion Solicitud { get; set; }
        public int DocumentoRequeridoId { get; set; }
        public DocumentoRequerido DocumentoRequerido { get; set; }
        public string ArchivoURL { get; set; }
        public int Version { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int EstadoId { get; set; }
        public DocumentoEstado Estado { get; set; }
        public long? ValidadoPorId { get; set; }
        public User ValidadoPor { get; set; }
        public DateTime FechaSubida { get; set; }
    }

    public class DocumentoEstado : BaseFixedShortEntity
    {
        public const short PENDIENTE = 1; 
        public const short VALIDADO = 2; 
        public const short RECHAZADO = 3; 
        public const short VENCIDO = 4; 

        public string Descripcion { get; set; }
    }
}
