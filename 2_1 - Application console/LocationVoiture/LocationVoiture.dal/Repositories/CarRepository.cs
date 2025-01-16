using LocationVoiture.dal.Entities;
using Npgsql;
using System;
using LocationVoiture.dal.CustomException;

namespace LocationVoiture.dal.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DBAccess _connection;

    public CarRepository()
    {
        _connection = new DBAccess(); 
    }

    public List<Car> GetAll(bool onlyAvailable)
    {
        List<Car> cars = new List<Car>();
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand("SELECT * FROM getallcars(@onlyavailable)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@onlyavailable", onlyAvailable);

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Car c = new Car
                {
                    Id = (int)reader["id"],
                    LicensePlate = (string)reader["license_plate"],
                    Color = (string)reader["color"],
                    ModelName = (string)reader["model_name"],
                    ParkingCode = reader["parking_code"] as string ?? string.Empty,
                };
                cars.Add(c);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error executing query: {command?.CommandText}. Error: {e.Message}", e.StackTrace);
        }
        finally
        {
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return cars;
    }

    public List<Car> GetAll()
    {
        return this.GetAll(false);
    }

    public Car? GetOneById(int id)
    {
        Car? car = null;
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            command = new NpgsqlCommand("SELECT * FROM getcarbyid(@id)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                car = new Car
                {
                    Id = (int)reader["id"],
                    LicensePlate = (string)reader["license_plate"],
                    Color = (string)reader["color"],
                    ModelName = (string)reader["model_name"],
                    ParkingCode = reader["parking_code"] == DBNull.Value ? null : (string)reader["parking_code"]
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
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return car;
    }

    public Car? Create(Car car)
    {
        int insert = 0;
        NpgsqlCommand command = null;
        
        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT createcar(@License, @Color, @ParkingCode, @ModelName)", _connection._SqlConnection);

            command.Parameters.AddWithValue("@License", car.LicensePlate);
            command.Parameters.AddWithValue("@Color", car.Color);
            command.Parameters.AddWithValue("@ParkingCode", car.ParkingId);
            command.Parameters.AddWithValue("@ModelName", car.ModelId);
            
            var result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                throw new Exception("Failed to insert the category.");
            
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

        return insert != 0 ? this.GetOneById(insert) : null;
    }

    public Car Update(Car entity)
    {
        NpgsqlCommand command = null;
        
        try
        {
            if (this.GetOneById(entity.Id) is not null)
            {
                _connection.OpenConnection();
                
                command = new NpgsqlCommand("SELECT * from updateparking(@id)", _connection._SqlConnection);
                
                command.Parameters.AddWithValue("@id", entity.Id);
                
                int rowsAffected = command.ExecuteNonQuery();
                
                return rowsAffected != 0 ? this.GetOneById(entity.Id) : null;
            }
            else
            {
                throw new Exception("Car not found.");
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
    public bool Delete(Car entity)
    {
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand(@"SELECT deletecar(@id)", _connection._SqlConnection);
            
            command.Parameters.AddWithValue("@id", entity.Id);
            
            if(GetOneById(entity.Id) is not null)
            {
                int insert = command.ExecuteNonQuery();

                return insert != 0;
            }
            else
                throw new Exception("Car not found");
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error executing query: {command?.CommandText}. Error: {e.Message}", e.StackTrace);

        }
        finally
        {
            _connection.CloseConnection();
        }
        
    }

    public Decimal GetAmount(int id)
    {
        decimal dailyRate = 0 ;
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * from getcaramount(@id)", _connection._SqlConnection);
            
            command.Parameters.AddWithValue("@id", id);

            var result = command.ExecuteScalar();
            
            if (result != null && result != DBNull.Value)
                dailyRate = Convert.ToDecimal(result);
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error executing query: {command?.CommandText}. Error: {e.Message}", e.StackTrace);
        }
        finally
        {
            _connection.CloseConnection();
        }

        return dailyRate;
    }

    #region Parking 
    
    public Parking GetParkingCode(int parking_code)
    {
        Parking? parking = null;
        NpgsqlCommand command = null;
        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM getparkingcode(@parkingCode)", _connection._SqlConnection);


            command.Parameters.AddWithValue("@parkingcode", parking_code);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                parking = new Parking
                {
                    Id = (int)reader["id"],
                    Code = (string)reader["code"],
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

        return parking;
    }
    
    public List<Parking> GetAllParkingCode(bool onlyAvailable)
    {
        List<Parking> parkings = new List<Parking>();
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("SELECT * FROM getallparkingcodes(@onlyAvailable)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@onlyAvailable", onlyAvailable);

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Parking p = new Parking
                {
                    Id = (int)reader["id"],
                    Code = (string)reader["code"],
                };
                parkings.Add(p);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            throw new DBAccessException($"Error executing query: {command?.CommandText}. Error: {e.Message}", e.StackTrace);

        }
        finally
        {
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return parkings;
    }
    
    public bool GetFreeParkingPlace(int carId)
    {
        NpgsqlCommand command = null;
        
        try
        {
            _connection.OpenConnection();
            
            // prendre le premier id de parking trouvé qui est libre
            command = new NpgsqlCommand("SELECT assignfreeparkingplace(@carId)", _connection._SqlConnection);
            command.Parameters.AddWithValue("@carId", carId);

            var dbResult = command.ExecuteScalar();
            return dbResult != null && (bool)dbResult;
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
    
    #endregion
}