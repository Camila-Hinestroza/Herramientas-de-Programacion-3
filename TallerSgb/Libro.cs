
using System;

public class Libro
{
    public string Titulo { get; }
    public string Autor { get; }
    public int AñoPublicación { get; }
    public bool Disponibilidad { get; set; }

    public Libro(string titulo, string autor, int añoPublicación)
    {
        Titulo = titulo;
        Autor = autor;
        AñoPublicación = añoPublicación;
        Disponibilidad = true; 
    }
}
