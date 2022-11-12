using System;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public static class ListaJugadores
    {
        /// <summary>
        /// Esta clase contiene la lista de todos los jugadores estos jugadores siempre aparecen para poder usarlo en el torneo que se desee
        /// siempre que el torneo lo permita(en caso que no el interfaz visual se encarga de no mostrarlos como opcion para el mismo)
        /// ademas contiene bots(jugadores) precreados los cuales puedes eliminar, tres de ellos goloso y el resto son aleatorios 
        /// es una clase estatica que se inicia en la primera escena(escena de carga) de nuestra aplicacion una unica vez
        /// para esta lista cada Jugador tiene una propiedad int codigo que varia segun su posicion en la lista para de esta manera acomodar la 
        /// accesibilidad de los mismos...una vez se agrega o elimina un jugador a esta lista los codigos se acutualizan automaticamente
        /// </summary>

        public static List<Jugador> jugadores { get; private set; }
        public static void Iniciar()
        {
            //Instancia la lista de jugadores y agreaga los precreados
            jugadores = new List<Jugador>();
            BotsPrecreados();
        }
        public static void Add(Jugador jugador)
        {
            //Agreaga un jugador a la lista y le asigna un codigo
            jugadores.Add(jugador);
            jugador.Codigo = jugadores.Count - 1;
        }
        static void BotsPrecreados()
        {
            //Crea los siguientes jugadores
            Add(new JugadorGoloso("Mauricio Bot"));
            Add(new JugadorGoloso("Marcos Bot"));
            Add(new JugadorGoloso("Samuel Bot"));
            Add(new JugadorAleatorio("Gabriel Bot"));
            Add(new JugadorAleatorio("Pedro Bot"));
            Add(new JugadorAleatorio("Francis Bot"));
            Add(new JugadorAleatorio("Marta Bot"));
            Add(new JugadorAleatorio("Alex Bot"));
            Add(new JugadorAleatorio("Juan Bot"));
            Add(new JugadorAleatorio("Estel Bot"));
        }
        public static void Eliminar(int n) { jugadores.Remove(jugadores[n]); Actualizar(); } //Elimina el jugador en la posicion n
        public static void Eliminar(Jugador n) { jugadores.Remove(jugadores[n.Codigo]); Actualizar(); } // Elimina el jugador en la posicion codigo de n
        static void Actualizar() { for (int i = 0; i < jugadores.Count; i++) jugadores[i].Codigo = i; }// actualiza el codigo de todos los jugadores
    }
    public abstract class Jugador
    {
        //public List<Type> juegosQuePuedeJugar { get; protected set; }
        public int Puntuacion;
        public List<Type> juegosImplementados { get; protected set; }
        public string tipo;//String del tipo de jugador que es
        public int Codigo;//codigo para su posicion en lista
        public string Nombre { get; protected set; }//Nombre del jugador
        public Equipo equipo { get; protected set; }//Equipo del jugador solo influye en juegos por equipos
        public void CambiarEquipo(Equipo _equipo) { equipo = _equipo; }//Metodo publico para cambiar el equipo del jugador
        public abstract void Jugar(Juego juego);///Realizar la jugada del mismo...variable para cada jugador
        public override string ToString()
        {
            return Nombre + " > " + tipo;
        }
        public virtual bool PuedeJugar(Juego juego) { return true; }//Define si puede o no jugar un juego dado...variable para cada jugador
    }
    public class JugadorGoloso : Jugador
    {
        //Este jugador tiene su jugada definida para cada juego solo debe llamarla en el juego el cual este jugando
        public JugadorGoloso(string nombre) { Nombre = nombre; tipo = "Goloso"; }
        public override void Jugar(Juego juego) { juego.JugadaGolosa(); }
    }
    public class JugadorAleatorio : Jugador
    {
        //Este jugador tiene su jugada definida para cada juego solo debe llamarla en el juego el cual este jugando
        public JugadorAleatorio(string nombre) { Nombre = nombre; tipo = "Aleatorio"; }
        public override void Jugar(Juego juego) { juego.JugadaAleatoria(); }
    }
    public class JugadorPersonal : Jugador
    {
        //Este jugador existe para que los usuarios jueguen Artifact... no existe la opcion de agreagarlo a un torneo
        public JugadorPersonal(string nombre) { Nombre = nombre; tipo = "Humano"; }
        public override void Jugar(Juego juego) {/*No hace nada exactamente*/ }
    }
    public class JugadorLoco : Jugador
    {
        /// <summary>
        /// Esta es un ejemplo de la implemantacion de un nuevo jugador, completamente extensible sin modificar el codigo fuente...
        /// debes definir como se comporta el mismo, que juego puede jugar, y para cada uno de estos crear un metodo para ello como se ve
        /// a continuacion...este jugador juega Artifact y Tic Tac Toe
        /// </summary>
        public JugadorLoco(string nombre)
        {
            juegosImplementados = new List<Type>();
            juegosImplementados.Add((new TicTacToe()).GetType());
            juegosImplementados.Add((new Artifact()).GetType());
            Nombre = nombre;
            tipo = "Loco";
        }
        public override bool PuedeJugar(Juego juego)
        {
            // el jugador loco esta implementado para juegos dados...en caso que se kiera usar en cualquier juego se debe devolver true y modi-
            //ficar como se comporta usando jugadas genericas(Golosa o Aleatoria)
            return juegosImplementados.Contains(juego.Tipo);
        }
        public override void Jugar(Juego juego)
        {
            //Prueba para cada caso para saber que juego es el que esta jugando...si no es el caso lanza exepcion y sale del try
            try { Jugar(juego as Artifact); return; } catch { }
            try { Jugar(juego as TicTacToe); return; } catch { }
            //en caso de que se desee que juegue cualquier juego se debe agregar la linea juego.JugadaAleatoria() o Aleatoria; 
        }
        void Jugar(TicTacToe juego)
        {
            //Juega de tal forma que siempre usa la primera jugada valida...

            int x = juego.jugadasValidas[0].Item1;
            int y = juego.jugadasValidas[0].Item2;
            if (juego.JugadorActual == 0)
            { juego.tablero[x, y] = TicTacToe.marca.x; juego.Parser[x * 3 + y] = "x"; }
            else
            { juego.tablero[x, y] = TicTacToe.marca.o; juego.Parser[x * 3 + y] = "o"; }
            juego.jugadasValidas.Remove(juego.jugadasValidas[0]);
            juego.GuardarJugada(juego.jugadores[juego.JugadorActual], x, y);
        }
        void Jugar(Artifact juego)
        {
            //En el juego de artifact se comporta como un jugador aleatorio
            juego.JugadaAleatoria();
        }
    }
}