using System;
using System.Collections.Generic;

// Clase abstracta - Abstracción
abstract class Evento
{
    // Encapsulación con propiedades
    public string Nombre { get; private set; }
    public string Lugar { get; private set; }
    public double Precio { get; private set; }
    public int EntradasDisponibles { get; private set; } = 100;

    public Evento(string nombre, string lugar, double precio)
    {
        Nombre = nombre;
        Lugar = lugar;
        Precio = precio;
    }

    // Método polimórfico
    public abstract void MostrarInfo();

    public void VenderEntrada(int cantidad)
    {
        if (cantidad <= EntradasDisponibles)
        {
            EntradasDisponibles -= cantidad;
            Console.WriteLine($"{cantidad} entrada(s) vendida(s) para {Nombre}.");
        }
        else
        {
            Console.WriteLine("No hay suficientes entradas disponibles.");
        }
    }
}

// Herencia
class Concierto : Evento
{
    public string Artista { get; private set; }

    public Concierto(string nombre, string lugar, double precio, string artista)
        : base(nombre, lugar, precio)
    {
        Artista = artista;
    }

    // Polimorfismo
    public override void MostrarInfo()
    {
        Console.WriteLine($"🎵 Concierto: {Nombre} | Lugar: {Lugar} | Artista: {Artista} | Precio: ${Precio} | Entradas: {EntradasDisponibles}");
    }
}

// Otra subclase
class ObraTeatro : Evento
{
    public string Director { get; private set; }

    public ObraTeatro(string nombre, string lugar, double precio, string director)
        : base(nombre, lugar, precio)
    {
        Director = director;
    }

    public override void MostrarInfo()
    {
        Console.WriteLine($"🎭 Obra de Teatro: {Nombre} | Lugar: {Lugar} | Director: {Director} | Precio: ${Precio} | Entradas: {EntradasDisponibles}");
    }
}

// Clase cliente
class Cliente
{
    public string Nombre { get; set; }

    public Cliente(string nombre)
    {
        Nombre = nombre;
    }

    public void ComprarEntrada(Evento evento, int cantidad)
    {
        Console.WriteLine($"\n{Nombre} quiere comprar {cantidad} entrada(s):");
        evento.MostrarInfo();
        evento.VenderEntrada(cantidad);
    }
}

// Programa principal
class Program
{
    static void Main()
{
    // Crear eventos
    Concierto concierto1 = new Concierto("RockFest", "Estadio Nacional", 1500, "Banda X");
    ObraTeatro obra1 = new ObraTeatro("Hamlet", "Teatro Real", 800, "Juan Pérez");

    // Crear cliente
    Console.WriteLine("Bienvenido a MiTaquillaRD.");
    Console.Write("Ingrese su nombre: ");
    string nombreCliente = Console.ReadLine();
    Cliente cliente = new Cliente(nombreCliente);

    bool continuar = true;

    while (continuar)
    {
        Console.Clear();
        Console.WriteLine($"\nHola {cliente.Nombre}, elige un evento:");
        Console.WriteLine("1. Concierto - RockFest");
        Console.WriteLine("2. Obra de Teatro - Hamlet");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine();

        Evento eventoSeleccionado = null;

        if (opcion == "1")
            eventoSeleccionado = concierto1;
        else if (opcion == "2")
            eventoSeleccionado = obra1;
        else
        {
            Console.WriteLine("Opción no válida.");
            continue;
        }

        Console.Write("¿Cuántas entradas deseas comprar?: ");
        if (int.TryParse(Console.ReadLine(), out int cantidad))
        {
            cliente.ComprarEntrada(eventoSeleccionado, cantidad);
        }
        else
        {
            Console.WriteLine("Cantidad inválida.");
        }

        Console.Write("\n¿Deseas comprar otra entrada? (s/n): ");
        string respuesta = Console.ReadLine().ToLower();
        if (respuesta != "s")
            continuar = false;
    }

    Console.WriteLine("\n--- Estado final de eventos ---");
    concierto1.MostrarInfo();
    obra1.MostrarInfo();
    Console.WriteLine("\nGracias por usar el sistema.");
}

}
