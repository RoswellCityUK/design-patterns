using DesignPatternsCSharp.Builder.FacatedBuilder;

namespace DesignPatternsCSharp.Builder.StepwiseBuilder
{
    // Define the product class
    public class Product
    {
        // Attributes of the product
        public string? Name { get; set; }
        public double Price { get; set; }
        public Currency Currency { get; set; }

        // String representation of the product
        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Currency: {Currency}";
        }
    }

    // Enum to represent different currencies
    public enum Currency { USD, EUR, GBP }

    #region Product Builder Interfaces
    public interface ISpecifyProductName
    {
        public ISpecifyProductCurrency Named(string name);
    }

    public interface ISpecifyProductCurrency
    {
        public ISpecifyProductPrice PricedIn(Currency currency);
    }

    public interface ISpecifyProductPrice
    {
        public IBuildProduct WithPriceTag(double price);
    }

    public interface IBuildProduct
    {
        public Product Build();
    }
    #endregion

    public class ProductBuilder
    {
        // Builder method to start building a product object
        public static ISpecifyProductName Create()
        {
            return new Impl();
        }

        // Internal implementation of the builder interfaces
        private class Impl :
            ISpecifyProductName,
            ISpecifyProductCurrency,
            ISpecifyProductPrice,
            IBuildProduct
        {
            // Product instance being built
            private readonly Product _product = new();

            public ISpecifyProductCurrency Named(string name)
            {
                _product.Name = name;
                return this;
            }

            // Set the product's price, converting it based on set currency
            public IBuildProduct WithPriceTag(double price)
            {
                // Stepwise Builder allows us to make sure that the Currency is set first,
                // and then based on the specified currency convert the value before storying it in the object.
                switch (_product.Currency)
                {
                    case Currency.EUR:
                        price *= 1.54;
                        break;
                    case Currency.GBP:
                        price *= 1.28;
                        break;
                    case Currency.USD:
                    default:
                        break;
                }

                _product.Price = price;
                return this;
            }

            public ISpecifyProductPrice PricedIn(Currency currency)
            {
                _product.Currency = currency;
                return this;
            }

            // Build the final product
            public Product Build() => _product;

            // Optionally we can use explicit converter:
            // public static implicit operator Product(Impl pb)
            // {
            //     return pb._product;
            // }
        }
    }

    /// <summary>
    /// Example demonstrates the use of the stepwise builder pattern to construct a Product object.
    /// It creates a ProductBuilder instance and configures the product's attributes using the fluent API.
    /// The Step Wise Builder is forcing to use specific order of steps while building an object.
    /// This can be usefull while for example next value depends on the previous one.
    /// </summary>
    public class Example
    {
        public Example()
        {
            // Construct a Product object using the stepwise builder pattern.
            Product product = ProductBuilder.Create()
                .Named("Porsche")
                .PricedIn(Currency.USD)
                .WithPriceTag(100000.00).Build();
        }
    }
}
