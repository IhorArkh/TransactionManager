using System.Globalization;
using CsvHelper.Configuration;
using TransactionManager.Application.TransactionRecord.Queries.DTOs;

namespace TransactionManager.Application.Services.CsvHelperService.Mapping;

public class TransactionInClientsTimeZoneWriteMap : ClassMap<TransactionOccuredInClientsTimeZoneDto>
{
    public TransactionInClientsTimeZoneWriteMap()
    {
        Map(x => x.TransactionRecordId).Name("transaction_id");
        Map(x => x.Name).Name("name");
        Map(x => x.Email).Name("email");
        Map(x => x.Amount).Name("amount").Convert(x =>
        {
            decimal amount = x.Value.Amount;
            return "$" + amount.ToString(CultureInfo.GetCultureInfo("en-US"));
        });

        Map(x => x.TransactionDate).Name("transaction_datetime_utc");
        Map(x => x.ClientsDateTime).Name("clients_datetime");
        Map(x => x.ClientLocation).Name("client_location");
    }
}