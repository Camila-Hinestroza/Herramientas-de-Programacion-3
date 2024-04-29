
using System;

class Program
{
    static void Main(string[] args)
    {
        CatalogoBiblioteca catalogo = new CatalogoBiblioteca();

        // libros de catálogo
        catalogo.AgregarLibro(new Libro("The Great Gatsby", "F. Scott Fitzgerald", 1925));
        catalogo.AgregarLibro(new Libro("To Kill a Mockingbird", "Harper Lee", 1960));
        catalogo.AgregarLibro(new Libro("1984", "George Orwell", 1949));

        // Mostrar el catálogo
        catalogo.MostrarCatalogo();

        // Agregar usuarios 
        Console.WriteLine("\nIngrese el nombre del usuario:");
        string nombreUsuario = Console.ReadLine();
        Usuario usuario = new Usuario(nombreUsuario);
        catalogo.AgregarUsuario(usuario);
        Console.WriteLine($"El usuario '{nombreUsuario}' ha sido agregado.");

        // prestar y devolver libros
        while (true)
        {
            Console.WriteLine("\nElegir opcion:");
            Console.WriteLine("1. Prestar libro");
            Console.WriteLine("2. Devolver libro");
            Console.WriteLine("3. Mostrar Catalogo");
            Console.WriteLine("4. Salir");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("\nEscriba el numero del libro a prestar:");
                    if (int.TryParse(Console.ReadLine(), out int numeroLibro))
                    {
                        Libro libroPrestado = catalogo.NumerodeLibro(numeroLibro);
                        if (libroPrestado != null)
                        {
                            catalogo.PrestarLibro(libroPrestado, usuario);
                        }
                        else
                        {
                            Console.WriteLine("Numero de libro invalido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Caracter invalido, ingrese el numero.");
                    }
                    break;
                case "2":
                    Console.WriteLine("\nEscriba el numero del libro a devolver:");
                    if (int.TryParse(Console.ReadLine(), out int devolverNumero))
                    {
                        Libro libroDevuelto = catalogo.NumerodeLibro(devolverNumero);
                        if (libroDevuelto != null)
                        {
                            catalogo.DevolverLibro(libroDevuelto);
                        }
                        else
                        {
                            Console.WriteLine("Numero de libro invalido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Caracter invalido, ingrese el numero.");
                    }
                    break;
                case "3":
                    catalogo.MostrarCatalogo();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opcion invalida, intente de nuevo");
                    break;
            }
        }
    }
}
