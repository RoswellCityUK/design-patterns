namespace DesignPatternsCSharp.Builder.FluentBuilder
{
    /// <summary>
    /// The Fluent Builder design pattern is used to construct complex objects step by step
    /// using a fluent and intuitive API. Each method in the builder returns the builder instance
    /// itself, enabling method chaining and allowing for the easy configuration of object properties.
    /// This pattern is particularly useful when constructing objects with multiple optional parameters,
    /// providing a clear and readable way to create instances.
    ///
    /// In this implementation, the Recipe class represents a recipe object with various properties.
    /// The FluentRecipeBuilder class offers methods to set each property of the recipe in a concise manner.
    /// The Fluent Builder pattern simplifies the process of constructing a Recipe instance by chaining
    /// together calls to the builder methods.
    ///
    /// Good Practices:
    /// - Use this pattern when you need to construct objects with many optional parameters.
    /// - Enhance the builder with methods that allow setting different attributes of the object.
    /// - Provide an intuitive API that allows setting attributes in any order.
    /// - Ensure that the final build method returns the fully constructed object.
    ///
    /// Use Cases:
    /// - Building objects with multiple optional properties, especially when there are different ways
    ///   to initialize the object.
    /// - Creating objects where the order of attribute setting might vary.
    /// - Improving code readability and maintainability by providing a clear and structured way
    ///   to create instances of complex objects.
    /// - Using Builder Design Pattern can eliminate need for multiple and complex constructors.
    /// 
    /// Read more about Fluent Builder design pattern here:
    /// <see href="https://dzone.com/articles/fluent-builder-pattern">dzone.com</see>
    /// <see href="https://medium.com/xebia-engineering/fluent-builder-pattern-with-a-real-world-example-7b61be375a40">by xebia-engineering@medium.com</see>
    /// </summary>


    /// <summary>
    /// Represents a recipe with various properties such as name, description, cooking time, ingredients, and steps.
    /// </summary>
    public class Recipe
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CookingTime { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// Makes sure that the ingredients and steps lists are initialized.
        /// </summary>
        public Recipe()
        {
            Ingredients = new List<string>();
            Steps = new List<string>();
        }
    }

    /// <summary>
    /// Implementation of the fluent builder for constructing recipes.
    /// This design pattern simplifies the construction of complex objects by allowing method chaining.
    /// Each method returns a FluentRecipeBuilder instance, enabling a concise and readable construction process.
    /// </summary>
    public class FluentRecipeBuilder
    {
        /// <summary>
        /// Stores the recipe being built.
        /// </summary>
        private readonly Recipe _recipe;

        public FluentRecipeBuilder()
        {
            // Initialize the recipe.
            _recipe = new Recipe();
        }

        #region FluentRecipeBuilder methods for setting properties
        public FluentRecipeBuilder AddName(string name)
        {
            _recipe.Name = name;
            return this;
        }

        public FluentRecipeBuilder AddDescription(string description)
        {
            _recipe.Description = description;
            return this;
        }

        public FluentRecipeBuilder AddCookingTime(int cookingTime)
        {
            _recipe.CookingTime = cookingTime;
            return this;
        }

        public FluentRecipeBuilder AddIngredient(string ingredient)
        {
            _recipe.Ingredients.Add(ingredient);
            return this;
        }

        public FluentRecipeBuilder AddIngredients(List<string> ingredients)
        {
            _recipe.Ingredients.AddRange(ingredients);
            return this;
        }

        public FluentRecipeBuilder SetIngredients(List<string> ingredients)
        {
            _recipe.Ingredients = ingredients;
            return this;
        }

        public FluentRecipeBuilder AddStep(string step)
        {
            _recipe.Steps.Add(step);
            return this;
        }

        public FluentRecipeBuilder AddSteps(List<string> steps)
        {
            _recipe.Steps.AddRange(steps);
            return this;
        }

        public FluentRecipeBuilder SetSteps(List<string> steps)
        {
            _recipe.Steps = steps;
            return this;
        }
        #endregion

        /// <summary>
        /// Builds and returns the final constructed recipe.
        /// </summary>
        /// <returns>The constructed recipe.</returns>
        public Recipe Build()
        {
            return _recipe;
        }
    }

    /// <summary>
    /// Example usage of the Fluent Builder pattern.
    /// Demonstrates how to create a recipe using the FluentRecipeBuilder to achieve a clear and readable syntax.
    /// </summary>
    public class FluentBuilderExample { 
        public Recipe Recipe { get; set; }

        public FluentBuilderExample()
        {
            // Creating a recipe using the FluentRecipeBuilder to set properties in a chain.
            Recipe = new FluentRecipeBuilder()
                .AddName("Pizza")
                .AddDescription("A delicious pizza")
                .AddCookingTime(30)
                .SetIngredients(new List<string> { "Flour", "Tomato", "Cheese" })
                .AddIngredient("Mushrooms")
                .SetSteps(new List<string> { "Mix ingredients", "Cook in oven" })
                .AddStep("Enjoy!")
                .Build(); // This returns the final recipe object.
        }
    }
}
