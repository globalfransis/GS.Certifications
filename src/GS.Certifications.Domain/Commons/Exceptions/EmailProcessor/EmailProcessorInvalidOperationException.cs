using System;
using System.Runtime.Serialization;

namespace GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;

public class EmailProcessorInvalidOperationException : InvalidOperationException
{
    public virtual string StatusCode { get; } = "PRC_ERR";  // Propiedad virtual
    public EmailProcessorInvalidOperationException() { }
    public EmailProcessorInvalidOperationException(string message) : base(message) { }
    public EmailProcessorInvalidOperationException(string message, Exception innerException) : base(message, innerException) { }
    protected EmailProcessorInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
