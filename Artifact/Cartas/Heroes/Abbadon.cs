using System;
namespace LogicoNuevo
{
    public class Abbadon : Heroe
    {
        public Abbadon()
        {
            update = new Update();
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Abadon";
            color = Color.Verde;
            codigo = 1;
            Vida = 9;
            VidaMaxima = 9;
            VidaActual = 9;
            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;
            Damage = 4;
            DamageTotal = 4;
            DamageActual = 4;
            Actualizar += Refrescar;
            Regeneracion = 1;
            CartaAsignada = new AphoticShield();
        }
    }

    public class AphoticShield : Hechizo
    {
        Temporizador _Duracion;
        Unidad unidadAfectada;

        public AphoticShield()
        {
            Tipo = "Hechizo";
            Nombre = "Aphotic Shield";
            color = Color.Verde;
            codigo = 7;
            CostoDeMana = 2;
            Target = true;
            Aliado = true;
        }

        void ActivarTemporizador()
        {
            _Duracion = new Temporizador(1);
            _Duracion.Evento += EliminarEfectos;
            _Duracion.Activar();
        }

        public override void Efecto()
        {
            throw new System.NotImplementedException();
        }

        public override void Efecto(Unidad unidad)
        {
            if (!Costos()) return;

            unidadAfectada = unidad;
            unidad.Actualizar += AumentarLaArmadura;
            unidad.Actualizar(unidad);
            unidad.AumentaArmadura = new Tuple<bool, int>(true, 2);
            ActivarTemporizador();
            _Duracion.AddUnidad(unidad);
        }
        void AumentarLaArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual += 2;
            unidad.RebotarActual += 2;
        }

        void EliminarEfectos(Unidad unidad)
        {
            unidad.Actualizar -= AumentarLaArmadura;
            unidad.Actualizar(unidad);
        }
    }
}





