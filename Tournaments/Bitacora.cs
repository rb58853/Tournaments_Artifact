using System.Collections.Generic;

namespace LogicoNuevo
{
    public class Bitacora
    {
        static public bool torneos;
        static public bool partidas;
        static public bool juegos;
        static public bool jugadas;

        static List<string> Todo;
        static List<string> TorneoPartidasJuegos;
        static List<string> TorneoPartidasJugadas;
        static List<string> TorneoJuegosJugadas;
        static List<string> TorneoPartidas;
        static List<string> TorneoJuegos;
        static List<string> TorneoJugadas;
        static List<string> Torneos;
        static List<string> PartidasJuegosJugadas;
        static List<string> PartidasJuegos;
        static List<string> PartidasJugadas;
        static List<string> Partidas;
        static List<string> JuegosJugadas;
        static List<string> Juegos;
        static List<string> Jugadas;

        static void ReiniciarPuntuacion()
        {
            for (int i = 0; i < ListaJugadores.jugadores.Count; i++)
                ListaJugadores.jugadores[i].Puntuacion = 0;
        }
        public static void Reiniciar()
        {
            ReiniciarPuntuacion();
            Todo = new List<string>();
            TorneoPartidasJuegos = new List<string>();
            TorneoPartidasJugadas = new List<string>();
            TorneoJuegosJugadas = new List<string>();
            TorneoPartidas = new List<string>();
            TorneoJuegos = new List<string>();
            TorneoJugadas = new List<string>();
            Torneos = new List<string>();
            PartidasJuegosJugadas = new List<string>();
            PartidasJuegos = new List<string>();
            PartidasJugadas = new List<string>();
            Partidas = new List<string>();
            JuegosJugadas = new List<string>();
            Juegos = new List<string>();
            Jugadas = new List<string>();
        }
        public static List<string> Filtro()
        {
            if (jugadas && juegos && partidas && torneos)
                return Todo;
            if (jugadas && juegos && partidas)
                return PartidasJuegosJugadas;
            if (jugadas && juegos && partidas)
                return PartidasJuegos;
            if (jugadas && juegos && torneos)
                return TorneoJuegosJugadas;
            if (jugadas && partidas && torneos)
                return TorneoPartidasJugadas;
            if (juegos && partidas && torneos)
                return TorneoPartidasJuegos;
            if (jugadas && partidas)
                return PartidasJugadas;
            if (jugadas && juegos)
                return JuegosJugadas;
            if (jugadas && juegos)
                return JuegosJugadas;
            if (jugadas && torneos)
                return TorneoJugadas;
            if (partidas && torneos)
                return TorneoPartidas;
            if (juegos && torneos)
                return TorneoJuegos;
            if (juegos && partidas)
                return PartidasJuegos;
            if (juegos)
                return Juegos;
            if (torneos)
                return Torneos;
            if (partidas)
                return Partidas;
            if (jugadas)
                return Jugadas;
            return new List<string>();
        }
        public static void AddJugada(string jugada)
        {
            Todo.Add(jugada);
            TorneoPartidasJugadas.Add(jugada);
            TorneoJuegosJugadas.Add(jugada);
            TorneoJugadas.Add(jugada);
            PartidasJuegosJugadas.Add(jugada);
            PartidasJugadas.Add(jugada);
            JuegosJugadas.Add(jugada);
            Jugadas.Add(jugada);
        }
        public static void AddJuego(string notificacion)
        {
            Todo.Add(notificacion);
            TorneoPartidasJuegos.Add(notificacion);
            TorneoJuegosJugadas.Add(notificacion);
            PartidasJuegosJugadas.Add(notificacion);
            PartidasJuegos.Add(notificacion);
            JuegosJugadas.Add(notificacion);
            Juegos.Add(notificacion);
            TorneoJuegos.Add(notificacion);
        }
        public static void AddPartida(string notificacion)
        {
            Todo.Add(notificacion);
            TorneoPartidasJuegos.Add(notificacion);
            TorneoPartidasJugadas.Add(notificacion);
            PartidasJuegosJugadas.Add(notificacion);
            PartidasJuegos.Add(notificacion);
            PartidasJugadas.Add(notificacion);
            Partidas.Add(notificacion);
            TorneoPartidas.Add(notificacion);
        }
        public static void AddTorneo(string notificacion)
        {
            Todo.Add(notificacion);
            TorneoPartidasJuegos.Add(notificacion);
            TorneoPartidasJugadas.Add(notificacion);
            TorneoJuegosJugadas.Add(notificacion);
            TorneoJugadas.Add(notificacion);
            Torneos.Add(notificacion);
            TorneoJuegos.Add(notificacion);
            TorneoPartidas.Add(notificacion);
        }
    }
}


