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

    public class PresentacionSolicitudDocumentosSinValidarException : Exception
    {
        public PresentacionSolicitudDocumentosSinValidarException() : base($"Uno o más documentos de la solicitud no están validados.")
        {
        }
    }

    public class AprobacionSolicitudDocumentosInvalidosException : Exception
    {
        public AprobacionSolicitudDocumentosInvalidosException() : base($"Uno o más documentos de la solicitud no son válidos.")
        {
        }
    }


    public class SolicitudVigenciaNulaException : Exception
    {
        public SolicitudVigenciaNulaException() : base($"La vigencia de la solicitud es requerida.")
        {
        }
    }

    public class SolicitudVigenciaInvalidaException : Exception
    {
        public SolicitudVigenciaInvalidaException() : base($"La vigencia de la solicitud es inválida.")
        {
        }
    }

    public class DocumentoVigenciaNulaException : Exception
    {
        public DocumentoVigenciaNulaException() : base($"La vigencia del documento es requerida.")
        {
        }
    }

    public class DocumentoArchivoNuloException : Exception
    {
        public DocumentoArchivoNuloException() : base($"No se ha importado un archivo para el documento.")
        {
        }
    }

    public class DocumentoVigenciaInvalidaException : Exception
    {
        public DocumentoVigenciaInvalidaException() : base($"La vigencia del documento es inválida.")
        {
        }
    }

    public class SolicitudEliminacionEstadoInvalidoException : Exception
    {
        public SolicitudEliminacionEstadoInvalidoException() : base($"No se puede eliminar esta solicitud.")
        {
        }
    }
}
