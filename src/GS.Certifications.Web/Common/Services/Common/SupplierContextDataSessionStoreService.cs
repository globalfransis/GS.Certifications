using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace GS.Certifications.Web.Common.Services.Common;

public class SupplierContextDataSessionStoreService : ISupplierContextDataTempStoreService
{
    const string __KEY_BOARDING_CONTEXT_DATA = "__KEY_BOARDING_CONTEXT_DATA";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public SupplierContextDataSessionStoreService(IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }



}
