#!/bin/bash

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -Q "SELECT 1" > /dev/null 2>&1
while [ $? -ne 0 ]; do
    echo "SQL Server is unavailable - sleeping"
    sleep 2
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -Q "SELECT 1" > /dev/null 2>&1
done

echo "SQL Server started - running initialization scripts"

# Run initialization scripts
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -i /init/schema.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -i /init/seed.sql

echo "Database initialization complete"

# Keep container running
/opt/mssql/bin/sqlservr
