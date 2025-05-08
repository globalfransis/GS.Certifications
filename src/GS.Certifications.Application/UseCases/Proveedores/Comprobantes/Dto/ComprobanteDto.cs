using AutoMapper;
using GSF.Application.Common.Mappings;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Impuestos;
using GS.Certifications.Domain.Entities.Percepciones;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;

public class ComprobanteDto : IMapFrom<Comprobante>
{
    public int Id { get; set; }
    public string Guid { get; set; }
    public string NroIdentificacionFiscalPro { get; set; }
    //public string DomicilioPro { get; set; }
    public short? CategoriaTipoEmisorId { get; set; }
    public short? ComprobanteTipoId { get; set; }
    public string ComprobanteTipo { get; set; }
    public string ComprobanteTipoDescAbreviada { get; set; }
    public short? CategoriaTipoReceptorId { get; set; }
    public string NroIdentificacionFiscalCompany { get; set; }
    public int? PuntoVenta { get; set; }
    public string Letra { get; set; }
    public int? Numero { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaRegistracion { get; set; }
    public int? TipoCodigoAutorizacionId { get; set; }
    public string CodigoAutorizacion { get; set; }
    public long? MonedaId { get; set; }
    public decimal? ImporteNeto { get; set; }
    public decimal? ImporteTotal { get; set; }
    public decimal? ImporteIVA { get; set; }
    public decimal? ImporteBonificacion { get; set; }
    public decimal? ImportePercepcionIVA { get; set; }
    public decimal? ImportePercepcionIIBB { get; set; }
    public decimal? ImportePercepcionMunicipal { get; set; }
    public decimal? ImporteImpuestosInternos { get; set; }
    public decimal? ImporteOtrosTributosProv { get; set; }
    public decimal? Iva21 { get; set; } = default;
    public decimal? Iva105 { get; set; } = default;
    public int? EmpresaId { get; set; }
    public long? CompanyId { get; set; }
    public string CodigoHES { get; set; }
    public int? ComprobanteEstadoId { get; set; }
    public string Comentarios { get; set; }
    public string MotivoRechazo { get; set; }
    public string Moneda { get; set; }
    public string Estado { get; set; }
    public List<ComprobanteDetalleDto> Detalles { get; set; } = new();
    public List<ComprobanteImpuestoDto> Impuestos { get; set; } = new();
    public List<ComprobantePercepcionDto> Percepciones { get; set; } = new();
    public string ImporteTotalFormateado => ImporteTotal?.ToString("C", CultureInfo.GetCultureInfo("es-AR"));

    public int? PeriodoId { get; set; }

    public string Proveedor { get; set; }
    public string Empresa { get; set; }

    public bool? ValidacionQR { get; set; } = false;

    public short? EstadoValidacionARCAId { get; set; } = EstadoValidacionARCA.NO_VALIDADA;
    public short? EstadoValidacionARCAQRId { get; set; } = EstadoValidacionARCA.NO_VALIDADA;
    public string ObservacionesARCA { get; set; }

    public short? CondicionVentaId { get; set; } = CondicionVenta.OTRO;
    public CondicionVenta CondicionVenta { get; set; }
    public string NroRemito { get; set; }
    public string NroOrdenCompra { get; set; }
    public decimal? Cotizacion { get; set; }

    public DateTime? FechaVencimiento { get; set; }
    public DateTime? FechaVencimientoCodigoAutorizacion { get; set; }

    public short? PropietarioActualId { get; set; }

    public string NombreArchivo { get; set; }

    public string Comprobante =>
        $"{ComprobanteTipoDescAbreviada} {PuntoVenta:D5}-{Numero:D8}";
    public byte[] RowVersion { get; set; }

    public string ModifiedBy { get; set; }


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comprobante, ComprobanteDto>()
                .ForMember(dst => dst.ComprobanteTipo, opt => opt.MapFrom(src => src.ComprobanteTipo.Descripcion))
                .ForMember(dst => dst.ComprobanteTipoDescAbreviada, opt => opt.MapFrom(src => src.ComprobanteTipo.DescAbreviada))
                .ForMember(dst => dst.Moneda, opt => opt.MapFrom(src => src.Moneda.Code))
                .ForMember(dst => dst.Estado, opt => opt.MapFrom(src => src.ComprobanteEstado.Descripcion))
                // TODO: refactorizar! idealmente tengamos una prop Proveedor (RazonSocial) y otra que sea CUIT, y mostrar la concateniacion de ambos en el front
                // de ese modo separamos la obtencion de los datos de su presentación en el frontend
                .ForMember(dst => dst.Proveedor, opt => opt.MapFrom(src => $"{src.Empresa.RazonSocial} - CUIT: {src.NroIdentificacionFiscalPro}"))
                .ForMember(dst => dst.Empresa, opt => opt.MapFrom(src => src.Empresa.RazonSocial))
                //.ForMember(dst => dst.Iva21, opt => opt.MapFrom(src => GetIva21(src)))
                //.ForMember(dst => dst.Iva105, opt => opt.MapFrom(src => GetIva105(src)))
                ;
        }

        //private static decimal GetIva105(Comprobante src)
        //{
        //    var iva = src.Impuestos.Where(i => i.ImpuestoId == Impuesto.IVA_10_5).FirstOrDefault();
        //    return iva?.ImporteTotal is not null ? iva.ImporteTotal : default;

        //}

        //private static decimal GetIva21(Comprobante src)
        //{
        //    var iva = src.Impuestos.Where(i => i.ImpuestoId == Impuesto.IVA_21).FirstOrDefault();
        //    return iva?.ImporteTotal is not null ? iva.ImporteTotal : default;
        //}
    }
}

public class ComprobanteImpuestoDto : IMapFrom<ImpuestoDetalle>
{
    public int Id { get; set; }
    public long ComprobanteId { get; set; }
    public int ImpuestoId { get; set; }
    public string Impuesto { get; set; }
    public string Descripcion { get; set; }
    public decimal ImporteTotal { get; set; }
    public bool Deleted { get; set; } = false;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ImpuestoDetalle, ComprobanteImpuestoDto>()
                .ForMember(dst => dst.Impuesto, opt => opt.MapFrom(src => src.Impuesto.Descripcion));
        }
    }
}

public class ComprobantePercepcionDto : IMapFrom<PercepcionDetalle>
{
    public int Id { get; set; }
    public int ComprobanteId { get; set; }
    public int PercepcionId { get; set; }
    public string Percepcion { get; set; }
    public string Descripcion { get; set; }
    public decimal Alicuota { get; set; }
    public decimal ImporteTotal { get; set; }
    public bool Deleted { get; set; } = false;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PercepcionDetalle, ComprobantePercepcionDto>()
                .ForMember(dst => dst.Percepcion, opt => opt.MapFrom(src => src.Percepcion.Descripcion));

        }
    }
}

public class ComprobanteDetalleDto : IMapFrom<ComprobanteDetalle>
{
    public int Id { get; set; }
    public int ComprobanteId { get; set; }
    public short? UnidadMedidaId { get; set; }
    public string UnidadMedida { get; set; }
    public int? Cantidad { get; set; }
    public decimal? PrecioUnitario { get; set; }
    public decimal? ImporteBonificacion { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? ImporteIVA { get; set; }
    public bool? Exento { get; set; }
    public decimal? Alicuota { get; set; } = default;
    public int? OrdenCompraId { get; set; }
    public string Detalle { get; set; }
    public bool Deleted { get; set; } = false;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ComprobanteDetalle, ComprobanteDetalleDto>()
                .ForMember(dst => dst.UnidadMedida, opt => opt.MapFrom(src => src.UnidadMedida.CodigoAFIP));
        }
    }
}