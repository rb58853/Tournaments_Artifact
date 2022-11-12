using System;

namespace LogicoNuevo
{
    public class Bristleback : Heroe
    {
        public Bristleback()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Bristleback";
            color = Color.Rojo;
            codigo = 39;

            Damage = 8;
            DamageTotal = 8;
            DamageActual = 8;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 12;
            VidaMaxima = 12;
            VidaActual = 12;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new ViscousNasalGoo();
            Descripcion = "Modifica a BristleBack con +2 de armadura despues de que el heroe que lo bloquea muere. ";
        }
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            try
            {
                if (unidad.DebeMorir && unidad.EsHeroe)
                {
                    Actualizar += AumentarArmadura;
                    AgregarEfecto(ListaCartas.GetCarta(codigo));
                    Actualizar(this);
                }
            }
            catch { }
        }
        void AumentarArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual += 2;
        }
    }
    public class ViscousNasalGoo : Hechizo
    {
        public ViscousNasalGoo()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            codigo = 292;
            CostoDeMana = 4;
            color = Color.Rojo;
            Nombre = "Viscous Nasal Goo";
            Descripcion = "Modifica a una unidad con -2 de armadura";
        }

        public override void Efecto()
        {
            throw new NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;
            unidad.Actualizar += BajaArmadura;
            unidad.BajaArmadura = new Tuple<bool, int>(true, -2);
            unidad.Actualizar(unidad);
            unidad.AgregarEfecto(this);
        }
        void BajaArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual -= 2;
        }
    }
}



