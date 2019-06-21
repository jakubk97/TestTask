using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestTask
{
    class Model
    {
        private static MySqlConnection connection = DBConnection.Instance.Connection;
        static Queue qt = new Queue();
        #region Methods
        //Wcztanie elementów z bazy danych
        public Cars[] GetTests()
        {
            try
            {
                List<Cars> cars = new List<Cars>();
                string manufacturer, model, capacity;
                using (MySqlCommand command = new MySqlCommand(@"SELECT * FROM CARS ORDER BY MANUFACTURER", connection))
                {
                    connection.Open();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        manufacturer = dataReader["manufacturer"].ToString();
                        model = dataReader["model"].ToString();
                        capacity = dataReader["capacity"].ToString();
                        cars.Add(new Cars(manufacturer, model, capacity));
                    }
                }
                return cars.ToArray();
            }
            catch (Exception)
            {
                Console.WriteLine("\nBłąd podczas dodawania rekordu do bazy\nSprawdź połączenie z bazą\n");
                return new List<Cars>().ToArray(); ;
            }
            finally
            {
                connection.Close();
            }
        }
        //Sprawdzenie poprawności danych oraz dodanie elementu do kolejki
        public string AddToBuffer()
        {
            try
            {
                string manufacturer, model, capacity;
                Regex nameRegex = new Regex(@"^([A-Z])([a-z]+\b)$");
                Console.Write("Podaj producenta: ");
                manufacturer = Console.ReadLine();
                if (!nameRegex.IsMatch(manufacturer))
                    return "Podano złego producenta";

                nameRegex = new Regex(@"^[a-zA-Z0-9\s]+$");
                Console.Write("Podaj model: ");
                model = Console.ReadLine();
                if (!nameRegex.IsMatch(model) || model.Replace(" ", "").Length == 0)
                    return "Podano zły model";

                nameRegex = new Regex(@"(?<!\S)\d+(?![^\s.,?!])\.\d+(?![^\s.,?!])$");
                Console.Write("Podaj pojemność: ");
                capacity = Console.ReadLine();
                if (!nameRegex.IsMatch(capacity))
                    return "Podano złą pojemność";

                qt.Enqueue(new Cars(manufacturer, model, capacity));
                return "Pomyślnie dodano";
            }
            catch (Exception)
            {
                return "\nBłąd bufora\n";
            }
        }
        //Dodanie elementu podanego w parametrze do bazy
        private static void AddCar(Cars c)
        {
            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "INSERT INTO CARS(manufacturer,model,capacity) VALUES(@manufacturer, @model, @capacity)";
                    command.Parameters.AddWithValue("@manufacturer", c.Manufacturer);
                    command.Parameters.AddWithValue("@model", c.Model);
                    command.Parameters.AddWithValue("@capacity", c.Capacity);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nBłąd podczas dodawania rekordu do bazy\nSprawdź połączenie z bazą\n");
            }
        }
        //Wywoływane przez timer działanie - zbiera elementy z kolejki następnie wywołuje dodanie ich do bazy
        public void TimerCallback()
        {
            foreach (Object obj in qt)
            {
                AddCar(obj as Cars);
            }
            qt.Clear();
            GC.Collect();
        }
        #endregion
    }
}
