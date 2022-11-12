using System;
namespace LogicoNuevo
{
    public class MeleeCreep : Unidad
    {
        public MeleeCreep()
        {
            tipoDeCarta = TipoDeCarta.Unidad;
            Tipo = "Unidad";
            Melee = true;
            Damage = 2;
            DamageActual = 2;
            DamageTotal = 2;
            Vida = 4;
            VidaActual = 4;
            VidaMaxima = 4;
            codigo = -1;
            Actualizar += Refrescar;
            Nombre = "Melee Creep";
        }
    }
    public class AssassinApprentice : Unidad
    {

        public AssassinApprentice()
        {
            tipoDeCarta = TipoDeCarta.Unidad;
            Tipo = "Unidad";
            CostoDeMana = 2;
            Damage = 3;
            DamageActual = 3;
            DamageTotal = 3;
            Vida = 2;
            VidaActual = 2;
            VidaMaxima = 2;
            codigo = 12;
            color = Color.Negro;
            Actualizar += Refrescar;
            Nombre = "Assassin Apprentice";
        }
    }
    public class AssassinShadow : Unidad
    {
        Update update = new Update();
        public AssassinShadow()
        {
            tipoDeCarta = TipoDeCarta.Unidad;
            update.Evento += Efecto;
            Tipo = "Unidad";
            CostoDeMana = 7;
            Damage = 15;
            DamageActual = 15;
            DamageTotal = 15;
            Vida = 5;
            VidaActual = 5;
            VidaMaxima = 5;
            codigo = 13;
            color = Color.Negro;
            Actualizar += Refrescar;
            Nombre = "Assassin Shadow";
        }
        void Efecto()
        {
            for (int i = 0; i < 7; i++)
                try { Actualizar -= BajarDamage; } catch { }
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    if (LineaActual.Unidades[i] != null && LineaActual.Unidades[i] != this)
                        Actualizar += BajarDamage;
                }
                catch { }
            }
            try
            { Actualizar(this); }
            catch { }
        }
        void BajarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(-2);
        }
    }
    public class BronzeLegionare : Unidad
    {

        public BronzeLegionare()
        {
            Tipo = "Unidad";
            CostoDeMana = 2;
            Damage = 4;
            DamageActual = 4;
            DamageTotal = 4;
            Armadura = 2;
            ArmaduraActual = 2;
            ArmaduraTotal = 2;
            Vida = 2;
            VidaActual = 2;
            VidaMaxima = 2;
            codigo = 41;
            color = Color.Rojo;
            Actualizar += Refrescar;
            Nombre = "Bronze Legionare";
        }
    }
    public class CentaurHunter : Unidad
    {

        public CentaurHunter()
        {
            Tipo = "Unidad";
            CostoDeMana = 5;
            Damage = 4;
            DamageActual = 4;
            DamageTotal = 4;
            Vida = 8;
            VidaActual = 8;
            VidaMaxima = 8;
            codigo = 46;
            color = Color.Rojo;
            Actualizar += Refrescar;
            Nombre = "Centaur Hunter";
        }
    }
    public class HellbearCrippler : Unidad
    {

        public HellbearCrippler()
        {
            Tipo = "Unidad";
            CostoDeMana = 3;
            Damage = 3;
            DamageActual = 3;
            DamageTotal = 3;
            Vida = 3;
            VidaActual = 3;
            VidaMaxima = 3;
            codigo = 116;
            color = Color.Rojo;
            Actualizar += Refrescar;

            Nombre = "Hellbear Crippler";
            Descripcion = "Cuando Hellbera Crippler ataca a una unidad, modifica a esta con -1 de ataque.";
        }
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            unidad.Actualizar += BajarAtaque;
            unidad.Actualizar(unidad);
        }
        void BajarAtaque(Unidad unidad)
        {
            unidad.VariarAtaque(-1);
        }
    }
    public class LegionStandartBearer : Unidad
    {
        public LegionStandartBearer()
        {
            update = new Update();
            update.Evento += Efecto;
            Tipo = "Unidad";
            CostoDeMana = 5;
            Damage = 0;
            DamageActual = 0;
            DamageTotal = 0;
            Vida = 6;
            VidaActual = 6;
            VidaMaxima = 6;
            codigo = 143;
            color = Color.Rojo;
            Actualizar += Refrescar;
            Nombre = "Legion Standart Bearer";
            Descripcion = "Los aliados cercanos a Legion Standart Bearer obtienen +4 de ataque";
        }

        void AumentarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(4);
        }

        int posicion = 0;

        void Efecto()
        {
            if (!Muerta && LineaActual != null)
            {
                for (int i = 0; i < LineaActual.Unidades.Length; i++)
                {
                    try
                    {
                        if (LineaActual.Unidades[i] == this) posicion = i;
                        LineaActual.Unidades[i].Actualizar -= AumentarDamage;
                        LineaActual.Unidades[i].Actualizar(LineaActual.Unidades[i]);
                    }
                    catch { }
                }
                try
                {
                    LineaActual.Unidades[posicion + 1].Actualizar += AumentarDamage;
                    LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                }
                catch { }
                try
                {
                    LineaActual.Unidades[posicion - 1].Actualizar += AumentarDamage;
                    LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                }
                catch { }
            }
        }
        public override void Matar()
        {
            base.Matar();
            try
            {
                LineaActual.Unidades[posicion + 1].Actualizar -= AumentarDamage;
                LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
            }
            catch { }
            try
            {
                LineaActual.Unidades[posicion - 1].Actualizar -= AumentarDamage;
                LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
            }
            catch { }
        }
    }
    public class LoyalBeast : Unidad
    {

        public LoyalBeast()
        {
            Tipo = "Unidad";
            CostoDeMana = 3;

            Damage = 3;
            DamageActual = 3;
            DamageTotal = 3;

            Armadura = 1;
            ArmaduraActual = 1;
            ArmaduraTotal = 1;

            Vida = 3;
            VidaActual = 3;
            VidaMaxima = 3;

            codigo = 149;//Shambles
            color = Color.Rojo;
            Actualizar += Refrescar;

            Nombre = "Loyal Beast";
            Descripcion = "Cuando Loya lBeast ataca a una unidad, modifica a esta con -1 de ataque.";
        }
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            unidad.Actualizar += BajarAtaque;
        }
        void BajarAtaque(Unidad unidad)
        {
            unidad.VariarAtaque(-1);
        }
    }
    public class MarrowfellBrawler : Unidad
    {

        public MarrowfellBrawler()
        {
            Tipo = "Unidad";
            CostoDeMana = 5;
            Damage = 6;
            DamageActual = 6;
            DamageTotal = 6;
            Vida = 16;
            VidaActual = 16;
            VidaMaxima = 16;
            codigo = 155;
            color = Color.Rojo;
            Actualizar += Refrescar;
            Nombre = "Marrowfell Brawler";
        }
    }
    public class RampagingHellbear : Unidad
    {
        public RampagingHellbear()
        {
            Tipo = "Unidad";
            CostoDeMana = 4;
            Damage = 2;
            DamageActual = 2;
            DamageTotal = 2;
            Vida = 3;
            VidaActual = 3;
            VidaMaxima = 3;
            codigo = 196;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "Rampaging Hellbear";
            Descripcion = " Despues de la fase de combate modifica a Rampaging Hellbear con +4 de ataque";
        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(4);
        }
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            Actualizar += AumentarDamage;
            Actualizar(this);
        }
        public override void Atacar(Estructura estructura)
        {
            base.Atacar(estructura);
            Actualizar += AumentarDamage;
            Actualizar(this);
        }
    }
    public class OgreConscript : Unidad
    {

        public OgreConscript()
        {
            Tipo = "Unidad";
            CostoDeMana = 6;
            Damage = 7;
            DamageActual = 7;
            DamageTotal = 7;
            Armadura = 2;
            ArmaduraActual = 2;
            ArmaduraTotal = 2;
            Vida = 7;
            VidaActual = 7;
            VidaMaxima = 7;
            codigo = 171;
            color = Color.Rojo;
            Actualizar += Refrescar;
            Nombre = "Ogre Conscript";
        }
    }
    public class SelfishCleric : Unidad
    {
        public SelfishCleric()
        {
            Tipo = "Unidad";
            CostoDeMana = 4;
            Damage = 2;
            DamageActual = 2;
            DamageTotal = 2;
            Vida = 3;
            VidaActual = 3;
            VidaMaxima = 3;
            codigo = 228;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "SelfishCleric";
            Descripcion = " Despues de la fase de combate Selfish Cleric regenera su salud al maximo";
        }
        void LlenarVida(Unidad unidad)
        {
            unidad.VariarVida(VidaMaxima);
        }
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            Actualizar += LlenarVida;
            Actualizar(this);
        }
        public override void Atacar(Estructura estructura)
        {
            base.Atacar(estructura);
            Actualizar += LlenarVida;
            Actualizar(this);
        }
    }
    public class ThunderhideAlpha : Unidad
    {
        public ThunderhideAlpha()
        {
            Tipo = "Unidad";
            CostoDeMana = 9;
            Damage = 25;
            DamageActual = 25;
            DamageTotal = 25;
            Vida = 25;
            VidaActual = 25;
            VidaMaxima = 25;
            codigo = 265;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "Thunderhide Alpha";
        }
    }
    public class ThunderhidePack : Unidad
    {
        public ThunderhidePack()
        {
            Tipo = "Unidad";
            CostoDeMana = 8;
            Siege = 6;
            Damage = 14;
            DamageActual = 14;
            DamageTotal = 14;
            Vida = 14;
            VidaActual = 14;
            VidaMaxima = 14;
            codigo = 266;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "Thunderhide Pack";
            Descripcion = "Cerco 6 (Daño a las estructuras en si no ataca)";
        }
    }
    public class VhoulMartyr : Unidad
    {
        public VhoulMartyr()
        {
            Tipo = "Unidad";
            CostoDeMana = 2;
            Damage = 2;
            DamageActual = 2;
            DamageTotal = 2;
            Vida = 2;
            VidaActual = 2;
            VidaMaxima = 2;
            codigo = 289;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "VhoulMartyr";
            Descripcion = "Cuando Vhoul Martyr muere modifica a los aliados con +1 de ataque y +1 de vida";
        }
        public override void Matar()
        {
            Muerta = true;
            DamageRecibido = (int)-VidaActual;
            RecibeDamage = true;
            VidaActual = 0;

            for (int i = 0; i < LineaActual.Unidades.Length; i++)
            {
                try
                {
                    LineaActual.Unidades[i].VidaMaxima += 1; LineaActual.Unidades[i].VariarVida(1);
                    LineaActual.Unidades[i].Actualizar += AumentarDamage;
                    LineaActual.Unidades[i].Actualizar(LineaActual.Unidades[i]);
                }
                catch { }
            }
            LineaActual = null;

        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(1);
        }
    }
}