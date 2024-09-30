# TestTask
Clone the repo.
Open .sln file in VisualStudio
To run the app execute docker-compose up -d. It should start the SQL Server.
OpenPackageManagerConsole and execute add-migration with name 'SomeName' next execute update-database. now you should have The DB.
Now you can run the app from Visual Studio.

If You want to run the app Using Docker. Open docker-compose file, uncoment everything and execute docker-compose up -d.
NOTE: Nedd to perform migration.

