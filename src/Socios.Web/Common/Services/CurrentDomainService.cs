using GSF.Application.Common.Interfaces;
using GSF.Application.Security.SecurityTemp;

namespace Socios.Web.Common.Services;

public class CurrentDomainService : ICurrentDomainService
{
    //public const long _Const_DomainFIdm = BoardingDbContextSeed.BackOfficeIdm;
    public long? _DomainFIdm;
    //private string _Name;
    public string _Description;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISecurityTempStoreService _securityTempStoreService;

    public CurrentDomainService(IHttpContextAccessor httpContextAccessor, ISecurityTempStoreService securityTempStoreService)
    {
        _httpContextAccessor = httpContextAccessor;
        _securityTempStoreService = securityTempStoreService;
        //UserLogin = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        //SessionId = httpContextAccessor.HttpContext?.Session.Id;

        //string UserLoggedId = httpContextAccessor.HttpContext?.User?.FindFirstValue("user_id");
        //if (UserLoggedId != null) { 
        //    UserId = long.Parse(UserLoggedId);
        //}

        //if (!_SystemUse.HasValue)
        //{
        //    var claimValue = httpContextAccessor.HttpContext?.User?.FindFirstValue(Constants.ClaimSystemUser);
        //    if (!string.IsNullOrEmpty(claimValue))
        //    {
        //        _SystemUse = _SystemUse.HasValue ? _SystemUse.Value : bool.Parse(claimValue);
        //        SystemUse = _SystemUse.Value;
        //    }
        //}
    }

    public long DomainFIdm
    {
        get
        {
            if (!_DomainFIdm.HasValue)
            {
                //In some Web projects the DomainFIdm can remain as a constant Value.
                //_DomainFIdm = _Const_DomainFIdm;

                //In other projects the value can change, so must be stored and obtained in a session value.
                //var value = Task.Run(async () => await _securityTempStoreService.GetCurrentDomainFAsync()).Result;
                var value = _securityTempStoreService.GetCurrentDomainFAsync().GetAwaiter().GetResult();
                _DomainFIdm = value;
            }
            return _DomainFIdm.Value;
        }
    }

    public string Name
    {
        get
        {
            return "";
        }
    }

    public string Description
    {
        get
        {
            //Example using values in session.
            //if(!string.IsNullOrEmpty(_Description))
            //{
            //    var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirstValue(Constants.ClaimSystemUser);
            //    if (!string.IsNullOrEmpty(claimValue))
            //        _Description = claimValue;
            //}
            //return _Description;
            return "";
        }
    }
}