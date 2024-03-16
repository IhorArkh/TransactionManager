using System.Collections;
using CsvHelper.Configuration;

namespace TransactionManager.Application.Interfaces;

public interface ICsvHelperService
{
    IEnumerable<T> ReadCsv<T>(Stream file);
    byte[] WriteToCsv(IEnumerable data, ClassMap classMap);
}