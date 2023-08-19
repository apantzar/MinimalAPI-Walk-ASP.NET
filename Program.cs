using Microsoft.EntityFrameworkCore;
using MinimalShoppingListApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseInMemoryDatabase("ShoppingListApi"));


var app = builder.Build();

//Get endpoint
app.MapGet("/shoppinglist", async (ApiDbContext db) => await db.Groceries.ToListAsync());

//Get by id
app.MapGet("shoppinlst/{id}", async(int id, ApiDbContext db) =>
{
   var grocery = await db.Groceries.FindAsync(id);
   return grocery != null? Results.Ok(grocery) : Results.NotFound();    

});

//Post endpoint
app.MapPost("/shoppinglist", async (Grocery grocery, ApiDbContext db) =>
{

    db.Groceries.Add(grocery);
    await db.SaveChangesAsync();
    return Results.Created($"/shoppinglist/{grocery.Id}", grocery);
});



//Delete endpoint
app.MapDelete("/shoppinglist/{id}", async (int id, ApiDbContext db) =>
{

    
    var grocery = await db.Groceries.FindAsync(id);

    if (grocery != null) {
        db.Groceries.Remove(grocery!);
        await db.SaveChangesAsync();
        return Results.NoContent();

    }
    else
    {
        return Results.NotFound();
    }
   


});



app.MapPut("/shoppinglist/{id}", async(int id, Grocery grocery_updated, ApiDbContext db) => {

    var groceryInDb = await db.Groceries.FindAsync(id);

    if (groceryInDb != null) {

        groceryInDb.Name = grocery_updated.Name;
        groceryInDb.Purcgased = grocery_updated.Purcgased;


        await db.SaveChangesAsync();

        return Results.Ok(groceryInDb);
    }
    else
    {
        return Results.NotFound();
    }


});

    

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
