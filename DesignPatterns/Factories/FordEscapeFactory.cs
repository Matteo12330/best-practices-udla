using DesignPatterns.ModelBuilder;
using DesignPatterns.Models;

namespace DesignPatterns.Factories
{
    // Clase concreta que representa la fábrica del modelo Escape
    // Aplica el patrón Factory Method
    public class FordEscapeFactory : CarFactory
    {
        public override Vehicle Create()
        {
            return new CarModelBuilder()
                .setBrand("Ford")
                .setModel("Escape")
                .setColor("Red")
                .Build();
        }
    }
}
