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
}
