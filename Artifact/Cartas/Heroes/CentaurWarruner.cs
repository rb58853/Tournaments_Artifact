namespace LogicoNuevo
{
    public class CentaurWarruner : Heroe
    {
        public CentaurWarruner()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            RebotarActual = 2;
            Rebotar = 2;
            Nombre = "Centaur Warruner";
            color = Color.Rojo;
            codigo = 47;

            Damage = 4;
            DamageTotal = 4;
            DamageActual = 4;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 14;
            VidaMaxima = 14;
            VidaActual = 14;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new DoubleEdge();

        }
    }
}



