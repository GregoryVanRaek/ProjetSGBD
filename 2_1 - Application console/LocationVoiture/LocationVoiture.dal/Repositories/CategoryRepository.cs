﻿using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;
using Npgsql;

namespace LocationVoiture.dal.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DBAccess _connection;
    
    public CategoryRepository(DBAccess connection)
    {
        _connection = connection; 
    }
    
    public List<Category> GetAll()
    {
        List<Category> categories = new List<Category>();
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(@"SELECT id, name, daily_rate
                                        FROM category ORDER BY id", _connection._SqlConnection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    DailyRate = (decimal)reader["daily_rate"]
                });
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while retrieving category", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }

        return categories;
    }

    public Category? GetOneById(int id)
    {
        Category? category = null;
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(@"SELECT id, name, daily_rate FROM CATEGORY WHERE id = @id"
                , _connection._SqlConnection);

            command.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                category = new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    DailyRate = (decimal)reader["daily_rate"]
                };
            }
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while trying to get category", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }

        return category;
    }

    public Category? Create(Category entity)
    {
        int insert = 0;
        NpgsqlCommand command;
        
        try
        {
            _connection.OpenConnection();

            command = new NpgsqlCommand(
                @"INSERT INTO CATEGORY(name, daily_rate) VALUES(@name, @daily_rate) RETURNING id"
                , _connection._SqlConnection);

            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@daily_rate", entity.DailyRate);
            
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

        return insert != 0 ? GetOneById(insert) : null;
    }

    public Category? Update(Category entity)
    {
        NpgsqlCommand command;

        try
        {
            if (GetOneById(entity.Id) is not null)
            {
                _connection.OpenConnection();

                command = new NpgsqlCommand(@"UPDATE CATEGORY SET name = @name, daily_rate = @daily_rate WHERE id = @id"
                    , _connection._SqlConnection);

                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@name", entity.Name);
                command.Parameters.AddWithValue("@daily_rate", entity.DailyRate);

                
                int insert = command.ExecuteNonQuery();

                return insert == 1 ? GetOneById(entity.Id) : null;
            }
            else
                throw new Exception("category not found");
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while updating category", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }
    }

    public bool Delete(Category entity)
    {
        NpgsqlCommand command;

        try
        {
            _connection.OpenConnection();
            
            command = new NpgsqlCommand(@"DELETE FROM CATEGORY WHERE id = @id"
                , _connection._SqlConnection);
            
            command.Parameters.AddWithValue("@id", entity.Id);
            
            if(GetOneById(entity.Id) is not null)
            {
                int insert = command.ExecuteNonQuery();

                return insert != 0;
            }
            else
                throw new Exception("Category not found");
        }
        catch (Exception e)
        {
            throw new DBAccessException("Error while deleting category", e.ToString());
        }
        finally
        {
            _connection.CloseConnection();
        }
    }
}