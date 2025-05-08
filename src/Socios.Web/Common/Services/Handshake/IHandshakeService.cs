using GS.Certifications.Application.Commons.Dtos.Sistemas;
using Socios.Web.Common.Services.Modules;

namespace Socios.Web.Common.Services.Handshake;

public interface IHandshakeService
{
    public Task<HandshakeResponseDto> ExecuteAsync(Module module);
}
