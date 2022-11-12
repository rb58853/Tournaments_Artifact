using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public class TicTacToe : Juego
    {
        Iterador iterador;
        static Random random = new Random();
        public int JugadorActual { get; private set; } //Define el jugador que debe jugar
        public bool Empate { get; private set; }//Define si el juego termino o no en empate
        public List<Tuple<int, int>> jugadasValidas { get; private set; }//Son las jugadas validas del juego... en este caso estan precreadas
        public List<Tuple<Jugador, int, int>> jugadas { get; private set; }//Guarda el progreso del juego jugad por jugada
        public string[] Parser { get; private set; }//Parsea el tablero a string "x" y "o" para llevarlos al visual
        public enum marca { vacio, x, o };//enum que define el tipo de tablero
        public marca[,] tablero { get; private set; }//Tablero de posiciones

        public TicTacToe()
        {
            /// <summary>
            /// Aqui debemos definir el tipo de partida del Tic Tac Toe, la misma es una clase interna...tansolo debemos definirla
            /// Instanciar(iniciar) el Parser,los jugadores,las jugadas, el tablero y agregar las jugadas validas(En el caso particular de este juego
            /// las jugadas validas estan precreadas en un metodo y no es necesario actualizarlas en cada iteracion...solo debemos eliminar la usada)
            /// </summary>
            Tipo = GetType();
            torneosPermitidos = new List<Type>();
            JugadorActual = 0;
            partida = new Partida().GetType();
            Parser = new string[9];
            jugadores = new List<Jugador>();
            jugadas = new List<Tuple<Jugador, int, int>>();
            AddJugadasValidas();
            tablero = new marca[3, 3];
            iterador = new Iterador(this);
        }
        public void GuardarJugada(Jugador jugador, int x, int y)
        {
            //Aqui se guarda la jugada hecha y se agrega la notificacion a la bitacora
            jugadas.Add(new Tuple<Jugador, int, int>(jugador, x, y));
            if (jugador == jugadores[0])
                Bitacora.AddJugada(jugador.Nombre + " jugó una ¨X¨ en la posicion <" + x + "," + y + ">.");
            else
                Bitacora.AddJugada(jugador.Nombre + " jugó una ¨O¨ en la posicion <" + x + "," + y + ">.");
        }
        void AddJugadasValidas()
        {
            //Se crean las jugadas validas para la nueva instancia de Tic Tac Toe
            jugadasValidas = new List<Tuple<int, int>>();
            for (int i = 0; i < 3; i++)
                for (int ii = 0; ii < 3; ii++)
                    jugadasValidas.Add(new Tuple<int, int>(i, ii));
        }
        public override void AddJugador(Jugador jugador)
        {
            //Agrega un jugador al juego
            if (jugadores.Count < 2)
                jugadores.Add(jugador);
            else throw new ArgumentException("LLeno");
        }
        void ComprobarFin()
        {
            //Este metodo comprueba si el juego termino... es decir comprueba si un jugador gano(reglas conocidas por todos) o 
            //si el teblero esta lleno en este caso el juego termina en empate

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                if (tablero[i, 0] != marca.vacio)
                    if (tablero[i, 0] == tablero[i, 1] && tablero[i, 1] == tablero[i, 2]) { ganador = jugadores[JugadorActual]; Termino = true; return; }
                if (tablero[0, i] != marca.vacio)
                    if (tablero[0, i] == tablero[1, i] && tablero[1, i] == tablero[2, i]) { ganador = jugadores[JugadorActual]; Termino = true; return; }
            }
            if (tablero[1, 1] != marca.vacio)
            {
                if (tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2]) { ganador = jugadores[JugadorActual]; Termino = true; return; }
                if (tablero[0, 2] == tablero[1, 1] && tablero[1, 1] == tablero[2, 0]) { ganador = jugadores[JugadorActual]; Termino = true; return; }
            }
            if (jugadasValidas.Count == 0)
            { Empate = true; Termino = true; }
        }
        public override void Jugar()
        {
            ///Devuelve la jugada que se debe realizar en caso de que exita... y agrega lo necesario a la bitacora

            if (jugadasValidas.Count == 9)//si todas las jugadas validas existen esta es la primera jugada, por tanto "Comienza el juego"
                Bitacora.AddJuego("Comenzo el juego " + jugadores[0].Nombre + " vs " + jugadores[1].Nombre);

            marca[,] tableroTemporal = ClonarTablero(tablero);
            jugadores[JugadorActual].Jugar(this);//El jugador se encarga de hacer su jugada es este juego(this)
            if (HuboTrampa(tableroTemporal, tablero))
            {
                //Se comprueba si hubo algun movimiento invalido en ese caso el jugador actual pierde acto seguido
                ganador = jugadores[(JugadorActual + 1) % 2];
                Termino = true;
            }
            ComprobarFin();//Comprueban si termina el juego
            JugadorActual = (JugadorActual + 1) % 2;//Cambia el jugador actual una vez juegue
            if (Termino)
            {
                //Esta parte solo existe para agreagar notificaciones a la bitacora y puntuar el empate
                if (Empate)
                {
                    jugadores[0].Puntuacion += 1;
                    jugadores[1].Puntuacion += 1;
                    Bitacora.AddJuego("Termino el juego " + jugadores[0].Nombre + " vs " + jugadores[1].Nombre);
                    Bitacora.AddJuego("El juego termino en empate");
                    return;
                }
                Bitacora.AddJuego("Termino el juego " + jugadores[0].Nombre + " vs " + jugadores[1].Nombre);
                Bitacora.AddJuego("El ganador del juego fue " + ganador);
                ganador.Puntuacion += 3;
            }
        }
        public override void Iterar()
        {
            //Itera por cada jugada (delegate void Jugadas) y acto seguido ejecuta el Current 
            iterador.MoveNext();
            iterador.Current();
        }
        marca[,] ClonarTablero(marca[,] _tablero)
        {
            //devuelve una instacia clonada de marca[,] _tablero
            marca[,] resultado = new marca[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    resultado[i, j] = _tablero[i, j];
            return resultado;
        }
        bool HuboTrampa(marca[,] tablero0, marca[,] tablero1)
        {
            ///comprueba que no se haya hecho una jugada invalida ...si el tablero 
            if (EspaciosVacios(tablero0) <= EspaciosVacios(tablero1)) return true;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tablero0[i, j] != marca.vacio && tablero1[i, j] != marca.vacio)
                        if (tablero0[i, j] != tablero1[i, j]) return true;
            return false;
        }
        int EspaciosVacios(marca[,] tablero0)
        {
            //Devuelve la cantidad de espacios vacios que tiene el tablero
            int resultado = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tablero0[i, j] == marca.vacio) resultado++;
            return resultado;
        }

        #region JugadaAleatoria
        public override void JugadaAleatoria()
        {
            // El juego se encarga de generar la proxima jugada de forma aleatoria para que el jugador aleatorio pase a ejecutarla 
            int n = random.Next(0, jugadasValidas.Count);
            int x = jugadasValidas[n].Item1;//Asigna la posicion de la jugada valida n a las x
            int y = jugadasValidas[n].Item2;//Asigna la posicion de la jugada valida n a las y
            //Marca la "x" o "o" segun el jugador que sea
            if (JugadorActual == 0)
            { tablero[x, y] = marca.x; Parser[x * 3 + y] = "x"; }
            else
            { tablero[x, y] = marca.o; Parser[x * 3 + y] = "o"; }
            jugadasValidas.Remove(jugadasValidas[n]);//Elimina la jugada valida utilizada
            GuardarJugada(jugadores[JugadorActual], x, y);//Guarda la jugada realizada
        }
        #endregion
        #region JugadaGolosa
        bool GanaJugador1()
        {
            //Devuelve si con el Tablero actual gana el jugador 1(el tablero es modificado con un posible proximo estado antes de usar este metodo)
            for (int k = 0; k < 3; k++)
            {
                if ((tablero[k, 0] == tablero[k, 1] && tablero[k, 1] == tablero[k, 2] && tablero[k, 2] == marca.x)
                || (tablero[0, k] == tablero[1, k] && tablero[1, k] == tablero[2, k] && tablero[2, k] == marca.x))
                    return true;
            }
            if (tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2] && tablero[1, 1] == marca.x) return true;
            if (tablero[0, 2] == tablero[1, 1] && tablero[1, 1] == tablero[2, 0] && tablero[1, 1] == marca.x) return true;
            return false;
        }
        bool GanaJugador2()
        {
            //Devuelve si con el Tablero actual gana el jugador 1(el tablero es modificado con un posible proximo estado antes de usar este metodo)
            for (int k = 0; k < 3; k++)
            {
                if ((tablero[k, 0] == tablero[k, 1] && tablero[k, 1] == tablero[k, 2] && tablero[k, 2] == marca.o)
                || (tablero[0, k] == tablero[1, k] && tablero[1, k] == tablero[2, k] && tablero[2, k] == marca.o))
                    return true;
            }
            if (tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2] && tablero[1, 1] == marca.o) return true;
            if (tablero[0, 2] == tablero[1, 1] && tablero[1, 1] == tablero[2, 0] && tablero[1, 1] == marca.o) return true;
            return false;
        }
        public override void JugadaGolosa()
        {
            #region Ganar
            //Comprueba si puede ganar un una posible jugada de ser asi se mantiene el estado modificado, si no el estado vuelve a su normalidad
            for (int i = 0; i < jugadasValidas.Count; i++)
            {
                int x = jugadasValidas[i].Item1;
                int y = jugadasValidas[i].Item2;

                if (JugadorActual == 0)
                {
                    tablero[x, y] = marca.x;//Aqui se varia el estado del tablero
                    if (GanaJugador1())
                    { GuardarJugada(jugadores[0], x, y); Parser[x * 3 + y] = "x"; jugadasValidas.Remove(jugadasValidas[i]); return; }
                    else tablero[x, y] = marca.vacio;//Aqui el estado vuelve a la normalidad ....analogo en lo proximo
                }
                else
                {
                    tablero[x, y] = marca.o;
                    if (GanaJugador2())
                    { GuardarJugada(jugadores[1], x, y); Parser[x * 3 + y] = "o"; jugadasValidas.Remove(jugadasValidas[i]); return; }
                    else tablero[x, y] = marca.vacio;
                }
            }
            #endregion
            #region Evitar Derrota
            //Comprueba si puede perder un una posible jugada de ser asi evita este estado jugando en la posicion en la cual gana el contrario...

            for (int i = 0; i < jugadasValidas.Count; i++)
            {
                int x = jugadasValidas[i].Item1;
                int y = jugadasValidas[i].Item2;

                if (JugadorActual == 0)
                {
                    tablero[x, y] = marca.o;
                    if (GanaJugador2())
                    { GuardarJugada(jugadores[0], x, y); Parser[x * 3 + y] = "x"; tablero[x, y] = marca.x; jugadasValidas.Remove(jugadasValidas[i]); return; }
                    else tablero[x, y] = marca.vacio;
                }
                else
                {
                    tablero[x, y] = marca.x;
                    if (GanaJugador1())
                    { GuardarJugada(jugadores[1], x, y); Parser[x * 3 + y] = "o"; tablero[x, y] = marca.o; jugadasValidas.Remove(jugadasValidas[i]); return; }
                    else tablero[x, y] = marca.vacio;
                }
            }
            #endregion
            //Si el jugador no puede ganar o evitar la derrota en una jugada entonces juega aleatorio
            JugadaAleatoria();
        }


        #endregion
        #region Iterador
        public delegate void Jugadas();
        internal class Iterador : IEnumerator<Jugadas>
        {
            TicTacToe juego;
            public Iterador(TicTacToe _juego)
            { juego = _juego; }
            public Jugadas Current { get; protected set; }
            object IEnumerator.Current { get => Current; }
            public void Dispose() { }
            public bool MoveNext()
            {
                // itera por cada jugada (void) hasta que termine el juego, una vez termina el mismo devuelve falso;
                if (juego.Termino) return false;
                Current = new Jugadas(juego.Jugar);
                return true;
            }
            public void Reset() { }
        }
        #endregion
        #region Partida
        internal class Partida : LogicoNuevo.Partida
        {
            Iterador iterador;//Iterador (Enumerator) de jugadas
            public Partida()
            {
                //La partida se inicializa con un iterador, la lista de jugadores y juego vacias
                iterador = new Iterador(this);
                jugadores = new List<Jugador>();
                juegos = new List<Juego>();
            }
            public override bool EnEjecucion()
            {
                //Si el tablero esta vacio o se termino el juego entonces no esta en ejecucion 
                if (TerminoLaPartida()) return false;
                for (int i = 0; i < 3; i++)
                    for (int ii = 0; ii < 3; ii++)
                        if ((juegos[0] as TicTacToe).tablero[i, ii] != marca.vacio) return true;
                return false;
            }
            public override bool TerminoElJuego()
            {
                //La partida de Tic Tac Toe termina con un unico Juego...asi lo defino 
                if (juegos[juegos.Count - 1].ganador != null)
                    Ganador = juegos[juegos.Count - 1].ganador;
                if (juegos[juegos.Count - 1].equipoGanador != null)
                    EquipoGanador = juegos[juegos.Count - 1].equipoGanador;

                return juegos[juegos.Count - 1].Termino;
            }
            public override bool TerminoLaPartida()
            {
                //Devuelve si termino la partida
                return juegos[juegos.Count - 1].Termino;
            }
            public override void AddJuego()
            {
                juegos.Add(new TicTacToe());//Agrega un juego a la partida
                AddJugadoresAlJuegoActual();//Intenta agregar los jugadores al juego dado
            }
            public override bool TodosEstanDentro()
            {
                //si hay dos jugadores dentro entonces ya estan todos
                return jugadores.Count == 2;
            }
            public override string ToString()
            {
                return jugadores[0].Nombre + " vs " + jugadores[1].Nombre;
            }
            public override void Iterar()
            {
                //Itera por cada juego para ejecutarlo
                iterador.MoveNext();
            }
            public override void TerminarPartida()
            {
                //Itera por cada juego para terminar la partida
                while (iterador.MoveNext()) ;
            }
            internal class Iterador : IEnumerator
            {
                Partida partida;
                public Iterador(Partida _partida)
                {
                    partida = _partida;
                }
                public object Current => throw new NotImplementedException();
                public bool MoveNext()
                {
                    // itera por cada juego hasta que termine la Partida, una vez termina el mismo devuelve falso;
                    if (partida.TerminoLaPartida()) return false;
                    partida.TerminarJuego();
                    return true;
                }
                public void Reset() { }
            }
        }
        #endregion
    }
}