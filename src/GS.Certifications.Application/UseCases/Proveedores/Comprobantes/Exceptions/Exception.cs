using System.Collections.Generic;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;

[System.Serializable]
public class ComprobanteYaExisteException : System.Exception
{
    public ComprobanteYaExisteException(int nro) : base($"El comprobante {nro:D8} ya existe.") { }
}

[System.Serializable]
public class ComprobanteDetallesVaciosException : System.Exception
{
    public ComprobanteDetallesVaciosException() : base("El comprobante debe tener al menos un detalle.") { }
}

[System.Serializable]
public class ComprobanteTotalNetoInvalidoException : System.Exception
{
    public ComprobanteTotalNetoInvalidoException() : base("La suma de los ítems sin impuestos menos las bonificaciones debe ser igual al total neto.") { }
}

[System.Serializable]
public class ComprobanteTotalImporteIVAInvalido : System.Exception
{
    public ComprobanteTotalImporteIVAInvalido() : base("El importe total de IVA es inválido.") { }
}

[System.Serializable]
public class ComprobanteTotalImporteImpuestosInternosInvalido : System.Exception
{
    public ComprobanteTotalImporteImpuestosInternosInvalido() : base("El importe total de impuestos internos es inválido.") { }
}

[System.Serializable]
public class ComprobanteTotalImporteImpuestosProvInvalido : System.Exception
{
    public ComprobanteTotalImporteImpuestosProvInvalido() : base("El importe total de impuestos provinciales es inválido.") { }
}

[System.Serializable]
public class ComprobanteTotalPercepcionesIVAInvalido : System.Exception
{
    public ComprobanteTotalPercepcionesIVAInvalido() : base("El importe total de percepciones IVA es inválido.") { }
}

[System.Serializable]
public class ComprobanteTotalPercepcionesIIBBInvalido : System.Exception
{
    public ComprobanteTotalPercepcionesIIBBInvalido() : base("El importe total de percepciones de IIBB es inválido.") { }
}

[System.Serializable]
public class ComprobanteTotalPercepcionesMunicipalInvalido : System.Exception
{
    public ComprobanteTotalPercepcionesMunicipalInvalido() : base("El importe total de percepciones municipales es inválido.") { }
}

[System.Serializable]
public class ComprobanteSubtotalInvalidoException : System.Exception
{
    public ComprobanteSubtotalInvalidoException() : base("La suma de los ítems sin impuestos debe ser igual al subtotal.") { }
}

[System.Serializable]
public class ComprobanteTotalInvalidoException : System.Exception
{
    public ComprobanteTotalInvalidoException() : base("El total del comprobante no coincide con el cálculo esperado. " +
                "Verifique los importes de los ítems, los impuestos, las bonificaciones y las percepciones.")
    { }

    public ComprobanteTotalInvalidoException(decimal totalEsperado, decimal importeTotal) : base($"El total del comprobante no coincide con el cálculo esperado. " +
                $"Total calculado: {totalEsperado}, Total ingresado: {importeTotal}. " +
                $"Verifique los importes de los ítems, los impuestos, las bonificaciones y las percepciones.")
    {

    }
}

[System.Serializable]
public class ComprobanteImporteBonificacionInvalidoException : System.Exception
{
    public ComprobanteImporteBonificacionInvalidoException() : base("El Importe Bonificación debe ser igual a la suma de las bonificaciones de los ítems.") { }
}

[System.Serializable]
public class Iva21InvalidoException : System.Exception
{
    public Iva21InvalidoException() : base("IVA 21 es inválido.") { }
}

[System.Serializable]
public class Iva105InvalidoException : System.Exception
{
    public Iva105InvalidoException() : base("IVA 10.5 es inválido.") { }
}

[System.Serializable]
public class ComprobanteYaFueAprobadoException : System.Exception
{
    public ComprobanteYaFueAprobadoException() : base("El comprobante ya fue aprobado.") { }
}

[System.Serializable]
public class ComprobanteSinConstatarEnARCAException : System.Exception
{
    public ComprobanteSinConstatarEnARCAException() : base("El comprobante no es fue constatado en ARCA.") { }
}

[System.Serializable]
public class ComprobantePeriodoRequeridoException : System.Exception
{
    public ComprobantePeriodoRequeridoException() : base("Se requiere asignar un período al comprobante.") { }
}

[System.Serializable]
public class ComprobanteQRInvalido : System.Exception
{
    public ComprobanteQRInvalido() : base("El código QR del comprobante es inválido.") { }

    public ComprobanteQRInvalido(string message) : base(message)
    {
    }

    public ComprobanteQRInvalido(string v, Newtonsoft.Json.JsonException jsonEx) : base($"Error deserializando JSON del QR: {jsonEx.Message}") { }
}

[System.Serializable]
public class ComprobanteDetalleInvalidoException : System.Exception
{
    private List<(int, string, string)> errores = new(); // Lista de tuplas para almacenar los errores de los detalles

    public List<(int, string, string)> Errores => errores;

    public ComprobanteDetalleInvalidoException(List<(int, string, string)> errores)
    {
        this.errores = errores;
    }
}

[System.Serializable]
public class ComprobantePercepcionesInvalidasException : System.Exception
{
    private List<(int, string, string)> errores = new(); // Lista de tuplas para almacenar los errores de los detalles

    public List<(int, string, string)> Errores => errores;

    public ComprobantePercepcionesInvalidasException(List<(int, string, string)> errores)
    {
        this.errores = errores;
    }
}

