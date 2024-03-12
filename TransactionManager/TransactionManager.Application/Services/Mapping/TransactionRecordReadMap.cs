using System.Globalization;
using CsvHelper.Configuration;

namespace TransactionManager.Application.Services.Mapping;

public class TransactionRecordReadMap : ClassMap<Domain.TransactionRecord>
{
    public TransactionRecordReadMap()
    {
        Map(x => x.TransactionRecordId).Name("transaction_id");
        Map(x => x.Name).Name("name");
        Map(x => x.Email).Name("email");
        Map(x => x.Amount).Name("amount").Convert(x =>
        {
            string amount = x.Row.GetField<string>("amount").Remove(0, 1);
            return Convert.ToDecimal(amount, CultureInfo.GetCultureInfo("en-US"));
        });
        
        Map(x => x.TransactionDate).Name("transaction_date");
        Map(x => x.ClientLocation).Name("client_location");
    }
}