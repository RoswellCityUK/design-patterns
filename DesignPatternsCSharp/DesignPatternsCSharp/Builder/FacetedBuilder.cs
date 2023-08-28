namespace DesignPatternsCSharp.Builder.FacatedBuilder
{
    /// <summary>
    /// The Faceted Builder Design Pattern allows the step-by-step construction of complex objects
    /// through a fluent and intuitive API. It employs a hierarchical builder structure to add specific
    /// attributes to the object. This pattern is especially beneficial for building objects with numerous
    /// optional parameters, ensuring clear and readable code.
    ///
    /// In this implementation, the Car class represents the object being built. A series of nested builder
    /// classes (CarInfoBuilder, CarPaintBuilder, and CarTypeBuilder) provide methods to set various attributes
    /// of the car. This pattern accommodates the addition of new attributes without modifying existing code.
    ///
    /// Best Practices:
    /// - Employ this pattern for constructing objects with numerous optional parameters.
    /// - Enhance builders using inheritance to incrementally extend functionality.
    /// - Adhere to a clear naming convention to reflect the attributes being set in each builder layer.
    /// - Provide an intuitive API for setting attributes in any order.
    /// - Ensure that the final build step returns the fully constructed object.
    ///
    /// Use Cases:
    /// - Constructing objects with many optional parameters, particularly those with hierarchical structures.
    /// - Creating objects where the sequence of attribute setting may vary.
    /// - Improving code readability and maintainability by structuring the object creation process.
    /// - Beneficial for grouping specific properties of an object.
    /// 
    /// Notes:
    /// - The Faceted Builder pattern is a variation of the Fluent Builder pattern.
    /// - While the Faceted Builder is useful for grouping object properties and builder functionalities.
    /// 
    /// Read more about Faceted Builder Design Pattern here:
    /// <see href="https://edup.code.blog/2020/03/06/faceted-builder-pattern/">edup.code.blog</see>
    /// <see href="https://medium.com/@mohithmarisetti_58912/a-mysterious-yet-powerful-variation-of-a-well-known-design-pattern-9a33a7625681">by mohithmarisetti_58912@medium.com</see>
    /// </summary>


    /// <summary>
    /// The Car class represents a complex object that can be constructed using a faceted builder pattern.
    /// It encapsulates attributes like make, model, year of production, paint color, paint type, and car type.
    /// </summary>
    public class Car
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int YearOfProduction { get; set; }
        public PaintColour PaintColour { get; set; }
        public PaintType PaintType { get; set; }
        public CarType Type { get; set; }

        /// <summary>
        /// Generates a string representation of the Car object's attributes.
        /// </summary>
        /// <returns>A string representing the Car object.</returns>
        public override string ToString()
        {
            return $"Make: {Make}, Model: {Model}, Year: {YearOfProduction}, Colour: {PaintColour}, Paint Type: {PaintType}, Type: {Type}";
        }
    }

    // Enums representing various properties of a car.
    public enum CarType { Sedan, Hatchback, SUV, Coupe }
    public enum PaintColour { Black, White, Red, Blue, Green, Yellow, Orange, Purple, Pink, Brown, Grey }
    public enum PaintType { Metallic, Matte, Gloss }

    /// <summary>
    /// The CarBuilderFacade class provides a simplified and fluent API for constructing a Car object.
    /// It utilizes a set of nested builders to gradually set the attributes of the Car.
    /// </summary>
    public class CarBuilderFacade
    {
        protected Car _car = new Car();

        public CarInfoBuilder Info => new CarInfoBuilder(_car);
        public CarPaintBuilder Paint => new CarPaintBuilder(_car);
        public CarTypeBuilder Type => new CarTypeBuilder(_car);

        // public Car Build() => _car;  // This is optional, as the implicit operator will allows cast to Car object.

        // This is an implicit operator that allows the CarBuilderFacade to be cast to a Car.
        public static implicit operator Car(CarBuilderFacade cb)
        {
            return cb._car;
        }
    }

    /// <summary>
    /// The CarInfoBuilder class extends the CarBuilderFacade so it allows chaining all the facated builders.
    /// It provides methods for setting the make, model, and year of production attributes.
    /// </summary>
    public class CarInfoBuilder : CarBuilderFacade
    {
        protected Car car;

        public CarInfoBuilder(Car car)
        {
            this.car = car;
        }

        #region CarInforBuilder methods for setting the properties
        public CarInfoBuilder ProducedBy(string make)
        {
            car.Make = make;
            return this;
        }

        public CarInfoBuilder OfModel(string model)
        {
            car.Model = model;
            return this;
        }

        public CarInfoBuilder ProducedIn(int year)
        {
            car.YearOfProduction = year;
            return this;
        }
        #endregion
    }

    /// <summary>
    /// The CarPaintBuilder class extends the CarBuilderFacade so it allows chaining all the facated builders.
    /// It provides a method for specifying the paint color and type.
    /// </summary>
    public class CarPaintBuilder : CarBuilderFacade
    {
        protected Car car;

        public CarPaintBuilder(Car car)
        {
            this.car = car;
        }
        #region CarPainBuilder methods for setting the properties
        public CarPaintBuilder Painted(PaintColour colour, PaintType type)
        {
            car.PaintColour = colour;
            car.PaintType = type;
            return this;
        }
        #endregion
    }

    /// <summary>
    /// The CarTypeBuilder class extends the CarBuilderFacade so it allows chaining all the facated builders.
    /// It provides a method for specifying the car's type, such as Sedan or Hatchback.
    /// </summary>
    public class CarTypeBuilder : CarBuilderFacade
    {
        protected Car car;

        public CarTypeBuilder(Car car)
        {
            this.car = car;
        }

        #region CarTypeBuilder methods for setting the properties
        public CarTypeBuilder OfType(CarType type)
        {
            car.Type = type;
            return this;
        }
        #endregion
    }

    /// <summary>
    /// Example demonstrates the use of the faceted builder pattern to construct a Car object.
    /// It creates a CarBuilderFacade instance and configures the car's attributes using the fluent API.
    /// </summary>
    public class Example
    {
        public Example()
        {
            // Construct a Car object using the faceted builder pattern.
            Car car = new CarBuilderFacade()
                .Info
                    .ProducedBy("Ford")
                    .OfModel("Mustang")
                    .ProducedIn(1989)
                .Paint
                    .Painted(PaintColour.Black, PaintType.Metallic)
                .Type
                    .OfType(CarType.Sedan);
                //.Build();     // We don't have to use Build() method as the implicit operator allows us to cast Builder into Car object.
                // We can remove implicit operator and uncomment Build() method if we want to force the user to call the Build() method explicitly.
        }
    }
}
