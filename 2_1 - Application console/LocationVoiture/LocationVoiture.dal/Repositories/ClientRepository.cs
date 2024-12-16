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

            command = new NpgsqlCommand(@"SELECT id, last_name, first_name, email, 
                                        (address).street AS street,
                                        (address).postal_code AS postal_code,
                                        (address).city AS city,
                                        (address).country AS country,
                                        driving_license, birth_date 
                                        FROM client ORDER BY last_name"
                , _connection._SqlConnection);

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
            throw new DBAccessException(command.CommandText, e.Message);
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

            command = new NpgsqlCommand(@"SELECT id, last_name, first_name, email, 
                                        (address).street AS street,
                                        (address).postal_code AS postal_code,
                                        (address).city AS city,
                                        (address).country AS country,
                                        driving_license, birth_date 
                                        FROM client
                                        WHERE id = @id"
                , _connection._SqlConnection);

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

            command = new NpgsqlCommand(@"SELECT id, last_name, first_name, email, 
                                        (address).street AS street,
                                        (address).postal_code AS postal_code,
                                        (address).city AS city,
                                        (address).country AS country,
                                        driving_license, birth_date 
                                        FROM client
                                        WHERE last_name = @name"
                , _connection._SqlConnection);

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

            command = new NpgsqlCommand(@"SELECT id, last_name, first_name, email, 
                                        (address).street AS street,
                                        (address).postal_code AS postal_code,
                                        (address).city AS city,
                                        (address).country AS country,
                                        driving_license, birth_date 
                                        FROM client
                                        WHERE email = @email"
                , _connection._SqlConnection);

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
        int insert = 0;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(
                @"INSERT INTO CLIENT(last_name, first_name, email, address, driving_license, birth_date)
                                                 VALUES(@last_name, @first_name, @email, ROW(@street, @postal_code, @city, @country), @driving_license, @birth_date)"
                , _connection._SqlConnection);

            command.Parameters.AddWithValue("@last_name", entity.Lastname);
            command.Parameters.AddWithValue("@first_name", entity.Firstname);
            command.Parameters.AddWithValue("@email", entity.Email);
            command.Parameters.AddWithValue("@street", entity.Address.Street);
            command.Parameters.AddWithValue("@postal_code", entity.Address.PostalCode);
            command.Parameters.AddWithValue("@city", entity.Address.City);
            command.Parameters.AddWithValue("@country", entity.Address.Country);
            command.Parameters.AddWithValue("@driving_license", entity.DrivingLicense);
            command.Parameters.AddWithValue("@birth_date", entity.BirthDate);

            insert = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new DBAccessException(command.CommandText, e.Message);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return insert == 1 ? GetOneByEmail(entity.Email) : null;
    }

    public Client? Update(Client entity)
    {
        NpgsqlCommand command = null;

        try
        {
            if (GetOneById(entity.Id) is not null)
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand(@"UPDATE CLIENT
                                                    SET last_name = @last_name, first_name = @first_name , 
                                                        email = @email, address = ROW(@street, @postal_code, @city, @country)
                                                      , driving_license = @driving_license, birth_date = @birth_date
                                                    WHERE id = @id"
                    , _connection._SqlConnection);

                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@last_name", entity.Lastname);
                command.Parameters.AddWithValue("@first_name", entity.Firstname);
                command.Parameters.AddWithValue("@email", entity.Email);
                command.Parameters.AddWithValue("@street", entity.Address.Street);
                command.Parameters.AddWithValue("@postal_code", entity.Address.PostalCode);
                command.Parameters.AddWithValue("@city", entity.Address.City);
                command.Parameters.AddWithValue("@country", entity.Address.Country);
                command.Parameters.AddWithValue("@driving_license", entity.DrivingLicense);
                command.Parameters.AddWithValue("@birth_date", entity.BirthDate);

                int insert = command.ExecuteNonQuery();

                return insert == 1 ? GetOneById(entity.Id) : null;
            }
            else
            {
                throw new Exception("Client not found");
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException(command.CommandText, e.Message);
        }
        finally
        {
            _connection.CloseConnection();
        }
    }

    public bool Delete(Client entity)
    {
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand(@"DELETE FROM CLIENT WHERE id = @id"
                                                 , _connection._SqlConnection);
            
            command.Parameters.AddWithValue("@id", entity.Id);
            
            if(GetOneById(entity.Id) is not null)
            {
                int insert = command.ExecuteNonQuery();

                return insert != 0;
            }
            else
                throw new Exception("Client not found");
        }
        catch (Exception e)
        {
            throw new DBAccessException(command.CommandText, e.Message);
        }
        finally
        {
            _connection.CloseConnection();
        }
    }
}