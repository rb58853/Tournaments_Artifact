using System;
using System.Timers;

namespace LogicoNuevo
{
    public class AndOneForMe : Hechizo
    {

        public AndOneForMe()
        {
            tipoDeCarta = TipoDeCarta.Hechizo;
            Tipo = "Hechizo";
            codigo = 0;
            CostoDeMana = 4;
            color = Color.Azul;
            Nombre = "And One For Me";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class ActOfDefiance : Hechizo
    {
        Temporizador _Duracion;
        Unidad unidadAfectada;
        public ActOfDefiance()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            codigo = 2;
            CostoDeMana = 5;
            color = Color.Verde;
            Nombre = "Act Of Defiance ";
        }
        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarSilencio;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidadAfectada = unidad;
            unidad.Actualizar += unidadAfectada.Silenciar;
            unidad.Actualizar(unidad);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void EliminarSilencio(Unidad unidad)
        {
            unidad.Actualizar -= unidad.Silenciar;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }


    }
    public class AghanimSanctum : Hechizo
    {

        public AghanimSanctum()
        {
            Tipo = "Hechizo";
            codigo = 3;
            CostoDeMana = 4;
            color = Color.Azul;
            Nombre = "Aghanim's Sanctum";
        }

        public override void Efecto()
        {
            if (!Costos()) return;
            Poseedor.VariarMana(Poseedor.Mana);
        }


        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class AllseeingOneFavor : Hechizo
    {

        public AllseeingOneFavor()
        {
            Tipo = "Hechizo";
            Target = true;
            //  Aliado = true;
            codigo = 4;
            CostoDeMana = 4;
            color = Color.Verde;
            Nombre = "Allseeing One's Favor";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {

            //////este efecto hay que arreglarlo, Ver
            if (!Costos()) return;
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    unidad.LineaActual.Unidades[i].Actualizar += AumentarRegeneracion;
                    unidad.LineaActual.Unidades[i].Actualizar(unidad.LineaActual.Unidades[i]);
                }
                catch { }
            }
        }
        void AumentarRegeneracion(Unidad unidad)
        {
            unidad.Regeneracion += 2;
        }

        public override bool Exepcion(Unidad unidad)
        {
            if (unidad.Equipo == Equipo)
            {

                if (unidad.color != Color.Verde || !unidad.EsHeroe)
                {
                    exepcion = "Solo puedes seleccionar un heroe verde";
                    return true;
                }
                else
                { return false; }
            }
            else
            {
                exepcion = "Debes seleccionar una unidad aliada";
                return false;
            }
        }
    }
    public class AltarOfTheMadMoon : Hechizo
    {

        public AltarOfTheMadMoon()
        {
            Tipo = "Hechizo";
            codigo = 5;
            CostoDeMana = 4;
            color = Color.Verde;
            Nombre = "Altar Of The Mad Moon";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            if (Equipo == Team.Radiant)
            {
                for (int i = 0; i < juego.Radiant.lineas[juego.lineaActual].Unidades.Length; i++)
                {
                    if (juego.Radiant.lineas[juego.lineaActual].Unidades[i].Melee)
                        juego.Radiant.lineas[juego.lineaActual].Unidades[i].Regeneracion += 2;
                }
            }
            else
            {
                for (int i = 0; i < juego.Dire.lineas[juego.lineaActual].Unidades.Length; i++)
                {
                    if (juego.Dire.lineas[juego.lineaActual].Unidades[i].Melee)
                        juego.Dire.lineas[juego.lineaActual].Unidades[i].Regeneracion += 2;
                }
            }
        }



        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class Annilation : Hechizo
    {

        public Annilation()
        {
            Tipo = "Hechizo";
            codigo = 6;
            CostoDeMana = 4;
            color = Color.Azul;
            Nombre = "Annilation";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            for (int i = 0; i < juego.Radiant.lineas[juego.lineaActual].Unidades.Length; i++)
            { try { juego.Radiant.lineas[juego.lineaActual].Unidades[i].Matar(); } catch { } }
            for (int i = 0; i < juego.Dire.lineas[juego.lineaActual].Unidades.Length; i++)
            { try { juego.Dire.lineas[juego.lineaActual].Unidades[i].Matar(); } catch { } }
        }
        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class ArcaneAssault : Hechizo
    {

        public ArcaneAssault()
        {
            Tipo = "Hechizo";
            codigo = 9;
            CostoDeMana = 4;
            color = Color.Azul;
            Nombre = "Arcane Assault";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            if (Equipo == Team.Radiant)
                juego.Dire.lineas[juego.lineaActual].estructura.VariarVida(-2);
            else
                juego.Radiant.lineas[juego.lineaActual].estructura.VariarVida(-2);

            ///////Falta Robar Una carta o votar no se bien
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class ArcaneCensure : Hechizo
    {

        public ArcaneCensure()
        {
            Tipo = "Hechizo";
            codigo = 10;
            CostoDeMana = 4;
            color = Color.Negro;
            Nombre = "Arcane Censure";
        }

        public override void Efecto()
        {


            if (Equipo == Team.Radiant)
            {

                juego.Dire.lineas[juego.lineaActual].estructura.Mana -= 1;
                juego.Dire.lineas[juego.lineaActual].estructura.VariarMana(-1);

            }
            else
            {
                juego.Radiant.lineas[juego.lineaActual].estructura.Mana -= 1;
                juego.Radiant.lineas[juego.lineaActual].estructura.VariarMana(-1);
            }

        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class ArmTheRebellion : Hechizo
    {
        Unidad unidadAfectada;
        public ArmTheRebellion()
        {
            Tipo = "Hechizo";
            codigo = 11;
            CostoDeMana = 4;
            color = Color.Verde;
            Nombre = "Arcane Censure";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            for (int i = 0; i < bando.lineas[juego.lineaActual].Unidades.Length; i++)
                try
                {
                    if (!bando.lineas[juego.lineaActual].Unidades[i].EsHeroe)
                    {
                        bando.lineas[juego.lineaActual].Unidades[i].Actualizar += AumentarEstadistica;
                        bando.lineas[juego.lineaActual].Unidades[i].AumentaArmadura = new Tuple<bool, int>(true, 1);
                        bando.lineas[juego.lineaActual].Unidades[i].AumentaDamage = new Tuple<bool, int>(true, 2);
                        bando.lineas[juego.lineaActual].Unidades[i].Actualizar(bando.lineas[juego.lineaActual].Unidades[i]);
                    }
                }
                catch { }
        }
        void AumentarEstadistica(Unidad unidad)
        {
            unidad.ArmaduraActual += 1;
            unidad.DamageActual += 2;

        }
        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class Assassinate : Hechizo
    {
        public Assassinate()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            codigo = 15;
            CostoDeMana = 7;
            color = Color.Negro;
            Nombre = "Assassinate";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidad.VariarVida(-10);
            unidad.RecibeDamage = true;
            unidad.DamageRecibido = -10;
            if (unidad.Muerta)
            {
                if (unidad.EsHeroe)
                    bando.VariarDinero(5);
                else
                    bando.VariarDinero(1);
            }
        }
    }
    public class AssaultLadders : Hechizo
    {

        public AssaultLadders()
        {
            Tipo = "Hechizo";
            codigo = 16;
            CostoDeMana = 4;
            color = Color.Negro;
            Nombre = "Assault Ladders";
        }

        public override void Efecto()
        {
            //implementar
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class AssuredDestruction : Hechizo
    {

        public AssuredDestruction()
        {
            codigo = 17;
            CostoDeMana = 3;
            color = Color.Negro;
            Nombre = "Assured Destruction";
        }

        public override void Efecto()
        {
            if (!Costos()) return;
            for (int i = 0; i < 5; i++)
            {
                bando.mazo.heroes[i].Actualizar += AumentarSiege;
                bando.mazo.heroes[i].Actualizar(bando.mazo.heroes[i]);
            }
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
        void AumentarSiege(Unidad unidad)
        {
            unidad.SiegeActual += 4;
        }
    }
    public class AstralImprisonment : Hechizo
    {
        Temporizador _Duracion;
        public AstralImprisonment()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            Aliado = true;
            codigo = 18;
            CostoDeMana = 4;
            color = Color.Azul;
            Nombre = "Astral Imprisonment";
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarStun;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidad.Actualizar += unidad.Silenciar;
            unidad.Actualizar += unidad.Desarmar;
            unidad.Actualizar(unidad);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void EliminarStun(Unidad unidad)
        {
            unidad.Actualizar -= unidad.Silenciar;
            unidad.Actualizar -= unidad.Desarmar;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }
    }
    public class AtAnyCost : Hechizo
    {

        public AtAnyCost()
        {
            Tipo = "Hechizo";
            codigo = 19;
            CostoDeMana = 3;
            color = Color.Azul;
            Nombre = "At Any Cost";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            for (int i = 0; i < juego.Radiant.lineas[juego.lineaActual].Unidades.Length; i++)
            {
                try
                {
                    juego.Radiant.lineas[juego.lineaActual].Unidades[i].VariarVida(-6);
                    juego.Radiant.lineas[juego.lineaActual].Unidades[i].Fuego = true;
                    juego.Radiant.lineas[juego.lineaActual].Unidades[i].RecibeDamage = true;
                    juego.Radiant.lineas[juego.lineaActual].Unidades[i].DamageRecibido = -6;
                }
                catch { }
            }
            for (int i = 0; i < juego.Dire.lineas[juego.lineaActual].Unidades.Length; i++)
            {
                try
                {
                    juego.Dire.lineas[juego.lineaActual].Unidades[i].VariarVida(-6);
                    juego.Dire.lineas[juego.lineaActual].Unidades[i].Fuego = true;
                    juego.Dire.lineas[juego.lineaActual].Unidades[i].RecibeDamage = true;
                    juego.Dire.lineas[juego.lineaActual].Unidades[i].DamageRecibido = -6;
                }
                catch { }
            }

        }
        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class AvernusBlessingt : Hechizo
    {
        Unidad unidadAfectada;
        public AvernusBlessingt()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 20;
            CostoDeMana = 3;
            color = Color.Verde;
            Nombre = "Avernus Blessingt";
            Descripcion = "Modifica a una unidad con +2 de ataque";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidadAfectada = unidad;
            unidad.Actualizar += AumentarDamage;
            unidad.AumentaDamage = new Tuple<bool, int>(true, 2);
            unidad.Actualizar(unidad);
            unidad.AgregarEfecto(this);
        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.DamageActual += 2;
        }
    }
    public class BallLightning : Hechizo
    {

        public BallLightning()
        {
            Tipo = "Hechizo";
            codigo = 22;
            CostoDeMana = 3;
            color = Color.Negro;
            Nombre = "Ball Lightning";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            /// Hay Que implementar el swap Visual
            throw new NotImplementedException();
        }
    }
    public class Barracks : Hechizo
    {

        public Barracks()
        {
            codigo = 24;
            CostoDeMana = 5;
            color = Color.Azul;
            Nombre = "Barracks";
        }
        public override void Efecto()
        {
            if (!Costos()) return;
            InvocadorDeCreeps.InvocarCreepsRadiants(1);
        }
        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class BattlefieldControl : Hechizo
    {

        public BattlefieldControl()
        {
            Tipo = "Hechizo";
            codigo = 25;
            CostoDeMana = 1;
            color = Color.Azul;
            Nombre = "Battlefield Control";
        }

        public override void Efecto()
        {
            //Implemantar el sawp creo
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class Bellow : Hechizo
    {

        public Bellow()
        {
            Tipo = "Hechizo";
            codigo = 27;
            CostoDeMana = 2;
            color = Color.Verde;
            Nombre = "Bellow";
        }

        public override void Efecto()
        {
            //Implemantar el sawp 
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class BetterLateThanNever : Hechizo
    {

        public BetterLateThanNever()
        {
            Tipo = "Hechizo";
            codigo = 29;
            CostoDeMana = 3;
            color = Color.Azul;
            Nombre = "Better Late Than Never";
        }

        public override void Efecto()
        {
            if (!Costos()) return;
            InvocadorDeCreeps.InvocarCreepsRadiants(1);
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class BitterEnemies : Hechizo
    {
        public BitterEnemies()
        {
            Tipo = "Hechizo";
            codigo = 30;
            CostoDeMana = 2;
            color = Color.Negro;
            Nombre = "Bitter Enemies";
        }

        public override void Efecto()
        {
            //no entiendo bien
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class BoltOfDamocles : Hechizo
    {

        public BoltOfDamocles()
        {
            Tipo = "Hechizo";
            codigo = 35;
            CostoDeMana = 10;
            color = Color.Azul;
            Nombre = "Bolt Of Damocles";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            if (Equipo == Team.Dire)
                juego.Radiant.lineas[juego.lineaActual].estructura.VariarVida(-20);
            else
                juego.Dire.lineas[juego.lineaActual].estructura.VariarVida(-20);
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class BurningOil : Hechizo
    {

        public BurningOil()
        {
            Tipo = "Hechizo";
            codigo = 42;
            CostoDeMana = 1;
            color = Color.Rojo;
            Nombre = "Burning Oil";
        }

        public override void Efecto()
        {
            //Esto lleva Implementacion nueva para seleccionar la linea que desees
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class BuyingTime : Hechizo
    {

        public BuyingTime()
        {
            Tipo = "Hechizo";
            codigo = 43;
            CostoDeMana = 3;
            color = Color.Azul;
            Nombre = "Buying Time";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class CallTheReserves : Hechizo
    {

        public CallTheReserves()
        {
            Tipo = "Hechizo";
            codigo = 44;
            CostoDeMana = 6;
            color = Color.Azul;
            Nombre = "Call The Reserves";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            InvocadorDeCreeps.InvocarCreepsRadiants(2);
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class CaugthUnprepared : Hechizo
    {
        Temporizador _Duracion;
        Unidad unidadAfectada;

        public CaugthUnprepared()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            codigo = 45;
            CostoDeMana = 4;
            color = Color.Verde;
            Nombre = "Caugth Unprepared";
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarStun;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidadAfectada = unidad;
            unidad.Actualizar += unidadAfectada.Silenciar;
            unidad.Actualizar += unidadAfectada.Desarmar;
            unidad.Actualizar(unidad);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void EliminarStun(Unidad unidad)
        {
            unidad.Actualizar -= unidad.Silenciar;
            unidad.Actualizar -= unidad.Desarmar;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }


    }
    public class ChainFrost : Hechizo
    {
        Timer timer;
        int contador;
        Unidad afectada;
        public ChainFrost()
        {
            Target = true;
            Enemigo = true;
            Tipo = "Hechizo";
            codigo = 48;
            CostoDeMana = 7;
            color = Color.Negro;
            Nombre = "Chain Frost";
        }
        void ActivarTimer()
        {
            timer = new Timer(800);
            timer.Elapsed += timer_Tick;
            timer.Enabled = true;
            timer.AutoReset = true;
        }

        void timer_Tick(System.Object source, ElapsedEventArgs e)
        {
            afectada.Hielo = true;
            afectada.VariarVida(-3);
            if (afectada.VidaActual < 0) afectada.Matar();
            SeleccionarProximaUnidad();
            contador++;
            if (SoloHayUnaUnidad) timer.Enabled = false;
            if (contador == 7) { timer.Enabled = false; contador = 0; }
        }


        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            for (int i = 0; i < unidad.LineaActual.Unidades.Length; i++)
            {
                if (unidad.LineaActual.Unidades[i] == unidad)
                { posicionActual = i; break; }
            }
            linea = unidad.LineaActual;

            unidad.Hielo = true;
            unidad.VariarVida(-3);
            contador++;
            SeleccionarProximaUnidad();
            if (SoloHayUnaUnidad) return;
            ActivarTimer();
        }

        bool SoloHayUnaUnidad;
        int posicionActual = 0;
        Linea linea;

        void SeleccionarProximaUnidad()
        {
            int Moverse = new Random().Next(0, 2);

            if (Moverse == 0)
            {
                if (posicionActual != 0)
                    for (int i = posicionActual - 1; i >= 0; i--)
                    {
                        if (linea.Unidades[i] != null)
                        { afectada = linea.Unidades[i]; posicionActual = i; return; }
                    }

                if (posicionActual != linea.Unidades.Length - 1)
                    for (int i = posicionActual + 1; i < linea.Unidades.Length; i++)
                    {
                        if (linea.Unidades[i] != null)
                        { afectada = linea.Unidades[i]; posicionActual = i; return; }
                    }
            }
            else
            {
                if (posicionActual != linea.Unidades.Length - 1)
                    for (int i = posicionActual + 1; i < linea.Unidades.Length; i++)
                    {
                        if (linea.Unidades[i] != null)
                        { afectada = linea.Unidades[i]; posicionActual = i; return; }
                    }

                if (posicionActual != 0)
                    for (int i = posicionActual - 1; i >= 0; i--)
                    {
                        if (linea.Unidades[i] != null)
                        { afectada = linea.Unidades[i]; posicionActual = i; return; }
                    }
            }


            SoloHayUnaUnidad = true;
        }
    }
    public class ClearTheDeck : Hechizo
    {
        Temporizador _Duracion;

        public ClearTheDeck()
        {
            Tipo = "Hechizo";
            codigo = 56;
            CostoDeMana = 4;
            color = Color.Rojo;
            Nombre = "Clear The Deck";
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarEfecto;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            ActivarTemporizador();

            for (int i = 0; i < 5; i++)
            {
                bando.mazo.heroes[i].Actualizar += AumentarHendidura;
                bando.mazo.heroes[i].Actualizar(bando.mazo.heroes[i]);
                _Duracion.AddUnidad(bando.mazo.heroes[i]);
            }
            _Duracion.Evento += EliminarEfecto;
        }

        public override void Efecto(Unidad unidad)
        {
        }
        void EliminarEfecto(Unidad unidad)
        {
            unidad.Actualizar -= AumentarHendidura;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }
        void AumentarHendidura(Unidad unidad)
        {
            unidad.HendiduraActual += 4;
        }
    }
    public class CombatTraining : Hechizo
    {

        public CombatTraining()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 59;
            CostoDeMana = 3;
            color = Color.Rojo;
            Nombre = "Combat Training ";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidad.Actualizar += AumentarDamage;
            unidad.Actualizar(unidad);
            unidad.AumentaDamage = new Tuple<bool, int>(true, 2);
        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(2);
        }

        public override bool Exepcion(Unidad unidad)
        {
            if (unidad.Equipo == Equipo)
            {

                if (!unidad.EsHeroe)
                {
                    exepcion = "Solo puedes seleccionar un heroe";
                    return true;
                }
                else
                { return false; }
            }
            else
            {
                exepcion = "Debes seleccionar una unidad aliada";
                return true;
            }
        }
    }
    public class CripplingBlow : Hechizo
    {

        public CripplingBlow()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            codigo = 65;
            CostoDeMana = 4;
            color = Color.Rojo;
            Nombre = "Crippling Blow";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidad.Actualizar += ReducirDamage;
            unidad.Actualizar(unidad);
            unidad.BajaDamage = new Tuple<bool, int>(true, -2);
        }
        void ReducirDamage(Unidad unidad)
        {
            unidad.VariarAtaque(-2);
        }

        public override bool Exepcion(Unidad unidad)
        {
            if (unidad.Equipo != Equipo)
            {

                if (!unidad.EsHeroe)
                {
                    exepcion = "Solo puedes seleccionar un heroe";
                    return true;
                }
                else
                { return false; }
            }
            else
            {
                exepcion = "Debes seleccionar una unidad enemiga";
                return true;
            }
        }
    }
    public class CurseOfAtrophy : Hechizo
    {

        public CurseOfAtrophy()
        {
            Tipo = "Hechizo";
            codigo = 68;
            CostoDeMana = 6;
            color = Color.Verde;
            Nombre = "Curse of Atrophy";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            for (int i = 0; i < 5; i++)
            {
                bandoOpuesto().mazo.heroes[i].Actualizar += ReducirDamage;
                bandoOpuesto().mazo.heroes[i].Actualizar(bandoOpuesto().mazo.heroes[i]);
                bandoOpuesto().mazo.heroes[i].BajaDamage = new Tuple<bool, int>(true, -2);
            }
        }

        public override void Efecto(Unidad unidad)
        {
        }
        void ReducirDamage(Unidad unidad)
        {
            unidad.VariarAtaque(-2);
        }
    }
    public class DefendTheWeak : Hechizo
    {
        Unidad afectada;
        public DefendTheWeak()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 72;
            CostoDeMana = 2;
            color = Color.Verde;
            Nombre = "Defend The Weak";
        }

        public override void Efecto()
        {

        }

        public override void Efecto(Unidad unidad)
        {
            afectada = unidad;
            try
            {
                if (unidad.update.Activo)
                    unidad.update.Evento += update;
                else
                {
                    unidad.update = new Update();
                    unidad.update.Evento += update;
                }
            }
            catch
            {
                unidad.update = new Update();
                unidad.update.Evento += update;
            }
        }

        void update()
        {
            if (!afectada.Muerta && afectada.LineaActual != null)
            {
                for (int i = 0; i < afectada.LineaActual.Unidades.Length; i++)
                {
                    try
                    {
                        //afectada.LineaActual.Unidades[i].Actualizar -= AumentarArmadura;
                        //afectada.LineaActual.Unidades[i].Actualizar(afectada.LineaActual.Unidades[i]);
                    }
                    catch { }
                }
                try
                {
                    if (!afectada.LineaActual.Unidades[afectada.PosicionEnLinea() + 1].PoseeElEfecto(this))
                    {
                        afectada.LineaActual.Unidades[afectada.PosicionEnLinea() + 1].AgregarEfecto(this);
                        afectada.LineaActual.Unidades[afectada.PosicionEnLinea() + 1].Actualizar += AumentarArmadura;
                        afectada.LineaActual.Unidades[afectada.PosicionEnLinea() + 1].Actualizar(afectada.LineaActual.Unidades[afectada.PosicionEnLinea() + 1]);
                    }
                }
                catch { }
                try
                {
                    if (!afectada.LineaActual.Unidades[afectada.PosicionEnLinea() - 1].PoseeElEfecto(this))
                    {
                        afectada.LineaActual.Unidades[afectada.PosicionEnLinea() - 1].AgregarEfecto(this);
                        afectada.LineaActual.Unidades[afectada.PosicionEnLinea() - 1].Actualizar += AumentarArmadura;
                        afectada.LineaActual.Unidades[afectada.PosicionEnLinea() - 1].Actualizar(afectada.LineaActual.Unidades[afectada.PosicionEnLinea() - 1]);
                    }
                }
                catch { }
            }
            else
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    for (int i = 0; i < afectada.LineaActual.Unidades.Length; i++)
                    {
                        try
                        {
                            afectada.bando.lineas[ii].Unidades[i].Actualizar -= AumentarArmadura;
                            afectada.bando.lineas[ii].Unidades[i].Actualizar(afectada.LineaActual.Unidades[i]);
                        }
                        catch { }
                    }
                }
            }
        }
        void AumentarArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual += 2;
        }

    }
    public class DefensiveStance : Hechizo
    {
        Temporizador _Duracion;

        public DefensiveStance()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 74;
            CostoDeMana = 3;
            color = Color.Rojo;
            Nombre = "Defensive Stance";
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarEfecto;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidad.Actualizar += AumentarArmadura;
            unidad.Actualizar(unidad);
            unidad.AumentaArmadura = new Tuple<bool, int>(true, 3);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void EliminarEfecto(Unidad unidad)
        {
            unidad.Actualizar -= AumentarArmadura;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }
        void AumentarArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual += 3;
        }

        public override bool Exepcion(Unidad unidad)
        {
            if (unidad.Equipo == Equipo)
            {

                if (!unidad.EsHeroe)
                {
                    exepcion = "Solo puedes seleccionar un heroe";
                    return true;
                }
                else
                { return false; }
            }
            else
            {
                exepcion = "Debes seleccionar una unidad aliada";
                return true;
            }
        }
    }
    public class DivineIntervention : Hechizo
    {
        public DivineIntervention()
        {
            Tipo = "Hechizo";
            //      Target = true;
            //      Aliado = true;
            codigo = 81;
            CostoDeMana = 3;
            color = Color.Verde;
            Nombre = "Defensive Stance";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }
    }
    public class DivinePurpose : Hechizo
    {

        public DivinePurpose()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 82;
            CostoDeMana = 7;
            color = Color.Verde;
            Nombre = "Divine Purpose";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidad.Actualizar += HacerInmune;
            unidad.Actualizar(unidad);
        }
        void HacerInmune(Unidad unidad)
        {
            unidad.Inmune = true;
        }
    }
    public class DoubleEdge : Hechizo
    {
        Temporizador _Duracion;

        public DoubleEdge()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 83;
            CostoDeMana = 1;
            color = Color.Rojo;
            Nombre = "Double Edge";
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarEfecto;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidad.Actualizar += VariarEstadisticas;
            unidad.Actualizar(unidad);
            unidad.AumentaDamage = new Tuple<bool, int>(true, 8);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void EliminarEfecto(Unidad unidad)
        {
            unidad.Actualizar -= VariarEstadisticas;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }
        void VariarEstadisticas(Unidad unidad)
        {
            unidad.ArmaduraActual -= 8;
            unidad.VariarAtaque(8);
        }
    }
    public class Gust : Hechizo
    {
        Temporizador _Duracion;

        public Gust()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            codigo = 112;
            CostoDeMana = 1;
            color = Color.Verde;
            Nombre = "Gust";
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarEfecto;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidad.Actualizar += Silenciar;
            unidad.Actualizar(unidad);
            _Duracion.AddUnidad(unidad);

            try
            {
                unidad.LineaActual.Unidades[Posicion(unidad) + 1].Actualizar += Silenciar;
                unidad.LineaActual.Unidades[Posicion(unidad) + 1].Actualizar(unidad.LineaActual.Unidades[Posicion(unidad) + 1]);
                _Duracion.AddUnidad(unidad.LineaActual.Unidades[Posicion(unidad) + 1]);
            }
            catch { }

            try
            {
                unidad.LineaActual.Unidades[Posicion(unidad) - 1].Actualizar += Silenciar;
                unidad.LineaActual.Unidades[Posicion(unidad) - 1].Actualizar(unidad.LineaActual.Unidades[Posicion(unidad) - 1]);
                _Duracion.AddUnidad(unidad.LineaActual.Unidades[Posicion(unidad) - 1]);
            }
            catch { }

            ActivarTemporizador();
        }
        public int Posicion(Unidad unidad)
        {
            try
            {
                for (int i = 0; i < unidad.LineaActual.Unidades.Length; i++)
                {
                    if (unidad.LineaActual.Unidades[i] == unidad) return i;
                }
            }
            catch { }
            throw new ArgumentException("No esta en linea");
        }
        void EliminarEfecto(Unidad unidad)
        {
            unidad.Actualizar -= Silenciar;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }
        void Silenciar(Unidad unidad)
        {
            unidad.Silenciar(unidad);
        }
    }
    public class VerdantRefuge : Hechizo
    {
        Unidad unidadAfectada;
        public VerdantRefuge()
        {
            Tipo = "Hechizo";
            codigo = 287;
            CostoDeMana = 5;
            color = Color.Verde;
            Nombre = "Verdant Refuge";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            for (int ii = 0; ii < 3; ii++)
                for (int i = 0; i < bando.lineas[ii].Unidades.Length; i++)
                {
                    try
                    {
                        bando.lineas[ii].Unidades[i].Actualizar += AumentarEstadistica;
                        bando.lineas[ii].Unidades[i].Actualizar(bando.lineas[ii].Unidades[i]);
                        bando.lineas[ii].Unidades[i].AumentaArmadura = new Tuple<bool, int>(true, 1);
                    }
                    catch { }
                }
        }
        void AumentarEstadistica(Unidad unidad)
        {
            unidad.ArmaduraActual += 1;
        }
        public override void Efecto(Unidad unidad)
        {
            throw new NotImplementedException();
        }

    }
}