using DesignPatterns.ModelBuilder;
using DesignPatterns.Models;

namespace DesignPatterns.Factories
{
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
