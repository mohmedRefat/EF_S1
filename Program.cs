using Microsoft.EntityFrameworkCore;
using Data;

using var context = new BookStoreContext();

Console.WriteLine($"Can connect: {context.Database.CanConnect()}");

if (!context.Books.Any())
{
    var author = new Author
    {
        FirstName = "Naguib",
        LastName = "Mahfouz",
        Email = "naguib@mail.com",
        Biography = "Nobel laureate in literature",
        DateOfBirth = new DateTime(1911, 12, 11)
    };

    var category = new Category
    {
        Name = "Fiction",
        Description = "Novels and stories",
        IsActive = true
    };

    context.Books.Add(new Book
    {
        Title = "Cairo Trilogy",
        ISBN = "9781234567890",
        Price = 250m,
        NumberOfPages = 1500,
        PublishedYear = 1956,
        IsInStock = true,
        Author = author,
        Category = category
    });

    context.SaveChanges();
    Console.WriteLine("Sample data inserted.");
}

foreach (var b in context.Books.Include(b => b.Author).Include(b => b.Category))
{
    Console.WriteLine($"{b.Id} | {b.Title} | {b.Author.FirstName} {b.Author.LastName} | {b.Category.Name} | {b.Price} | {(b.IsInStock ? "In stock" : "Out of stock")}");
}