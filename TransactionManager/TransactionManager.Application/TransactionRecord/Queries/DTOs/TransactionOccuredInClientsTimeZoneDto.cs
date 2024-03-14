﻿namespace TransactionManager.Application.TransactionRecord.Queries.DTOs;

public class TransactionOccuredInClientsTimeZoneDto
{
    public string TransactionRecordId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime ClientsDateTime { get; set; }
    public string ClientLocation { get; set; }
}