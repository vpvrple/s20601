# How to run the app
-  Clone the repo.
```bash
git clone https://github.com/vpvrple/s20601.git
```
-  Open solution (`s20601.sln`) file with Visual Studio.
-  Adjust connection string to `appsettings.json` or to User Secrets.
```bash
# example connection string for local sql server instance
"Data Source=<yourLocalHostname>;Initial Catalog=<YourDbName>;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
```
-  Run `CreateDatabaseWithSampleData.sql` on your SQL Server instance to create database with sample data. The script is included with the project (`CreateDatabaseWithSampleData.sql`).
- Build and run the project.