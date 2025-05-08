namespace GS.Certifications.Application.Commons.Dtos.Sistemas;

public record HandshakeResponseDto
{
    public string Token { get; set; }
    public string EncriptionKey { get; set; }
}
