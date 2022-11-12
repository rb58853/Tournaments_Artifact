using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public delegate void TemporizadorDelgate();
    public class Artifact : Juego
    {
        Iterador iterador;//iterador(Enumerator) de jugadas
        public static Random random = new Random();
        public TemporizadorDelgate EventoTiempo; //Este evento es para crecer el contador de cada temporizador 
        public Bando Dire { get; private set; }//Bando de los dire
        public Bando Radiant { get; private set; }//Bando de los Radiant
        public Bando bandoGanador;//El bando ganador es null hast finalizar el juego
        public int Turno;//devuelve la cantidad de turnos que se han jugado
        public int lineaActual;//numero de la linea en la que se esta jugando
        public bool Visual;// para definir el tipo de juego, en caso de ser visual el logico depende de scripts visuales...los torneos son no Visual
        public enum turno { Radiant, Dire, Tienda }
        public turno turnoActual { get; private set; }//Devuelve el turno en el que se esta jugando
        public bool CreepsDefaults;//Solo se utiliza en Modo Visual
        public int l1; public int l2; public int l3; public int l4; //Solo se utiliza en Modo Visual

        public Artifact()
        {
            iterador = new Iterador(this);
            Tipo = GetType();
            torneosPermitidos = new List<Type>();
            partida = new Partida().GetType();

            jugadores = new List<Jugador>();
            Turno = 1;
        }
        public void Gana(Jugador jugador)
        {
            //Hace que gane el jugador dado
            ganador = jugador;
        }
        public void Terminar()
        {
            //Hace terminar el juego...este metodo es llamado en la clase Estructura
            Termino = true;
            Bitacora.AddJuego("Termino el juego " + jugadores[0].Nombre + " vs " + jugadores[1].Nombre);
            Bitacora.AddJuego("El ganador fue " + ganador);
            ganador.Puntuacion += 3;
        }
        public void ComenzarJuegoNoVisual()
        {
            //en este metodo se crea todo lo default del inicio del juego
            Bitacora.AddJuego("Comenzo el juego " + jugadores[0] + " vs " + jugadores[1]);
            for (int i = 0; i < 3; i++)
            {
                Radiant.lineas[i].AddUnidad(Radiant.mazo.heroes[i]);//agrega un heroe en cada linea
                Dire.lineas[i].AddUnidad(Dire.mazo.heroes[i]);      //agrega un heroe en cada linea
            }

            Radiant.lineas[0].AddUnidad(new MeleeCreep()); //agrega una unidad Melee Creep en la linea dada
            Radiant.lineas[0].AddUnidad(new MeleeCreep()); //agrega una unidad Melee Creep en la linea dada
            Dire.lineas[0].AddUnidad(new MeleeCreep());    //agrega una unidad Melee Creep en la linea dada
            Radiant.lineas[1].AddUnidad(new MeleeCreep()); //agrega una unidad Melee Creep en la linea dada
            Dire.lineas[1].AddUnidad(new MeleeCreep());    //agrega una unidad Melee Creep en la linea dada
            Dire.lineas[2].AddUnidad(new MeleeCreep());    //agrega una unidad Melee Creep en la linea dada
            Dire.lineas[2].AddUnidad(new MeleeCreep());    //agrega una unidad Melee Creep en la linea dada
            Radiant.lineas[2].AddUnidad(new MeleeCreep()); //agrega una unidad Melee Creep en la linea dada

            for (int i = 0; i < 5; i++)
            {
                RobarCarta(Radiant);
                RobarCarta(Dire);
            }

            Temporizador temporizadorHeroe3 = new Temporizador(1); //Crea un temporizador de un turno para el 4to Heroe
            temporizadorHeroe3.Evento += RevivirHeroe; //Agrega el Metodo revivir al Temporizador
            temporizadorHeroe3.AddUnidad(Radiant.mazo.heroes[3]); //Agrega la unidad al temporizador
            temporizadorHeroe3.Activar();//activa el temporizadors

            Temporizador temporizadorHeroe4 = new Temporizador(2);
            temporizadorHeroe4.AddUnidad(Radiant.mazo.heroes[4]);
            temporizadorHeroe4.Evento += RevivirHeroe;
            temporizadorHeroe4.Activar();

            Radiant.mazo.heroes[3].Matar();
            Radiant.mazo.heroes[4].Matar();

            Temporizador temporizadorHeroe3Dire = new Temporizador(1);
            temporizadorHeroe3Dire.Evento += RevivirHeroe;
            temporizadorHeroe3Dire.AddUnidad(Dire.mazo.heroes[3]);
            temporizadorHeroe3Dire.Activar();

            Temporizador temporizadorHeroe4Dire = new Temporizador(2);
            temporizadorHeroe4Dire.AddUnidad(Dire.mazo.heroes[4]);
            temporizadorHeroe4Dire.Evento += RevivirHeroe;
            temporizadorHeroe4Dire.Activar();

            Dire.mazo.heroes[3].Matar();
            Dire.mazo.heroes[4].Matar();

        }
        void RevivirHeroe(Unidad unidad)
        {
            (unidad as Heroe).Renacer();
        }
        public void RobarCarta(Bando bando)
        {
            bando.mazo.MazoMano(bando.mazo.cartas[bando.mazo.cartas.Count - 1]);
        }
        public void NuevoTurno()
        {
            if (Visual)
            {
                if (Identidad.Conexion == Identidad.TipoDeRed.Servidor)
                {
                    Radiant.InstanciarDosCreepsVisuales();
                    Dire.InstanciarDosCreepsVisuales();
                }
            }
            else
            {
                Radiant.RenacerHeroeNoVisual();
                Dire.RenacerHeroeNoVisual();
                RobarCarta(Radiant);
                RobarCarta(Radiant);
                RobarCarta(Dire);
                RobarCarta(Dire);
                Radiant.InstanciarDosCreepsNoVisuales();
                Dire.InstanciarDosCreepsNoVisuales();
            }

        }
        public void CambiarTurno()
        {
            if (turnoActual == turno.Radiant)
            {
                turnoActual = turno.Dire;
            }
            else
            {
                turnoActual = turno.Radiant;
                AumentarLinea();
            }
            if (Visual)
                AccionesGlobales.cambiarTurno();
        }
        void AumentarLinea()
        {
            ActualizarLosAtaquesEnLaLineaActual();
            if (!Visual)
            {
                if (lineaActual != 3)
                {
                    AtacarEnLinea(lineaActual);
                    Radiant.lineas[lineaActual].estructura.Refrescar();
                    Dire.lineas[lineaActual].estructura.Refrescar();
                }
                lineaActual = (lineaActual + 1) % 4;
                if (lineaActual == 0)
                    AumentarTurno();
            }
        }
        void ActualizarLosAtaquesEnLaLineaActual()
        {
            ///Actualiza las unidades que debe atacar izquierda, centro o dercha
            if (Visual)
            {
                if (lineaActual == 2) return;

                for (int i = 0; i < Radiant.lineas[0].Unidades.Length; i++)
                {
                    try { Radiant.lineas[(lineaActual + 1) % 4].Unidades[i].ActualizarLaProximaUnidad_a_Atacar(); } catch { };
                    try { Dire.lineas[(lineaActual + 1) % 4].Unidades[i].ActualizarLaProximaUnidad_a_Atacar(); } catch { };
                }
            }
            else
            {
                if (lineaActual == 3) return;

                for (int i = 0; i < Radiant.lineas[0].Unidades.Length; i++)
                {
                    try { Radiant.lineas[(lineaActual) % 4].Unidades[i].ActualizarLaProximaUnidad_a_Atacar(); } catch { };
                    try { Dire.lineas[(lineaActual) % 4].Unidades[i].ActualizarLaProximaUnidad_a_Atacar(); } catch { };
                }
            }
        }
        public void AumentarTurno()
        {
            //Aumenta el turno
            ////importante EventoTiempo Crece el contador de cada uno de los temporisadores de las cartas que se usen en el juego 
            Turno++;
            NuevoTurno();
            AumentarManaTorres();
            try
            { EventoTiempo(); }
            catch { }
        }
        void AumentarManaTorres()
        {
            //Aumenta el mana total de las torres en 1

            for (int i = 0; i < 3; i++)
            {
                Radiant.lineas[i].estructura.AumentarMana();
                Dire.lineas[i].estructura.AumentarMana();
            }
        }
        public void AtacarEnLinea(int n)
        {
            // Ataca en la linea actual 
            int maximo = 0;
            try
            {
                maximo = Math.Max(Dire.lineas[n].Unidades.Length, Radiant.lineas[n].Unidades.Length);
                for (int i = 0; i < maximo; i++)
                {
                    try { Radiant.lineas[n].Unidades[i].Atacar(); } catch { }
                    try { Dire.lineas[n].Unidades[i].Atacar(); } catch { }
                }
            }
            catch { }
            maximo = 7;
            for (int i = 0; i < maximo; i++)
            {
                try
                {
                    if (Radiant.lineas[n].Unidades[i].DebeMorir)
                        Radiant.lineas[n].Unidades[i].Matar();
                    if (Radiant.lineas[n].Unidades[i].Muerta)
                        Radiant.lineas[n].MatarUnidad(Radiant.lineas[n].Unidades[i]);
                }
                catch { }
                try
                {
                    if (Dire.lineas[n].Unidades[i].DebeMorir)
                        Dire.lineas[n].Unidades[i].Matar();
                    if (Dire.lineas[n].Unidades[i].Muerta)
                        Dire.lineas[n].MatarUnidad(Dire.lineas[n].Unidades[i]);
                }
                catch { }
                if (Radiant.lineas[n].estructura.RecibioDamage)
                    Radiant.lineas[n].estructura.RecibeDamage = true;
                if (Dire.lineas[n].estructura.RecibioDamage)
                    Dire.lineas[n].estructura.RecibeDamage = true;
            }
        }
        public void AddRadiant(Mazo mazo)
        {
            //Agrega el mazo a los radiant
            Radiant = new Bando(mazo, this);
            Radiant.AddJugador(jugadores[0]);
            Radiant.nombre = "Radiant";
        }
        public void AddDire(Mazo mazo)
        {
            Dire = new Bando(mazo, this);
            Dire.AddJugador(jugadores[1]);
            Dire.nombre = "Dire";
        }
        public override void AddJugador(Jugador jugador)
        {
            jugadores.Add(jugador);
        }
        public override void JugadaAleatoria()
        {
            if (Visual)
            {
                if (turnoActual == turno.Tienda)
                {
                    Radiant.JugadaAleatoriaVisual();
                    Dire.JugadaAleatoriaVisual();
                    return;
                }
                if (turnoActual == turno.Radiant)
                    Radiant.JugadaAleatoriaVisual();
                else Dire.JugadaAleatoriaVisual();
            }
            else
            {
                //De forma random hay una probabilidad de 1/3 de pasar el turno , sino juega
                if (random.Next(0, 3) == 0)
                    CambiarTurno();
                else
                    JugadaGolosa();
            }
        }
        public override void JugadaGolosa()
        {
            //Jugara Hasta que existan jugadas validas en otro caso pas el turno
            if (!Visual)
            {
                if (turnoActual == turno.Radiant)
                    Radiant.JugadaGolosaNoVisual();
                else
                    Dire.JugadaGolosaNoVisual();
            }
        }
        public override void Jugar()
        {
            if (Visual)
            {
                if (lineaActual == 3)
                {
                    if (turnoActual == turno.Dire)
                    {
                        jugadores[0].Jugar(this);
                        jugadores[1].Jugar(this);
                    }
                    return;
                }
            }
            //Segun el turno juega el jugador que le corresponde 
            if (turnoActual == turno.Radiant)
                jugadores[0].Jugar(this);
            if (turnoActual == turno.Dire)
                jugadores[1].Jugar(this);
        }
        public override void Iterar()
        {
            //Itera cada Jugada
            iterador.MoveNext();
        }
        #region Partida
        internal class Partida : LogicoNuevo.Partida
        {
            bool comenzo;
            Iterador iterador;
            public Partida()
            {
                iterador = new Iterador(this);
                Equipos = new List<Equipo>();
                jugadores = new List<Jugador>();
                juegos = new List<Juego>();
            }
            public override bool EnEjecucion()
            {
                return comenzo && !TerminoLaPartida();
            }
            public override bool TerminoElJuego()
            {
                //La partida de Artifact termina con un unico Juego...asi lo defino 
                if (juegos[juegos.Count - 1].ganador != null)
                    Ganador = juegos[juegos.Count - 1].ganador;
                if (juegos[juegos.Count - 1].equipoGanador != null)
                    EquipoGanador = juegos[juegos.Count - 1].equipoGanador;

                return juegos[juegos.Count - 1].Termino;
            }
            public override bool TerminoLaPartida()
            {
                return juegos[juegos.Count - 1].Termino;
            }
            public override void AddJuego()
            {
                juegos.Add(new Artifact());
                AddJugadoresAlJuegoActual();
            }
            public override void AddJugadoresAlJuegoActual()
            {
                base.AddJugadoresAlJuegoActual();
                (juegos[juegos.Count - 1] as Artifact).AddRadiant(ListaMazos.mazos[random.Next(0, ListaMazos.mazos.Count)]);
                (juegos[juegos.Count - 1] as Artifact).AddDire(ListaMazos.mazos[random.Next(0, ListaMazos.mazos.Count)]);
            }
            public override void ProximaJugada()
            {
                if (!comenzo)
                {
                    comenzo = true;
                    Bitacora.AddPartida("Comenzo la partida " + ToString());
                    (juegos[juegos.Count - 1] as Artifact).ComenzarJuegoNoVisual();
                }

                // Itera la proxima jugada llama a la jugada del juego actual...variable para cada Partida
                //Bitacora.AddPartida("") agrega un string a las partidas(string) de la bitacora... 

                if (TerminoLaPartida()) return;
                if (!comenzo)
                    Bitacora.AddPartida("Comenzo la partida " + ToString());
                juegos[juegos.Count - 1].Iterar();
                //juegos[juegos.Count - 1].Jugar();
                if (TerminoElJuego() && !TerminoLaPartida())
                    AddJuego();
                if (TerminoLaPartida())
                    Bitacora.AddPartida("Termino la partida " + ToString() + ".");
            }
            public override bool TodosEstanDentro()
            {
                return jugadores.Count == 2;
            }
            public override string ToString()
            {
                return jugadores[0].Nombre + " vs " + jugadores[1].Nombre;
            }
            public override void Iterar()
            {
                iterador.MoveNext();
            }
            public override void TerminarPartida()
            { while (iterador.MoveNext()) ; }

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
                    // itera por cada jugada (void) hasta que termine el juego, una vez termina el mismo devuelve falso;
                    if (partida.TerminoLaPartida()) return false;
                    partida.TerminarJuego();
                    return true;
                }
                public void Reset()
                {
                }
            }
        }
        #endregion
        internal class Iterador : IEnumerator
        {
            Artifact juego;
            public Iterador(Artifact _juego)
            {
                juego = _juego;
            }

            public object Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                // itera por cada jugada (void) hasta que termine el juego, una vez termina el mismo devuelve falso;
                if (juego.Termino) return false;
                juego.Jugar();
                return true;
            }
            public void Reset()
            {

            }
        }
    }
    public class Bando
    {
        public bool ProximoAfinalizar;
        public Artifact juego { get; private set; }
        public int Dinero { get; private set; }
        public bool Victoria { get; private set; }
        public Mazo mazo { get; private set; }
        public Linea[] lineas;
        public Jugador jugador { get; private set; }
        public string nombre;

        public Bando(Mazo _mazo, Artifact _juego)
        {
            juego = _juego;
            lineas = new Linea[3] { new Linea(juego, this), new Linea(juego, this), new Linea(juego, this) };

            ClonarMazo(_mazo);
            mazo.Barajar();
        }
        void ClonarMazo(Mazo _mazo)
        {
            mazo = new Mazo(_mazo.Nombre);
            for (int i = 0; i < _mazo.cartas.Count; i++)
            {
                Carta temp = ListaCartas.GetCarta(_mazo.cartas[i].codigo);
                mazo.AddCarta(temp);
                mazo.cartas[mazo.cartas.Count - 1].bando = this;
                mazo.cartas[mazo.cartas.Count - 1].AddJuego(juego);
            }
            for (int i = 0; i < 5; i++)
            {
                Heroe temp = ListaCartas.GetCarta(_mazo.heroes[i].codigo) as Heroe;
                mazo.AddHeroe(temp);
                mazo.heroes[mazo.heroes.Count - 1].bando = this;
                mazo.heroes[mazo.heroes.Count - 1].AddJuego(juego);
            }
            for (int i = 0; i < _mazo.items.Count; i++)
            {
                Item temp = ListaCartas.GetCarta(_mazo.items[i].codigo) as Item;
                mazo.AddItems(temp);
                mazo.items[mazo.items.Count - 1].bando = this;
                mazo.items[mazo.items.Count - 1].AddJuego(juego);
            }
        }
        public void AddJugador(Jugador _jugador)
        {
            jugador = _jugador;
        }
        public void VariarDinero(int n)
        {
            Dinero += n;
        }
        public List<Tuple<int, int, int, int>> GenerarJugadasValidas()
        {
            List<Tuple<int, int, int, int>> jugadas = new List<Tuple<int, int, int, int>>();
            for (int i = 0; i < mazo.Mano.Count; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    for (int iii = -1; iii < lineas[0].Unidades.Length; iii++)
                    {
                        for (int iiii = 0; iiii < 2; iiii++)
                        {
                            Tuple<int, int, int, int> temporal = new Tuple<int, int, int, int>(i, ii, iii, iiii);
                            if (EsValida(temporal))
                                jugadas.Add(temporal);
                        }
                    }
                }
            }
            return jugadas;
        }
        public bool EsValida(Tuple<int, int, int, int> jugada)
        {
            if (juego.lineaActual == 3) return false;

            if (mazo.Mano[jugada.Item1].EsItem)
            {
                if (jugada.Item4 == 1)
                    return false;
                if (jugada.Item4 == 0)
                {
                    try { if (lineas[jugada.Item2].Unidades[jugada.Item3].EsHeroe) return true; else return false; } catch { return false; }
                }
            }

            if (jugada.Item2 != juego.lineaActual) return false; ///// esto hay que cambiarlo si exite algun hechizo que pinche en cualquier linea
                                                                 //////falta Lo de la unidad para ello implementar el bool es unidad;
            if (!mazo.Mano[jugada.Item1].PuedeJugarse()) return false;

            try
            {
                if (!(mazo.Mano[jugada.Item1] as Hechizo).Target)
                {
                    if (jugada.Item3 != -1)
                        return false;
                }
                else
                {
                    if (jugada.Item4 == 0)
                        try
                        {
                            if ((mazo.Mano[jugada.Item1] as Hechizo).Exepcion(lineas[jugada.Item2].Unidades[jugada.Item3]))
                                return false;
                        }
                        catch { return false; }

                    if (jugada.Item4 == 1)
                    {
                        try
                        {
                            if ((mazo.Mano[jugada.Item1] as Hechizo).Exepcion(mazo.heroes[0].bandoOpuesto().lineas[jugada.Item2].Unidades[jugada.Item3]))
                                return false;
                        }
                        catch
                        {
                            return false;
                        }
                    }

                }
            }
            catch { }
            return true;
        }
        public void JugadaAleatoriaVisual()
        {
            try
            {
                Tuple<int, int, int, int> jugada = GenerarJugadasValidas()[new Random().Next(0, GenerarJugadasValidas().Count)];
                Ordenar.EnviarUnaOrden(jugada);
            }
            catch
            {
                juego.CambiarTurno();
            }
        }
        public void JugadaGolosaNoVisual()
        {
            //Los Bots Aleatorios no Compran
            if (juego.lineaActual == 3)
            {
                juego.CambiarTurno();
                Bitacora.AddJugada(nombre + " Paso turno");
            }
            try
            {
                Tuple<int, int, int, int> orden = GenerarJugadasValidas()[new Random().Next(0, GenerarJugadasValidas().Count)];

                if (orden.Item1 == -1 && orden.Item2 == -1 && orden.Item3 == -1 && orden.Item4 == -1)
                {
                    juego.CambiarTurno();
                    Bitacora.AddJugada(nombre + " Paso turno");
                    return;
                }
                AccionCarta(orden);
            }
            catch
            {
                Bitacora.AddJugada(nombre + " Paso turno");
                juego.CambiarTurno();
            }
        }
        public void AccionCarta(Tuple<int, int, int, int> orden)
        {
            try
            {
                lineas[juego.lineaActual].AddUnidad(mazo.Mano[orden.Item1] as Unidad);
                mazo.Mano.Remove(mazo.Mano[orden.Item1]);
                return;

            }
            catch { };
            try
            {
                if (mazo.Mano[orden.Item1].EsItem)
                {
                    if ((mazo.Mano[orden.Item1] as Item).tipoItem == Item.TipoItem.Damage)
                        (lineas[orden.Item2].Unidades[orden.Item3] as Heroe).AddItemDamage(mazo.Mano[orden.Item1] as ItemDamage);
                    if ((mazo.Mano[orden.Item1] as Item).tipoItem == Item.TipoItem.Armadura)
                        (lineas[orden.Item2].Unidades[orden.Item3] as Heroe).AddItemArmadura(mazo.Mano[orden.Item1] as ItemArmadura);
                    if ((mazo.Mano[orden.Item1] as Item).tipoItem == Item.TipoItem.Vida)
                        (lineas[orden.Item2].Unidades[orden.Item3] as Heroe).AddItemVida(mazo.Mano[orden.Item1] as ItemVida);
                    mazo.Mano.Remove(mazo.Mano[orden.Item1]);
                    return;
                }
            }
            catch { }
            try
            {
                if (!(mazo.Mano[orden.Item1] as Hechizo).Target)
                {
                    (mazo.Mano[orden.Item1] as Hechizo).Efecto();
                    Bitacora.AddJugada(nombre + " activo la carta de hechizo " + mazo.Mano[orden.Item1]);
                    if (mazo.Mano[orden.Item1].Descripcion != null)
                        Bitacora.AddJugada(mazo.Mano[orden.Item1].Nombre + " " + mazo.Mano[orden.Item1].Descripcion);
                }
                if ((mazo.Mano[orden.Item1] as Hechizo).Target)
                {
                    if (orden.Item4 == 0)
                    {
                        (mazo.Mano[orden.Item1] as Hechizo).Efecto(lineas[juego.lineaActual].Unidades[orden.Item3]);
                        Bitacora.AddJugada(nombre + " activo la carta de hechizo " + mazo.Mano[orden.Item1] + " sobre la unidad " +
                            lineas[juego.lineaActual].Unidades[orden.Item3]);
                        if (mazo.Mano[orden.Item1].Descripcion != null)
                            Bitacora.AddJugada(mazo.Mano[orden.Item1] + mazo.Mano[orden.Item1].Descripcion);
                    }
                    if (orden.Item4 == 1)
                    {
                        (mazo.Mano[orden.Item1] as Hechizo).Efecto(mazo.Mano[orden.Item1].bandoOpuesto().lineas[juego.lineaActual].Unidades[orden.Item3]);

                        Bitacora.AddJugada(nombre + " activo la carta de hechizo " + mazo.Mano[orden.Item1] + " sobre la unidad " +
                            mazo.Mano[orden.Item1].bandoOpuesto().lineas[juego.lineaActual].Unidades[orden.Item3]);
                        if (mazo.Mano[orden.Item1].Descripcion != null)
                            Bitacora.AddJugada(mazo.Mano[orden.Item1] + mazo.Mano[orden.Item1].Descripcion);
                    }
                }
                mazo.Mano.Remove(mazo.Mano[orden.Item1]);
                return;
            }
            catch { }
            juego.CambiarTurno();
        }

        public void InstanciarDosCreepsVisuales()
        {
            // return;
            Random random = new Random();
            juego.CreepsDefaults = true;
            juego.l1 = random.Next(0, 3);
            juego.l2 = random.Next(0, 3);
            juego.l3 = random.Next(0, 3);
            juego.l4 = random.Next(0, 3);

            if (this == juego.Radiant)
            {
                InvocadorDeCreeps.InvocarCreepsRadiants(2);
            }
            else
            {
                InvocadorDeCreeps.InvocarCreepsDire(2);
            }
        }
        public void InstanciarDosCreepsNoVisuales()
        {
            int temp1 = Artifact.random.Next(0, 3);
            int temp2 = Artifact.random.Next(0, 3);
            MeleeCreep Temporal1 = new MeleeCreep();
            MeleeCreep Temporal2 = new MeleeCreep();
            Temporal1.AddJuego(juego);
            Temporal2.AddJuego(juego);
            Temporal1.bando = this;
            Temporal2.bando = this;

            if (!lineas[temp1].MesaLLena())
                lineas[temp1].AddUnidad(new MeleeCreep());
            else
            {
                if (!lineas[0].MesaLLena())
                    lineas[0].AddUnidad(new MeleeCreep());
                if (!lineas[1].MesaLLena())
                    lineas[1].AddUnidad(new MeleeCreep());
                if (!lineas[2].MesaLLena())
                    lineas[2].AddUnidad(new MeleeCreep());
            }
            if (!lineas[temp2].MesaLLena())
                lineas[temp2].AddUnidad(new MeleeCreep());
            else
            {
                if (!lineas[0].MesaLLena())
                    lineas[0].AddUnidad(new MeleeCreep());
                if (!lineas[1].MesaLLena())
                    lineas[1].AddUnidad(new MeleeCreep());
                if (!lineas[2].MesaLLena())
                    lineas[2].AddUnidad(new MeleeCreep());
            }
        }
        public void RenacerHeroeNoVisual()
        {
            for (int i = 0; i < 5; i++)
                if (!mazo.heroes[i].Muerta && mazo.heroes[i].LineaActual == null && !mazo.heroes[i].EstaEnMano)
                {
                    mazo.MazoMano(mazo.heroes[i]);
                    Bitacora.AddJugada(mazo.heroes[i] + " equipo " + mazo.heroes[i].Equipo + " renacio.");
                }
        }
    }
    public class Mazo
    {
        public string Nombre;// { get; private set; }
        public List<Carta> cartas;
        public List<Heroe> heroes;
        public List<Item> items;
        public List<Carta> Mano;

        public Mazo(string nombre)
        {
            heroes = new List<Heroe>();
            items = new List<Item>();
            cartas = new List<Carta>();
            Nombre = nombre;
            Mano = new List<Carta>();
        }
        public void AddCarta(Carta carta)
        {
            cartas.Add(carta);
        }
        public void AddHeroe(Heroe heroe)
        {
            heroes.Add(heroe);
        }
        public void AddItems(Item item)
        {
            items.Add(item);
        }
        public void CambiarNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void MazoMano(Carta carta)
        {
            Mano.Add(carta);
            carta.EstaEnMano = true;
            try { cartas.Remove(carta); } catch { }
        }
        public void Barajar()
        {
            List<Carta> temporal = new List<Carta>();
            for (int i = 0; i < cartas.Count; i++)
            {
                temporal.Add(cartas[i]);
            }
            cartas.Clear();
            while (temporal.Count > 0)
            {
                int temp = Artifact.random.Next(0, temporal.Count);
                cartas.Add(temporal[temp]);
                temporal.Remove(temporal[temp]);
            }
        }
        public void Organizar()
        {
            List<Carta> TempCartas = new List<Carta>();
            for (int i = 0; i < cartas.Count; i++)
            {

            }
        }
        public int PosicionEnMano(Carta carta)
        {
            for (int i = 0; i < Mano.Count; i++)
            {
                if (carta == Mano[i]) return i;
            }
            return -1;
        }
        public int CantidadDelTipo(Type type)
        {
            int resultado = 0;
            for (int i = 0; i < cartas.Count; i++)
            {
                if (cartas[i].GetType() == type)
                    resultado++;
            }
            return resultado;
        }
        public void EliminarTipo(Type type)
        {
            for (int i = 0; i < cartas.Count; i++)
            {
                if (cartas[i].GetType() == type)
                {
                    cartas.Remove(cartas[i]);
                    return;
                }
            }
            for (int i = 0; i < heroes.Count; i++)
            {
                if (heroes[i].GetType() == type)
                {
                    heroes.Remove(heroes[i]);
                    return;
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetType() == type)
                {
                    items.Remove(items[i]);
                    return;
                }
            }
        }

        public override string ToString()
        { return Nombre; }
    }
    public class Linea
    {
        Artifact juego;
        Bando bando;
        public bool Azul { get; protected set; }
        public bool Rojo { get; protected set; }
        public bool Negro { get; protected set; }
        public bool Verde { get; protected set; }
        public Estructura estructura { get; protected set; }
        public Unidad[] Unidades { get; private set; }
        int[] OrdenDeJuego = new int[7] { 3, 4, 2, 5, 1, 6, 0 };
        public int ultimaPosicionEnMesa;
        public Linea(Artifact _juego, Bando _bando)
        {
            juego = _juego;
            bando = _bando;
            Unidades = new Unidad[7];
            estructura = new Estructura(juego, bando);
        }
        public void AddUnidad(Unidad unidad)
        {
            if (unidad.Melee)
            {
                unidad.AddJuego(juego);
                unidad.bando = bando;
            }
            Unidades[ProximaPosicionEnMesa()] = unidad;
            try
            {
                Bitacora.AddJugada("Se invoco a " + unidad + " en la linea " + unidad.juego.lineaActual + " de los " + unidad.bando.nombre);
                if (unidad.Descripcion != null)
                    Bitacora.AddJugada(unidad + ": " + unidad.Descripcion);
            }
            catch { }

            if (!unidad.juego.Visual && !unidad.Melee)
                unidad.bando.lineas[unidad.juego.lineaActual].estructura.VariarMana(-unidad.CostoDeMana);

            unidad.LineaActual = this;
            try
            {
                unidad.ActualizarLaProximaUnidad_a_Atacar();
                unidad.bando.mazo.Mano.Remove(unidad);
                unidad.EstaEnMano = false;
            }
            catch { }
            unidad.Jugado();
            Colores();
        }
        public void MatarUnidad(Unidad unidad)
        {
            try
            {
                for (int i = 0; i < Unidades.Length; i++)
                {
                    if (Unidades[i] == unidad)
                    { Unidades[i] = null; break; }
                }
                Colores();
            }
            catch { }
        }


        public void Colores()
        {
            Azul = false; Rojo = false; Negro = false; Verde = false;

            for (int i = 0; i < Unidades.Length; i++)
            {
                Heroe temporal;
                try
                {
                    temporal = Unidades[i] as Heroe;
                    if (temporal.color == Carta.Color.Azul) Azul = true;
                    if (temporal.color == Carta.Color.Negro) Negro = true;
                    if (temporal.color == Carta.Color.Rojo) Rojo = true;
                    if (temporal.color == Carta.Color.Verde) Verde = true;
                }
                catch { continue; }
            }
        }
        public int ProximaPosicionEnMesa()
        {
            for (int i = 0; i < 7; i++)
            {
                if (Unidades[OrdenDeJuego[i]] == null)
                {
                    ultimaPosicionEnMesa = OrdenDeJuego[i];
                    return OrdenDeJuego[i];
                }
            }
            throw new NotImplementedException();
        }
        public bool MesaLLena()
        {
            for (int i = 0; i < Unidades.Length; i++)
            {
                if (Unidades[i] == null) return false;
            }
            return true;
        }
        public List<Heroe> Heroes()
        {
            List<Heroe> Resultado = new List<Heroe>();
            for (int i = 0; i < Unidades.Length; i++)
            {
                try
                {
                    if (Unidades[i].EsHeroe)
                        Resultado.Add(Unidades[i] as Heroe);
                }
                catch { }
            }
            return Resultado;
        }

    }
    public class Estructura
    {
        Artifact juego;
        Bando bando;
        public int Rebotar;
        public bool NoMana;
        public bool Ancestro { get; private set; }

        public bool RecibioDamage;
        public bool RecibeDamage;
        public int DamageRecibido;

        public int Vida { get; protected set; }
        public int Mana;//{ get; protected set; }
        public int VidaActual { get; protected set; }
        public int ManaActual { get; protected set; }
        public Estructura()
        {
            Vida = 40;
            VidaActual = 40;
            Mana = 3;
            ManaActual = 3;
        }

        public Estructura(Artifact artifact, Bando _bando)
        {
            juego = artifact; bando = _bando;
            Vida = 40;
            VidaActual = 40;
            Mana = 3;
            ManaActual = 3;
        }
        public void AumentarMana()
        {
            Mana++;
            ManaActual++;
        }
        public void Refrescar()
        {
            ManaActual = Mana;
        }
        public void VariarVida(int n)
        {
            VidaActual += n;
            if (VidaActual > Vida) VidaActual = Vida;
            if (VidaActual < 0)
            {
                if (bando.ProximoAfinalizar && !juego.Termino)
                {
                    Vida = 0;
                    VidaActual = 0;
                    juego.bandoGanador = bando.mazo.heroes[0].bandoOpuesto();
                    juego.Gana(juego.bandoGanador.jugador);
                    juego.Terminar();
                    return;
                }
                bando.ProximoAfinalizar = true;
                Ancestro = true;
                Vida = 80;
                VidaActual += 80;
            }
        }
        public void VariarManaMaximo(int n)
        {
            Mana += n;
            ManaActual += n;
            if (ManaActual < 0)
                ManaActual = 0;
        }
        public void VariarMana(int n)
        {
            ManaActual += n;
            if (ManaActual > Mana) ManaActual = Mana;
        }
    }

}