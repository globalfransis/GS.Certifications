using System;

namespace GS.Certifications.Application.Commons.Dtos.SfeApi;

public record ValidarComprobanteRequestDto
{
    public string Modo { get; set; }
    public long CuitEmisor { get; set; }
    public int PtoVta { get; set; }
    public int CbteTipo { get; set; }
    public long CbteNro { get; set; }
    public DateOnly CbteFch { get; set; }
    public double ImpTotal { get; set; }
    public string CodAutorizacion { get; set; }

    public int DocTipoReceptor { get; set; } = 80;
    public long DocNroReceptor { get; set; }
}
