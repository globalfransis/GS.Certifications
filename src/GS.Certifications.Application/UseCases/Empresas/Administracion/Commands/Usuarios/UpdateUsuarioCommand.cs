using DocumentFormat.OpenXml.InkML;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using GS.Certifications.Domain.Entities.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.Usuarios
{
    public class UpdateUsuarioCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        public int EmpresaPortalId { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<RolTipoDto> Roles { get; set; }
    }

    public class UpdateUsuarioExternoCommandHandler : BaseRequestHandler<Unit, UpdateUsuarioCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;

        // Add services

        public UpdateUsuarioExternoCommandHandler(ICertificationsDbContext context)
        {
            _context = context;
            // Inject dependencies
        }

        protected async override Task<Unit> HandleRequestAsync(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            UsuarioEmpresaPortal usuario = await _context
                .UsuarioEmpresasPortales
                .Include(u => u.Roles)
                    .ThenInclude(ud => ud.RolTipo)
                .Where(src => src.Id == request.Id).FirstOrDefaultAsync();

            List<UsuarioEmpresaPortalRol> listaRoles = usuario.Roles.ToList();

            if (request.Roles.Count != 0)
            {
                foreach (RolTipoDto rol in request.Roles)
                {
                    if (!usuario.Roles.Any(r => r.RolTipo.Idm == rol.Idm))
                    {
                        UsuarioEmpresaPortalRol empresaRol = new UsuarioEmpresaPortalRol()
                        {
                            RolTipoId = rol.Idm,
                            Habilitado = true
                        };
                        listaRoles.Add(empresaRol);
                    }
                }
            }

            foreach (UsuarioEmpresaPortalRol usuarioEmpresaPortalRol in usuario.Roles)
            {
                if (!request.Roles.Any(r => r.Idm == usuarioEmpresaPortalRol.RolTipoId))
                {
                    _context.UsuarioEmpresaPortalRol.Remove(usuarioEmpresaPortalRol);
                    listaRoles.Remove(usuarioEmpresaPortalRol);
                }
            }

            usuario.Roles = listaRoles;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
