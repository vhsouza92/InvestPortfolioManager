#!/bin/bash

# Aguardando o SQL Server iniciar
echo "Aguardando o SQL Server iniciar..."
sleep 30s

# Rodando o script para criar o banco de dados e popular com dados iniciais
echo "Rodando o script para criar o banco de dados e popular com dados iniciais..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P Your_password123 -d master -i /tmp/CreateDatabase.sql
