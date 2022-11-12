namespace LogicoNuevo
{
    public class Zeus : Heroe
    {
        public Zeus()
        {
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Zeus";
            color = Color.Azul;
            codigo = 300;

            Damage = 3;
            DamageTotal = 3;
            DamageActual = 3;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 7;
            VidaMaxima = 7;
            VidaActual = 7;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new ThundergodsWrath();
        }
    }
    public class ThundergodsWrath : Hechizo
    {
        public ThundergodsWrath()
        {
            Tipo = "Hechizo";
            codigo = 264;
            CostoDeMana = 7;
            color = Color.Azul;
            Nombre = "Thundergod's Wrath";
        }

        public override void Efecto()
        {
            if (!Costos()) return;

            for (int ii = 0; ii < 3; ii++)
            {
                for (int i = 0; i < bandoOpuesto().lineas[ii].Unidades.Length; i++)
                    try
                    {
                        if (bandoOpuesto().lineas[ii].Unidades[i].EsHeroe)
                        {
                            bandoOpuesto().lineas[ii].Unidades[i].VariarVida(4);
                            bandoOpuesto().lineas[ii].Unidades[i].Rayo = true;
                        }
                    }
                    catch { }
            }
        }
        public override void Efecto(Unidad unidad)
        {
        }
    }
}


