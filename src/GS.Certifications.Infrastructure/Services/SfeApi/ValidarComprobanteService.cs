using GS.Certifications.Application.Commons.Dtos.SfeApi;
using GS.Certifications.Application.Interfaces.SfeApi;
using System;
using System.Threading.Tasks;

namespace GS.Certifications.Infrastructure.Services.SfeApi;

public class ValidarComprobanteService : IValidarComprobanteService
{
    private readonly ValidarComprobanteConfiguration _configuration;

    public ValidarComprobanteService(ValidarComprobanteConfiguration configuration)
    {
        _configuration = configuration;

    }
    public async Task<ValidarComprobanteResponseDto> ValidarComprobanteAsync(long cuitOperador, ValidarComprobanteRequestDto comprobante)
    {
        throw new NotImplementedException();
    }
}
