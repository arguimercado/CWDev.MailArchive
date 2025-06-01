# CWDev.MailArchive

CWDev.MailArchive is a backend service for processing, logging, and archiving emails from IMAP mailboxes. It automates the retrieval, deduplication, and storage of emails, ensuring that all processed messages are recorded and any errors are logged for further inspection.

## Features

- Connects to IMAP servers and processes incoming emails
- Prevents duplicate processing by tracking the latest processed emails per mailbox
- Logs all email processing actions and errors
- Supports storage of email data and attachments via a blob service
- Structured with dependency injection for easy service management
- Built with .NET, Entity Framework Core, and MailKit

## Architecture Overview

- **MailRepository:** Handles retrieval and logging of email processing actions and errors.
- **SmtpService:** Orchestrates connection to IMAP servers, processes new emails, and manages deduplication.
- **BlobService:** Stores email files and attachments.
- **MailProcessingContext:** Entity Framework Core context for mail and error logs.
- **API Layer:** Supports scheduled fetching and processing of emails.

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- SQL Server (for database)
- Access to IMAP mailboxes

### Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/arguimercado/CWDev.MailArchive.git
   cd CWDev.MailArchive
   ```

2. Configure your connection strings and settings in `appsettings.json` (see sample values in the code).

3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```

4. Build and run the service:
   ```sh
   dotnet build
   dotnet run
   ```

### Usage

- On startup, the service will connect to configured mailboxes, fetch new emails, and process them.
- Email logs and error logs are stored in the database.
- Blob storage is used for email file persistence.

## Technologies Used

- .NET 8
- Entity Framework Core
- MailKit (IMAP/SMTP support)
- MediatR (for CQRS in API)
- Microsoft.Extensions.DependencyInjection

## Contribution

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See `LICENSE.txt` for details.