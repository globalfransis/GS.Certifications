using GS.Certifications.Application.Commons.Dtos.Sistemas;
using GS.Certifications.Application.Interfaces;
using GSF.Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using GS.Certifications.Domain.Commons.Constants;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Sistemas.Commnads;

public class HandshakeCommand : IRequest<HandshakeResponseDto>
{
    public string ApiKey { get; set; }

    public class HandshakeCommandHandler : IRequestHandler<HandshakeCommand, HandshakeResponseDto>
    {
        private readonly IHandshakeTokenService _handshakeTokenService;
        private readonly IConfiguration _configuration;

        public HandshakeCommandHandler(IHandshakeTokenService handshakeTokenService, IConfiguration configuration)
        {
            _handshakeTokenService = handshakeTokenService;
            _configuration = configuration;
        }
        public Task<HandshakeResponseDto> Handle(HandshakeCommand request, CancellationToken cancellationToken)
        {

            string apikey = request.ApiKey;
            ModuleConfiguration module = _configuration.GetSection("GS.MIFRO.Module").Get<ModuleConfiguration>();
            string validApiKey = module.ApiKey;
            if (!validApiKey.Equals(apikey)) throw new ValidationErrorException("apikey", "apikey invalida");

            string domainF = DomainFIdmConstants.Socios.ToString();
            var encriptionKey = GenerateSecureKey();

            var token = _handshakeTokenService.GetHandshakeToken(apikey, encriptionKey, domainF);
            return Task.FromResult(new HandshakeResponseDto() { EncriptionKey = encriptionKey, Token = token });
        }
    }

    public static string GenerateSecureKey()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] keyBytes = new byte[32]; // 256 bits
            rng.GetBytes(keyBytes);
            return Convert.ToBase64String(keyBytes); // Se devuelve en Base64
        }
    }

    public record ModuleConfiguration
    {
        public string ApiKey { get; set; }
    }
}
