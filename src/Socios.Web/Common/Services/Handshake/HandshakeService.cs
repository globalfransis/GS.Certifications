using GS.Certifications.Application.Commons.Dtos.Sistemas;
using Socios.Web.Common.Services.Modules;
using System.Text;
using System.Text.Json;

namespace Socios.Web.Common.Services.Handshake;

public class HandshakeService : IHandshakeService
{
    public async Task<HandshakeResponseDto> ExecuteAsync(Module module)
    {

        // Crear el objeto a enviar en el body
        var requestBody = new { apiKey = module.ApiKey };
        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");


        HttpClient httpClient = new HttpClient();
        var response = await httpClient.PostAsync(module.HandShakeUrl, jsonContent);
        response.EnsureSuccessStatusCode(); // Lanza excepción si la respuesta no es exitosa

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<HandshakeResponseDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }


}
