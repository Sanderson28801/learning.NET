var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


var inventory = new List<Product>
{
    new Product(1, "Brake Pads", 50),
    new Product(2, "Oil Filter", 200),
    new Product(3, "Spark Plugs", 120)
};



app.MapGet("/", () => "Hello World!");

app.MapGet("/products", () => inventory);

app.MapGet("products/{id}", (int id) =>
{
    var product = inventory.FirstOrDefault(p => p.Id == id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/products", (Product newProduct) =>
{
    inventory.Add(newProduct);
    return Results.Ok();
});

app.Run();


record class Product(int Id, string Name, int Stock);

