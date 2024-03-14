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

    public async Task<IEnumerable<TransactionRecord>> GetTransactionsByYear(int year)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();

            try
            {
                // 2 day of month instead of 1 required because of 00:00:00 default DateTime
                var lastDayOfPrevYear = new DateTime(year - 1, 12, 31);
                var firstDayOfNextYear = new DateTime(year + 1, 1, 2);

                var transactions = await connection.QueryAsync<TransactionRecord>(@"
                    SELECT *
                    FROM TransactionRecords
                    WHERE (TransactionDate >= @LastDayOfPrevYear AND TransactionDate <= @firstDayOfNextYear);
                ", new { LastDayOfPrevYear = lastDayOfPrevYear, FirstDayOfNextYear = firstDayOfNextYear });

                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task<IEnumerable<TransactionRecord>> GetTransactionsByMonth(int year, int month)
    {
        if (month == default)
            throw new Exception("Error during getting transactionRecords by month. Month is default.");

        using (var connection = CreateConnection())
        {
            connection.Open();

            try
            {
                // 2 day of month instead of 1 required because of 00:00:00 default DateTime
                var firstDayOfNextMonth = new DateTime(year, month + 1, 2);
                var lastDayOfPrevMonth = new DateTime(year, month, 1).AddDays(-1);

                var transactions = await connection.QueryAsync<TransactionRecord>(@"
                    SELECT *
                    FROM TransactionRecords
                    WHERE (TransactionDate >= @LastDayOfPrevMonth AND TransactionDate <= @FirstDayOfNextMonth);
                ", new { FirstDayOfNextMonth = firstDayOfNextMonth, LastDayOfPrevMonth = lastDayOfPrevMonth });

                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}