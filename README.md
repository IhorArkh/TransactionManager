# TransactionManager
API service for managing transactions.

## Setup steps
- Provide your connection string in appsettings file.
- Provide your token from ipinfo.io in appsettings file.

## Data for testing
- You can find data for testing in "dataForTesting.csv" file located in root  folder.
- **Make sure your own testing data has the same structure as the testing file.** Otherwise the application will not work correctly.

## Use cases
- Add transactions
  - User have ability to upload csv file with transactions to database. If the transaction with same ID is already exists in the database, it will be ignored.
- Get trasactions
  - User have ability to get csv file with transactions occured during specified year or month in ***clients time zone***.
  - User have ability to get csv file with transactions occured during specified year or month in ***his time zone***. Time zone will be determined by IP.

Situations when, for example, the client made a transaction at 23:00 on 12/31/2023 UTC, but in your time zone was already 2023 - handled. Such records will be added to file when you get transactions for 2023. Other similar situations handled too.

## Used technologies, libraries and 3rd party services
- ASP.NET Core Web API
- MediatR - for sending queries/commands to their handlers. 
- Entity Framework Core - for creating database schema.
- Dapper - for querying the database.
- CsvHelper - for reading and writing to CSV files.
- GeoTimeZone - for getting time zone by location coordinates.
- TimeZoneConverter - for converting UTC time to local.
- IPpinfo.io API - for getting user location coordinates by IP.
