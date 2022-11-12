using UnityEngine;

namespace LogicoNuevo
{
    public class BountyHunter : Heroe
    {
        public BountyHunter()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Bounty Hunter";
            color = Color.Negro;
            codigo = 37;

            Damage = 7;
            DamageTotal = 7;
            DamageActual = 7;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 7;
            VidaMaxima = 7;
            VidaActual = 7;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }
    public class Sniper : Heroe
    {
        public Sniper()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Sniper";
            color = Color.Negro;
            codigo = 241;///CambiarCodigo

            Damage = 5;
            DamageTotal = 5;
            DamageActual = 5;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 6;
            VidaMaxima = 6;
            VidaActual = 6;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }
    public class Luna : Heroe
    {
        public Luna()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Luna";
            color = Color.Azul;
            codigo = 150;///CambiarCodigo

            Damage = 3;
            DamageTotal = 3;
            DamageActual = 3;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 8;
            VidaMaxima = 8;
            VidaActual = 8;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }
    public class Kanna : Heroe
    {
        public Kanna()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Kanna";
            color = Color.Azul;
            codigo = 134;///CambiarCodigo

            Damage = 2;
            DamageTotal = 2;
            DamageActual = 2;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 12;
            VidaMaxima = 12;
            VidaActual = 12;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }
    public class SkywrathMage : Heroe
    {
        public SkywrathMage()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Skywrath Mage";
            color = Color.Azul;
            codigo = 236;///CambiarCodigo

            Damage = 3;
            DamageTotal = 3;
            DamageActual = 3;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 6;
            VidaMaxima = 6;
            VidaActual = 6;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }

    public class Viper : Heroe
    {
        public Viper()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Viper";
            color = Color.Verde;
            codigo = 290;///CambiarCodigo

            Damage = 4;
            DamageTotal = 4;
            DamageActual = 4;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 10;
            VidaMaxima = 10;
            VidaActual = 10;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }
    public class Tinker : Heroe
    {
        public Tinker()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Tinker";
            color = Color.Negro;
            codigo = 271;///CambiarCodigo

            Damage = 7;
            DamageTotal = 7;
            DamageActual = 7;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 5;
            VidaMaxima = 5;
            VidaActual = 5;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BloodRage();
        }
    }
}



