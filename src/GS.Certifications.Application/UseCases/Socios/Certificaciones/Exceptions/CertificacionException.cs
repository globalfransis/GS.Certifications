using System;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions
{
    [Serializable]
    public class CertificacionInexistenteException : Exception
    {
        public CertificacionInexistenteException() : base("La certificación no existe.")
        {
        }
    }

    [Serializable]
    public class SolicitudCertificacionInexistenteException : Exception
    {
        public SolicitudCertificacionInexistenteException() : base("La solicitud de certificación no existe.")
        {
        }
    }

    [Serializable]
    public class DocumentoInexistenteException : Exception
    {
        public DocumentoInexistenteException() : base("El documento no existe.")
        {
        }
    }

    public class YaExisteSolicitudCertificacionException : Exception
    {
        public YaExisteSolicitudCertificacionException() : base($"Ya existe una solicitud en curso para la certificación indicada.")
        {
        }
    }

    public class PresentacionSolicitudDocumentosInvalidosException : Exception
    {
        public PresentacionSolicitudDocumentosInvalidosException() : base($"Uno o más documentos de la solicitud no fueron presentados.")
        {
        }
    }
}
