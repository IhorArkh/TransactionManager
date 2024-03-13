using System.Globalization;
using CsvHelper.Configuration;

namespace TransactionManager.Application.Services.CsvHelperService.Mapping;

public class TransactRecordWriteMap : ClassMap<Domain.TransactionRecord>
{
    public TransactRecordWriteMap()
    {
        Map(x => x.TransactionRecordId).Name("transaction_id");
        Map(x => x.Name).Name("name");
        Map(x => x.Email).Name("email");
        Map(x => x.Amount).Name("amount").Convert(x =>
        {
            decimal amount = x.Value.Amount;
            return "$" + amount.ToString(CultureInfo.GetCultureInfo("en-US"));
        });

        Map(x => x.TransactionDate).Name("transaction_date");
        Map(x => x.ClientLocation).Name("client_location");
    }
}