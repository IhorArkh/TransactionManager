using MediatR;
using Microsoft.AspNetCore.Http;

namespace TransactionManager.Application.Features.TransactionRecord.Commands.AddTransactionRecord;

public class AddTransactionRecordCommand : IRequest
{
    public IFormFile File { get; set; }
}