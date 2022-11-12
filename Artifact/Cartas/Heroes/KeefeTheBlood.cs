using System;

namespace LogicoNuevo
{
    public class KeefeTheBlood : Heroe
    {
        public KeefeTheBlood()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "KeefeTheBlood";
            color = Color.Rojo;
            codigo = 135;

            Damage = 6;
            DamageTotal = 6;
            DamageActual = 6;

            Armadura = 1;
            ArmaduraTotal = 1;
            ArmaduraActual = 1;

            Vida = 11;
            VidaMaxima = 11;
            VidaActual = 11;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new FightingInstinct();
        }
    }
    public class FightingInstinct : Hechizo
    {
        public FightingInstinct()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 97;
            CostoDeMana = 3;
            color = Color.Rojo;
            Nombre = "FightingInstinct";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidad.Actualizar += AumentarDamageArmadura;
            unidad.AumentaDamage = new Tuple<bool, int>(true, 1);
            unidad.AumentaArmadura = new Tuple<bool, int>(true, 1);
            unidad.Actualizar(unidad);
        }
        void AumentarDamageArmadura(Unidad unidad)
        {
            unidad.DamageActual += 1;
            unidad.ArmaduraActual += 1;
        }

        public override bool Exepcion(Unidad unidad)
        {
            if (unidad.Equipo == Equipo)
            {

                if (!unidad.EsHeroe || unidad.color != Color.Rojo)
                {
                    exepcion = "Solo puedes seleccionar un heroe rojo";
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
}



