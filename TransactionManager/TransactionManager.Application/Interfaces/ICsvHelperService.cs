using System.Collections;

namespace TransactionManager.Application.Interfaces;

public interface ICsvHelperService
{
    IEnumerable<T> ReadCsv<T>(Stream file);
    byte[] WriteToCsv(IEnumerable data);
}