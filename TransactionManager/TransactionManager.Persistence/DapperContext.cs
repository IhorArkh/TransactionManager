using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TransactionManager.Domain;

namespace TransactionManager.Persistence;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task AddTransactionRecords(IEnumerable<TransactionRecord> transactionRecords)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();

            try
            {
                foreach (var record in transactionRecords)
                {
                    await connection.ExecuteAsync(@"
                    MERGE INTO TransactionRecords AS target
                    USING (VALUES (@TransactionRecordId, @Name, @Email, @Amount, @TransactionDate, @ClientLocation)) 
                        AS source (TransactionRecordId, Name, Email, Amount, TransactionDate, ClientLocation)
                        ON target.TransactionRecordId = source.TransactionRecordId
                    WHEN NOT MATCHED THEN
                        INSERT (TransactionRecordId, Name, Email, Amount, TransactionDate, ClientLocation)
                        VALUES (source.TransactionRecordId, source.Name, source.Email, source.Amount, source.TransactionDate, source.ClientLocation);
                ", record);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByYear(int year)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();

            try
            {
                var transactions = await connection.QueryAsync<TransactionRecord>(@"
                    SELECT *
                    FROM TransactionRecords
                    WHERE YEAR(TransactionDate) = @Year OR (YEAR(TransactionDate) = @PrevYear AND MONTH(TransactionDate) = 12 AND DAY(TransactionDate) = 31)
                ", new { Year = year, PrevYear = year - 1 });

                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}