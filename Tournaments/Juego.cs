using System;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public abstract class Juego
    {
        public int MaximoDeJugadoresPorEquipo { get; protected set; }
        public List<Type> torneosPermitidos { get; protected set; }//Un juego no puede jugarse en cualquier torneo
        public bool EsEnEquipos { get; protected set; }// Define si el juego es en equipos o en solitario 
        public Type Tipo { get; protected set; }//Define el tipo de juego
        public Type partida { get; protected set; }// Define el tipo de partida del juego, cada juego tiene su internal class partida
        public Jugador ganador { get; protected set; }// Este es el jugador ganador 
        public Equipo equipoGanador { get; protected set; }// Este es el equipo ganador 
        public bool Termino { get; protected set; }//Define si un juego termino
        public List<Jugador> jugadores { get; protected set; }// Jugadores del juego
        public List<Equipo> Equipos { get; protected set; }// Equipos del juego

        public abstract void Iterar();// Itera una jugada
        public abstract void Jugar();// Devuelve una jugada
        public abstract void JugadaGolosa();//Todos los juegos tienen interna la jugada golosa
        public abstract void JugadaAleatoria();//Todos los juegos tienen interna la jugada aleatoria
        public abstract void AddJugador(Jugador jugador);//Agrega un jugador al juego
        public virtual void AddEquipo(Equipo equipo) { Equipos.Add(equipo); }//Agrega un jugador al juego
    }
    public class Equipo
    {
        /// <summary>
        /// Los equipos existen para los juegos que se juegan en equipo solo para ellos...
        /// un Equipo consta de una lista de jugadores  un maximo (CantidadDeJugadores) de jugadores el cual seria la 
        /// cantidad de jugadores que debe tener su lista... por ejemplo si se crea un juego domino este se juega en
        /// equipos de dos personas un equipo posee un nombre para identificarlo junto a sus jugadores
        /// </summary>

        public int Puntuacion; // puntuacion del equipo
        public List<Jugador> jugadores { get; private set; }//lista de jugadores de este equipo
        int CantidadDeJugadores;//cantidad de jugadores que debe tener el equipo
        string Nombre;//el nombre que identifica el equipo

        public Equipo(string nombre, int maximo)
        {
            //Un equipo se inicializa con cero puntos y cero jugadores
            Puntuacion = 0;
            jugadores = new List<Jugador>();
            CantidadDeJugadores = maximo;
        }
        public void AddJugador(Jugador jugador)
        {
            //Agrega Jugadores al equipo
            if (jugadores.Count < CantidadDeJugadores)
                jugadores.Add(jugador);
        }
        public void EliminarJugador(Jugador jugador)
        {
            //Elimina Jugadores del equipo...no es necesario para cumplir con lo que se pide
            jugadores.Remove(jugador);
        }
        public bool EstaLLeno()
        {
            //Devuelve si el equipo tiene la cantidad de jugadores que debe
            return jugadores.Count >= CantidadDeJugadores;
        }
        public override string ToString()
        {
            // Identifica el equipo en la bitacora o en cualquier texto
            string temporal = "( ";
            for (int i = 0; i < jugadores.Count; i++)
                temporal += jugadores[i].Nombre + " ";
            temporal += ")";
            return Nombre + temporal;
        }
    }
}