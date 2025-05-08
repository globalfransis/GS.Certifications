using System;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;

[Serializable]
public class ComprobanteInexistenteException : Exception
{
    public ComprobanteInexistenteException() : base("El comprobante no existe.")
    {
    }
}