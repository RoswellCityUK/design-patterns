namespace DesignPatternsCSharp.Builder.FluentBuilderWithInheritance
{
    /// <summary>
    /// The Fluent Builder with Inheritance design pattern is used to construct complex objects
    /// step by step using a fluent and intuitive API. It utilizes inheritance to create a hierarchy
    /// of builders that add specific attributes to the object being built. This pattern allows for
    /// clear and readable code when constructing objects with multiple optional parameters.
    ///
    /// In this implementation, the Car class represents the object being built, and a series of nested
    /// builder classes (CarInfoBuilder, CarPaintBuilder, and CarTypeBuilder) provide methods to set
    /// different attributes of the car. The pattern allows flexibility in adding new attributes to the
    /// object-building process without altering existing code.
    ///
    /// Good Practices:
    /// - Use this pattern when you need to construct complex objects with many optional parameters.
    /// - Enhance the builders using inheritance to extend functionality step by step.
    /// - Follow a clear naming convention to reflect the attributes being set by each builder layer.
    /// - Provide an intuitive API that allows setting attributes in any order.
    /// - Ensure that the final build step returns the fully constructed object.
    ///
    /// Use Cases:
    /// - Building objects with many optional parameters, especially when they have a hierarchical structure.
    /// - Creating objects where the order of attribute setting might vary.
    /// - Promoting code readability and maintainability by organizing the object creation process.
    /// - When project requires to implement inheritance between builders.
    /// - Good for grouping specific properties of an object.
    /// - Using Builder Design Pattern can eliminate need for multiple and complex constructors.
    /// 
    /// Notes:
    /// - The Fluent Builder with Inheritance pattern is a variation of the Fluent Builder pattern.
    /// - The Fluent Builder with Inheritance pattern can be good for grouping specific properties of an object and builder functionalities,
    ///     but better for this is Faceted Builder pattern.
    ///     
    /// Read more about Fluent Builder with Inheritance Recursive Generics design pattern here:
    /// <see href="https://code-maze.com/fluent-builder-recursive-generics/">code-maze.com</see>
    /// </summary>


    /// <summary>
    /// The Car class represents a car object that can be built using a fluent builder pattern.
    /// This class exposes a Builder class which facilitates the creation of a car object in a single line.
    /// </summary>
    public class Car
    {
        // Properties of the car
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int YearOfProduction { get; set; }
        public PaintColour PaintColour { get; set; }
        public PaintType PaintType { get; set; }
        public CarType Type { get; set; }

        // Nested Builder class for constructing car instances using a fluent interface.
        public class Builder : CarTypeBuilder<Builder>
        {
            internal Builder() { }
        }

        // A static property to create a new instance of the `Builder` class.
        public static Builder New => new Builder();
    }

    // Enums representing various properties of a car.
    public enum CarType { Sedan, Hatchback, SUV, Coupe }
    public enum PaintColour { Black, White, Red, Blue, Green, Yellow, Orange, Purple, Pink, Brown, Grey }
    public enum PaintType { Metallic, Matte, Gloss }

    /// <summary>
    /// The FluentCarBuilderBase class is the base class for the builder pattern.
    /// It provides a base API for building a car object.
    /// </summary>
    public class FluentCarBuilderBase
    {
        /// <summary>
        /// Stores the car instance being built.
        /// Set as protected to allow access from derived classes.
        /// </summary>
        protected Car _car = new Car();

        /// <summary>
        /// Builds and returns the constructed car instance.
        /// </summary>
        /// <returns>The built car instance.</returns>
        public Car Build() => _car;
    }

    /// <summary>
    /// The CarInfoBuilder class inherits from the FluentCarBuilderBase and extends it.
    /// It provides methods for setting general information about the car, such as make, model, and production year.
    /// This step aims to capture basic car information.
    /// </summary>
    /// <typeparam name="SELF">This generic type will allows us to extend this builder with another class that will introduce additional functionalities.</typeparam>
    public class CarInfoBuilder<SELF>
        : FluentCarBuilderBase
        where SELF : CarInfoBuilder<SELF>
    {
        #region CarInfoBuilde methods for setting properties
        public SELF ProducedBy(string make)
        {
            _car.Make = make;
            return (SELF) this;
        }

        public SELF OfModel(string model)
        {
            _car.Model = model;
            return (SELF) this;
        }

        public SELF ProducedIn(int year)
        {
            _car.YearOfProduction = year;
            return (SELF) this;
        }
        #endregion
    }

    /// <summary>
    /// The CarPaintBuilder class further extends the previous builder layer (CarInfoBuilder).
    /// It provides methods for setting the paint color and type of the car.
    /// This step adds information about the car's paint.
    /// </summary>
    /// <typeparam name="SELF">This generic type will allows us to extend this builder with another class that will introduce additional functionalities.</typeparam>
    public class CarPaintBuilder<SELF>
        : CarInfoBuilder<CarPaintBuilder<SELF>>
        where SELF : CarPaintBuilder<SELF>
    {
        #region CarPainBuilder methods for setting properties
        public SELF Painted (PaintColour colour, PaintType type)
        {
            _car.PaintColour = colour;
            _car.PaintType = type;
            return (SELF) this;
        }
        #endregion
    }

    /// <summary>
    /// The CarTypeBuilder class extends the CarPaintBuilder.
    /// It provides a method for setting the type of the car (e.g., Sedan, Hatchback).
    /// This step finalizes the car's configuration by setting its type.
    /// </summary>
    /// <typeparam name="SELF">This generic type allows us to extend this builder with another class that introduces additional functionalities.</typeparam>
    public class CarTypeBuilder<SELF>
        : CarPaintBuilder<CarTypeBuilder<SELF>>
        where SELF : CarTypeBuilder<SELF>
    {
        #region CarTypeBuilder methods for setting properties
        public SELF OfType(CarType type)
        {
            _car.Type = type;
            return (SELF) this;
        }
        #endregion
    }

    /// <summary>
    /// Example usage of the Fluent Builder pattern.
    /// Demonstrates how to create a Car object using the Builder Design Pattern to achieve a clear and readable syntax.
    /// This pattern is extremly useful for better organising our code and reausability.
    /// </summary>
    public class Example
    {
        public Example()
        {
            // The following code demonstrates how to use the fluent builder pattern to construct a car object.
            // It is worth to notice that we can now use the methods exposed by any builder layer in any order.
            Car car = Car.New
                .OfType(CarType.Sedan)
                .ProducedBy("Ford")
                .OfModel("Mustang")
                .Painted(PaintColour.Black, PaintType.Metallic)
                .ProducedIn(1989)
                .Build();
        }
    }
}
