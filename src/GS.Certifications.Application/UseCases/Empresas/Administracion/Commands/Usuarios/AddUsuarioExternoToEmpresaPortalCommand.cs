using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Token;
using GSF.Application.Services.Security.Groups;
using GSF.Application.Services.Security.Users;
using GSF.Application.Services.Security.Users.Dtos;
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
    public class AddUsuarioExternoToEmpresaPortalCommand : IRequest<Unit>//, IUsuarioEmpresaCreate // Adjust TResponse properly
    {
        public int EmpresaPortalId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<RolTipoDto> Roles { get; set; }

    }

    public class AddUsuarioExternoToEmpresaPortalCommandHandler :
        BaseRequestHandler<Unit, AddUsuarioExternoToEmpresaPortalCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;
        private readonly ICurrentCompanyService _companyService;
        private readonly IGroupService _groupService;
        private readonly IUsuarioEmpresaPortalService _usuarioEmpresaPortalService;
        private readonly IUserService _userService;
        private readonly ITokenManagerService _tokenService;
        private readonly ICurrentUserService _currentUserService;
        // Add services

        public AddUsuarioExternoToEmpresaPortalCommandHandler
            (ICertificationsDbContext context, IEmpresaPortalService service,
            ICurrentCompanyService currentCompany, IGroupService groupService,
            IUsuarioEmpresaPortalService usuarioEmpresaPortalService,
            IUserService userService,
            ITokenManagerService tokenService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _companyService = currentCompany;
            _groupService = groupService;
            _usuarioEmpresaPortalService = usuarioEmpresaPortalService;
            _userService = userService;
            _tokenService = tokenService;
            _currentUserService = currentUserService;
            // Inject dependencies
        }

        protected override async Task<Unit> HandleRequestAsync
            (AddUsuarioExternoToEmpresaPortalCommand request, CancellationToken cancellationToken)
        {
            User usuarioExistente = await _userService.GetByEmailAsync(request.Email, DomainFIdmConstants.Socios);
            if (usuarioExistente != null)
                await AgregarUsuarioExistente(usuarioExistente, request.Roles, request.EmpresaPortalId);
            else
                await AgregarNuevoUsuario(request);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task AgregarNuevoUsuario(AddUsuarioExternoToEmpresaPortalCommand request)
        {
            SecurityUserCompaniesDto currentCompany = await _companyService.GetCurrentCompanyAsync();
            Group grupo = await _groupService.GetByInternalCoreAsync(GroupsInternalCodeConstants.Socio);

            CreateRequestDto createRequestDto = new()
            {
                Login = request.Login,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdNumber = request.IdNumber,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                GroupOwnerId = (await _companyService.GetCurrentCompanyGroupAsync()).Id,  //para corregir
                SystemUse = false,
                UserTypeIdm = UserTypeIdmConstants.Socio, 
                Activated = false,
                Password = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()
            };

            CompaniesRequestDto companiesRequestDto = new();

            var companyUserGroups = new List<(long, long)>();

            companyUserGroups.Add(new() { Item1 = currentCompany.Id, Item2 = grupo.Id });

            companiesRequestDto.CompaniesUsersGroups = companyUserGroups;

            //long currentUserId = _currentUserService.UserId;

            User usuario = await _userService.CreateAsync(createRequestDto, companiesRequestDto);

            //User usuario = new()
            //{
            //    Login = request.Login,
            //    Email = request.Email,
            //    FirstName = request.FirstName,
            //    LastName = request.LastName,
            //    IdNumber = request.IdNumber,
            //    PhoneNumber = request.PhoneNumber,
            //    GroupOwnerId = (await _companyService.GetCurrentCompanyGroupAsync()).Id,  //para corregir
            //    SystemUse = false,
            //    UserTypeIdm = UserTypeIdmConstants.Socios, 
            //    Activated = true
            //};

            //CompanyUserGroup cug = new()
            //{
            //    Group = grupo,
            //    GroupId = grupo.Id,
            //    User = usuario,
            //    UserId = usuario.Id,
            //    CompanyId = currentCompany.Id
            //};
            //usuario.CompaniesUsersGroups.Add(cug);

            //usuario.SetPassword(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());

            UsuarioEmpresaPortal uep = new()
            {
                User = usuario,
                DomainFIdm = DomainFIdmConstants.Socios,
                EmpresaPortalId = request.EmpresaPortalId,
                FechaRegistracion = DateTime.Now,
                Habilitado = true
            };

            List<UsuarioEmpresaPortalRol> listaRoles = new List<UsuarioEmpresaPortalRol>();

            foreach (RolTipoDto rol in request.Roles)
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

            EmpresaPortal empresaPortal = await _context.EmpresasPortales.FirstOrDefaultAsync(src => src.Id == request.EmpresaPortalId);

            usuario.PostSaveDomainEvents.Add(
                new VinculacionUsuarioNuevoEmpresaEvent
                (usuario, DomainFIdmConstants.Socios, empresaPortal));

            _context.UserExternos.Add(uep);
        }
        private async Task AgregarUsuarioExistente(User usuario, List<RolTipoDto> roles, int empresaPortalId)
        {
            SecurityUserCompaniesDto currentCompany = await _companyService.GetCurrentCompanyAsync();
            Group grupo = await _groupService.GetByInternalCoreAsync(GroupsInternalCodeConstants.Socio);

            if (!usuario.CompaniesUsersGroups.Any(u => u.CompanyId == currentCompany.Id))
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

            IEnumerable<UsuarioEmpresaPortal> ueps = await _usuarioEmpresaPortalService.GetAllAsync(new Commons.Services.UsuarioEmpresaPortales.GetAllRequestDto()
            {
                UserId = usuario.Id,
                EmpresaPortalId = empresaPortalId,
            });

            if (ueps.Any())
                throw new ValidationErrorException
                        ("UsuarioExterno", "El usuario ya esta relacionado con la empresa portal");

            UsuarioEmpresaPortal uep = new()
            {
                UserId = usuario.Id,
                DomainFIdm = DomainFIdmConstants.Socios,
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

            VinculacionUsuarioEmpresaEvent vinculacionEvent = new(usuario, DomainFIdmConstants.Socios, empresaPortal);

            usuario.PreSaveDomainEvents.Add(vinculacionEvent);

            _context.UserExternos.Add(uep);
        }
    }
}
