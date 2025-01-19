using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;
using Npgsql;

namespace LocationVoiture.dal.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly DBAccess _connection;

    public ClientRepository(DBAccess connection)
    {
        _connection = connection; 
    }
    
    public List<Client> GetAll()
    {
        List<Client> clients = new List<Client>();
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM GetAllClients()", _connection._SqlConnection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                clients.Add(new Client
                {
                    Id = (int)reader["id"],
                    Firstname = (string)reader["first_name"],
                    Lastname = (string)reader["last_name"],
                    Email = (string)reader["email"],
                    Address = new Address
                    {
                        Street = (string)reader["street"],
                        PostalCode = (string)reader["postal_code"],
                        City = (string)reader["city"],
                        Country = (string)reader["country"],
                    },
                    DrivingLicense = (string)reader["driving_license"],
                    BirthDate = (DateTime)reader["birth_date"],
                });
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error executing query: {command?.CommandText}. Error: {e.Message}", e.StackTrace);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return clients;
    }

    public Client? GetOneById(int givenId)
    {
        Client client = null;
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM GetClientById(@id)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@id", givenId);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                client = new Client
                {
                    Id = (int)reader["id"],
                    Firstname = (string)reader["first_name"],
                    Lastname = (string)reader["last_name"],
                    Email = (string)reader["email"],
                    Address = new Address
                    {
                        Street = (string)reader["street"],
                        PostalCode = (string)reader["postal_code"],
                        City = (string)reader["city"],
                        Country = (string)reader["country"],
                    },
                    DrivingLicense = (string)reader["driving_license"],
                    BirthDate = (DateTime)reader["birth_date"],
                };
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw new DBAccessException(command.CommandText, e.Message);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return client;
    }

    
    public Client? GetOneByName(string givenName)
    {
        Client? client = null;
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM getclientbyname(@name)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@name", givenName);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                client = new Client
                {
                    Id = (int)reader["id"],
                    Firstname = (string)reader["first_name"],
                    Lastname = (string)reader["last_name"],
                    Email = (string)reader["email"],
                    Address = new Address
                    {
                        Street = (string)reader["street"],
                        PostalCode = (string)reader["postal_code"],
                        City = (string)reader["city"],
                        Country = (string)reader["country"],
                    },
                    DrivingLicense = (string)reader["driving_license"],
                    BirthDate = (DateTime)reader["birth_date"],
                };
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw new DBAccessException(command.CommandText, e.Message);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return client;
    }


    public Client? GetOneByEmail(string email)
    {
        Client? client = null;
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM GetClientByEmail(@email)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@email", email);

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                client = new Client
                {
                    Id = (int)reader["id"],
                    Firstname = (string)reader["first_name"],
                    Lastname = (string)reader["last_name"],
                    Email = (string)reader["email"],
                    Address = new Address
                    {
                        Street = (string)reader["street"],
                        PostalCode = (string)reader["postal_code"],
                        City = (string)reader["city"],
                        Country = (string)reader["country"],
                    },
                    DrivingLicense = (string)reader["driving_license"],
                    BirthDate = (DateTime)reader["birth_date"],
                };
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw new DBAccessException(command.CommandText, e.Message);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return client;
    }


    public Client? Create(Client entity)
    {
        NpgsqlCommand command = null;
        int clientId = 0;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM createclient(@last_name, @first_name, @email, @street, @postal_code, @city, @country, @driving_license, @birth_date)", _connection._SqlConnection);

            command.Parameters.AddWithValue("@last_name", entity.Lastname);
            command.Parameters.AddWithValue("@first_name", entity.Firstname);
            command.Parameters.AddWithValue("@email", entity.Email);
            command.Parameters.AddWithValue("@street", entity.Address.Street);
            command.Parameters.AddWithValue("@postal_code", entity.Address.PostalCode);
            command.Parameters.AddWithValue("@city", entity.Address.City);
            command.Parameters.AddWithValue("@country", entity.Address.Country);
            command.Parameters.AddWithValue("@driving_license", entity.DrivingLicense);
            command.Parameters.AddWithValue("@birth_date", entity.BirthDate);

            clientId = (int)command.ExecuteScalar();
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error executing query: {command?.CommandText}. Error: {e.Message}", e.StackTrace);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return GetOneById(clientId);
    }
    
    public Client? Update(Client entity)
{
    try
    {
        if (GetOneById(entity.Id) is null)
        {
            throw new Exception("Client not found");
        }

        _connection.OpenConnection();

        using (var command = new NpgsqlCommand(
            "SELECT UpdateClient(@id, @last_name, @first_name, @email, @street, @postal_code, @city, @country, @driving_license, @birth_date)", 
            _connection._SqlConnection))
        {
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@last_name", entity.Lastname ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@first_name", entity.Firstname ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@email", entity.Email ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@street", entity.Address?.Street ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@postal_code", entity.Address?.PostalCode ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@city", entity.Address?.City ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@country", entity.Address?.Country ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@driving_license", entity.DrivingLicense ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@birth_date", entity.BirthDate);

            var result = (bool)command.ExecuteScalar();

            if (!result)
            {
                throw new Exception("Client update failed.");
            }

            return GetOneById(entity.Id);
        }
    }
    catch (Exception e)
    {
        throw new DBAccessException($"Error updating client: {e.Message}", e.StackTrace);
    }
    finally
    {
        _connection.CloseConnection();
    }
}

    public bool Delete(Client entity)
    {
        try
        {
            if (GetOneById(entity.Id) is null)
            {
                throw new Exception("Client not found");
            }

            _connection.OpenConnection();

            using (var command = new NpgsqlCommand(
                "SELECT DeleteClient(@id)", 
                _connection._SqlConnection))
            {
                command.Parameters.AddWithValue("@id", entity.Id);
                return (bool)command.ExecuteScalar();
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error deleting client: {e.Message}", e.StackTrace);
        }
        finally
        {
            _connection.CloseConnection();
        }
    }

}