#!/bin/bash

# Wait for SQL Server to be up and running
until /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P Your_password123 -d master -Q "SELECT 1" > /dev/null 2>&1; do
  echo "Waiting for SQL Server to start..."
  sleep 1
done

# Run the script to create the database and tables
echo "Run the script to create the database and tables..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P Your_password123 -d master -i /tmp/CreateDatabase.sql
