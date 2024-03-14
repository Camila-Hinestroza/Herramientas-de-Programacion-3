using System;

class TallerPptCamilaHinestroza
{
    static void Main(string[] args)
    {
        bool seguirJugando = true;

        while (seguirJugando)
        {
            Console.WriteLine("¡Bienvenido a el juego de priedra, papel y tijera!");

            int rondas = 5;
            int numeroIntentos = 0;
            int vecesGanadasUsuario = 0;
            int vecesGanadasConsola = 0,

            while (numeroIntentos < rondas)
            {
                rondasJugadas++;
                Console.WriteLine($"\nRonda {numeroIntentos}:");
                Console.WriteLine("Elige piedra, papel o tijera:");
                string opcionJugador = Console.ReadLine().ToLower();

                Random rand = new Random();
                int numeroAleatorio = rand.Next(1, 4);
                string opcionComputadora = "";

                switch (numeroAleatorio)
                {
                    case 1:
                        opcionComputadora = "piedra";
                        break;
                    cadotnetse 2:
                        opcionComputadora = "papel";
                        break;
                    case 3:
                        opcionComputadora = "tijera";
                        break;
                    default:
                        opcionComputadora = "";
                        break;
                }

                Console.WriteLine($"Has elegido: {opcionJugador}");
                Console.WriteLine($"La computadora ha elegido: {opcionComputadora}");

                if (opcionJugador == opcionComputadora)
                {
                    Console.WriteLine("Empate en esta ronda.");
                }
                else if ((opcionJugador == "piedra" && opcionComputadora == "tijera") ||
                         (opcionJugador == "papel" && opcionComputadora == "piedra") ||
                         (opcionJugador == "tijera" && opcionComputadora == "papel"))
                {
                    Console.WriteLine("¡Ganaste esta ronda!");
                    victoriasJugador++;
                }
                else
                {
                    Console.WriteLine("Perdiste esta ronda.");
                    victoriasComputadora++;
                }
            }

            if (victoriasJugador > victoriasComputadora)
            {
                Console.WriteLine("\n¡Felicidades! Has ganado el juego.");
            }
            else if (victoriasJugador < victoriasComputadora)
            {
                Console.WriteLine("\nLa computadoraz ha ganado el juego. Mejor suerte la próxima vez.");
            }
            else
            {
                Console.WriteLine("\nEl juego ha terminado en empate.");
            }

            Console.WriteLine("\n¿Quieres seguir jugando? (s/n)");
            char respuesta = Console.ReadKey().KeyChar;
            seguirJugando = (respuesta == 's' || respuesta == 'S');

            Console.Clear(); // Limpiar la consola para la siguiente partida
        }

        Console.WriteLine("\nGracias por jugar a Piedra, Papel o Tijera ¡Hasta luego!");
    }
}
