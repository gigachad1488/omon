using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using omon.Models;

namespace omon;

public static class Db
{
    private static MySqlConnection connection;
    
    static Db()
    {
        connection = new MySqlConnection("server=10.10.1.24;uid=user_01;pwd=user01pro;database=pro1_6");
    }

    #region Storage

    public static List<Storage> GetAllStorages()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        List<Storage> storages = new List<Storage>();
        MySqlCommand command = new MySqlCommand("select * from Storage", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Storage storage = new Storage();
            storage.id = reader.GetInt32(0);
            storage.name = reader.GetString(1);
            storage.address = reader.GetString(2);
            storages.Add(storage);
        }
        connection.Close();

        return storages;
    }

    public static void AddStorage(Storage storage)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("insert into Storage (Name, Address) value (@n, @ad)", connection);
        command.Parameters.AddWithValue("@n", storage.name);
        command.Parameters.AddWithValue("@ad", storage.address);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void DeleteStorage(Storage storage)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Storage WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", storage.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void ChangeStorage(Storage storage)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("update Storage set Name = @n, Address = @ad where id = @id", connection);
        command.Parameters.AddWithValue("@id", storage.id);
        command.Parameters.AddWithValue("@n", storage.name);
        command.Parameters.AddWithValue("@ad", storage.address);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion
}