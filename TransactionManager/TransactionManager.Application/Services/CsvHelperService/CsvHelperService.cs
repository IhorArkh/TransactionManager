using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using TransactionManager.Application.Exceptions;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.Services.CsvHelperService.Mapping;
using Exception = System.Exception;

namespace TransactionManager.Application.Services.CsvHelperService;

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
            throw new CsvHelperReadException(ex.Message);
        }
    }

    public async Task<byte[]> WriteToCsvAsync(IEnumerable data, ClassMap classMap)
    {
        using var memoryStream = new MemoryStream();
        await using (var streamWriter = new StreamWriter(memoryStream))
        {
            await using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.Context.RegisterClassMap(classMap);
                await csvWriter.WriteRecordsAsync(data);
            }
        }

        return memoryStream.ToArray();
    }
}