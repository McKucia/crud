using CRUD;
using System;

namespace RentCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            DbController Cars = new DbController("localhost", "3306", "root", "AlfaRomeo147!", "carsalon");

            Car newCar = new Car
            {
                Brand = "Mazda",
                Model = "mx-4",
                Year = 2001,
                Price = 140
            };
            //Cars.Create(newCar);
            //Cars.Delete(24);
            //Cars.Edit(newCar);
            Cars.Get();


        }
    }
}
