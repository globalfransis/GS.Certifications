using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Comprobantes;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.ModosLecturas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;

#region Interfaces Servicio y Parámetros

/// <summary>
/// Define las operaciones disponibles para la gestión de Comprobantes.
/// </summary>
public interface IComprobanteService
{
    /// <summary>
    /// Obtiene un comprobante específico de forma asíncrona por su identificador único.
    /// </summary>
    /// <param name="id">El ID del comprobante a obtener.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> encontrado.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra ningún comprobante con el ID especificado.</exception>
    Task<Comprobante> GetAsync(int id);

    /// <summary>
    /// Obtiene una lista paginada de comprobantes de forma asíncrona, aplicando filtros especificados.
    /// </summary>
    /// <param name="parameters">Los parámetros de consulta y paginación para filtrar los comprobantes.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene un <see cref="IPaginatedQueryResult{Comprobante}"/> con los comprobantes encontrados y la información de paginación.</returns>
    Task<IPaginatedQueryResult<Comprobante>> GetManyAsync(IComprobanteQueryParameter parameters);

    /// <summary>
    /// Analiza un archivo de comprobante (por ejemplo, un PDF) de forma asíncrona para extraer sus datos.
    /// </summary>
    /// <param name="bytesSource">Los datos binarios del archivo a analizar.</param>
    /// <param name="parameters">Parámetros adicionales para el análisis, como el ID de la compañía.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene un <see cref="IComprobanteAnalysisResult"/> con los datos extraídos del comprobante.</returns>
    Task<IComprobanteAnalysisResult> AnalyzeAsync(BinaryData bytesSource, IComprobanteAnalysisParameter parameters);

    /// <summary>
    /// Crea un nuevo comprobante de forma asíncrona, en estado BORRADOR por defecto.
    /// </summary>
    /// <param name="c">Los datos para crear el nuevo comprobante.</param>
    /// <param name="estado">El estado inicial del nuevo comprobante.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> recién creado.</returns>
    /// <exception cref="ComprobanteYaExisteException">Se lanza si ya existe un comprobante con el mismo número para la misma empresa.</exception>
    /// <exception cref="ComprobanteDetallesVaciosException">Se lanza si no se proporcionan detalles para el comprobante.</exception>
    /// <exception cref="ComprobanteDetalleInvalidoException">Se lanza si algún detalle del comprobante tiene datos inválidos (cantidad, subtotal, etc.).</exception>
    /// <exception cref="ComprobanteImporteBonificacionInvalidoException">Se lanza si el importe total de bonificación no coincide con la suma de bonificaciones de los detalles.</exception>
    /// <exception cref="ComprobanteTotalNetoInvalidoException">Se lanza si el importe neto no coincide con el cálculo esperado basado en los detalles.</exception>
    /// <exception cref="ComprobantePercepcionesInvalidasException">Se lanza si alguna percepción tiene datos inválidos.</exception>
    /// <exception cref="ComprobanteTotalImporteIVAInvalido">Se lanza si el importe total de IVA no coincide con la suma de los impuestos de tipo IVA.</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosInternosInvalido">Se lanza si el importe total de Impuestos Internos no coincide con la suma de los impuestos de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosProvInvalido">Se lanza si el importe total de Otros Tributos Provinciales no coincide con la suma de los impuestos de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIVAInvalido">Se lanza si el importe total de Percepciones de IVA no coincide con la suma de las percepciones de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIIBBInvalido">Se lanza si el importe total de Percepciones de IIBB no coincide con la suma de las percepciones de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesMunicipalInvalido">Se lanza si el importe total de Percepciones Municipales no coincide con la suma de las percepciones de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalInvalidoException">Se lanza si el importe total del comprobante no coincide con la suma de todos sus componentes (neto, impuestos, percepciones).</exception>
    Task<Comprobante> CreateAsync(IComprobanteCreate c, EstadoComprobante estado = EstadoComprobante.BORRADOR);

    /// <summary>
    /// Guarda un comprobante como borrador de forma asíncrona.
    /// </summary>
    /// <param name="c">Los datos del comprobante a guardar como borrador.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> guardado como borrador.</returns>
    Task<Comprobante> SaveDraftAsync(IComprobanteCreate c);

    /// <summary>
    /// Genera un comprobante como borrador de forma asíncrona. No lo almacena en el dbContext
    /// </summary>
    /// <param name="c">Los datos del comprobante a guardar como borrador.</param>
    /// <returns>Una tarea que representa la operación asíncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> guardado como borrador.</returns>
    Task<Comprobante> GenerateDraftAsync(IComprobanteCreate c);

    /// <summary>
    /// Genera un comprobante como borrador de forma asíncrona en base a los datos de un analisis. No lo almacena en el dbContext
    /// </summary>
    /// <remarks>
    /// Establece el estado del comprobante a un estado de borrador.
    /// No realiza las validaciones estrictas de <see cref="CreateAsync"/>.
    /// </remarks>
    Task<Comprobante> GenerateDraftAsync(IComprobanteAnalysisResult analysis, EmpresaPortal empresaPortal);



    Task ConfirmAsync(int id, IComprobanteConfirm parameters);

    Task AuthorizeAsync(int id, IComprobanteAuthorize parameters);


    /// <summary>
    /// Actualiza un comprobante que se encuentra en estado de borrador de forma asíncrona.
    /// </summary>
    /// <param name="c">Los datos actualizados del borrador del comprobante.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a actualizar.</exception>
    Task UpdateDraftAsync(IComprobanteUpdate c);

    /// <summary>
    /// Actualiza un comprobante existente de forma asíncrona y lo marca como confirmado. Puede usarse para confirmar un borrador.
    /// Se ejecutan las validaciones correspondientes a la integridad y consistencia de los datos del comprobante.
    /// </summary>
    /// <param name="c">Los datos actualizados del comprobante.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a actualizar.</exception>
    /// <exception cref="Exception">Se lanza si el comprobante actualizado se queda sin detalles.</exception>
    /// <exception cref="ComprobanteDetalleInvalidoException">Se lanza si algún detalle del comprobante tiene datos inválidos.</exception>
    /// <exception cref="ComprobanteImporteBonificacionInvalidoException">Se lanza si el importe total de bonificación no coincide.</exception>
    /// <exception cref="ComprobanteTotalNetoInvalidoException">Se lanza si el importe neto no coincide.</exception>
    /// <exception cref="ComprobantePercepcionesInvalidasException">Se lanza si alguna percepción tiene datos inválidos.</exception>
    /// <exception cref="ComprobanteTotalImporteIVAInvalido">Se lanza si el importe total de IVA no coincide.</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosInternosInvalido">Se lanza si el importe total de Impuestos Internos no coincide.</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosProvInvalido">Se lanza si el importe total de Otros Tributos Provinciales no coincide.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIVAInvalido">Se lanza si el importe total de Percepciones de IVA no coincide.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIIBBInvalido">Se lanza si el importe total de Percepciones de IIBB no coincide.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesMunicipalInvalido">Se lanza si el importe total de Percepciones Municipales no coincide.</exception>
    /// <exception cref="ComprobanteTotalInvalidoException">Se lanza si el importe total del comprobante no coincide.</exception>
    Task UpdateAsync(IComprobanteUpdate c);



    /// <inheritdoc/>
    /// <remarks>
    /// Actualiza un comprobante
    /// </remarks>
    Task<Comprobante> UpdateAsync(int comprobanteId, Comprobante c);



    /// <inheritdoc/>
    /// <remarks>
    /// Actualiza un comprobante
    /// </remarks>
    Comprobante Update(Comprobante comprobante, Comprobante c);

    /// <summary>
    /// Elimina un comprobante de forma asíncrona por su ID.
    /// </summary>
    /// <param name="id">El ID del comprobante a eliminar.</param>
    /// <param name="rowVersion">El token de concurrencia para evitar eliminaciones conflictivas.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a eliminar.</exception>
    Task DeleteAsync(int id, byte[] rowVersion);

    /// <summary>
    /// Verifica de forma asíncrona la validez y autorización de los datos de cabecera
    /// de un comprobante contra un servicio externo (ej. AFIP).
    /// </summary>
    /// <param name="cabecera">Los datos de la cabecera del comprobante a verificar,
    /// obtenidos típicamente de un análisis de documento o código QR.</param>
    /// <param name="cuitCompany">El número de CUIT de la company.</param>
    /// <returns>
    /// Una tarea que representa la operación asíncrona. El resultado de la tarea contiene
    /// un <see cref="IComprobanteConstatacionResult"/> que indica si el comprobante está
    /// autorizado (<see cref="EstadoConstatacion.Ok"/>) o no (<see cref="EstadoConstatacion.Error"/>)
    /// y las observaciones asociadas devueltas por el servicio de validación.
    /// </returns>
    /// <remarks>
    /// Este método NO persiste el comprobante, solo realiza la consulta de validación externa.
    /// </remarks>
    Task<IComprobanteConstatacionResult> VerifyAsync(IComprobanteCabecera cabecera, long cuitCompany);

    /// <summary>
    /// Verifica el estado de un comprobante existente contra el servicio externo ARCA/AFIP
    /// y actualiza su estado de validación interno.
    /// </summary>
    /// <param name="comprobanteId">Identificador del comprobante a verificar.</param>
    /// <param name="rowVersion">El token de concurrencia para evitar actualizaciones del estado en ARCA conflictivas.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Si no se encuentra el comprobante con el ID especificado.</exception>
    /// <exception cref="InvalidOperationException">Si ocurre un error durante el mapeo de datos necesarios para la verificación.</exception>
    Task VerifyEstadoARCAAsync(int comprobobanteId, byte[] rowVersion);

    /// <summary>
    /// Aprueba un comprobante de forma asíncrona.
    /// </summary>
    /// <param name="id">El ID del comprobante a aprobar.</param>
    /// <param name="approveParameters">Parámetros para la aprobación, incluyendo el RowVersion para concurrencia.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a aprobar.</exception>
    /// <exception cref="ComprobanteYaFueAprobadoException">Se lanza si el comprobante ya se encuentra aprobado.</exception>
    Task ApproveAsync(int id, IComprobanteApprove approveParameters);

    Task UpdatePeriodoAsync(int id, IComprobantePeriodoUpdate parameters);

    /// <summary>
    /// Rechaza un comprobante de forma asíncrona.
    /// </summary>
    /// <param name="id">El ID del comprobante a rechazar.</param>
    /// <param name="rejectParameters">Parámetros para el rechazo, incluyendo el motivo, usuario y RowVersion.</param>
    /// <returns>Una tarea que representa la operación asíncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a rechazar.</exception>
    Task RejectAsync(int id, IComprobanteReject rejectParameters);
}

/// <summary>
/// Define los parámetros necesarios para rechazar un comprobante.
/// </summary>
public interface IComprobanteReject
{
    /// <summary>
    /// Motivo textual por el cual se rechaza el comprobante.
    /// </summary>
    string MotivoRechazo { get; set; }
    /// <summary>
    /// Identificador del usuario que realiza el rechazo.
    /// </summary>
    string UsuarioRechazo { get; set; }
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento del rechazo.
    /// </summary>
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define los parámetros necesarios para aprobar un comprobante.
/// </summary>
public interface IComprobanteApprove
{
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento de la aprobación.
    /// </summary>
    byte[] RowVersion { get; set; }
}

public interface IComprobantePeriodoUpdate
{
    int? PeriodoId { get; set; }
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define los parámetros necesarios para autorizar un comprobante.
/// </summary>
public interface IComprobanteAuthorize
{
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento de la autorización.
    /// </summary>
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define los parámetros necesarios para confirmar un comprobante.
/// </summary>
public interface IComprobanteConfirm
{
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento de la confirmación.
    /// </summary>
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define la estructura de datos de la cabecera de un comprobante, utilizada principalmente para la verificación inicial.
/// </summary>
public interface IComprobanteCabecera
{
    /// <summary>
    /// ID del comprobante (0 si es nuevo).
    /// </summary>
    int Id { get; set; }
    /// <summary>
    /// Versión del formato del QR.
    /// </summary>
    int ver { get; set; }
    /// <summary>
    /// Fecha de emisión del comprobante (formato yyyy-MM-dd).
    /// </summary>
    string fecha { get; set; }
    /// <summary>
    /// CUIT del emisor del comprobante.
    /// </summary>
    long cuit { get; set; }
    /// <summary>
    /// Punto de venta del comprobante.
    /// </summary>
    int ptoVta { get; set; }
    /// <summary>
    /// Código numérico del tipo de comprobante según AFIP.
    /// </summary>
    int tipoCmp { get; set; }
    /// <summary>
    /// Número del comprobante.
    /// </summary>
    int nroCmp { get; set; }
    /// <summary>
    /// Importe total del comprobante.
    /// </summary>
    decimal importe { get; set; }
    /// <summary>
    /// Código de la moneda ("PES", "DOL", etc.).
    /// </summary>
    string moneda { get; set; }
    /// <summary>
    /// Cotización de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? ctz { get; set; }
    /// <summary>
    /// Código del tipo de documento del receptor según AFIP (ej: 80 para CUIT).
    /// </summary>
    int tipoDocRec { get; set; }
    /// <summary>
    /// Número de documento del receptor (CUIT/CUIL/DNI).
    /// </summary>
    long nroDocRec { get; set; }
    /// <summary>
    /// Tipo de código de autorización ("E" para CAE, "A" para CAEA).
    /// </summary>
    string tipoCodAut { get; set; }
    /// <summary>
    /// Código de autorización (CAE o CAEA).
    /// </summary>
    long codAut { get; set; }
}

/// <summary>
/// Define los parámetros adicionales para la operación de análisis de documentos.
/// </summary>
public interface IComprobanteAnalysisParameter
{
    /// <summary>
    /// ID de la compañía (tenant) a la que pertenece el comprobante analizado. Usado para buscar impuestos/percepciones específicos.
    /// </summary>
    long CompanyId { get; set; }
    /// /// <summary>
    /// ID de la empresa portal.
    /// </summary>
    int? EmpresaId { get; set; }
    ICollection<ModoAnalisis> Modos { get; set; }
}

/// <summary>
/// Define los parámetros de consulta para buscar y filtrar comprobantes. Hereda de IQueryParameter para incluir paginación y ordenamiento.
/// </summary>
public interface IComprobanteQueryParameter : IQueryParameter
{
    /// <summary>
    /// Filtra por Número de Identificación Fiscal (CUIT/CUIL) del proveedor (permite búsqueda parcial).
    /// </summary>
    string NroIdentificacionFiscalPro { get; set; }
    /// <summary>
    /// Filtra por ID de la categoría fiscal del emisor.
    /// </summary>
    short? CategoriaTipoEmisorId { get; set; }
    /// <summary>
    /// Filtra por ID del tipo de comprobante.
    /// </summary>
    short? ComprobanteTipoId { get; set; }
    /// <summary>
    /// Filtra por ID de la categoría fiscal del receptor.
    /// </summary>
    short? CategoriaTipoReceptorId { get; set; }
    /// <summary>
    /// Filtra por número del punto de venta.
    /// </summary>
    int? PuntoVenta { get; set; }
    /// <summary>
    /// Filtra por número de comprobante.
    /// </summary>
    int? Numero { get; set; }
    /// <summary>
    /// Filtra por fecha de emisión mínima (inclusive).
    /// </summary>
    DateTime? FechaEmisionDesde { get; set; }
    /// <summary>
    /// Filtra por fecha de emisión máxima (inclusive).
    /// </summary>
    DateTime? FechaEmisionHasta { get; set; }
    /// <summary>
    /// Filtra por fecha de registración mínima (inclusive).
    /// </summary>
    DateTime? FechaRegistracionDesde { get; set; }
    /// <summary>
    /// Filtra por fecha de registración máxima (inclusive).
    /// </summary>
    DateTime? FechaRegistracionHasta { get; set; }
    /// <summary>
    /// Filtra por ID del tipo de código de autorización.
    /// </summary>
    short? TipoCodigoAutorizacionId { get; set; }
    /// <summary>
    /// Filtra por ID de la empresa asociada.
    /// </summary>
    int? EmpresaId { get; set; }
    /// <summary>
    /// Filtra por ID de la compañía (tenant) asociada.
    /// </summary>
    long? CompanyId { get; set; }
    /// <summary>
    /// Filtra por ID del estado del comprobante.
    /// </summary>
    int? ComprobanteEstadoId { get; set; }
}

public enum EstadoComprobante
{
    ARCHIVO_SUBIDO = ComprobanteEstado.ARCHIVO_SUBIDO,
    EN_PROCESO_CARGA = ComprobanteEstado.EN_PROCESO_CARGA,
    ERRORES_ARCA = ComprobanteEstado.ERRORES_ARCA,
    CONFIRMADO = ComprobanteEstado.CONFIRMADO,
    ACUSE_RECIBO_CLIENTE = ComprobanteEstado.ACUSE_RECIBO_CLIENTE,
    APROBADA_CLIENTE = ComprobanteEstado.APROBADA_CLIENTE,
    RECHAZADA_CLIENTE = ComprobanteEstado.RECHAZADA_CLIENTE,
    BORRADOR = ComprobanteEstado.BORRADOR
}

public enum ModoAnalisis
{
    QR = ModoLectura.QR_IDM,
    OCR_DETALLE = ModoLectura.OCR_DETALLE_IDM,
    OCR_IMPUESTOS = ModoLectura.OCR_IMPUESTOS_IDM,
    OCR_CABECERA = ModoLectura.OCR_CABECERA_IDM,
    MANUAL = ModoLectura.MANUAL_IDM
}

#endregion

#region Interfaces DTO (Create, Update, Save, AnalysisResult)

/// <summary>
/// Define la estructura de datos para la creación de un nuevo comprobante confirmado.
/// </summary>
public interface IComprobanteCreate
{
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalPro"/>
    string NroIdentificacionFiscalPro { get; set; }
    //string DomicilioPro { get; set; } // Comentado en el original
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoEmisorId"/>
    short? CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ComprobanteTipoId"/>
    short? ComprobanteTipoId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoReceptorId"/>
    short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalCompany"/>
    string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.PuntoVenta"/>
    int PuntoVenta { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Letra"/>
    string Letra { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Numero"/>
    int Numero { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.FechaEmision"/>
    DateTime? FechaEmision { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.TipoCodigoAutorizacionId"/>
    short? TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CodigoAutorizacion"/>
    string CodigoAutorizacion { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.MonedaId"/>
    long? MonedaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteNeto"/>
    decimal ImporteNeto { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteTotal"/>
    decimal ImporteTotal { get; set; }
    /// <summary>
    /// Importe total de IVA discriminado en el comprobante.
    /// </summary>
    decimal ImporteIVA { get; set; }
    /// <summary>
    /// Importe total de bonificaciones/descuentos aplicados en el comprobante.
    /// </summary>
    decimal ImporteBonificacion { get; set; }
    /// <summary>
    /// Importe total de percepción de IVA.
    /// </summary>
    decimal ImportePercepcionIVA { get; set; }
    /// <summary>
    /// Importe total de percepción de Ingresos Brutos (IIBB).
    /// </summary>
    decimal ImportePercepcionIIBB { get; set; }
    /// <summary>
    /// Importe total de percepción municipal.
    /// </summary>
    decimal ImportePercepcionMunicipal { get; set; }
    /// <summary>
    /// Importe total de impuestos internos.
    /// </summary>
    decimal ImporteImpuestosInternos { get; set; }
    /// <summary>
    /// Importe total de otros tributos provinciales.
    /// </summary>
    decimal ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.EmpresaId"/>
    int? EmpresaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CompanyId"/>
    long? CompanyId { get; set; }
    /// <summary>
    /// Código HES (Hoja de Entrada de Servicio), si aplica.
    /// </summary>
    string CodigoHES { get; set; }
    /// <summary>
    /// ID del estado inicial del comprobante (generalmente se establece internamente).
    /// </summary>
    int? ComprobanteEstadoId { get; set; }
    /// <summary>
    /// Comentarios u observaciones generales del comprobante.
    /// </summary>
    string Comentarios { get; set; }
    /// <summary>
    /// Lista de detalles (ítems) del comprobante a crear.
    /// </summary>
    List<IComprobanteDetalleCreate> Detalles { get; set; }
    /// <summary>
    /// Lista de impuestos asociados al comprobante a crear.
    /// </summary>
    List<IComprobanteImpuestoCreate> Impuestos { get; set; }
    /// <summary>
    /// Lista de percepciones asociadas al comprobante a crear.
    /// </summary>
    List<IComprobantePercepcionCreate> Percepciones { get; set; }
    /// <summary>
    /// Indica si el QR del comprobante fue leído y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extraído del código QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// El estado de la constatación (verificación) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacion { get; set; }
    /// <summary>
    /// El estado de la constatación (verificación) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constatación (verificación) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCA { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constatación (verificación) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCAQR { get; set; }
    /// <summary>
    /// Cotización de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? Cotizacion { get; set; }
    /// <summary>
    /// Número de remito asociado.
    /// </summary>
    string NroRemito { get; set; }
    /// <summary>
    ///  Número de orden de compra asociado.
    /// </summary>
    string NroOrdenCompra { get; set; }
    /// <summary>
    /// Referencia al origen desde donde fue cargado el comprobante.
    /// </summary>
    short OrigenId { get; set; }
    /// <summary>
    /// Referencia al propietario del comprobante.
    /// </summary>
    short PropietarioActualId { get; set; }
    /// <summary>
    /// Fecha de vencimiento del comprobante.
    /// </summary>
    DateTime? FechaVencimiento { get; set; }
    /// <summary>
    /// Fecha de vencimiento del código de autorización del comprobante.
    /// </summary>
    DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    /// <summary>
    /// Nombre original del archivo analizado.
    /// </summary>
    string FileName { get; set; }
    short? CondicionVentaId { get; set; }
}

/// <summary>
/// Define la estructura de datos para un detalle (ítem) al crear un comprobante.
/// </summary>
public interface IComprobanteDetalleCreate
{
    /// <summary>
    /// ID de la unidad de medida del ítem (opcional, default 'Unidad').
    /// </summary>
    short? UnidadMedidaId { get; set; }
    /// <summary>
    /// Cantidad del ítem.
    /// </summary>
    int Cantidad { get; set; }
    /// <summary>
    /// Precio por unidad del ítem.
    /// </summary>
    decimal PrecioUnitario { get; set; }
    /// <summary>
    /// Importe de bonificación aplicado a este ítem (opcional).
    /// </summary>
    decimal? ImporteBonificacion { get; set; }
    /// <summary>
    /// Subtotal del ítem (Cantidad * PrecioUnitario - ImporteBonificacion).
    /// </summary>
    decimal Subtotal { get; set; }
    /// <summary>
    /// Importe de IVA correspondiente a este ítem (opcional).
    /// </summary>
    decimal? ImporteIVA { get; set; }
    /// <summary>
    /// Indica si el ítem está exento de IVA (opcional).
    /// </summary>
    bool? Exento { get; set; }
    /// <summary>
    /// Alícuota de IVA aplicada a este ítem (opcional).
    /// </summary>
    decimal? Alicuota { get; set; }
    /// <summary>
    /// ID de la orden de compra relacionada (opcional).
    /// </summary>
    int? OrdenCompraId { get; set; }
    /// <summary>
    /// Descripción o detalle del ítem.
    /// </summary>
    string Detalle { get; set; }
}

/// <summary>
/// Define la estructura de datos para un impuesto al crear un comprobante.
/// </summary>
public interface IComprobanteImpuestoCreate
{
    /// <summary>
    /// ID del tipo de impuesto predefinido (opcional, si no, se usa la descripción).
    /// </summary>
    int? ImpuestoId { get; set; }
    /// <summary>
    /// Descripción del impuesto.
    /// </summary>
    string Descripcion { get; set; }
    /// <summary>
    /// Importe total del impuesto.
    /// </summary>
    decimal ImporteTotal { get; set; }
    /// <summary>
    /// Alícuota del impuesto (opcional, informativo).
    /// </summary>
    decimal? Alicuota { get; set; }
}

/// <summary>
/// Define la estructura de datos para una percepción al crear un comprobante.
/// </summary>
public interface IComprobantePercepcionCreate
{
    /// <summary>
    /// ID del tipo de percepción predefinido (opcional, si no, se usa la descripción).
    /// </summary>
    int? PercepcionId { get; set; }
    /// <summary>
    /// Descripción de la percepción.
    /// </summary>
    string Descripcion { get; set; }
    /// <summary>
    /// Alícuota aplicada para calcular la percepción.
    /// </summary>
    decimal Alicuota { get; set; }
    /// <summary>
    /// Importe total de la percepción.
    /// </summary>
    decimal ImporteTotal { get; set; }
}

/// <summary>
/// Define la estructura de datos para guardar (crear o actualizar borrador) un comprobante.
/// Incluye ID para identificar el registro existente en caso de actualización de borrador.
/// </summary>
public interface IComprobanteSave
{
    /// <summary>
    /// ID del comprobante (0 si es nuevo borrador, >0 si se actualiza un borrador existente).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalPro"/>
    string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoEmisorId"/>
    short CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ComprobanteTipoId"/>
    short ComprobanteTipoId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoReceptorId"/>
    short CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalCompany"/>
    string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.PuntoVenta"/>
    int PuntoVenta { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Letra"/>
    string Letra { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Numero"/>
    int Numero { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.FechaEmision"/>
    DateTime FechaEmision { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.TipoCodigoAutorizacionId"/>
    short TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CodigoAutorizacion"/>
    string CodigoAutorizacion { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.MonedaId"/>
    long MonedaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteNeto"/>
    decimal ImporteNeto { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteTotal"/>
    decimal ImporteTotal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteIVA"/>
    decimal ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteBonificacion"/>
    decimal ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionIVA"/>
    decimal ImportePercepcionIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionIIBB"/>
    decimal ImportePercepcionIIBB { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionMunicipal"/>
    decimal ImportePercepcionMunicipal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteImpuestosInternos"/>
    decimal ImporteImpuestosInternos { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteOtrosTributosProv"/>
    decimal ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.EmpresaId"/>
    int? EmpresaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CompanyId"/>
    long? CompanyId { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.CodigoHES"/>
    string CodigoHES { get; set; }
    /// <summary>
    /// ID del estado del comprobante (generalmente 'Borrador').
    /// </summary>
    int? ComprobanteEstadoId { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.Comentarios"/>
    string Comentarios { get; set; }
    /// <summary>
    /// Lista de detalles (ítems) del comprobante a guardar/actualizar en borrador.
    /// </summary>
    List<IComprobanteDetalleSave> Detalles { get; set; }
    /// <summary>
    /// Lista de impuestos asociados al comprobante a guardar/actualizar en borrador.
    /// </summary>
    List<IComprobanteImpuestoSave> Impuestos { get; set; }
    /// <summary>
    /// Lista de percepciones asociadas al comprobante a guardar/actualizar en borrador.
    /// </summary>
    List<IComprobantePercepcionSave> Percepciones { get; set; }
    /// <summary>
    /// Indica si el QR del comprobante fue leído y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extraído del código QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// El estado de la constatación (verificación) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacion { get; set; }
    /// <summary>
    /// El estado de la constatación (verificación) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constatación (verificación) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCA { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constatación (verificación) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCAQR { get; set; }
}

/// <summary>
/// Define la estructura de datos para un detalle (ítem) al guardar/actualizar un borrador de comprobante.
/// </summary>
public interface IComprobanteDetalleSave
{
    /// <summary>
    /// ID del detalle existente (si se actualiza, 0 si es nuevo).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.UnidadMedidaId"/>
    short? UnidadMedidaId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Cantidad"/> (puede ser nulo en borrador)
    int? Cantidad { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.PrecioUnitario"/> (puede ser nulo en borrador)
    decimal? PrecioUnitario { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteBonificacion"/> (puede ser nulo en borrador)
    decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Subtotal"/> (puede ser nulo en borrador)
    decimal? Subtotal { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteIVA"/> (puede ser nulo en borrador)
    decimal? ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Exento"/>
    bool? Exento { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.OrdenCompraId"/>
    int? OrdenCompraId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Detalle"/>
    string Detalle { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Alicuota"/> (puede ser nulo en borrador)
    decimal? Alicuota { get; set; }
    /// <summary>
    /// Indica si este detalle debe ser eliminado al actualizar el borrador.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos para un impuesto al guardar/actualizar un borrador de comprobante.
/// </summary>
public interface IComprobanteImpuestoSave
{
    /// <summary>
    /// ID del impuesto existente (si se actualiza, 0 si es nuevo).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImpuestoId"/>
    int? ImpuestoId { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.Descripcion"/>
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImporteTotal"/> (puede ser nulo en borrador)
    decimal? ImporteTotal { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.Alicuota"/> (puede ser nulo en borrador)
    decimal? Alicuota { get; set; }
    /// <summary>
    /// Indica si este impuesto debe ser eliminado al actualizar el borrador.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos para una percepción al guardar/actualizar un borrador de comprobante.
/// </summary>
public interface IComprobantePercepcionSave
{
    /// <summary>
    /// ID de la percepción existente (si se actualiza, 0 si es nueva).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.PercepcionId"/>
    int? PercepcionId { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Descripcion"/>
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Alicuota"/> (puede ser nulo en borrador)
    decimal? Alicuota { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.ImporteTotal"/> (puede ser nulo en borrador)
    decimal? ImporteTotal { get; set; }
    /// <summary>
    /// Indica si esta percepción debe ser eliminada al actualizar el borrador.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos para actualizar un comprobante existente (y confirmarlo).
/// </summary>
public interface IComprobanteUpdate
{
    /// <summary>
    /// ID del comprobante a actualizar.
    /// </summary>
    int Id { get; set; }
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento del rechazo.
    /// </summary>
    byte[] RowVersion { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalPro"/>
    string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoEmisorId"/>
    short CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ComprobanteTipoId"/>
    short ComprobanteTipoId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoReceptorId"/>
    short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalCompany"/>
    string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.PuntoVenta"/>
    int PuntoVenta { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Letra"/>
    string Letra { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Numero"/>
    int Numero { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.FechaEmision"/>
    DateTime FechaEmision { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.TipoCodigoAutorizacionId"/>
    short TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CodigoAutorizacion"/>
    string CodigoAutorizacion { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.MonedaId"/>
    long MonedaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteNeto"/>
    decimal ImporteNeto { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteTotal"/>
    decimal ImporteTotal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteIVA"/>
    decimal ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteBonificacion"/>
    decimal ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionIVA"/>
    decimal ImportePercepcionIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionIIBB"/>
    decimal ImportePercepcionIIBB { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionMunicipal"/>
    decimal ImportePercepcionMunicipal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteImpuestosInternos"/>
    decimal ImporteImpuestosInternos { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteOtrosTributosProv"/>
    decimal ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.EmpresaId"/> (Generalmente no se actualiza)
    int? EmpresaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CompanyId"/> (Generalmente no se actualiza)
    long? CompanyId { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.CodigoHES"/>
    string CodigoHES { get; set; }
    /// <summary>
    /// ID del estado final del comprobante (generalmente 'Confirmado').
    /// </summary>
    int? ComprobanteEstadoId { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.Comentarios"/>
    string Comentarios { get; set; }
    /// <summary>
    /// Lista de detalles (ítems) actualizados del comprobante.
    /// </summary>
    List<IComprobanteDetalleUpdate> Detalles { get; set; }
    /// <summary>
    /// Lista de impuestos actualizados asociados al comprobante.
    /// </summary>
    List<IComprobanteImpuestoUpdate> Impuestos { get; set; }
    /// <summary>
    /// Lista de percepciones actualizadas asociadas al comprobante.
    /// </summary>
    List<IComprobantePercepcionUpdate> Percepciones { get; set; }
    /// <summary>
    /// Indica si el QR del comprobante fue leído y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extraído del código QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// Cotización de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? Cotizacion { get; set; }
    /// <summary>
    /// Número de remito asociado.
    /// </summary>
    string NroRemito { get; set; }
    /// <summary>
    ///  Número de orden de compra asociado.
    /// </summary>
    string NroOrdenCompra { get; set; }
    /// <summary>
    /// Fecha de vencimiento del comprobante.
    /// </summary>
    DateTime? FechaVencimiento { get; set; }
    /// <summary>
    /// Fecha de vencimiento del código de autorización del comprobante.
    /// </summary>
    DateTime? FechaVencimientoCodigoAutorizacion { get; set; }

    short? CondicionVentaId { get; set; }
}

/// <summary>
/// Define la estructura de datos para un detalle (ítem) al actualizar un comprobante.
/// </summary>
public interface IComprobanteDetalleUpdate
{
    /// <summary>
    /// ID del detalle existente (si se actualiza, 0 si es nuevo).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.UnidadMedidaId"/>
    short? UnidadMedidaId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Cantidad"/>
    int Cantidad { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.PrecioUnitario"/>
    decimal PrecioUnitario { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteBonificacion"/>
    decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Subtotal"/>
    decimal Subtotal { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteIVA"/>
    decimal? ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Exento"/>
    bool? Exento { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Alicuota"/>
    decimal? Alicuota { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.OrdenCompraId"/>
    int? OrdenCompraId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Detalle"/>
    string Detalle { get; set; }
    /// <summary>
    /// Indica si este detalle debe ser eliminado al actualizar el comprobante.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos para un impuesto al actualizar un comprobante.
/// </summary>
public interface IComprobanteImpuestoUpdate
{
    /// <summary>
    /// ID del impuesto existente (si se actualiza, 0 si es nuevo).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImpuestoId"/>
    int? ImpuestoId { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.Descripcion"/>
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImporteTotal"/>
    decimal ImporteTotal { get; set; }
    /// <summary>
    /// Indica si este impuesto debe ser eliminado al actualizar el comprobante.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos para una percepción al actualizar un comprobante.
/// </summary>
public interface IComprobantePercepcionUpdate
{
    /// <summary>
    /// ID de la percepción existente (si se actualiza, 0 si es nueva).
    /// </summary>
    int Id { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.PercepcionId"/>
    int? PercepcionId { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Descripcion"/>
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Alicuota"/>
    decimal Alicuota { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.ImporteTotal"/>
    decimal ImporteTotal { get; set; }
    /// <summary>
    /// Indica si esta percepción debe ser eliminada al actualizar el comprobante.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos del resultado del análisis de un documento de comprobante.
/// </summary>
public interface IComprobanteAnalysisResult
{
    public string RazonSocialPro { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalPro"/> (Extraído)
    string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoEmisorId"/> (Inferido)
    short? CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ComprobanteTipoId"/> (Inferido)
    short? ComprobanteTipoId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoReceptorId"/> (Extraído/Inferido)
    short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalCompany"/> (Extraído)
    string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.PuntoVenta"/> (Extraído, como string)
    string PuntoVenta { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Letra"/> (Extraído)
    string Letra { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Numero"/> (Extraído, como string)
    string Numero { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.FechaEmision"/> (Extraída, como string yyyy-MM-dd)
    string FechaEmision { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.TipoCodigoAutorizacionId"/> (Inferido)
    short? TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CodigoAutorizacion"/> (Extraído)
    string CodigoAutorizacion { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.MonedaId"/> (Inferido)
    long? MonedaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteNeto"/> (Extraído/Calculado)
    decimal? ImporteNeto { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteTotal"/> (Extraído/Calculado)
    decimal? ImporteTotal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteIVA"/> (Calculado desde impuestos)
    decimal? ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteBonificacion"/> (Extraído/Calculado)
    decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionIVA"/> (Calculado desde percepciones)
    decimal? ImportePercepcionIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionIIBB"/> (Calculado desde percepciones)
    decimal? ImportePercepcionIIBB { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImportePercepcionMunicipal"/> (Calculado desde percepciones)
    decimal? ImportePercepcionMunicipal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteImpuestosInternos"/> (Calculado desde impuestos)
    decimal? ImporteImpuestosInternos { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteOtrosTributosProv"/> (Calculado desde impuestos)
    decimal? ImporteOtrosTributosProv { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.EmpresaId"/> (Opcional, podría venir de parámetros)   
    string CodigoHES { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.Comentarios"/> (Extraído)
    string Comentarios { get; set; }
    /// <summary>
    /// Lista de detalles (ítems) extraídos del comprobante.
    /// </summary>
    List<IComprobanteDetalleAnalysisResult> Detalles { get; set; }
    /// <summary>
    /// Lista de impuestos extraídos/inferidos del comprobante.
    /// </summary>
    List<IComprobanteImpuestoAnalysisResult> Impuestos { get; set; }
    /// <summary>
    /// Lista de percepciones extraídas/inferidas del comprobante.
    /// </summary>
    List<IComprobantePercepcionAnalysisResult> Percepciones { get; set; }
    /// <summary>
    /// Indica si el QR del comprobante fue leído y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extraído del código QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// El estado de la constatación (verificación) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <summary>
    /// El estado de la constatación (verificación) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacion { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constatación (verificación) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    string ObservacionesARCA { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constatación (verificación) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    string ObservacionesARCAQR { get; set; }
    /// <summary>
    /// Fecha de vencimiento del comprobante.
    /// </summary>
    DateTime? FechaVencimiento { get; set; }
    /// <summary>
    /// Fecha de vencimiento del código de autorización del comprobante.
    /// </summary>
    DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    /// <summary>
    /// Cotización de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? Cotizacion { get; set; }
    /// <summary>
    /// Número de remito asociado.
    /// </summary>
    string NroRemito { get; set; }
    /// <summary>
    ///  Número de orden de compra asociado.
    /// </summary>
    string NroOrdenCompra { get; set; }

    short? CondicionVentaId { get; set; }

    // Metadatos del archivo analizado
    /// <summary>
    /// Nombre original del archivo analizado.
    /// </summary>
    string FileName { get; set; }
    /// <summary>
    /// URL o ruta de origen del archivo analizado.
    /// </summary>
    string URL { get; set; }
    /// <summary>
    /// Ruta de la carpeta donde se encuentra el archivo analizado.
    /// </summary>
    string FolderPath { get; set; }
}

/// <summary>
/// Define la estructura de datos de un detalle (ítem) extraído del análisis de un comprobante.
/// </summary>
public interface IComprobanteDetalleAnalysisResult
{
    /// <inheritdoc cref="IComprobanteDetalleCreate.UnidadMedidaId"/> (Inferido)
    short? UnidadMedidaId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Cantidad"/> (Extraído)
    int? Cantidad { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.PrecioUnitario"/> (Extraído)
    decimal? PrecioUnitario { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteBonificacion"/> (Extraído/Calculado)
    decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Subtotal"/> (Extraído)
    decimal? Subtotal { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteIVA"/> (Calculado)
    decimal? ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Exento"/> (Inferido)
    bool? Exento { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Alicuota"/> (Extraído/Inferido)
    decimal? Alicuota { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.OrdenCompraId"/> (Opcional, podría extraerse)
    int? OrdenCompraId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Detalle"/> (Extraído)
    string Detalle { get; set; }
    /// <summary>
    /// ID del impuesto de IVA asociado a este detalle (inferido por alícuota).
    /// </summary>
    int? ImpuestoIVAId { get; set; }
}

/// <summary>
/// Define la estructura de datos de un impuesto extraído del análisis de un comprobante.
/// </summary>
public interface IComprobanteImpuestoAnalysisResult
{
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImpuestoId"/> (Inferido por descripción/alícuota)
    int? ImpuestoId { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.Descripcion"/> (Extraído)
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImporteTotal"/> (Extraído)
    decimal? ImporteTotal { get; set; }
    /// <summary>
    /// Valor de la alícuota extraída del documento.
    /// </summary>
    decimal? AlicuotaValor { get; set; }
    /// <summary>
    /// ID del tipo de impuesto (IVA, Interno, Provincial, etc.) inferido.
    /// </summary>
    short? TipoId { get; set; }
}

/// <summary>
/// Define la estructura de datos de una percepción extraída del análisis de un comprobante.
/// </summary>
public interface IComprobantePercepcionAnalysisResult
{
    /// <inheritdoc cref="IComprobantePercepcionCreate.PercepcionId"/> (Inferido por descripción)
    int? PercepcionId { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Descripcion"/> (Extraído)
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Alicuota"/> (Extraído)
    decimal? Alicuota { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.ImporteTotal"/> (Extraído)
    decimal? ImporteTotal { get; set; }
    /// <summary>
    /// ID del tipo de percepción (IVA, IIBB, Municipal) inferido.
    /// </summary>
    short? TipoId { get; set; }
}

/// <summary>
/// Define el resultado de la constatación (verificación) de un comprobante
/// contra una autoridad externa (ej. AFIP).
/// </summary>
public interface IComprobanteConstatacionResult
{
    /// <summary>
    /// Indica el estado de la constatación (Ok si está autorizado, Error si no lo está o hubo problemas).
    /// </summary>
    /// <seealso cref="EstadoConstatacion"/>
    EstadoConstatacion Estado { get; }

    /// <summary>
    /// Colección de mensajes u observaciones devueltas por el servicio de constatación
    /// (ej. motivos de rechazo, detalles del error, o mensajes informativos).
    /// </summary>
    ICollection<string> Observaciones { get; }
}

/// <summary>
/// Define los posibles estados resultado de la constatación de un comprobante.
/// </summary>
public enum EstadoConstatacion
{
    /// <summary>
    /// El comprobante fue validado exitosamente y está autorizado por la autoridad fiscal.
    /// </summary>
    Ok = EstadoValidacionARCA.VALIDADA,
    /// <summary>
    /// El comprobante no está autorizado, no fue encontrado por la autoridad fiscal.
    /// </summary>
    Rechazado = EstadoValidacionARCA.RECHAZADA,
    /// <summary>
    /// Hubo un error durante el proceso de constatación.
    /// Las observaciones pueden contener más detalles.
    /// </summary>
    Error = EstadoValidacionARCA.ERROR_VALIDACION,
    /// <summary>
    /// El comprobante no fue validado.
    /// </summary>
    SinConstatar = EstadoValidacionARCA.NO_VALIDADA,
}

#endregion
