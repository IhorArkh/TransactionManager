using System.Globalization;
using CsvHelper;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.Services.Mapping;

namespace TransactionManager.Application.Services;

public class CsvHelperService : ICsvHelperService
{
    public IEnumerable<T> ReadCsv<T>(Stream file)
    {
        var result = new List<T>();

        try
        {
            using (var streamReader = new StreamReader(file))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TransactionRecordReadMap>();

                    var records = csvReader.GetRecords<T>();

                    result.AddRange(records);
                }

                return result;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }   
    }

    public byte[] WriteToCsv<T>(IEnumerable<T> data)
    {
        throw new NotImplementedException();
    }
}