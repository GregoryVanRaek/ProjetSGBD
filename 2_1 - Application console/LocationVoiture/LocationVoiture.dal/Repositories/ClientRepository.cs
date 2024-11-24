using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
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
        NpgsqlCommand command;
        
        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand(@"SELECT id, last_name, first_name, email, 
                                        (address).street AS street,
                                        (address).postal_code AS postal_code,
                                        (address).city AS city,
                                        (address).country AS country,
                                        driving_license, birth_date 
                                        FROM client", _connection._SqlConnection);

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
            _connection.CloseConnection();
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while retrieving clients", e.ToString());
        }

        return clients;
    }

    public Client? GetOneById(int givenId)
    {
        Client client = null;
        NpgsqlCommand command;
        
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
            _connection.CloseConnection();
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while retrieving clients", e.ToString());
        }

        return client;
    }
    
    public Client? GetOneByName(string givenName)
    {
        Client? client = null;
        NpgsqlCommand command;
        
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
            _connection.CloseConnection();
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while retrieving clients", e.ToString());
        }

        return client;
    }

    public Client Create(Client entity)
    {
        throw new NotImplementedException();
    }

    public Client Update(Client entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Client entity)
    {
        throw new NotImplementedException();
    }


}