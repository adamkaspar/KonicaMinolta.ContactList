using System.Net;

namespace KonicaMinolta.Shared.Domain.Exceptions;

public record ExceptionDto(Guid CorrelationId, HttpStatusCode StatusCode, string Message);
