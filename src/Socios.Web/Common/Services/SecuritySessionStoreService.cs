using AutoMapper;
using GS.Certifications.Domain.Commons.Constants;
using GSF.Application.Security.Options.Queries;
using GSF.Application.Security.SecurityTemp;
using GSF.Application.Security.Services;
using MediatR;

namespace Socios.Web.Common.Services;


public class SecuritySessionStoreService : ISecurityTempStoreService
{
    const string KEY_OPTIONS = "__optionsSession";
    const string KEY_PREVIOUS_OPTION = "__previousOptionSession";
    const string KEY_CURRENT_OPTION = "__currentOptionSession";
    const string KEY_GRANTS = "__grantsSession";
    const string KEY_COMPANIES = "__companiesSession";
    const string KEY_CURRENT_COMPANY = "__currentCompanySession";
    const string KEY_CURRENT_DOMAINF = "__currentDomainFSession";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public SecuritySessionStoreService(IHttpContextAccessor httpContextAccessor,
                                       IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    #region Options

    public Task StoreOptionsAsync(List<OptionDto> options)
    {
        ClearOptionsAsync();
        _httpContextAccessor.HttpContext.Session.SetComplexData(KEY_OPTIONS, options);

        return Task.CompletedTask;
    }

    public Task StoreOptionsByPageAsync(string page, List<OptionDto> options)
    {
        string key = $"{KEY_OPTIONS}_{page}";
        ClearValueAsync(key);

        _httpContextAccessor.HttpContext.Session.SetComplexData(key, options);

        return Task.CompletedTask;
    }

    public Task<List<OptionDto>> GetOptionsAsync()
    {
        List<OptionDto> options = _httpContextAccessor.HttpContext.Session.GetComplexData<List<OptionDto>>(KEY_OPTIONS);

        return Task.FromResult(options);
    }

    public Task<List<OptionDto>> GetOptionsByPageAsync(string page)
    {
        List<OptionDto> options = _httpContextAccessor.HttpContext.Session.GetComplexData<List<OptionDto>>($"{KEY_OPTIONS}_{page}");

        return Task.FromResult(options);
    }

    public Task ClearOptionsAsync()
    {
        ClearValueAsync(KEY_OPTIONS);
        ClearValueAsync(KEY_CURRENT_OPTION);

        return Task.CompletedTask;
    }

    public Task<Unit> SetPreviousOptionAsync(int optionId)
    {
        _httpContextAccessor.HttpContext.Session.SetInt32(KEY_PREVIOUS_OPTION, optionId);
        return Task.FromResult(Unit.Value);
    }

    public Task<Unit> SetCurrentOptionAsync(int optionId)
    {
        _httpContextAccessor.HttpContext.Session.SetInt32(KEY_CURRENT_OPTION, optionId);
        return Task.FromResult(Unit.Value);
    }


    public Task<int> GetPreviousOptionAsync()
    {
        int? valor = _httpContextAccessor.HttpContext.Session.GetInt32(KEY_PREVIOUS_OPTION);
        return Task.FromResult(valor ?? -1);
    }

    public Task<int> GetCurrentOptionAsync()
    {
        int? valor = _httpContextAccessor.HttpContext.Session.GetInt32(KEY_CURRENT_OPTION);
        return Task.FromResult(valor ?? -1);
    }

    #endregion

    #region Grants

    public Task StoreGrantsAsync(List<SecurityGrantDto> grants)
    {
        ClearGrantsAsync();

        _httpContextAccessor.HttpContext.Session.SetComplexData(KEY_GRANTS, grants);

        return Task.CompletedTask;
    }

    public Task<List<SecurityGrantDto>> GetGrantsAsync(/*long optionId*/)
    {
        List<SecurityGrantDto> grants = _httpContextAccessor.HttpContext.Session.
                                 GetComplexData<List<SecurityGrantDto>>(KEY_GRANTS)
                                /*.Where(p => p.OptionId == optionId).ToList()*/;

        return Task.FromResult(grants);
    }

    public Task ClearGrantsAsync()
    {
        ClearValueAsync(KEY_GRANTS);

        return Task.CompletedTask;
    }

    public Task<SecurityGrantDto> GetGrantAsync(string grantName)
    {
        return Task.FromResult(_httpContextAccessor.HttpContext.Session.
                               GetComplexData<List<SecurityGrantDto>>(KEY_GRANTS).
                               Where(g => string.Compare(g.Name.ToLower(), grantName.ToLower()) == 0).FirstOrDefault());
    }

    #endregion

    #region Companies

    public Task StoreUserCompaniesAsync(List<SecurityUserCompaniesDto> companies)
    {
        ClearUserCompaniesAsync();

        _httpContextAccessor.HttpContext.Session.SetComplexData(KEY_COMPANIES, companies);

        return Task.CompletedTask;
    }

    public Task<List<SecurityUserCompaniesDto>> GetUserCompaniesAsync()
    {
        List<SecurityUserCompaniesDto> companies = _httpContextAccessor.HttpContext.Session.GetComplexData<List<SecurityUserCompaniesDto>>(KEY_COMPANIES);

        //if (companies == null) throw new Exception("No se encontraron companias almacenadas en sesión");

        return Task.FromResult(companies);
    }

    public Task<SecurityUserCompaniesDto> GetCurrentUserCompanyAsync()
    {
        return Task.FromResult(_httpContextAccessor.HttpContext.Session.GetComplexData<SecurityUserCompaniesDto>(KEY_CURRENT_COMPANY));
    }

    public void ClearUserCompaniesAsync()
    {
        ClearValueAsync(KEY_COMPANIES);
        ClearValueAsync(KEY_CURRENT_COMPANY);
    }

    public Task<Unit> SetCurrentUserCompanyAsync(SecurityUserCompaniesDto company)
    {
        _httpContextAccessor.HttpContext.Session.SetComplexData(KEY_CURRENT_COMPANY, company);

        return Task.FromResult(Unit.Value);
    }

    #endregion

    #region DomainF

    public Task<Unit> SetCurrentDomainFAsync(long domainFIdm)
    {
        //In some Web projects the DomainFIdm can remain as a constant Value. So theres nothing to set.
        //TODO - GJB: Can be replace for a throw new NotImplementedException
        return Task.FromResult(Unit.Value);

        //In other projects the value can change, so must be stored in a session value.
        //_httpContextAccessor.HttpContext.Session.SetInt32(KEY_CURRENT_DOMAINF, (int)domainFIdm);
        //return Task.FromResult(Unit.Value);
    }

    public Task<int> GetCurrentDomainFAsync()
    {
        //In some Web projects the DomainFIdm can remain as a constant Value.
        //TODO - GJB: Can be replace for a throw new NotImplementedException
        int? valor = (int)DomainFIdmConstants.Socios;
        return Task.FromResult(valor ?? -1);

        //In other projects the value can change, so must be stored and obtained in a session value.
        //int? valor = _httpContextAccessor.HttpContext.Session.GetInt32(KEY_CURRENT_DOMAINF);
        //return Task.FromResult(valor ?? -1);
    }

    public void ClearDomainFAsync()
    {
        ClearValueAsync(KEY_CURRENT_DOMAINF);
    }

    #endregion

    public Task ClearValueAsync(string key)
    {
        _httpContextAccessor.HttpContext.Session.Remove(key);

        return Task.CompletedTask;
    }
}
