using MediatR;
using TransactionManager.Application.Interfaces;
using TransactionManager.Persistence;

namespace TransactionManager.Application.Features.TransactionRecord.Commands.AddTransactionRecord;

public class AddTransactionRecordHandler : IRequestHandler<AddTransactionRecordCommand>
{
    private readonly DapperContext _dapperContext;
    private readonly ICsvHelperService _csvHelperService;

    public AddTransactionRecordHandler(DapperContext dapperContext, ICsvHelperService csvHelperService)
    {
        _dapperContext = dapperContext;
        _csvHelperService = csvHelperService;
    }
    
    public async Task Handle(AddTransactionRecordCommand request, CancellationToken cancellationToken)
    {
        var transactionRecords = 
            _csvHelperService.ReadCsv<Domain.TransactionRecord>(request.File.OpenReadStream());
        
        await _dapperContext.AddTransactionRecords(transactionRecords);
    }
}