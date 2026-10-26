using System;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public int Copies { get; set; }
}

public class Library
{
    private Book[] books = new Book[5];

  
    public Book this[int i]
    {
        get { return books[i]; }
        set { books[i] = value; }
    }

   
    public Book this[string title]
    {
        get
        {
            foreach (Book b in books)
            {
                if (b != null && b.Title.Equals(title,StringComparison.OrdinalIgnoreCase))
                {
                    return b;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(title),"Book title not found.");
        }
    }
}

public delegate void BookOperation(Book b);

class Program
{
    static void DisplayBook(Book b)
    {
        Console.WriteLine($"Title: {b.Title}, Author: {b.Author}," +$"Price: {b.Price}, Copies: {b.Copies}");
    }

    static void DisplayExpensive(Book b)
    {
        if (b.Price > 1000)
        {
            Console.WriteLine($"Expensive Book: {b.Title} - {b.Price}");
        }
    }

    static void Main(string[] args)
    {
        Library library = new Library();

     
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"\nEnter details for Book {i + 1}");

            while (true)
            {
                try
                {
                    Book b = new Book();

                    Console.Write("Enter title: ");
                    b.Title = Console.ReadLine();

                    Console.Write("Enter author: ");
                    b.Author = Console.ReadLine();

                    Console.Write("Enter price: ");
                    b.Price = double.Parse(Console.ReadLine());

                    Console.Write("Enter copies: ");
                    b.Copies = int.Parse(Console.ReadLine());

                    if (b.Copies < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(b.Copies),"Copies cannot be negative.");
                    }

                    library[i] = b;
                    
                    break;
                }

                
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Enter a numeric price "+"and a whole number for copies. Try again.");
                }

                
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                
                finally
                {
                    Console.WriteLine("Input attempt completed.");
                }
            }
        }

        
        Console.WriteLine("\n--- Complete Book List ---");

        BookOperation operation = DisplayBook;
        
        operation += DisplayExpensive;

        for (int i = 0; i < 5; i++)
        {
            operation(library[i]);
        }

       
        Book mostExpensive = library[0];

        for (int i = 1; i < 5; i++)
        {
            if (library[i].Price > mostExpensive.Price)
            {
                mostExpensive = library[i];
            }
        }

        Console.WriteLine("\n--- Most Expensive Book ---");
        
        DisplayBook(mostExpensive);

       
        Console.Write("\nEnter a title to search: ");
        
        string searchTitle = Console.ReadLine();

        try
        {
            Book found = library[searchTitle];

            Console.WriteLine("\nBook found:");
            
            DisplayBook(found);
        }

        
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Search error: " + ex.Message);
        }
    }
}
