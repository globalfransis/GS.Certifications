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
}
