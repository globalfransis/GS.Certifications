using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Services.Security.Groups;
using GSF.Application.Services.Security.Users;
using GSF.Domain.Entities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Commons.Constants;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Seguridad;
using GS.Certifications.Domain.Entities.Seguridad.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.Usuarios
{
    public class AddUsuarioInternoToEmpresaPortalCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int EmpresaPortalId { get; set; }
        public string Email { get; set; }
        public List<RolTipoDto> Roles { get; set; }
    }

    public class AddUsuarioInternoToEmpresaPortalCommandHandler : BaseRequestHandler<Unit, AddUsuarioInternoToEmpresaPortalCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly ICurrentCompanyService _companyService;
        private readonly IGroupService _groupService;
        private readonly IUsuarioEmpresaPortalService _usuarioEmpresaPortalService;
        private readonly IUserService _userService;

        // Add services

        public AddUsuarioInternoToEmpresaPortalCommandHandler
        (ICertificationsDbContext context, IEmpresaPortalService service,
            ICurrentCompanyService currentCompany, IGroupService groupService,
            IUsuarioEmpresaPortalService usuarioEmpresaPortalService, IUserService userService)
        {
            _context = context;
            _companyService = currentCompany;
            _groupService = groupService;
            _usuarioEmpresaPortalService = usuarioEmpresaPortalService;
            _userService = userService;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(AddUsuarioInternoToEmpresaPortalCommand request, CancellationToken cancellationToken)
        {
            User usuario = await _userService.GetByEmailAsync(request.Email, DomainFIdmConstants.Backoffice);
            await AgregarUsuarioExistente(usuario, request.Roles, request.EmpresaPortalId);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task AgregarUsuarioExistente(User usuario, List<RolTipoDto> roles, int empresaPortalId)
        {
            SecurityUserCompaniesDto currentCompany = await _companyService.GetCurrentCompanyAsync();
            Group grupo = await _groupService.GetByInternalCoreAsync(GroupsInternalCodeConstants.Socio);

            if (!usuario.CompaniesUsersGroups.Any(u => u.CompanyId == currentCompany.Id && u.Group == grupo))
            {

                CompanyUserGroup cug = new()
                {
                    Group = grupo,
                    GroupId = grupo.Id,
                    User = usuario,
                    UserId = usuario.Id,
                    CompanyId = currentCompany.Id
                };
                usuario.CompaniesUsersGroups.Add(cug);
            }

            IEnumerable<UsuarioEmpresaPortal> ueps = await _usuarioEmpresaPortalService.GetAllAsync(new GetAllRequestDto()
            {
                UserId = usuario.Id,
                EmpresaPortalId = empresaPortalId,
            });

            if (ueps.Any())
            {
                throw new ValidationErrorException
                        ("UsuarioInterno", "El usuario ya esta relacionado con la empresa portal");
            }

            UsuarioEmpresaPortal uep = new()
            {
                UserId = usuario.Id,
                DomainFIdm = DomainFIdmConstants.Backoffice,
                EmpresaPortalId = empresaPortalId,
                FechaRegistracion = DateTime.Now,
                Habilitado = true
            };

            List<UsuarioEmpresaPortalRol> listaRoles = new List<UsuarioEmpresaPortalRol>();

            foreach (RolTipoDto rol in roles)
            {
                RolTipo rolTipo = _context.RolesTipos
                    .Where(u => u.Idm == rol.Idm).FirstOrDefault();

                UsuarioEmpresaPortalRol usuarioEmpresaPortalRol = new()
                {
                    RolTipo = rolTipo,
                    Habilitado = true
                };

                listaRoles.Add(usuarioEmpresaPortalRol);
            }
            uep.Roles = listaRoles;

            EmpresaPortal empresaPortal = await _context.EmpresasPortales.FirstOrDefaultAsync(src => src.Id == empresaPortalId);

            VinculacionUsuarioEmpresaEvent vinculacionEvent = new(usuario, DomainFIdmConstants.Backoffice, empresaPortal);

            usuario.PreSaveDomainEvents.Add(vinculacionEvent);

            _context.UserExternos.Add(uep);
        }
    }
}
