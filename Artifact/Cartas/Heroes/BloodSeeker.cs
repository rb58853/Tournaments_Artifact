using System;
namespace LogicoNuevo
{
    public class BloodSeeker : Heroe
    {
        public BloodSeeker()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Blood Seeker";
            color = Color.Negro;
            codigo = 34;

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
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            if (unidad.DebeMorir)
            {
                VariarVida(VidaMaxima - VidaActual);
            }

        }
    }
    public class BloodRage : Hechizo
    {
        Temporizador _Duracion;
        public BloodRage()
        {
            Tipo = "Hechizo";
            Target = true;
            Enemigo = true;
            Aliado = true;
            codigo = 33;
            CostoDeMana = 4;
            color = Color.Negro;
            Nombre = "Blood Rage";
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

            unidad.Actualizar += unidad.Silenciar;
            unidad.Actualizar += AumentarAtaque;
            unidad.Actualizar(unidad);
            unidad.AumentaDamage = new Tuple<bool, int>(true, 4);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void EliminarSilencio(Unidad unidad)
        {
            unidad.Actualizar -= unidad.Silenciar;
            unidad.Actualizar -= AumentarAtaque;
            unidad.Actualizar(unidad);
            _Duracion.Desactivar();
        }
        void AumentarAtaque(Unidad unidad)
        {
            unidad.VariarAtaque(4);
        }
    }
}



