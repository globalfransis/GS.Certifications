using AutoMapper;

namespace Socios.Web.Common.Services.Common;

public class SociosContextDataSessionStoreService : ISociosContextDataTempStoreService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public SociosContextDataSessionStoreService(IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }



}
