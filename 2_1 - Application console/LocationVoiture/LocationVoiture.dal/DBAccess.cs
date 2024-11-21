using System.Data;
using Npgsql;

namespace LocationVoiture.dal;

public class DBAccess
{
    public NpgsqlConnection _SqlConnection;
    private string _connectionString = "Server=Localhost;Port=5432;Database=test;UserID=postgres;Password=P@ssword;";

    public void OpenConnection()
    {
        try
        {
            if (this._SqlConnection == null || this._SqlConnection.State == ConnectionState.Closed)
            {
                this._SqlConnection = new NpgsqlConnection(_connectionString);
                this._SqlConnection.Open();
            }
        }
        catch (Exception e)
        {
            throw new Exception("DB connection impossible: " + e.Message);
        }
    }

    public void CloseConnection()
    {
        this._SqlConnection.Close();
    }
    
}