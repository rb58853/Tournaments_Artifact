using System;

namespace LogicoNuevo
{
    public class Magnus : Heroe
    {
        public Magnus()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Magnus";
            color = Color.Verde;
            codigo = 152;

            Damage = 4;
            DamageTotal = 4;
            DamageActual = 4;

            Armadura = 1;
            ArmaduraTotal = 1;
            ArmaduraActual = 1;

            Vida = 9;
            VidaMaxima = 9;
            VidaActual = 9;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new Empower();
        }
    }
    public class Empower : Hechizo
    {
        public Empower()
        {
            Tipo = "Hechizo";
            Target = true;
            Aliado = true;
            codigo = 90;
            CostoDeMana = 4;
            color = Color.Verde;
            Nombre = "Empower";
            Descripcion = "Modifica a una unidad con +3 de ataque y +3 de hendidura";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidad.Actualizar += AumentarDamage;
            unidad.AumentaDamage = new Tuple<bool, int>(true, 3);
            unidad.Actualizar(unidad);
            unidad.AgregarEfecto(this);
        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.DamageActual += 3;
            unidad.HendiduraActual += 3;
        }
    }
}



