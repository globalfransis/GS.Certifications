namespace GS.Certifications.Application.Commons.Dtos.EmailProcessor;

public record EmailProcessorResultDto
{
    public int? CantidadProcesado { get; set; }
    public double? DuracionMins { get; set; }
}
