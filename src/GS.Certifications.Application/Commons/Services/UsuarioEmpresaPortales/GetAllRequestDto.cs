namespace GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;

public record GetAllRequestDto
{
    public long? UserId { get; set; }

    public long? EmpresaPortalId { get; set; }

    public bool? Habilitado { get; set; }
}
