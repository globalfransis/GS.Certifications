using GSF.Application.Common.Interfaces;
using GSF.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using GS.Certifications.Web.Common.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    private readonly IMailSender _mailSender;
    private readonly IAppInfoService _appInfoService;
    private readonly ITargetPathResolver _targetPathResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _environment;
    private readonly ApplicationConfiguration _configuration;

    [BindProperty] public string LogFilter { get; set; }
    [BindProperty] public string UserDescription { get; set; }
    [BindProperty] public bool IsReportDispatched { get; set; } = false;
    [BindProperty] public bool FirstLoad { get; set; } = true;

    [BindProperty] public string ErrorMessage { get; set; }

    public ErrorModel(IMailSender mailSender,
                      IAppInfoService appInfoService,
                      ITargetPathResolver targetPathResolver,
                      IHttpContextAccessor httpContextAccessor,
                      IWebHostEnvironment environment,
                      ApplicationConfiguration configuration)
    {
        _mailSender = mailSender;
        _appInfoService = appInfoService;
        _targetPathResolver = targetPathResolver;
        _httpContextAccessor = httpContextAccessor;
        _environment = environment;
        _configuration = configuration;
    }

    public void OnGet()
    {
        CaptureExceptionData();
        FirstLoad = false;
    }

    private void CaptureExceptionData()
    {
        LogFilter = $"SessionId='{_httpContextAccessor.HttpContext.Session.Id}' and RequestId='{_httpContextAccessor.HttpContext.TraceIdentifier}'";
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        string Path = exceptionHandlerPathFeature?.Path;
        string Type = exceptionHandlerPathFeature?.Error.GetType()?.Name;
        string Message = exceptionHandlerPathFeature?.Error.Message;

        ErrorMessage = Message;

        string StackTrace = exceptionHandlerPathFeature?.Error.StackTrace;

        _httpContextAccessor.HttpContext.Session.SetComplexData("_ERROR_INFO", (Path, Type, Message, StackTrace));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (FirstLoad)
        {
            CaptureExceptionData();
            FirstLoad = false;
            return Page();
        }
        else
        {
            try
            {
                var errorData = _httpContextAccessor.HttpContext.Session.GetComplexData<(string Path, string Type, string Message, string StackTrace)>("_ERROR_INFO");
                string templatePath = _appInfoService.CompleteFileSystemPath(@"static\ErrorReportTemplate.html");
                string htmlBody;

                string recoveryPath = _targetPathResolver.GetResolvedUri(string.Empty);
                string recoveryHost = _targetPathResolver.GetHost();
                string scheme = _targetPathResolver.GetScheme();

                using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
                {
                    htmlBody = SourceReader.ReadToEnd();
                    htmlBody = htmlBody.Replace("{System}", $"{_environment.ApplicationName} - {_environment.EnvironmentName}");
                    htmlBody = htmlBody.Replace("{root}", $"{scheme}://{recoveryHost}{recoveryPath}");
                    htmlBody = htmlBody.Replace("{UserName}", _httpContextAccessor.HttpContext.User.Identity.Name);
                    htmlBody = htmlBody.Replace("{UserDescription}", UserDescription);
                    htmlBody = htmlBody.Replace("{LogFilter}", LogFilter);
                    htmlBody = htmlBody.Replace("{Path}", errorData.Path);
                    htmlBody = htmlBody.Replace("{Type}", errorData.Type);
                    htmlBody = htmlBody.Replace("{Message}", errorData.Message);
                    htmlBody = htmlBody.Replace("{StackTrace}", errorData.StackTrace);
                }

                await _mailSender.EnviarAsync(_configuration.ErrorReportMailRecipient, $"Reporte de Error - {_environment.ApplicationName}", htmlBody);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error al enviar reporte de error: {ex.Message}", ex);
            }

            return RedirectToPage("/Error", "ReportDispatched");
        }
    }

    public void OnGetReportDispatched()
    {
        IsReportDispatched = true;
    }

}
