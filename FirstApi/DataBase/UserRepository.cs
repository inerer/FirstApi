using System.Reflection.Metadata;
using FirstApi.Models.Client;
using FirstApi.Services.ServicesInterface;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace FirstApi.DataBase;

public class UserRepository : IClientService
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public IActionResult GetClient(int id)
    {
        connection.Open();
        Client client = new Client();
        string query = @"select * from client where client_id = $1";
        NpgsqlCommand cmd = new NpgsqlCommand(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        using (cmd)
        {
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    client.ClientId = reader.GetInt32(reader.GetOrdinal("client_id"));
                    client.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                    client.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                    
                    var middleName = reader.GetValue(reader.GetOrdinal("middle_name"));

                    client.MiddleName = middleName == DBNull.Value ? null : middleName.ToString();
                }
            }  
        }
        connection.Close();
        return new OkObjectResult(client);
    }
    public IActionResult AddClient(Client client)
    {
       connection.Open();
       string query = @" insert into client(last_name, first_name, middle_name) values ($1, $2, $3)";
       NpgsqlCommand command = new NpgsqlCommand(query, connection)
       {
           Parameters =
           {
               new NpgsqlParameter() { Value = client.LastName },
               new NpgsqlParameter() { Value = client.FirstName },
               new NpgsqlParameter() { Value = client.MiddleName }
           }
       };
       try
       {
           command.ExecuteNonQuery();
           return new AcceptedResult();
       }
       catch (Exception e)
       {
           return new BadRequestResult();
       }
       finally
       {
           connection.Close();
       }
    }

    public IActionResult EditClient(Client client)
    {
        connection.Open();
        string query = @"update client set last_name=($1), first_name=($2), middle_name=($3) where client_id=($4)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = client.LastName },
                new NpgsqlParameter() { Value = client.FirstName },
                new NpgsqlParameter() { Value = client.MiddleName },
                new NpgsqlParameter() { Value = client.ClientId }
            }
        };
        try
        {
            // выполняем команду
            return command.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        // что-то не так с данными (ну вдруг)
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }

    public IActionResult DeleteClient(int id)
    {
        connection.Open();
        string query = @"delete from client where client_id=($1)";
        NpgsqlCommand command = new NpgsqlCommand(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = id }
            }
        };
        try
        {
            return command.ExecuteNonQuery() > 0 ? new AcceptedResult() : new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
        finally
        {
            connection.Close();
        }
    }
}