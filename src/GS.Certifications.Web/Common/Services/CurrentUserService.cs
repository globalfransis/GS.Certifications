using GSF.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using GS.Certifications.Web.Common;
using System.Security.Claims;

namespace GS.Certifications.Web.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    public string _UserLogin;
    public string _SessionId;
    private bool? _SystemUse;
    public long? _UserId;

    //public long UserId { get; }
    //public string UserLogin { get; }
    //public string SessionId { get; }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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

    public long UserId
    {
        get
        {
            if (!_UserId.HasValue)
            {
                string UserLoggedId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("user_id");
                if (UserLoggedId != null)
                    _UserId = long.Parse(UserLoggedId);
            }
            return _UserId.Value;
        }
    }

    public string UserLogin
    {
        get
        {
            if (string.IsNullOrEmpty(_UserLogin))
                _UserLogin = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return _UserLogin;
        }
    }

    public string SessionId
    {
        get
        {
            if (string.IsNullOrEmpty(_SessionId))
                _SessionId = _httpContextAccessor.HttpContext?.Session.Id;
            return _SessionId;
        }
    }

    public bool SystemUse
    {
        get
        {
            if (!_SystemUse.HasValue)
            {
                var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirstValue(Constants.ClaimSystemUser);
                if (!string.IsNullOrEmpty(claimValue))
                    _SystemUse = bool.Parse(claimValue);
                //_SystemUse = bool.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(Constants.ClaimSystemUser));
            }
            return _SystemUse.Value;
        }
    }
}