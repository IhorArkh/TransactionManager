using MediatR;

namespace TransactionManager.Application.Features.TransactionRecord.Queries.GetTransactionsOccuredInUsersTimeZone;

public record GetTransactionsOccuredInUsersTimeZoneQuery : IRequest<byte[]>
{
    public int Year { get; set; }
    public int Month { get; set; }
}