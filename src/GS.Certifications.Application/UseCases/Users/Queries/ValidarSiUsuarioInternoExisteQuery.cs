using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Services.Security.Users;
using GSF.Domain.Entities.Security;
using MediatR;
using GS.Certifications.Domain.Commons.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Users.Queries
{
    public class ValidarSiUsuarioInternoExisteQuery : IRequest<bool>
    {
        public string Email { get; set; }
    }

    // Query handler definition
    public class ValidarSiUsuarioInternoExisteQueryHandler : BaseRequestHandler<bool, ValidarSiUsuarioInternoExisteQuery, bool>
    {
        private readonly IUserService _service;

        public ValidarSiUsuarioInternoExisteQueryHandler(IUserService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }

        protected override async Task<bool> HandleRequestAsync(ValidarSiUsuarioInternoExisteQuery request, CancellationToken cancellationToken)
        {
            User usuario = await _service.GetByEmailAsync(request.Email, UserTypeIdmConstants.Backend);
            return usuario != null;
        }
    }
}
