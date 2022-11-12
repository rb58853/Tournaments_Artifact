using System;
namespace LogicoNuevo
{
    public class Axe : Heroe
    {
        public Axe()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Axe";
            color = Color.Rojo;
            codigo = 21;

            Damage = 6;
            DamageTotal = 6;
            DamageActual = 6;

            Armadura = 2;
            ArmaduraTotal = 2;
            ArmaduraActual = 2;

            Vida = 10;
            VidaMaxima = 10;
            VidaActual = 10;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new BerserkerCall();
    }
        }
    public class BerserkerCall : Hechizo
    {

        public BerserkerCall()
        {
            Tipo = "Hechizo";
            codigo = 27;
            CostoDeMana = 6;
            color = Color.Rojo;
            Nombre = "Barracks";
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
}



