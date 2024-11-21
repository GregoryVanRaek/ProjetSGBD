using LocationVoiture.dal.Entities;
using Npgsql;

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
                  FROM car c 
                  LEFT JOIN parking p ON c.parking_id = p.id
                  LEFT JOIN model m ON c.model_id = m.id;",
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
            throw new Exception("An error occurred while getting all cars: " + e.Message);
        }
        finally
        {
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return cars;
    }

    public Car? GetOneById(int key)
    {
        throw new NotImplementedException();
    }

    public Car Create(Car entity)
    {
        throw new NotImplementedException();
    }

    public Car Update(Car entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Car entity)
    {
        throw new NotImplementedException();
    }
}