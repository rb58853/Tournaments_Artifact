using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public abstract class Torneo
    {
        public IteradorTorneos iterador { get; protected set; }
        public List<Type> juegosPermitidos { get; protected set; }//Un torneo no permite cualquier juego, esto define cuales son permitidos
        public Type tipoTorneo { get; protected set; }// Esto se usa para Modificar aspectos en los juegos (Puede jugar este tipo de torneo)
        public List<Jugador> jugadores { get; protected set; }//Los jugadores que participan en el torneo
        public List<Equipo> Equipos { get; protected set; }//Los jugadores que participan en el torneo de ser el caso
        public List<Partida> partidas { get; protected set; }//Las Paridas del torneos(ya sean precreadas o por crear) 
        public Type juego { get; protected set; }//El tipo de juego que juega este torneo(ejemplo: Tic Tac Toe , Artifact)
        public Juego juegoInstancia { get; protected set; }//Esta propiedad se usa solo como instancia(ejemplo: if(juegoInstancia.Equipo){...})

        public bool TerminoElTorneo { get; protected set; }//Devuelve si el torneo termino
        public int PartidaActual;//Contador que lleva cual es la partida actual

        public virtual void AddEquipo(Equipo equipo)
        { Equipos.Add(equipo); Bitacora.AddTorneo(equipo.ToString() + " se unio al torneo."); }
        public virtual void AddJugador(Jugador jugador)
        { jugadores.Add(jugador); Bitacora.AddTorneo(jugador.Nombre + " se unio al torneo."); }
        public virtual void AddPartida()
        { partidas.Add(Activator.CreateInstance((Activator.CreateInstance(juego) as Juego).partida) as Partida); }

        public virtual bool ElJuegoEstaPermitido()
        {
            // Este es un metodo bilateral que permite que al implementar un nuevo torneo o un nuevo juego hacer esto generico 
            // sin tocar el codigo fuente modificando solo el nuevo Juego/Torneo implementado...
            return juegosPermitidos.Contains(juego) || juegoInstancia.torneosPermitidos.Contains(tipoTorneo);
        }
        public abstract void CrearPartidas();
        public abstract void ProximaJugada();
        public abstract void TerminarPartida();
        public abstract void TerminarJuego();
        public abstract void TerminarTorneo();
        public class IteradorTorneos : IEnumerator
        {
            //iterador publico que ejecuta el torneo jugada por partida por partida
            Torneo torneo;
            public IteradorTorneos(Torneo _torneo)
            {
                torneo = _torneo;
            }
            public object Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                if (torneo.TerminoElTorneo)
                    return false;
                torneo.partidas[torneo.PartidaActual].TerminarPartida();
                torneo.PartidaActual = (torneo.PartidaActual + 1) % torneo.partidas.Count;
                return true;
            }
            public void Reset()
            {
            }
        }
    }
    public class DosDos : Torneo
    {
        public List<Jugador> Calificacion { get; private set; }//Lista de calificacion del torneo Dos a Dos, caso jugadores
        public List<Equipo> EquiposCalificacion { get; private set; }//Lista de calificacion del torneo Dos a Dos, caso equipos
        public DosDos(Juego _juego)
        {
            iterador = new IteradorTorneos(this);//crea una instancia del Iterador de torneos
            tipoTorneo = GetType(); // guarda el tipo del torneo
            juego = _juego.GetType(); // guarda el tipo del juego que tiene el torneo
            juegoInstancia = _juego; //guarda una instancia del juego del torneo 
            Calificacion = new List<Jugador>();
            EquiposCalificacion = new List<Equipo>();
            Equipos = new List<Equipo>();
            jugadores = new List<Jugador>();
            partidas = new List<Partida>();
            AddJuegosPermitidos();//agrega juegos permitidos
            if (!ElJuegoEstaPermitido())//lanza exepcion en caso que no se permita el juego...nunk se llega q esto dado que el visual no lo permite
                throw new ArgumentException("No se puede jugar el juego en este torneo");
        }
        internal void AddJuegosPermitidos()
        {
            juegosPermitidos = new List<Type>();
            juegosPermitidos.Add(new TicTacToe().GetType());
            juegosPermitidos.Add(new Artifact().GetType());
        }
        public override void CrearPartidas()
        {
            //Crea las partidas todos contra todos tipo de partidas de dos jugadores(o equipos) para cada uno de los casos segun el estilo del 
            //juego (booleano que devuelve si el juego es o no en equipos)

            if (!juegoInstancia.EsEnEquipos)
                for (int i = 0; i < jugadores.Count; i++)
                    for (int ii = i + 1; ii < jugadores.Count; ii++)
                    {
                        AddPartida();
                        partidas[partidas.Count - 1].AddJugador(jugadores[i]);
                        partidas[partidas.Count - 1].AddJugador(jugadores[ii]);
                    }
            else
            {
                for (int i = 0; i < jugadores.Count; i++)
                    for (int ii = i + 1; ii < jugadores.Count; ii++)
                    {
                        AddPartida();
                        partidas[partidas.Count - 1].AddEquipo(Equipos[i]);
                        partidas[partidas.Count - 1].AddEquipo(Equipos[ii]);
                    }
            }
        }
        public override void ProximaJugada()
        {
            //Itera la proxima jugada y comprueba si termino el torneo, dicha jugada se ejecuta en Juego 
            partidas[PartidaActual].ProximaJugada();
            Comprobar();
        }
        public override void TerminarJuego()
        {
            //Itera el proximo juego y comprueba si termino el torneo, juego se ejecuta en Partida
            partidas[PartidaActual].Iterar();
            Comprobar();
        }
        public override void TerminarPartida()
        {
            //Itera la proxima jugada y comprueba si termino el torneo
            iterador.MoveNext();
            Comprobar();
        }
        public override void TerminarTorneo()
        {
            //Llama al iterador.moveNext hasta que se termina el torneo
            while (iterador.MoveNext()) { Comprobar(); }
            TerminoElTorneo = true;
        }
        void Comprobar()
        {
            //Comprueba Partida por Partida si ya acabaron y si todas acabaron entonces el torneo termina
            for (int i = 0; i < partidas.Count; i++)
            {
                if (!partidas[i].TerminoLaPartida()) return;
            }
            TerminoElTorneo = true;
            Bitacora.AddTorneo("Termino el Torneo de Dos a Dos");
            ImprimirCalificacion();
        }
        void Calificar()
        {
            //Usa la calificacion Temporal de un jugador(o equipo)para crear la tabla de calificacion
            if (juegoInstancia.EsEnEquipos)
            {
                while (Equipos.Count > 0)
                {
                    Equipo temporal = Equipos[0];
                    for (int i = 0; i < jugadores.Count; i++)
                        if (jugadores[i].Puntuacion > temporal.Puntuacion)
                            temporal = Equipos[i];
                    EquiposCalificacion.Add(temporal);
                    Equipos.Remove(temporal);
                }
            }
            else
            {
                while (jugadores.Count > 0)
                {
                    Jugador temporal = jugadores[0];
                    for (int i = 0; i < jugadores.Count; i++)
                        if (jugadores[i].Puntuacion > temporal.Puntuacion)
                            temporal = jugadores[i];
                    Calificacion.Add(temporal);
                    jugadores.Remove(temporal);
                }
            }
        }
        void ImprimirCalificacion()
        {
            //Imprime la Lista de Calificacion
            Calificar();
            Bitacora.AddTorneo("Calificacion: ");

            if (!juegoInstancia.EsEnEquipos)
                for (int i = 0; i < Calificacion.Count; i++)
                    Bitacora.AddTorneo((int)(i + 1) + ") " + Calificacion[i].ToString() + " : " + Calificacion[i].Puntuacion);
            else
            {
                for (int i = 0; i < Calificacion.Count; i++)
                    Bitacora.AddTorneo((int)(i + 1) + ") " + EquiposCalificacion[i].ToString() + " : " + EquiposCalificacion[i].Puntuacion);
            }
        }
    }
    public class Titulo : Torneo
    {
        int posicion = 0;
        Jugador Ganador;
        Equipo EquipoGanador;
        public Titulo(Juego _juego)
        {
            iterador = new IteradorTorneos(this);
            tipoTorneo = GetType();
            juego = _juego.GetType();
            juegoInstancia = _juego;
            jugadores = new List<Jugador>();
            Equipos = new List<Equipo>();
            partidas = new List<Partida>();
            AddJuegosPermitidos();
            if (!ElJuegoEstaPermitido())
                throw new ArgumentException("No se puede jugar el juego en este torneo");
        }
        internal void AddJuegosPermitidos()
        {
            juegosPermitidos = new List<Type>();
            juegosPermitidos.Add(new TicTacToe().GetType());
            juegosPermitidos.Add(new Artifact().GetType());
        }
        public override void AddJugador(Jugador jugador)
        {
            base.AddJugador(jugador);
            Ganador = jugadores[0];
        }
        public override void AddEquipo(Equipo equipo)
        {
            base.AddEquipo(equipo);
            EquipoGanador = Equipos[0];
        }
        public override void CrearPartidas()
        {
            if (!juegoInstancia.EsEnEquipos)
            {
                if (posicion == jugadores.Count)
                {
                    if (partidas[partidas.Count - 1].Ganador != null)
                        Ganador = partidas[partidas.Count - 1].Ganador;

                    Bitacora.AddTorneo("Termino el torneo de Titulo");
                    Bitacora.AddTorneo("El ganador es: " + Ganador.ToString());

                    TerminoElTorneo = true;
                    return;
                }
                try
                {
                    if (partidas[partidas.Count - 1].Ganador != null)
                        Ganador = partidas[partidas.Count - 1].Ganador;
                }
                catch { }

                AddPartida();
                partidas[partidas.Count - 1].AddJugador(Ganador);

                for (int i = posicion; i < jugadores.Count; i++)
                {
                    if (jugadores[i] != Ganador)
                    { partidas[partidas.Count - 1].AddJugador(jugadores[i]); posicion = i + 1; break; }
                }
            }
            else
            {
                if (posicion == Equipos.Count)
                {
                    if (partidas[partidas.Count - 1].EquipoGanador != null)
                        EquipoGanador = partidas[partidas.Count - 1].EquipoGanador;

                    Bitacora.AddTorneo("Termino el torneo de Titulo");
                    Bitacora.AddTorneo("El ganador es: " + EquipoGanador.ToString());

                    TerminoElTorneo = true;
                    return;
                }

                try
                {
                    if (partidas[partidas.Count - 1].EquipoGanador != null)
                        EquipoGanador = partidas[partidas.Count - 1].EquipoGanador;
                }
                catch { }
                AddPartida();
                partidas[partidas.Count - 1].AddEquipo(EquipoGanador);

                for (int i = posicion; i < Equipos.Count; i++)
                {
                    if (Equipos[i] != EquipoGanador)
                    { partidas[partidas.Count - 1].AddEquipo(Equipos[i]); posicion = i + 1; break; }
                }
            }
        }
        public override void ProximaJugada()
        {
            if (!TerminoElTorneo)
            {
                partidas[PartidaActual].ProximaJugada();
                if (partidas[partidas.Count - 1].TerminoLaPartida()) CrearPartidas();
            }
        }
        public override void TerminarJuego()
        {
            if (!TerminoElTorneo)
            {
                partidas[PartidaActual].TerminarJuego();
                if (partidas[partidas.Count - 1].TerminoLaPartida()) CrearPartidas();
            }
        }
        public override void TerminarPartida()
        {
            iterador.MoveNext();
            if (partidas[partidas.Count - 1].TerminoLaPartida()) CrearPartidas();
        }
        public override void TerminarTorneo()
        {
            PartidaActual = partidas.Count - 1;
            while (iterador.MoveNext()) { CrearPartidas(); PartidaActual = partidas.Count - 1; }
            PartidaActual = 0;
        }
    }
}