namespace DesignPatternsCSharp.Builder.SimpleBuilder
{
    /// <summary>
    /// The Simple Builder Design Pattern is used to construct objects with multiple properties
    /// step by step using a simple API. It allows you to create complex objects by gradually
    /// setting their attributes. Each method in the builder corresponds to setting one property
    /// of the object, providing a systematic way of building the object.
    ///
    /// Good Practices:
    /// - Use this pattern when you need to construct objects with multiple properties, especially
    ///   when those properties are optional or have defaults.
    /// - Promotes code readability and maintainability by organizing the object creation process.
    /// - Provides a clear separation between the construction process and the final object.
    ///
    /// Use Cases:
    /// - Constructing objects with numerous optional properties.
    /// - Simplifying the process of creating objects with multiple attributes.
    /// - When you want to keep the process of constructing an object separate from its representation.
    /// - Using Builder Design Pattern can eliminate need for multiple and complex constructors.
    ///
    /// Notes:
    /// - This example demonstrates a simplified version of the builder pattern where each method
    ///   sets a single property. More complex scenarios might involve chaining methods or using
    ///   inheritance for a fluent API.
    /// - If you require more sophisticated variations, consider looking into the Fluent Builder
    ///   pattern or other design patterns like the Faceted Builder for even better grouping
    ///   of properties and functionalities.
    ///   
    /// Read more about the Builder Design Pattern:
    /// <see href="https://en.wikipedia.org/wiki/Builder_pattern">wikipedia.org</see>
    /// </summary>


    /// <summary>
    /// Example recipe class used in the builder design pattern example
    /// </summary>
    public class Recipe
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CookingTime { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe()
        {
            Ingredients = new List<string>();
            Steps = new List<string>();
        }
    }

    /// <summary>
    /// Builder example
    /// This design pattern is used to create objects with a lot of properties
    /// It is the simpliest implementation of the builder pattern
    /// Each method allows to set one property of the object in systematic way
    /// </summary>
    public class RecipeBuilder
    {
        /// <summary>
        /// Stores the recipe being built.
        /// </summary>
        private readonly Recipe _recipe;

        public RecipeBuilder()
        {
            // Initialize the recipe.
            _recipe = new Recipe();
        }

        #region RecipeBuilder methods for setting properties
        public void AddName(string name)
        {
            _recipe.Name = name;
        }

        public void AddDescription(string description)
        {
            _recipe.Description = description;
        }

        public void AddCookingTime(int cookingTime)
        {
            _recipe.CookingTime = cookingTime;
        }

        public void SetIngredients(List<string> ingredients)
        {
            _recipe.Ingredients = ingredients;
        }

        public void SetSteps(List<string> steps)
        {
            _recipe.Steps = steps;
        }
        #endregion

        public Recipe Build() => _recipe;
    }

    public class BuilderExample
    {
        public Recipe Recipe { get; set; }

        public BuilderExample()
        {
            var recipeBuilder = new RecipeBuilder();
            recipeBuilder.AddName("Pizza");
            recipeBuilder.AddDescription("Pizza is a flatbread generally topped with tomato sauce and cheese and baked in an oven. It is commonly topped with a selection of meats, vegetables and condiments.");
            recipeBuilder.AddCookingTime(30);
            recipeBuilder.SetIngredients(
                new List<string>
                { 
                    "Flour", 
                    "Tomato Sauce", 
                    "Cheese", 
                    "Pepperoni" 
                }
            );
            recipeBuilder.SetSteps(
                new List<string>
                { 
                    "Mix the flour with water", 
                    "Add the tomato sauce", 
                    "Add the cheese", 
                    "Add the pepperoni", 
                    "Bake in the oven" 
                }
            );
            Recipe = recipeBuilder.Build();
        }
    }
}
