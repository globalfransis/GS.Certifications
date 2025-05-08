using System.Collections.Generic;

namespace GS.Certifications.Application.Commons.Dtos.SfeApi;

public record ValidarComprobanteResponseDto
{
    public bool Autorizado { get; set; }
    public List<string> Observaciones { get; set; }

    public ValidarComprobanteResponseDto(bool autorizado, List<string> observaciones)
    {
        Autorizado = autorizado;
        Observaciones = observaciones;
    }
}
