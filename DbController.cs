using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using RentCRUD;

namespace CRUD
{
    class DbController
    {
        public string LoginUser;
        public string DbName;
        public MySqlConnection conn;
        private string ConnectionString;

        public DbController(string server, string port, string userName, string password, string database)
        {
            LoginUser = userName;
            DbName = database;
            ConnectionString = 
                "server=" + server +
                ";Port=" + port +
                ";userid=" + userName +
                ";password=" + password +
                ";database=" + database;
            ConnectToDB();
        }

        ~DbController() => conn.Close();

        public void ConnectToDB()
        {
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
            }
        }

        public void Get(int id)
        {
            string query = "SELECT * FROM car WHERE Car_ID=" + id;

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 10;

            var dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Console.WriteLine(
                        "Brand: {0}\n" +
                        "Model: {1}\n" +
                        "Year: {2}\n" +
                        "Price: {3}\n", 
                        dataReader.GetString(1), 
                        dataReader.GetString(2), 
                        dataReader.GetString(3), 
                        dataReader.GetString(4));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            dataReader.Close();
        }

        public void Get()
        {
            string query = "SELECT * FROM car";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 10;

            var dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Console.WriteLine(
                        "Brand: {0}\n" +
                        "Model: {1}\n" +
                        "Year: {2}\n" +
                        "Price: {3}\n",
                        dataReader.GetString(1),
                        dataReader.GetString(2),
                        dataReader.GetString(3),
                        dataReader.GetString(4));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            dataReader.Close();
        }

        public void Create(Car car)
        {
            if (IsValid(car))
            {
                string query = String.Format("INSERT INTO car(Brand, Model, Year, Price)" +
                    " values('{0}', '{1}', {2}, {3})", car.Brand, car.Model, car.Year, car.Price);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.CommandTimeout = 10;
                cmd.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("Invalid Input");
                return;
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM car WHERE Car_ID=" + id;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.CommandTimeout = 10;
            cmd.ExecuteNonQuery();
        }

        public void Edit(Car car)
        {
            if (IsValid(car))
            {
                string query = String.Format("UPDATE car SET " +
                    "Brand='{0}', Model='{1}', Year={2}, Price={3}" +
                    " WHERE Brand='{0}'", 
                    car.Brand, car.Model, car.Year, car.Price);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.CommandTimeout = 10;
                cmd.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("Invalid Input");
                return;
            }
        }

        private bool IsValid(Car car)
        {
            if (String.IsNullOrEmpty(car.Brand) ||
                String.IsNullOrEmpty(car.Model) ||
                String.IsNullOrEmpty(car.Year.ToString()) ||
                String.IsNullOrEmpty(car.Price.ToString()))
            {
                return false;
            }
            return true;
        }
    }
}
