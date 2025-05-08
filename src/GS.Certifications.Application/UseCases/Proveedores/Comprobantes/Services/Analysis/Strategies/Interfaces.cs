using Azure.AI.DocumentIntelligence;
using GS.AI.DocumentIntelligence.Legacy.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services.Analysis.Strategies
{
    // --- Interfaces para las Estrategias de Análisis ---

    // Podríamos necesitar un objeto contexto para pasar datos entre estrategias
    public class AnalisysContext
    {
        public DocumentAnalysisResult AnalysisResult { get; } // Resultado del servicio de IA
        public DocumentFieldDictionary ExtractedFields { get; }
        public IComprobanteAnalysisParameter Parameters { get; }
        public ComprobanteAnalysisResult ResultInProgress { get; } // El objeto que se va construyendo
        public string QrJson { get; set; } // Json decodificado del QR si existe

        public AnalisysContext(DocumentAnalysisResult aiDocument, IComprobanteAnalysisParameter parameters, ComprobanteAnalysisResult resultInProgress)
        {
            AnalysisResult = aiDocument;
            Parameters = parameters;
            ResultInProgress = resultInProgress;
            ExtractedFields = aiDocument.ExtractedFields;
        }
    }

    // Estrategia para procesar la cabecera de un comprobante
    public interface IComprobanteHeaderStrategy
    {
        /// <summary>
        /// Extrae y mapea los datos de la cabecera del comprobante.
        /// Puede usar datos del QR y/o campos extraídos por IA.
        /// Puede interactuar con la BD para buscar IDs relacionados (Tipo Cbte, Moneda, etc.).
        /// Puede llamar a VerifyAsync si la validación del QR es parte de la extracción.
        /// </summary>
        /// <param name="context">Contexto con datos de entrada y el resultado en progreso.</param>
        /// <returns>Task</returns>
        Task PopulateHeaderAsync(AnalisysContext context);
    }

    // Estrategia para procesar los items/detalles
    public interface IComprobanteDetailStrategy
    {
        /// <summary>
        /// Extrae y mapea los detalles (items) del comprobante.
        /// Puede interactuar con la BD para buscar IDs (UnidadMedida, ImpuestoIVA).
        /// </summary>
        /// <param name="context">Contexto con datos de entrada y el resultado en progreso.</param>
        /// <returns>Task</returns>
        Task PopulateDetailsAsync(AnalisysContext context);
    }

    // Estrategia para procesar impuestos (excluyendo IVA de detalles)
    public interface IComprobanteTaxStrategy
    {
        /// <summary>
        /// Extrae, normaliza, mapea y busca IDs de impuestos globales/resumidos.
        /// Actualiza la colección de Impuestos y los importes totales relacionados en el resultado.
        /// </summary>
        /// <param name="context">Contexto con datos de entrada y el resultado en progreso.</param>
        /// <returns>Task</returns>
        Task PopulateTaxesAsync(AnalisysContext context);
    }

    // Estrategia para procesar percepciones
    public interface IComprobantePerceptionStrategy
    {
        /// <summary>
        /// Extrae, normaliza, mapea y busca IDs de percepciones.
        /// Actualiza la colección de Percepciones y los importes totales relacionados en el resultado.
        /// </summary>
        /// <param name="context">Contexto con datos de entrada y el resultado en progreso.</param>
        /// <returns>Task</returns>
        Task PopulatePerceptionsAsync(AnalisysContext context);
    }

    // Estrategia para calcular/validar totales finales
    public interface IComprobanteTotalStrategy
    {
        /// <summary>
        /// Calcula o valida los importes Neto y Total finales basados en los datos ya poblados
        /// en el resultado y/o los campos extraídos por IA.
        /// </summary>
        /// <param name="context">Contexto con datos de entrada y el resultado en progreso.</param>
        /// <returns>Task</returns>
        Task PopulateTotalsAsync(AnalisysContext context);
    }
}
