
using System;
using System.Collections.Generic;

public class CatalogoBiblioteca : ICatalogo
{
    private List<Libro> libros;
    private List<Usuario> usuarios;

    public CatalogoBiblioteca()
    {
        libros = new List<Libro>();
        usuarios = new List<Usuario>();
    }

    public void AgregarLibro(Libro libro)
    {
        libros.Add(libro);
    }

    public void EliminarLibro(Libro libro)
    {
        libros.Remove(libro);
    }

    public void AgregarUsuario(Usuario usuario)
    {
        usuarios.Add(usuario);
    }

    public void PrestarLibro(Libro libro, Usuario usuario)
    {
        if (libros.Contains(libro) && libro.Disponibilidad && usuarios.Contains(usuario))
        {
            libro.Disponibilidad = false;
            Console.WriteLine($"El libro '{libro.Titulo}' ha sido prestado a {usuario.Nombre}.");
        }
        else
        {
            Console.WriteLine("Préstamo no exitoso. Verifique si el libro está disponible y si el usuario está registrado.");
        }
    }


    public void DevolverLibro(Libro libro)
    {
        if (libros.Contains(libro))
        {
            if (libro.Disponibilidad)
            {
                Console.WriteLine($"El libro '{libro.Titulo}' ya se encontraba en la biblioteca.");
            }
            else
            {
                libro.Disponibilidad = true;
                Console.WriteLine($"El libro '{libro.Titulo}' ha sido devuelto.");
            }
        }
        else
        {
            Console.WriteLine("Libro no encontrado en el catálogo.");
        }
    }


    public void MostrarCatalogo()
    {
        Console.WriteLine("Catalogo de libros:");
        for (int i = 0; i < libros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Titulo: {libros[i].Titulo}, Autor: {libros[i].Autor}, Disponibilidad: {(libros[i].Disponibilidad ? "Si" : "No")}");
        }
    }

    public Libro NumerodeLibro(int numero)
    {
        if (numero >= 1 && numero <= libros.Count)
        {
            return libros[numero - 1];
        }
        else
        {
            return null;
        }
    }
}

