using Newtonsoft.Json;
public class Product
{
    public string Name { get; set; } =  string.Empty;
    public decimal Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}
class Program
{
    static void Main()
    {
        string json = """{"Name": "Laptop", "Price": 999.99, "Tags": ["Electronics", "Computers"]}""";
        Product? product = JsonConvert.DeserializeObject<Product>(json);
        Console.WriteLine($"Name: {product?.Name}, Price: {product?.Price}, Tags: {string.Join(", ", product?.Tags ?? new List<string>())}");
    
    
        Product newProduct = new Product
        {
            Name = "Smartphone",
            Price = 109.99m,
            Tags = new List<string> { "Electronics", "Mobile" }
        };

        string newJson = JsonConvert.SerializeObject(newProduct);
        Console.WriteLine($"Serialized JSON: {newJson}");
    }
}
