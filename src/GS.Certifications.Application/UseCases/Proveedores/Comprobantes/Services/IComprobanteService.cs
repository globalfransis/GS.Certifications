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

#region Interfaces Servicio y Par�metros

/// <summary>
/// Define las operaciones disponibles para la gesti�n de Comprobantes.
/// </summary>
public interface IComprobanteService
{
    /// <summary>
    /// Obtiene un comprobante espec�fico de forma as�ncrona por su identificador �nico.
    /// </summary>
    /// <param name="id">El ID del comprobante a obtener.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> encontrado.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra ning�n comprobante con el ID especificado.</exception>
    Task<Comprobante> GetAsync(int id);

    /// <summary>
    /// Obtiene una lista paginada de comprobantes de forma as�ncrona, aplicando filtros especificados.
    /// </summary>
    /// <param name="parameters">Los par�metros de consulta y paginaci�n para filtrar los comprobantes.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene un <see cref="IPaginatedQueryResult{Comprobante}"/> con los comprobantes encontrados y la informaci�n de paginaci�n.</returns>
    Task<IPaginatedQueryResult<Comprobante>> GetManyAsync(IComprobanteQueryParameter parameters);

    /// <summary>
    /// Analiza un archivo de comprobante (por ejemplo, un PDF) de forma as�ncrona para extraer sus datos.
    /// </summary>
    /// <param name="bytesSource">Los datos binarios del archivo a analizar.</param>
    /// <param name="parameters">Par�metros adicionales para el an�lisis, como el ID de la compa��a.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene un <see cref="IComprobanteAnalysisResult"/> con los datos extra�dos del comprobante.</returns>
    Task<IComprobanteAnalysisResult> AnalyzeAsync(BinaryData bytesSource, IComprobanteAnalysisParameter parameters);

    /// <summary>
    /// Crea un nuevo comprobante de forma as�ncrona, en estado BORRADOR por defecto.
    /// </summary>
    /// <param name="c">Los datos para crear el nuevo comprobante.</param>
    /// <param name="estado">El estado inicial del nuevo comprobante.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> reci�n creado.</returns>
    /// <exception cref="ComprobanteYaExisteException">Se lanza si ya existe un comprobante con el mismo n�mero para la misma empresa.</exception>
    /// <exception cref="ComprobanteDetallesVaciosException">Se lanza si no se proporcionan detalles para el comprobante.</exception>
    /// <exception cref="ComprobanteDetalleInvalidoException">Se lanza si alg�n detalle del comprobante tiene datos inv�lidos (cantidad, subtotal, etc.).</exception>
    /// <exception cref="ComprobanteImporteBonificacionInvalidoException">Se lanza si el importe total de bonificaci�n no coincide con la suma de bonificaciones de los detalles.</exception>
    /// <exception cref="ComprobanteTotalNetoInvalidoException">Se lanza si el importe neto no coincide con el c�lculo esperado basado en los detalles.</exception>
    /// <exception cref="ComprobantePercepcionesInvalidasException">Se lanza si alguna percepci�n tiene datos inv�lidos.</exception>
    /// <exception cref="ComprobanteTotalImporteIVAInvalido">Se lanza si el importe total de IVA no coincide con la suma de los impuestos de tipo IVA.</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosInternosInvalido">Se lanza si el importe total de Impuestos Internos no coincide con la suma de los impuestos de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalImporteImpuestosProvInvalido">Se lanza si el importe total de Otros Tributos Provinciales no coincide con la suma de los impuestos de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIVAInvalido">Se lanza si el importe total de Percepciones de IVA no coincide con la suma de las percepciones de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesIIBBInvalido">Se lanza si el importe total de Percepciones de IIBB no coincide con la suma de las percepciones de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalPercepcionesMunicipalInvalido">Se lanza si el importe total de Percepciones Municipales no coincide con la suma de las percepciones de ese tipo.</exception>
    /// <exception cref="ComprobanteTotalInvalidoException">Se lanza si el importe total del comprobante no coincide con la suma de todos sus componentes (neto, impuestos, percepciones).</exception>
    Task<Comprobante> CreateAsync(IComprobanteCreate c, EstadoComprobante estado = EstadoComprobante.BORRADOR);

    /// <summary>
    /// Guarda un comprobante como borrador de forma as�ncrona.
    /// </summary>
    /// <param name="c">Los datos del comprobante a guardar como borrador.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> guardado como borrador.</returns>
    Task<Comprobante> SaveDraftAsync(IComprobanteCreate c);

    /// <summary>
    /// Genera un comprobante como borrador de forma as�ncrona. No lo almacena en el dbContext
    /// </summary>
    /// <param name="c">Los datos del comprobante a guardar como borrador.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene el <see cref="Comprobante"/> guardado como borrador.</returns>
    Task<Comprobante> GenerateDraftAsync(IComprobanteCreate c);

    /// <summary>
    /// Genera un comprobante como borrador de forma as�ncrona en base a los datos de un analisis. No lo almacena en el dbContext
    /// </summary>
    /// <remarks>
    /// Establece el estado del comprobante a un estado de borrador.
    /// No realiza las validaciones estrictas de <see cref="CreateAsync"/>.
    /// </remarks>
    Task<Comprobante> GenerateDraftAsync(IComprobanteAnalysisResult analysis, EmpresaPortal empresaPortal);



    Task ConfirmAsync(int id, IComprobanteConfirm parameters);

    Task AuthorizeAsync(int id, IComprobanteAuthorize parameters);


    /// <summary>
    /// Actualiza un comprobante que se encuentra en estado de borrador de forma as�ncrona.
    /// </summary>
    /// <param name="c">Los datos actualizados del borrador del comprobante.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a actualizar.</exception>
    Task UpdateDraftAsync(IComprobanteUpdate c);

    /// <summary>
    /// Actualiza un comprobante existente de forma as�ncrona y lo marca como confirmado. Puede usarse para confirmar un borrador.
    /// Se ejecutan las validaciones correspondientes a la integridad y consistencia de los datos del comprobante.
    /// </summary>
    /// <param name="c">Los datos actualizados del comprobante.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a actualizar.</exception>
    /// <exception cref="Exception">Se lanza si el comprobante actualizado se queda sin detalles.</exception>
    /// <exception cref="ComprobanteDetalleInvalidoException">Se lanza si alg�n detalle del comprobante tiene datos inv�lidos.</exception>
    /// <exception cref="ComprobanteImporteBonificacionInvalidoException">Se lanza si el importe total de bonificaci�n no coincide.</exception>
    /// <exception cref="ComprobanteTotalNetoInvalidoException">Se lanza si el importe neto no coincide.</exception>
    /// <exception cref="ComprobantePercepcionesInvalidasException">Se lanza si alguna percepci�n tiene datos inv�lidos.</exception>
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
    /// Elimina un comprobante de forma as�ncrona por su ID.
    /// </summary>
    /// <param name="id">El ID del comprobante a eliminar.</param>
    /// <param name="rowVersion">El token de concurrencia para evitar eliminaciones conflictivas.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a eliminar.</exception>
    Task DeleteAsync(int id, byte[] rowVersion);

    /// <summary>
    /// Verifica de forma as�ncrona la validez y autorizaci�n de los datos de cabecera
    /// de un comprobante contra un servicio externo (ej. AFIP).
    /// </summary>
    /// <param name="cabecera">Los datos de la cabecera del comprobante a verificar,
    /// obtenidos t�picamente de un an�lisis de documento o c�digo QR.</param>
    /// <param name="cuitCompany">El n�mero de CUIT de la company.</param>
    /// <returns>
    /// Una tarea que representa la operaci�n as�ncrona. El resultado de la tarea contiene
    /// un <see cref="IComprobanteConstatacionResult"/> que indica si el comprobante est�
    /// autorizado (<see cref="EstadoConstatacion.Ok"/>) o no (<see cref="EstadoConstatacion.Error"/>)
    /// y las observaciones asociadas devueltas por el servicio de validaci�n.
    /// </returns>
    /// <remarks>
    /// Este m�todo NO persiste el comprobante, solo realiza la consulta de validaci�n externa.
    /// </remarks>
    Task<IComprobanteConstatacionResult> VerifyAsync(IComprobanteCabecera cabecera, long cuitCompany);

    /// <summary>
    /// Verifica el estado de un comprobante existente contra el servicio externo ARCA/AFIP
    /// y actualiza su estado de validaci�n interno.
    /// </summary>
    /// <param name="comprobanteId">Identificador del comprobante a verificar.</param>
    /// <param name="rowVersion">El token de concurrencia para evitar actualizaciones del estado en ARCA conflictivas.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Si no se encuentra el comprobante con el ID especificado.</exception>
    /// <exception cref="InvalidOperationException">Si ocurre un error durante el mapeo de datos necesarios para la verificaci�n.</exception>
    Task VerifyEstadoARCAAsync(int comprobobanteId, byte[] rowVersion);

    /// <summary>
    /// Aprueba un comprobante de forma as�ncrona.
    /// </summary>
    /// <param name="id">El ID del comprobante a aprobar.</param>
    /// <param name="approveParameters">Par�metros para la aprobaci�n, incluyendo el RowVersion para concurrencia.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a aprobar.</exception>
    /// <exception cref="ComprobanteYaFueAprobadoException">Se lanza si el comprobante ya se encuentra aprobado.</exception>
    Task ApproveAsync(int id, IComprobanteApprove approveParameters);

    Task UpdatePeriodoAsync(int id, IComprobantePeriodoUpdate parameters);

    /// <summary>
    /// Rechaza un comprobante de forma as�ncrona.
    /// </summary>
    /// <param name="id">El ID del comprobante a rechazar.</param>
    /// <param name="rejectParameters">Par�metros para el rechazo, incluyendo el motivo, usuario y RowVersion.</param>
    /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
    /// <exception cref="ComprobanteInexistenteException">Se lanza si no se encuentra el comprobante a rechazar.</exception>
    Task RejectAsync(int id, IComprobanteReject rejectParameters);
}

/// <summary>
/// Define los par�metros necesarios para rechazar un comprobante.
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
/// Define los par�metros necesarios para aprobar un comprobante.
/// </summary>
public interface IComprobanteApprove
{
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento de la aprobaci�n.
    /// </summary>
    byte[] RowVersion { get; set; }
}

public interface IComprobantePeriodoUpdate
{
    int? PeriodoId { get; set; }
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define los par�metros necesarios para autorizar un comprobante.
/// </summary>
public interface IComprobanteAuthorize
{
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento de la autorizaci�n.
    /// </summary>
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define los par�metros necesarios para confirmar un comprobante.
/// </summary>
public interface IComprobanteConfirm
{
    /// <summary>
    /// Token de concurrencia (RowVersion) del comprobante al momento de la confirmaci�n.
    /// </summary>
    byte[] RowVersion { get; set; }
}

/// <summary>
/// Define la estructura de datos de la cabecera de un comprobante, utilizada principalmente para la verificaci�n inicial.
/// </summary>
public interface IComprobanteCabecera
{
    /// <summary>
    /// ID del comprobante (0 si es nuevo).
    /// </summary>
    int Id { get; set; }
    /// <summary>
    /// Versi�n del formato del QR.
    /// </summary>
    int ver { get; set; }
    /// <summary>
    /// Fecha de emisi�n del comprobante (formato yyyy-MM-dd).
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
    /// C�digo num�rico del tipo de comprobante seg�n AFIP.
    /// </summary>
    int tipoCmp { get; set; }
    /// <summary>
    /// N�mero del comprobante.
    /// </summary>
    int nroCmp { get; set; }
    /// <summary>
    /// Importe total del comprobante.
    /// </summary>
    decimal importe { get; set; }
    /// <summary>
    /// C�digo de la moneda ("PES", "DOL", etc.).
    /// </summary>
    string moneda { get; set; }
    /// <summary>
    /// Cotizaci�n de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? ctz { get; set; }
    /// <summary>
    /// C�digo del tipo de documento del receptor seg�n AFIP (ej: 80 para CUIT).
    /// </summary>
    int tipoDocRec { get; set; }
    /// <summary>
    /// N�mero de documento del receptor (CUIT/CUIL/DNI).
    /// </summary>
    long nroDocRec { get; set; }
    /// <summary>
    /// Tipo de c�digo de autorizaci�n ("E" para CAE, "A" para CAEA).
    /// </summary>
    string tipoCodAut { get; set; }
    /// <summary>
    /// C�digo de autorizaci�n (CAE o CAEA).
    /// </summary>
    long codAut { get; set; }
}

/// <summary>
/// Define los par�metros adicionales para la operaci�n de an�lisis de documentos.
/// </summary>
public interface IComprobanteAnalysisParameter
{
    /// <summary>
    /// ID de la compa��a (tenant) a la que pertenece el comprobante analizado. Usado para buscar impuestos/percepciones espec�ficos.
    /// </summary>
    long CompanyId { get; set; }
    /// /// <summary>
    /// ID de la empresa portal.
    /// </summary>
    int? EmpresaId { get; set; }
    ICollection<ModoAnalisis> Modos { get; set; }
}

/// <summary>
/// Define los par�metros de consulta para buscar y filtrar comprobantes. Hereda de IQueryParameter para incluir paginaci�n y ordenamiento.
/// </summary>
public interface IComprobanteQueryParameter : IQueryParameter
{
    /// <summary>
    /// Filtra por N�mero de Identificaci�n Fiscal (CUIT/CUIL) del proveedor (permite b�squeda parcial).
    /// </summary>
    string NroIdentificacionFiscalPro { get; set; }
    /// <summary>
    /// Filtra por ID de la categor�a fiscal del emisor.
    /// </summary>
    short? CategoriaTipoEmisorId { get; set; }
    /// <summary>
    /// Filtra por ID del tipo de comprobante.
    /// </summary>
    short? ComprobanteTipoId { get; set; }
    /// <summary>
    /// Filtra por ID de la categor�a fiscal del receptor.
    /// </summary>
    short? CategoriaTipoReceptorId { get; set; }
    /// <summary>
    /// Filtra por n�mero del punto de venta.
    /// </summary>
    int? PuntoVenta { get; set; }
    /// <summary>
    /// Filtra por n�mero de comprobante.
    /// </summary>
    int? Numero { get; set; }
    /// <summary>
    /// Filtra por fecha de emisi�n m�nima (inclusive).
    /// </summary>
    DateTime? FechaEmisionDesde { get; set; }
    /// <summary>
    /// Filtra por fecha de emisi�n m�xima (inclusive).
    /// </summary>
    DateTime? FechaEmisionHasta { get; set; }
    /// <summary>
    /// Filtra por fecha de registraci�n m�nima (inclusive).
    /// </summary>
    DateTime? FechaRegistracionDesde { get; set; }
    /// <summary>
    /// Filtra por fecha de registraci�n m�xima (inclusive).
    /// </summary>
    DateTime? FechaRegistracionHasta { get; set; }
    /// <summary>
    /// Filtra por ID del tipo de c�digo de autorizaci�n.
    /// </summary>
    short? TipoCodigoAutorizacionId { get; set; }
    /// <summary>
    /// Filtra por ID de la empresa asociada.
    /// </summary>
    int? EmpresaId { get; set; }
    /// <summary>
    /// Filtra por ID de la compa��a (tenant) asociada.
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
/// Define la estructura de datos para la creaci�n de un nuevo comprobante confirmado.
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
    /// Importe total de percepci�n de IVA.
    /// </summary>
    decimal ImportePercepcionIVA { get; set; }
    /// <summary>
    /// Importe total de percepci�n de Ingresos Brutos (IIBB).
    /// </summary>
    decimal ImportePercepcionIIBB { get; set; }
    /// <summary>
    /// Importe total de percepci�n municipal.
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
    /// C�digo HES (Hoja de Entrada de Servicio), si aplica.
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
    /// Lista de detalles (�tems) del comprobante a crear.
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
    /// Indica si el QR del comprobante fue le�do y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extra�do del c�digo QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// El estado de la constataci�n (verificaci�n) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacion { get; set; }
    /// <summary>
    /// El estado de la constataci�n (verificaci�n) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constataci�n (verificaci�n) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCA { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constataci�n (verificaci�n) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCAQR { get; set; }
    /// <summary>
    /// Cotizaci�n de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? Cotizacion { get; set; }
    /// <summary>
    /// N�mero de remito asociado.
    /// </summary>
    string NroRemito { get; set; }
    /// <summary>
    ///  N�mero de orden de compra asociado.
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
    /// Fecha de vencimiento del c�digo de autorizaci�n del comprobante.
    /// </summary>
    DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    /// <summary>
    /// Nombre original del archivo analizado.
    /// </summary>
    string FileName { get; set; }
    short? CondicionVentaId { get; set; }
}

/// <summary>
/// Define la estructura de datos para un detalle (�tem) al crear un comprobante.
/// </summary>
public interface IComprobanteDetalleCreate
{
    /// <summary>
    /// ID de la unidad de medida del �tem (opcional, default 'Unidad').
    /// </summary>
    short? UnidadMedidaId { get; set; }
    /// <summary>
    /// Cantidad del �tem.
    /// </summary>
    int Cantidad { get; set; }
    /// <summary>
    /// Precio por unidad del �tem.
    /// </summary>
    decimal PrecioUnitario { get; set; }
    /// <summary>
    /// Importe de bonificaci�n aplicado a este �tem (opcional).
    /// </summary>
    decimal? ImporteBonificacion { get; set; }
    /// <summary>
    /// Subtotal del �tem (Cantidad * PrecioUnitario - ImporteBonificacion).
    /// </summary>
    decimal Subtotal { get; set; }
    /// <summary>
    /// Importe de IVA correspondiente a este �tem (opcional).
    /// </summary>
    decimal? ImporteIVA { get; set; }
    /// <summary>
    /// Indica si el �tem est� exento de IVA (opcional).
    /// </summary>
    bool? Exento { get; set; }
    /// <summary>
    /// Al�cuota de IVA aplicada a este �tem (opcional).
    /// </summary>
    decimal? Alicuota { get; set; }
    /// <summary>
    /// ID de la orden de compra relacionada (opcional).
    /// </summary>
    int? OrdenCompraId { get; set; }
    /// <summary>
    /// Descripci�n o detalle del �tem.
    /// </summary>
    string Detalle { get; set; }
}

/// <summary>
/// Define la estructura de datos para un impuesto al crear un comprobante.
/// </summary>
public interface IComprobanteImpuestoCreate
{
    /// <summary>
    /// ID del tipo de impuesto predefinido (opcional, si no, se usa la descripci�n).
    /// </summary>
    int? ImpuestoId { get; set; }
    /// <summary>
    /// Descripci�n del impuesto.
    /// </summary>
    string Descripcion { get; set; }
    /// <summary>
    /// Importe total del impuesto.
    /// </summary>
    decimal ImporteTotal { get; set; }
    /// <summary>
    /// Al�cuota del impuesto (opcional, informativo).
    /// </summary>
    decimal? Alicuota { get; set; }
}

/// <summary>
/// Define la estructura de datos para una percepci�n al crear un comprobante.
/// </summary>
public interface IComprobantePercepcionCreate
{
    /// <summary>
    /// ID del tipo de percepci�n predefinido (opcional, si no, se usa la descripci�n).
    /// </summary>
    int? PercepcionId { get; set; }
    /// <summary>
    /// Descripci�n de la percepci�n.
    /// </summary>
    string Descripcion { get; set; }
    /// <summary>
    /// Al�cuota aplicada para calcular la percepci�n.
    /// </summary>
    decimal Alicuota { get; set; }
    /// <summary>
    /// Importe total de la percepci�n.
    /// </summary>
    decimal ImporteTotal { get; set; }
}

/// <summary>
/// Define la estructura de datos para guardar (crear o actualizar borrador) un comprobante.
/// Incluye ID para identificar el registro existente en caso de actualizaci�n de borrador.
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
    /// Lista de detalles (�tems) del comprobante a guardar/actualizar en borrador.
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
    /// Indica si el QR del comprobante fue le�do y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extra�do del c�digo QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// El estado de la constataci�n (verificaci�n) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacion { get; set; }
    /// <summary>
    /// El estado de la constataci�n (verificaci�n) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constataci�n (verificaci�n) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCA { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constataci�n (verificaci�n) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    public string ObservacionesARCAQR { get; set; }
}

/// <summary>
/// Define la estructura de datos para un detalle (�tem) al guardar/actualizar un borrador de comprobante.
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
/// Define la estructura de datos para una percepci�n al guardar/actualizar un borrador de comprobante.
/// </summary>
public interface IComprobantePercepcionSave
{
    /// <summary>
    /// ID de la percepci�n existente (si se actualiza, 0 si es nueva).
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
    /// Indica si esta percepci�n debe ser eliminada al actualizar el borrador.
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
    /// Lista de detalles (�tems) actualizados del comprobante.
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
    /// Indica si el QR del comprobante fue le�do y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extra�do del c�digo QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// Cotizaci�n de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? Cotizacion { get; set; }
    /// <summary>
    /// N�mero de remito asociado.
    /// </summary>
    string NroRemito { get; set; }
    /// <summary>
    ///  N�mero de orden de compra asociado.
    /// </summary>
    string NroOrdenCompra { get; set; }
    /// <summary>
    /// Fecha de vencimiento del comprobante.
    /// </summary>
    DateTime? FechaVencimiento { get; set; }
    /// <summary>
    /// Fecha de vencimiento del c�digo de autorizaci�n del comprobante.
    /// </summary>
    DateTime? FechaVencimientoCodigoAutorizacion { get; set; }

    short? CondicionVentaId { get; set; }
}

/// <summary>
/// Define la estructura de datos para un detalle (�tem) al actualizar un comprobante.
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
/// Define la estructura de datos para una percepci�n al actualizar un comprobante.
/// </summary>
public interface IComprobantePercepcionUpdate
{
    /// <summary>
    /// ID de la percepci�n existente (si se actualiza, 0 si es nueva).
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
    /// Indica si esta percepci�n debe ser eliminada al actualizar el comprobante.
    /// </summary>
    bool Deleted { get; set; }
}

/// <summary>
/// Define la estructura de datos del resultado del an�lisis de un documento de comprobante.
/// </summary>
public interface IComprobanteAnalysisResult
{
    public string RazonSocialPro { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalPro"/> (Extra�do)
    string NroIdentificacionFiscalPro { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoEmisorId"/> (Inferido)
    short? CategoriaTipoEmisorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ComprobanteTipoId"/> (Inferido)
    short? ComprobanteTipoId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CategoriaTipoReceptorId"/> (Extra�do/Inferido)
    short? CategoriaTipoReceptorId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.NroIdentificacionFiscalCompany"/> (Extra�do)
    string NroIdentificacionFiscalCompany { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.PuntoVenta"/> (Extra�do, como string)
    string PuntoVenta { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Letra"/> (Extra�do)
    string Letra { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.Numero"/> (Extra�do, como string)
    string Numero { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.FechaEmision"/> (Extra�da, como string yyyy-MM-dd)
    string FechaEmision { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.TipoCodigoAutorizacionId"/> (Inferido)
    short? TipoCodigoAutorizacionId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.CodigoAutorizacion"/> (Extra�do)
    string CodigoAutorizacion { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.MonedaId"/> (Inferido)
    long? MonedaId { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteNeto"/> (Extra�do/Calculado)
    decimal? ImporteNeto { get; set; }
    /// <inheritdoc cref="IComprobanteCabecera.ImporteTotal"/> (Extra�do/Calculado)
    decimal? ImporteTotal { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteIVA"/> (Calculado desde impuestos)
    decimal? ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.ImporteBonificacion"/> (Extra�do/Calculado)
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
    /// <inheritdoc cref="IComprobanteCabecera.EmpresaId"/> (Opcional, podr�a venir de par�metros)   
    string CodigoHES { get; set; }
    /// <inheritdoc cref="IComprobanteCreate.Comentarios"/> (Extra�do)
    string Comentarios { get; set; }
    /// <summary>
    /// Lista de detalles (�tems) extra�dos del comprobante.
    /// </summary>
    List<IComprobanteDetalleAnalysisResult> Detalles { get; set; }
    /// <summary>
    /// Lista de impuestos extra�dos/inferidos del comprobante.
    /// </summary>
    List<IComprobanteImpuestoAnalysisResult> Impuestos { get; set; }
    /// <summary>
    /// Lista de percepciones extra�das/inferidas del comprobante.
    /// </summary>
    List<IComprobantePercepcionAnalysisResult> Percepciones { get; set; }
    /// <summary>
    /// Indica si el QR del comprobante fue le�do y procesado correctamente.
    /// </summary>
    bool? ValidacionQR { get; set; }
    /// <summary>
    /// Es el valor plano (JSON) extra�do del c�digo QR del comprobante.
    /// </summary>
    string QRValor { get; set; }
    /// <summary>
    /// El estado de la constataci�n (verificaci�n) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacionQR { get; set; }
    /// <summary>
    /// El estado de la constataci�n (verificaci�n) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    EstadoConstatacion EstadoConstatacion { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constataci�n (verificaci�n) del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    string ObservacionesARCA { get; set; }
    /// <summary>
    /// Observaciones sobre el estado de la constataci�n (verificaci�n) del QR del comprobante
    /// contra una autoridad externa (ej. AFIP).
    /// </summary>
    string ObservacionesARCAQR { get; set; }
    /// <summary>
    /// Fecha de vencimiento del comprobante.
    /// </summary>
    DateTime? FechaVencimiento { get; set; }
    /// <summary>
    /// Fecha de vencimiento del c�digo de autorizaci�n del comprobante.
    /// </summary>
    DateTime? FechaVencimientoCodigoAutorizacion { get; set; }
    /// <summary>
    /// Cotizaci�n de la moneda (si aplica, para moneda extranjera).
    /// </summary>
    decimal? Cotizacion { get; set; }
    /// <summary>
    /// N�mero de remito asociado.
    /// </summary>
    string NroRemito { get; set; }
    /// <summary>
    ///  N�mero de orden de compra asociado.
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
/// Define la estructura de datos de un detalle (�tem) extra�do del an�lisis de un comprobante.
/// </summary>
public interface IComprobanteDetalleAnalysisResult
{
    /// <inheritdoc cref="IComprobanteDetalleCreate.UnidadMedidaId"/> (Inferido)
    short? UnidadMedidaId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Cantidad"/> (Extra�do)
    int? Cantidad { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.PrecioUnitario"/> (Extra�do)
    decimal? PrecioUnitario { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteBonificacion"/> (Extra�do/Calculado)
    decimal? ImporteBonificacion { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Subtotal"/> (Extra�do)
    decimal? Subtotal { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.ImporteIVA"/> (Calculado)
    decimal? ImporteIVA { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Exento"/> (Inferido)
    bool? Exento { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Alicuota"/> (Extra�do/Inferido)
    decimal? Alicuota { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.OrdenCompraId"/> (Opcional, podr�a extraerse)
    int? OrdenCompraId { get; set; }
    /// <inheritdoc cref="IComprobanteDetalleCreate.Detalle"/> (Extra�do)
    string Detalle { get; set; }
    /// <summary>
    /// ID del impuesto de IVA asociado a este detalle (inferido por al�cuota).
    /// </summary>
    int? ImpuestoIVAId { get; set; }
}

/// <summary>
/// Define la estructura de datos de un impuesto extra�do del an�lisis de un comprobante.
/// </summary>
public interface IComprobanteImpuestoAnalysisResult
{
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImpuestoId"/> (Inferido por descripci�n/al�cuota)
    int? ImpuestoId { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.Descripcion"/> (Extra�do)
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobanteImpuestoCreate.ImporteTotal"/> (Extra�do)
    decimal? ImporteTotal { get; set; }
    /// <summary>
    /// Valor de la al�cuota extra�da del documento.
    /// </summary>
    decimal? AlicuotaValor { get; set; }
    /// <summary>
    /// ID del tipo de impuesto (IVA, Interno, Provincial, etc.) inferido.
    /// </summary>
    short? TipoId { get; set; }
}

/// <summary>
/// Define la estructura de datos de una percepci�n extra�da del an�lisis de un comprobante.
/// </summary>
public interface IComprobantePercepcionAnalysisResult
{
    /// <inheritdoc cref="IComprobantePercepcionCreate.PercepcionId"/> (Inferido por descripci�n)
    int? PercepcionId { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Descripcion"/> (Extra�do)
    string Descripcion { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.Alicuota"/> (Extra�do)
    decimal? Alicuota { get; set; }
    /// <inheritdoc cref="IComprobantePercepcionCreate.ImporteTotal"/> (Extra�do)
    decimal? ImporteTotal { get; set; }
    /// <summary>
    /// ID del tipo de percepci�n (IVA, IIBB, Municipal) inferido.
    /// </summary>
    short? TipoId { get; set; }
}

/// <summary>
/// Define el resultado de la constataci�n (verificaci�n) de un comprobante
/// contra una autoridad externa (ej. AFIP).
/// </summary>
public interface IComprobanteConstatacionResult
{
    /// <summary>
    /// Indica el estado de la constataci�n (Ok si est� autorizado, Error si no lo est� o hubo problemas).
    /// </summary>
    /// <seealso cref="EstadoConstatacion"/>
    EstadoConstatacion Estado { get; }

    /// <summary>
    /// Colecci�n de mensajes u observaciones devueltas por el servicio de constataci�n
    /// (ej. motivos de rechazo, detalles del error, o mensajes informativos).
    /// </summary>
    ICollection<string> Observaciones { get; }
}

/// <summary>
/// Define los posibles estados resultado de la constataci�n de un comprobante.
/// </summary>
public enum EstadoConstatacion
{
    /// <summary>
    /// El comprobante fue validado exitosamente y est� autorizado por la autoridad fiscal.
    /// </summary>
    Ok = EstadoValidacionARCA.VALIDADA,
    /// <summary>
    /// El comprobante no est� autorizado, no fue encontrado por la autoridad fiscal.
    /// </summary>
    Rechazado = EstadoValidacionARCA.RECHAZADA,
    /// <summary>
    /// Hubo un error durante el proceso de constataci�n.
    /// Las observaciones pueden contener m�s detalles.
    /// </summary>
    Error = EstadoValidacionARCA.ERROR_VALIDACION,
    /// <summary>
    /// El comprobante no fue validado.
    /// </summary>
    SinConstatar = EstadoValidacionARCA.NO_VALIDADA,
}

#endregion
