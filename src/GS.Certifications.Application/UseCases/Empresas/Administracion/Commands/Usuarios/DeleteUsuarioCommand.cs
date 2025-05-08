using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using GS.Certifications.Application.CQRS.DbContexts;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GS.Certifications.Domain.Entities.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Commands.Usuarios
{
    public class DeleteUsuarioCommand : IRequest<Unit> // Adjust TResponse properly
    {
        public int Id { get; set; } // Adjust Id type properly
        public byte[] RowVersion { get; set; }
        // Add properties
    }

    public class DeleteUsuarioExternoCommandHandler : BaseRequestHandler<Unit, DeleteUsuarioCommand, Unit> // Adjust TEntity and TResponse properly
    {
        // IMPORTANT: use the corresponding DbContext interface
        private readonly ICertificationsDbContext _context;

        // Add services

        public DeleteUsuarioExternoCommandHandler(ICertificationsDbContext context)
        {
            _context = context;
            // Inject dependencies
        }

        protected override async Task<Unit> HandleRequestAsync(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            UsuarioEmpresaPortal usuario = await _context.UsuarioEmpresasPortales.FirstOrDefaultAsync(src => src.Id == request.Id);

            IQueryable<UsuarioEmpresaPortalRol> roles = _context.UsuarioEmpresaPortalRol.Where(src => src.UsuarioEmpresaPortalId == request.Id).AsQueryable();

            _context.UsuarioEmpresaPortalRol.RemoveRange(roles);

            //var rolesList = roles.ToList();

            //foreach (var rol in rolesList)
            //{
            //    rol.IsDeleted = true;
            //}

            _context.UsuarioEmpresasPortales.Remove(usuario);

            //usuario.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
