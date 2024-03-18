using MediatR;
using Microsoft.AspNetCore.Http;

namespace TransactionManager.Application.Features.TransactionRecord.Commands.AddTransactionRecord;

public record AddTransactionRecordCommand : IRequest
{
    public IFormFile File { get; set; }
}