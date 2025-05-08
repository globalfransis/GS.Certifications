using GS.Certifications.Application.GSFExtensions.GSFWebFilteTransferService;
using GSF.Application.Extensions.GSFMediatR;
using GSFWebFileTransferService.Abstractions.Builder;
using GSFWebFileTransferService.Abstractions.DefaultValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;

public class DeleteTempComprobanteCommand : IRequest<Unit>
{
    public string TargetPath { get; set; }
}

public class DeleteTempComprobanteCommandHandler : BaseRequestHandler<Unit, DeleteTempComprobanteCommand, Unit>
{
    private readonly IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> webFileTransferServiceBuilder;


    public DeleteTempComprobanteCommandHandler(IWebFileTransferServiceBuilder<StorageTypeGSFWFTS, BasicFileConfigurationTypeGSFWFTS> webFileTransferServiceBuilder)
    {
        this.webFileTransferServiceBuilder = webFileTransferServiceBuilder;
    }

    protected override Task<Unit> HandleRequestAsync(DeleteTempComprobanteCommand request, CancellationToken cancellationToken)
    {
        var fileTransferService = webFileTransferServiceBuilder.GetIWebFileTransferService(StorageTypeGSFWFTS.FileSystemStorage, ProveedoresFileConfiguration.Comprobantes);

        fileTransferService.DeleteTempRepository(request.TargetPath, false, true);

        return Unit.Task;
    }
}
