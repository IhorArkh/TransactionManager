namespace TransactionManager.Application.Features.TransactionRecord.Queries.DTOs;

public class TransactionOccuredInUsersTimeZoneDto
{
    public string TransactionRecordId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime YourDateTime { get; set; }
    public string ClientLocation { get; set; }
}