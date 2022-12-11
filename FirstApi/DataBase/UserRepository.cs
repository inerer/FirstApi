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
        string query = @"select * from client where id = $1";
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
        throw new NotImplementedException();
    }

    public IActionResult EditClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteClient(Client client)
    {
        throw new NotImplementedException();
    }
}