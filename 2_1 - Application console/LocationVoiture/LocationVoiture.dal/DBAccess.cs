using System.Data;
using LocationVoiture.dal.CustomException;
using Npgsql;

namespace LocationVoiture.dal;

public class DBAccess
{
    public NpgsqlConnection _SqlConnection;
    private string _connectionString = "Server=Localhost;Port=5432;Database=postgres;UserID=postgres;Password=P@ssword;";

    public void OpenConnection()
    {
        try
        {
            this._SqlConnection = new NpgsqlConnection(_connectionString);
            this._SqlConnection.Open();
        }
        catch (Exception e)
        {
            throw new DBAccessException("DB connection impossible: ", e.Message);
        }
    }

    public void CloseConnection()
    {
        this._SqlConnection.Close();
        
    }
    
}