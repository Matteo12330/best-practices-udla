using System;

namespace DesignPatterns.Models
{
    // Clase concreta que representa un automóvil
    // Hereda de la clase abstracta Vehicle
    public class Car : Vehicle
    {
        public override int Tires => 4;

        public int Year { get; set; }
        public string Version { get; set; }
        public string Transmission { get; set; }

        public Car(string color, string brand, string model, int year) : base(color, brand, model)
        {
            Year = year;
            Id = Guid.NewGuid().ToString();
            Gallons = 0.0;
            Status = "Apagado";
        }
    }
}
