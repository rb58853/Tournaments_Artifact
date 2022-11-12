using System;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public abstract class Partida
    {
        /// <summary>
        /// Cada juego tiene su propia partida de esta manera definimos como se comporta la misma para cada uno
        /// Esto permite definir una partida como deseamos sin limitar nada, Ejemplo pudes definir como se termina una partida de
        /// domino por puntos o puedes definirla por cantidad de partidas...ademas nos permite definir la capacidad de un juego,
        /// si es por sistema de equipos o en solitario definir como se comporta la partida para cada caso 
        /// (Ejemplo: sumar puntos a un equipo de domino; implementamos variables "puntos Equipo K" y definimos el comportamiento)...
        /// </summary>
        public List<Equipo> Equipos { get; protected set; }//Los equipos de la partida
        public List<Jugador> jugadores { get; protected set; }//Los jugadores de la partida
        public List<Juego> juegos { get; protected set; }//Los juegos de la partida
        public Jugador Ganador { get; protected set; }
        public Equipo EquipoGanador { get; protected set; }

        public virtual bool EnEjecucion() { return false; }//Define si la partida comenzo o no
        public abstract bool TerminoLaPartida();//Define si termino la partida (como debe terminar), a gusto del desarrollador
        public abstract bool TerminoElJuego();//Define si el juego actual (ultimo juego de la lista) termino
        public abstract void AddJuego();//Agreaga un nuevo juego a la lista de juegos
        public abstract bool TodosEstanDentro();//Devuelve si el juego tiene todos los jugadores(o equipos) necesarios o predefinidos
        public abstract void Iterar();

        public virtual void AddJugadoresAlJuegoActual()
        {
            //Agreaga los jugadores de la partida al nuevo juego...variable para cada Partida
            for (int i = 0; i < jugadores.Count; i++)
                juegos[juegos.Count - 1].AddJugador(jugadores[i]);
        }
        public virtual void AddJugador(Jugador jugador)
        {
            //Agreaga un jugador a la partida...variable para cada Partida
            jugadores.Add(jugador);
            if (TodosEstanDentro())
                AddJuego();
        }
        public virtual void AddEquipo(Equipo equipo)
        {
            //Agrega un nuevo equipo a la partida ...variable para cada Partida
            Equipos.Add(equipo);
            if (TodosEstanDentro())
                AddJuego();
        }
        public virtual void ProximaJugada()
        {
            //Itera la proxima jugada llama a la jugada del juego actual...variable para cada Partida
            //Bitacora.AddPartida("") agrega un string a las partidas(string) de la bitacora... 

            if (TerminoLaPartida()) return;
            if (!EnEjecucion())
                Bitacora.AddPartida("Comenzo la partida " + ToString());
            juegos[juegos.Count - 1].Iterar();
            //juegos[juegos.Count - 1].Jugar();
            if (TerminoElJuego() && !TerminoLaPartida())
                AddJuego();
            if (TerminoLaPartida())
            {
                Bitacora.AddPartida("Termino la partida " + ToString() + ".");
                if (Ganador != null)
                    Bitacora.AddPartida("El ganador de la partida es " + Ganador.ToString());
                if (EquipoGanador != null)
                    Bitacora.AddPartida("El ganador de la partida es " + EquipoGanador.ToString());
            }
        }
        public virtual void TerminarJuego()
        {
            //Itera por todas las jugadas del juego actual para terminarlo...variable para cada Partida
            while (!juegos[juegos.Count - 1].Termino)
                ProximaJugada();
        }
        public virtual void TerminarPartida()
        {
            //Itera por todos los juegos de la partida para terminar la misma...variable para cada Partida
            while (!TerminoLaPartida())
                TerminarJuego();
        }
    }
}