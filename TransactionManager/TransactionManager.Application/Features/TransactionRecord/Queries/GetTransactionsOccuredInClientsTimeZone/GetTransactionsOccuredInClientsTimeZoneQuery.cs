using MediatR;

namespace TransactionManager.Application.Features.TransactionRecord.Queries.GetTransactionsOccuredInClientsTimeZone;

public class GetTransactionsOccuredInClientsTimeZoneQuery : IRequest<byte[]>
{
    public int Year { get; set; }
    public int Month { get; set; }
}