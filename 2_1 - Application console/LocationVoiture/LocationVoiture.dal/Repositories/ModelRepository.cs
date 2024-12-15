using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;
using Npgsql;
using Exception = System.Exception;

namespace LocationVoiture.dal.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly DBAccess _connection;

    public ModelRepository()
    {
        _connection = new DBAccess();
    }
    
    public List<Model> GetAll()
    {
        List<Model> models = new List<Model>();
        NpgsqlCommand command = null;

        try
        {
            _connection.OpenConnection();
            command = new NpgsqlCommand(
                @"SELECT m.ID, m.NAME, m.BRAND, m.SEAT_NUMBER, CATEGORY.ID
                          FROM MODEL m
                          LEFT JOIN CATEGORY
                          ON m.CATEGORYID = CATEGORY.ID",
                _connection._SqlConnection
            );

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Model m = new Model
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Brand = (string)reader["seat_number"],
                    SeatNumber = (int)reader["seat_number"],
                    CategoryId = (int)reader["category_id"]
                };
                models.Add(m);
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException("An error occurred while getting all models: ", e.Message);
        }
        finally
        {
            command?.Dispose();
            _connection?.CloseConnection();
        }

        return models;
    }

    public Model? GetOneById(int id)
    {
        Model? model = null;
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(@"SELECT m.ID, m.NAME, m.BRAND, m.SEAT_NUMBER, CATEGORY.ID
                          FROM MODEL m
                          LEFT JOIN CATEGORY
                          ON m.CATEGORYID = CATEGORY.ID
                          WHERE m.ID = @id"
                , _connection._SqlConnection);

            command.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                model = new Model
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Brand = (string)reader["seat_number"],
                    SeatNumber = (int)reader["seat_number"],
                    CategoryId = (int)reader["category_id"]
                };
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while trying to get model", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }

        return model;
    }

    public Model? Create(Model entity)
    {
        int insert = 0;
        NpgsqlCommand command;
        
        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(
                @"INSERT INTO MODEL(NAME, BRAND, SEAT_NUMBER, CATEGORY_ID) VALUES(@name, @brand, @seatNumber, @categoryId) RETURNING id"
                , _connection._SqlConnection);

            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@brand", entity.Brand);
            command.Parameters.AddWithValue("@seatNumber", entity.SeatNumber);
            command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
            
            var result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                throw new Exception("Failed to insert the model.");
            
            insert = Convert.ToInt32(result);
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while inserting model", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }

        return insert != 0 ? GetOneById(insert) : null;
    }

    public Model? Update(Model entity)
    {
        NpgsqlCommand command;

        try
        {
            if (GetOneById(entity.Id) is not null)
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand(@"UPDATE MODEL SET name = @name, BRAND = @brand, SEAT_NUMBER = @seatNumber, CATEGORY_ID = @categoryId 
             WHERE id = @id"
                    , _connection._SqlConnection);

                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@name", entity.Name);
                command.Parameters.AddWithValue("@brand", entity.Brand);
                command.Parameters.AddWithValue("@seatNumber", entity.SeatNumber);
                command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                
                int insert = command.ExecuteNonQuery();

                return insert == 1 ? GetOneById(entity.Id) : null;
            }
            else
                throw new Exception("model not found");
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while updating model", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }
    }

    public bool Delete(Model entity)
    {
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand(@"DELETE FROM MODEL WHERE id = @id"
                , _connection._SqlConnection);
            
            command.Parameters.AddWithValue("@id", entity.Id);
            
            if(GetOneById(entity.Id) is not null)
            {
                int insert = command.ExecuteNonQuery();

                return insert != 0;
            }
            else
                throw new Exception("Model not found");
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while deleting model", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }
    }
}