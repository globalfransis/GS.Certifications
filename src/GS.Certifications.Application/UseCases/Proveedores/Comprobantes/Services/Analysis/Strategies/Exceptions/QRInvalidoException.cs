using System;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies.Exceptions;

[Serializable]
public class QRInvalidoException : Exception
{
    public QRInvalidoException() : base("QR no procesado o inválido.") { }
}
