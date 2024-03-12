﻿namespace TransactionManager.Domain;

public class TransactionRecord
{
    public string TransactionRecordId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ClientLocation { get; set; }
}