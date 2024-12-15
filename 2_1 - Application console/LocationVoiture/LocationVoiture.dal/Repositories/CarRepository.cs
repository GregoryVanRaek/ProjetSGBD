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

    public List<Car> GetAll()
    {
        List<Car> cars = new List<Car>();
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            command = new NpgsqlCommand(
                @"SELECT c.id, c.license_plate, c.color, p.code AS parking_code, m.name AS model_name
                  FROM CAR c 
                  LEFT JOIN PARKING p ON c.parking_id = p.id
                  LEFT JOIN MODEL m ON c.model_id = m.id;",
                _connection._SqlConnection
            );

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Car c = new Car
                {
                    Id = (int)reader["id"],
                    LicensePlate = (string)reader["license_plate"],
                    Color = (string)reader["color"],
                    ModelName = (string)reader["model_name"],
                    ParkingCode = (string)reader["parking_code"]
                };
                cars.Add(c);
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException("An error occurred while getting all cars: ", e.Message);
        }
        finally
        {
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return cars;
    }

    public Car? GetOneById(int id)
    {
        Car? car = null;
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            command = new NpgsqlCommand(
                @"SELECT c.id, c.license_plate, c.color, p.code AS parking_code, m.name AS model_name
                  FROM CAR c 
                  LEFT JOIN PARKING p ON c.parking_id = p.id
                  LEFT JOIN MODEL m ON c.model_id = m.id
                  WHERE c.id = @id;",
                _connection._SqlConnection
            );
            
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
                    ParkingCode = (string)reader["parking_code"]
                };
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException("An error occurred while getting the car: ", e.Message);
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
        NpgsqlCommand command;
        
        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand("INSERT INTO car (license_plate, color, parking_id, model_id) "
                                        + "VALUES (@License, @Color, @ParkingCode, @ModelName) RETURNING id"
                                        , _connection._SqlConnection);

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
            throw new DBAccessException("Error while inserting category", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }

        return insert != 0 ? this.GetOneById(insert) : null;
    }

    public Car Update(Car entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Car entity)
    {
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand(@"DELETE FROM CAR WHERE id = @id"
                , _connection._SqlConnection);
            
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
            throw new DBAccessException("Error while deleting the car", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }
        
    }

    #region Parking 
    
    public Parking GetParkingCode(int parking_code)
    {
        Parking? parking = null;
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(@"SELECT * FROM parking WHERE id = @parkingcode"
                , _connection._SqlConnection);

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
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while trying to get parking place", e.ToString());
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

            if (onlyAvailable)
            {
                command = new NpgsqlCommand(
                    "SELECT * FROM parking " +
                            "WHERE id NOT IN (SELECT parking_id FROM car WHERE car.parking_id IS NOT NULL)",
                            _connection._SqlConnection);
            }
            else
                command = new NpgsqlCommand("SELECT * FROM parking",_connection._SqlConnection);

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
        }
        catch (Exception e)
        {
            throw new DBAccessException("An error occurred while getting all cars: ", e.Message);
        }
        finally
        {
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return parkings;
    }
    
    #endregion
}