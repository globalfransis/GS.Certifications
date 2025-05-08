using GS.Certifications.Application.Commons.Dtos.SfeApi;
using System.Threading.Tasks;

namespace GS.Certifications.Application.Interfaces.SfeApi;

public interface IValidarComprobanteService
{
    public Task<ValidarComprobanteResponseDto> ValidarComprobanteAsync(long cuitOperador, ValidarComprobanteRequestDto comprobante);
}
