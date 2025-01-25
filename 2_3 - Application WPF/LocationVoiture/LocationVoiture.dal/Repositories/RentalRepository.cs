using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;
using Npgsql;

namespace LocationVoiture.dal.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DBAccess _connection;

        public RentalRepository(DBAccess connection)
        {
            _connection = connection;
        }

        public List<Rental> GetAll()
        {
            return this.GetAll(false);
        }
        
        public List<Rental> GetAll(bool withCompletedAndCancelled)
        {
            List<Rental> rentals = new List<Rental>();
            NpgsqlCommand command = null;

            try
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand("SELECT * FROM getallrentals(@withcompletedandcancelled)", _connection._SqlConnection);
                command.Parameters.AddWithValue("@withcompletedandcancelled", withCompletedAndCancelled);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rentals.Add(new Rental
                    {
                        Id = (int)reader["id"],
                        CarId = (int)reader["car_id"],
                        ClientId = (int)reader["client_id"],
                        StartDate = (DateTime)reader["start_date"],
                        Duration = (int)reader["duration"],
                        Amount = (decimal)reader["amount"],
                        Status = (RentalStatus)Enum.Parse(typeof(RentalStatus), reader["status"].ToString())
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

            return rentals;
        }

        public Rental? GetOneById(int id)
        {
            Rental? rental = null;
            NpgsqlCommand command = null;

            try
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand("SELECT * FROM getrentalbyid(@id)", _connection._SqlConnection);
                
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    rental = new Rental
                    {
                        Id = (int)reader["id"],
                        CarId = (int)reader["car_id"],
                        ClientId = (int)reader["client_id"],
                        StartDate = (DateTime)reader["start_date"],
                        Duration = (int)reader["duration"],
                        Amount = (decimal)reader["amount"],
                        Status = (RentalStatus)Enum.Parse(typeof(RentalStatus), reader["status"].ToString())
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

            return rental;
        }

        public Rental? Create(Rental entity)
        {
            int insert = 0;
            NpgsqlCommand command = null;

            try
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand("SELECT createrental(@car_id, @client_id, @start_date, @duration, @amount, @status)"
                    , _connection._SqlConnection);

                command.Parameters.AddWithValue("@car_id", entity.CarId);
                command.Parameters.AddWithValue("@client_id", entity.ClientId);
                command.Parameters.AddWithValue("@start_date", entity.StartDate);
                command.Parameters.AddWithValue("@duration", entity.Duration);
                command.Parameters.AddWithValue("@amount", entity.Amount);
                command.Parameters.AddWithValue("@status", entity.Status.ToString());

                var result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    throw new Exception("Failed to insert the rental.");

                insert = Convert.ToInt32(result);
            }
            catch (Exception e)
            {
                throw new DBAccessException(command.CommandText, e.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }

            return insert != 0 ? GetOneById(insert) : null;
        }

        public Rental? Update(Rental entity)
        {
            NpgsqlCommand command = null;

            try
            {
                if (GetOneById(entity.Id) is not null)
                {
                    _connection.OpenConnection();

                    command = new NpgsqlCommand("SELECT updaterental(@id, @car_id, @client_id, @start_date, @duration, @amount, @status)"
                        , _connection._SqlConnection);
                    
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@car_id", entity.CarId);
                    command.Parameters.AddWithValue("@client_id", entity.ClientId);
                    command.Parameters.AddWithValue("@start_date", entity.StartDate);
                    command.Parameters.AddWithValue("@duration", entity.Duration);
                    command.Parameters.AddWithValue("@amount", entity.Amount);
                    command.Parameters.AddWithValue("@status", entity.Status.ToString());

                    int insert = command.ExecuteNonQuery();

                    return GetOneById(entity.Id) ;
                }
                else
                    throw new Exception("Rental not found");
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

        public bool Delete(Rental entity)
        {
            NpgsqlCommand command = null;

            try
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand("SELECT deleterental(@id)", _connection._SqlConnection);

                command.Parameters.AddWithValue("@id", entity.Id);

                if (GetOneById(entity.Id) is not null)
                {
                    int insert = command.ExecuteNonQuery();

                    return insert != 0;
                }
                else
                    throw new Exception("Rental not found");
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
}
