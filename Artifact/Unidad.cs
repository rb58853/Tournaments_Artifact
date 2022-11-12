using System;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public delegate void EventosVoid(Unidad unidad);

    public abstract class Carta
    {
        public int TotalDeEstaCarta { get; protected set; }
        public Artifact juego { get; private set; }
        public string Nombre { get; protected set; }
        public string Tipo { get; protected set; }
        public string Descripcion { get; protected set; }
        public int codigo { get; protected set; }
        public enum Team { Radiant, Dire }
        public enum Color { Negro, Rojo, Azul, Verde, Vacio }
        public enum TipoDeCarta { Unidad, Heroe, Hechizo, Item }
        public TipoDeCarta tipoDeCarta { get; protected set; }
        public Color color { get; protected set; }
        public Team Equipo;
        public Bando bando;
        public bool EstaEnMano;
        public bool EsItem;
        public override string ToString()
        {
            try
            {
                return Nombre;
            }
            catch { return "El nombre esta dando bateo"; }
        }
        public void AddCantidad(int n)
        {
            TotalDeEstaCarta = n;
        }
        public void AddJuego(Artifact artifact)
        {
            juego = artifact;
        }
        public Bando bandoOpuesto()
        {
            if (bando == juego.Radiant)
                return juego.Dire;
            else
                return juego.Radiant;
        }
        public virtual bool PuedeJugarse() { throw new NotImplementedException(); }
    }

    public class Unidad : Carta
    {
        public new TipoDeCarta tipoDeCarta = TipoDeCarta.Unidad;
        public EventosVoid Actualizar;

        public Update update;
        public List<Carta> Efectos = new List<Carta>();

        #region Efectos (Para Visual)
        public bool Hielo;
        public bool Fuego;
        public bool Rayo;
        public bool girar;
        #endregion

        public bool DebeMorir;
        public int Recompensa = 1;
        public enum Unidad_a_Atacar { Izquierda, Centro, Derecha }
        public Unidad_a_Atacar UnidadAtacada;
        public Unidad unidadAtacada;
        public Linea LineaActual;
        public Temporizador RegeneracionTimer;
        public bool Melee { get; protected set; }
        public Estructura estructura;
        public int CostoDeMana;
        public bool EsHeroe { get; protected set; }

        public Tuple<bool, int> AumentaArmadura;
        public Tuple<bool, int> AumentaDamage;
        public Tuple<bool, int> AumentaVida;
        public Tuple<bool, int> BajaVida;
        public Tuple<bool, int> BajaDamage;
        public Tuple<bool, int> BajaArmadura;

        public int Siege;
        public int SiegeActual;
        public int Hendidura;
        public int HendiduraActual;

        public bool RecibeDamage;
        public int DamageRecibido;// { get; protected set; }
        public bool Muerta { get; protected set; }
        public int Regeneracion;//{ get; protected set; }

        public float Damage { get; protected set; }
        public float DamageTotal;// { get; protected set; }
        public float DamageActual;// { get; protected set; }

        public float Armadura { get; protected set; }
        public float ArmaduraTotal;//{ get; protected set; }
        public float ArmaduraActual;// { get; protected set; }

        public float Vida { get; protected set; }
        public float VidaMaxima;// { get; protected set; }
        public float VidaActual { get; protected set; }
        public float PorcientoVida { get; protected set; }

        public bool Inmune;// { get; protected set; }
        public bool Silenciada { get; protected set; }
        public bool Desarmada { get; protected set; }

        public int Rebotar;
        public int RebotarActual;

        public void Atacar()
        {
            if (Muerta) return;
            unidadAtacada = null;
            try
            {
                if (UnidadAtacada == Unidad_a_Atacar.Izquierda)
                    unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea() - 1];

                if (UnidadAtacada == Unidad_a_Atacar.Centro)
                    unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()];

                if (UnidadAtacada == Unidad_a_Atacar.Derecha)
                    unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea() + 1];
            }
            catch
            {
                unidadAtacada = null;
            }
            if (unidadAtacada == null)
            {
                Atacar(bandoOpuesto().lineas[NumeroDeLineaActual()].estructura);
                return;
            }
            Atacar(unidadAtacada);
        }
        public virtual void Atacar(Unidad unidad)
        {

            if (Desarmada) { Regenerar(); AtacarTorreConSiege(unidad.LineaActual.estructura); return; }

            if (unidad.Inmune) { unidad.RecibeDamage = true; Regenerar(); return; }

            if ((-DamageActual) + unidad.ArmaduraActual < 0)
            {
                unidad.VariarVida((-DamageActual) + unidad.ArmaduraActual);
                unidad.RecibeDamage = true;
                unidad.DamageRecibido += (int)((-DamageActual) + unidad.ArmaduraActual);
                VariarVida(-unidad.RebotarActual);
            }

            if (HendiduraActual > 0)
            {
                //try { unidad.LineaActual.Unidades[unidad.PosicionEnLinea() + 1].VariarVida(-HendiduraActual); } catch { }
                //try { unidad.LineaActual.Unidades[unidad.PosicionEnLinea() - 1].VariarVida(-HendiduraActual); } catch { }
            }

            Regenerar();

            try { ComprobarMuerteEnDeUnidad(unidad.LineaActual.Unidades[unidad.PosicionEnLinea() + 1]); } catch { }
            try { ComprobarMuerteEnDeUnidad(unidad.LineaActual.Unidades[unidad.PosicionEnLinea() - 1]); } catch { }

            ComprobarMuerteEnDeUnidad(unidad);
        }
        public virtual void Atacar(Estructura estructura)
        {
            estructura.VariarVida(((int)-DamageActual));
            estructura.RecibioDamage = true;
            estructura.DamageRecibido += (int)-DamageActual;
            VariarVida(-estructura.Rebotar);
            Regenerar();
        }
        void ComprobarMuerteEnDeUnidad(Unidad unidad)
        {
            if (unidad.VidaActual <= 0)
            {
                unidad.DebeMorir = true;
                bando.VariarDinero(unidad.Recompensa);
            }
        }
        void AtacarTorreConSiege(Estructura estructura)
        {
            estructura.VariarVida(-SiegeActual);
            estructura.RecibioDamage = true;
            estructura.DamageRecibido += (int)-DamageActual;
        }
        public void VariarVida(float cambio)
        {
            if (cambio > 0) { AumentaVida = new Tuple<bool, int>(true, (int)cambio); }
            VidaActual += cambio;
            if (VidaActual > VidaMaxima) VidaActual = VidaMaxima;
            //  if (VidaActual <= 0) { Matar(); }
            // else Muerta = false;
        }
        public void VariarAtaque(int cambio)
        {
            DamageActual += cambio;
            if (DamageActual < 0) DamageActual = 0;
        }
        public void Refrescar(Unidad unidad)
        {
            tipoDeCarta = TipoDeCarta.Unidad;
            VidaMaxima = Vida;
            DamageTotal = Damage;
            ArmaduraTotal = Armadura;
            ArmaduraActual = ArmaduraTotal;
            DamageActual = DamageTotal;
            RebotarActual = Rebotar;
            SiegeActual = Siege;
            HendiduraActual = Hendidura;
            Regeneracion = 0;
            Inmune = false;
            Silenciada = false;
            Desarmada = false;
        }
        public void Silenciar(Unidad unidad)
        {
            Silenciada = true;
        }
        public void Desarmar(Unidad unidad)
        {
            Desarmada = true;
        }
        public virtual void Matar()
        {
            Muerta = true;
            LineaActual = null;
            DamageRecibido += (int)-VidaActual;
            RecibeDamage = true;
            VidaActual = 0;
        }
        public virtual void Jugado() { }
        public void Regenerar()
        {
            VariarVida(Regeneracion);
        }
        public virtual bool PuedeJugarseEnLinea(Linea linea)
        {
            try { linea.ProximaPosicionEnMesa(); } catch { return false; }
            if (CostoDeMana <= linea.estructura.ManaActual)
            {
                if (color == Color.Azul)
                    return linea.Azul;
                if (color == Color.Negro)
                    return linea.Negro;
                if (color == Color.Rojo)
                    return linea.Rojo;
                if (color == Color.Verde)
                    return linea.Verde;
            }
            return false;
        }
        public int PosicionEnLinea()
        {
            try
            {
                for (int i = 0; i < LineaActual.Unidades.Length; i++)
                {
                    if (LineaActual.Unidades[i] == this) return i;
                }
            }
            catch { }
            throw new ArgumentException("No esta en linea");
        }
        public override bool PuedeJugarse()
        {
            return PuedeJugarseEnLinea(bando.lineas[juego.lineaActual]);
            //return PuedeJugarseEnLinea(bando.lineas[0]);
        }
        public int NumeroDeLineaActual()
        {
            for (int i = 0; i < 3; i++)
                if (bando.lineas[i] == LineaActual) return i;

            return -1;
        }
        public void ActualizarLaProximaUnidad_a_Atacar()
        {
            if (Identidad.Conexion == Identidad.TipoDeRed.Cliente) return;

            int temporal = new Random().Next(0, 4);

            if (temporal == 0)
                try
                {
                    if (bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea() - 1] != null)
                    {
                        UnidadAtacada = Unidad_a_Atacar.Izquierda;
                        unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea() - 1];
                        EnviarActualizacionDeEstadosAlCliente();
                        return;
                    }
                    else
                    {
                        UnidadAtacada = Unidad_a_Atacar.Centro;
                        unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()];
                        EnviarActualizacionDeEstadosAlCliente();
                        return;
                    }
                }
                catch
                {
                    if (bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()] == null)
                    {
                        unidadAtacada = null;
                        UnidadAtacada = Unidad_a_Atacar.Centro;
                        EnviarActualizacionDeEstadosAlCliente();
                        return;

                    }
                    UnidadAtacada = Unidad_a_Atacar.Centro;
                    unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()];
                    EnviarActualizacionDeEstadosAlCliente();
                    return;
                }

            if (temporal == 3)
                try
                {
                    if (bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea() + 1] != null)
                    {
                        UnidadAtacada = Unidad_a_Atacar.Derecha;
                        unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea() + 1];
                        EnviarActualizacionDeEstadosAlCliente();
                        return;
                    }
                    else
                    {
                        UnidadAtacada = Unidad_a_Atacar.Centro;
                        unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()];
                        EnviarActualizacionDeEstadosAlCliente();
                        return;
                    }
                }
                catch
                {
                    if (bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()] == null)
                    {
                        unidadAtacada = null;
                        UnidadAtacada = Unidad_a_Atacar.Centro;
                        EnviarActualizacionDeEstadosAlCliente();
                        return;
                    }
                    UnidadAtacada = Unidad_a_Atacar.Centro;
                    unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()];
                    EnviarActualizacionDeEstadosAlCliente();
                    return;
                }

            if (bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()] == null)
            {
                unidadAtacada = null;
                UnidadAtacada = Unidad_a_Atacar.Centro;
                EnviarActualizacionDeEstadosAlCliente();
                return;
            }
            EnviarActualizacionDeEstadosAlCliente();
            UnidadAtacada = Unidad_a_Atacar.Centro;
            unidadAtacada = bandoOpuesto().lineas[NumeroDeLineaActual()].Unidades[PosicionEnLinea()];
        }
        public void ActualizarAtaqueComoCliente(int n)
        {
            if (n == 0)
            {
                UnidadAtacada = Unidad_a_Atacar.Izquierda;
                return;
            }
            if (n == 3)
            {
                UnidadAtacada = Unidad_a_Atacar.Derecha;
                return;
            }
            UnidadAtacada = Unidad_a_Atacar.Centro;
        }
        public void EnviarActualizacionDeEstadosAlCliente()
        {
            if (UnidadAtacada == Unidad_a_Atacar.Izquierda)
            {
                if (Equipo == Team.Radiant)
                    Multijugador.RecibirLaTuple(new Tuple<int, int, int, int>(-PosicionEnLinea(), NumeroDeLineaActual(), 0, 0));
                else
                    Multijugador.RecibirLaTuple(new Tuple<int, int, int, int>(-PosicionEnLinea(), NumeroDeLineaActual(), 1, 0));
            }

            if (UnidadAtacada == Unidad_a_Atacar.Centro)
            {
                if (Equipo == Team.Radiant)
                    Multijugador.RecibirLaTuple(new Tuple<int, int, int, int>(-PosicionEnLinea(), NumeroDeLineaActual(), 0, 1));
                else
                    Multijugador.RecibirLaTuple(new Tuple<int, int, int, int>(-PosicionEnLinea(), NumeroDeLineaActual(), 1, 1));
            }

            if (UnidadAtacada == Unidad_a_Atacar.Derecha)
            {
                if (Equipo == Team.Radiant)
                    Multijugador.RecibirLaTuple(new Tuple<int, int, int, int>(-PosicionEnLinea(), NumeroDeLineaActual(), 0, 3));
                else
                    Multijugador.RecibirLaTuple(new Tuple<int, int, int, int>(-PosicionEnLinea(), NumeroDeLineaActual(), 1, 3));
            }
        }
        public void AgregarEfecto(Carta efecto) { Efectos.Add(efecto); }
        public void EliminarEfecto(Carta efecto) { Efectos.Remove(efecto); }
        public bool PoseeElEfecto(Carta efecto)
        {
            for (int i = 0; i < Efectos.Count; i++)
            {
                if (Efectos[i] == efecto) return true;
            }
            return false;
        }
    }

    public class Heroe : Unidad
    {
        public new TipoDeCarta tipoDeCarta = TipoDeCarta.Heroe;
        Temporizador Renacimiento;
        public new int Recompensa = 5;
        public Carta CartaAsignada { get; protected set; }
        public Hechizo hechizo { get; protected set; }
        public ItemDamage itemDamage { get; protected set; }
        public ItemArmadura itemArmadura { get; protected set; }
        public ItemVida itemVida { get; protected set; }
        public void AddItemDamage(ItemDamage item)
        {
            try { Actualizar -= itemDamage.Variar; } catch { }
            itemDamage = item;
            Actualizar += itemDamage.Variar;
            Actualizar(this);
        }
        public void AddItemArmadura(ItemArmadura item)
        {
            try { Actualizar -= itemArmadura.Variar; } catch { }
            itemArmadura = item;
            Actualizar += itemArmadura.Variar;
            Actualizar(this);
        }
        public void AddItemVida(ItemVida item)
        {
            try { Actualizar -= itemVida.Variar; } catch { }
            itemVida = item;
            Actualizar += itemVida.Variar;
            Actualizar(this);
            VariarVida(itemVida.Valor);
        }
        public override bool PuedeJugarseEnLinea(Linea linea)
        {
            try { linea.ProximaPosicionEnMesa(); }
            catch
            {
                MesajeEstaticoExepcion.LanzarExepcion("La Mesa Esta LLena");
                return false;
            }
            return true;
        }

        public void Renacer()
        {
            VidaActual = VidaMaxima;
            //LineaActual = null;
            DebeMorir = false;
            Muerta = false;
        }
        public override void Matar()
        {
            base.Matar();
            ActivarTemporizador();
        }
        void Renacer(Unidad heroe)
        {
            ///if (!heroe.Muerta) return;
            (heroe as Heroe).Renacer();
        }
        void ActivarTemporizador()
        {
            Renacimiento = new Temporizador(2);
            Renacimiento.Evento += Renacer;
            Renacimiento.AddUnidad(this);
            Renacimiento.Activar();
        }
    }


    public abstract class Item : Carta
    {
        public new TipoDeCarta tipoDeCarta = TipoDeCarta.Item;
        public enum TipoItem { Damage, Armadura, Vida }
        public TipoItem tipoItem;
        public int Valor { get; protected set; }
        public int Precio { get; protected set; }
        public virtual void Efecto() { }
        public override bool PuedeJugarse()
        {
            return true;
        }
    }
    public abstract class ItemVida : Item
    {
        public virtual void Variar(Unidad heroe) { heroe.VidaMaxima += Valor; }
    }
    public abstract class ItemDamage : Item
    {
        public virtual void Variar(Unidad heroe) { heroe.DamageActual += Valor; }
    }
    public abstract class ItemArmadura : Item
    {
        public virtual void Variar(Unidad heroe) { heroe.ArmaduraActual += Valor; }
    }


    public abstract class Hechizo : Carta
    {
        public new TipoDeCarta tipoDeCarta = TipoDeCarta.Hechizo;
        public string exepcion { get; protected set; }
        public bool Aliado { get; protected set; }
        public bool Enemigo { get; protected set; }
        public bool Target { get; protected set; }
        public bool Activable;//{ get; protected set; }
        public float PorcientoDeRecarga { get; protected set; }
        public float Tiempo_de_Recarga_Maximo_Actual { get; protected set; }
        public float Tiempo_de_Recarga_Actual { get; protected set; }
        public float Tiempo_de_Recarga_Basico { get; protected set; }
        public float CostoDeVida { get; protected set; }
        public float CostoDeMana { get; protected set; }
        public float CostoDeManaActual { get; protected set; }
        public float Duracion { get; protected set; }
        public float DuracionActual { get; protected set; }
        public float DamageMagico { get; protected set; }
        public float DamageFisico { get; protected set; }
        protected Estructura Poseedor;//{ get; private set; }

        public virtual bool Costos()
        {
            if (PuedeActivarse())
            {
                Gastar();
                return true;
            }
            return false;
        }
        public bool PuedeActivarse()
        {
            AddPoseedor();
            if (Poseedor.ManaActual < CostoDeMana) { exepcion = "NO TIENES SUFICIENTE MANA"; Poseedor.NoMana = true; }
            if (!Colores()) exepcion = "NO PUEDES USAR ESE COLOR";
            return Poseedor.ManaActual >= CostoDeMana && Colores();
        }
        bool Colores()
        {
            if (color == Color.Azul)
                return bando.lineas[juego.lineaActual].Azul;
            if (color == Color.Negro)
                return bando.lineas[juego.lineaActual].Negro;
            if (color == Color.Rojo)
                return bando.lineas[juego.lineaActual].Rojo;
            if (color == Color.Verde)
                return bando.lineas[juego.lineaActual].Verde;

            throw new ArgumentException("La carta no tiene color definido");
        }
        public void Gastar()
        {
            Poseedor.VariarMana((int)-CostoDeMana);
        }

        public void AddPoseedor()
        {
            if (Equipo == Team.Radiant)
                Poseedor = juego.Radiant.lineas[juego.lineaActual].estructura;
            else
                Poseedor = juego.Dire.lineas[juego.lineaActual].estructura;
        }
        public virtual bool Exepcion(Unidad unidad)
        {
            if (unidad.NumeroDeLineaActual() != unidad.juego.lineaActual) return true;
            if (unidad.Equipo == Equipo)
            {
                if (Aliado)
                    return false;
                else
                {
                    exepcion = "Debes seleccionar una unidad enemiga";
                    return true;
                }
            }

            if (unidad.Equipo != Equipo)
            {
                if (Enemigo)
                    return false;
                else
                {
                    exepcion = "Debes seleccionar una unidad aliada";
                    return true;
                }
            }

            return false;
        }
        public virtual bool Exepcion()
        {
            if (!PuedeActivarse()) return false;
            else return true;
        }

        public override bool PuedeJugarse()
        {
            return PuedeActivarse();
        }

        public abstract void Efecto();
        public abstract void Efecto(Unidad unidad);
    }
}



