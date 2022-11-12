namespace LogicoNuevo
{
    public class FarvhanTheDreamer : Heroe
    {
        public FarvhanTheDreamer()
        {
            update = new Update();
            update.Evento += Efecto;

            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Farvhan The Dreamer";
            color = Color.Verde;
            codigo = 95;


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
            CartaAsignada = new ProwlerVanguard();
            Descripcion = "Las unidades aliadas cercanas adquieren +1 de armadura.";

        }
        int posicion = 0;
        void Efecto()
        {
            if (!Muerta && LineaActual != null)
            {
                for (int i = 0; i < LineaActual.Unidades.Length; i++)
                {
                    ///////Descomentar lo de dentro del try En Caso de Implementar los Swap 
                    try
                    {

                        //   LineaActual.Unidades[i].Actualizar -= AumentarArmadura;
                        //   LineaActual.Unidades[i].Actualizar(LineaActual.Unidades[i]);
                        //   LineaActual.Unidades[i].EliminarEfecto(this);
                        if (LineaActual.Unidades[i] == this) posicion = i;
                    }
                    catch { }
                }

                try
                {
                    if (!PoseeElEfecto(this))
                    {
                        AgregarEfecto(this);
                        Actualizar += AumentarArmadura;
                        Actualizar(this);
                    }
                }
                catch
                { }
                try
                {
                    if (!LineaActual.Unidades[posicion + 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion + 1].AgregarEfecto(this);
                        LineaActual.Unidades[posicion + 1].Actualizar += AumentarArmadura;
                        LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                    }
                }
                catch { }

                try
                {
                    if (!LineaActual.Unidades[posicion - 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion - 1].AgregarEfecto(this);
                        LineaActual.Unidades[posicion - 1].Actualizar += AumentarArmadura;
                        LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                    }
                }
                catch { }
            }
        }
        public override void Matar()
        {
            try
            {
                LineaActual.Unidades[posicion + 1].Actualizar -= AumentarArmadura;
                LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                LineaActual.Unidades[posicion + 1].EliminarEfecto(this);
            }
            catch { }
            try
            {
                LineaActual.Unidades[posicion - 1].Actualizar -= AumentarArmadura;
                LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                LineaActual.Unidades[posicion - 1].EliminarEfecto(this);
            }
            catch { }

            base.Matar();

        }

        void AumentarArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual += 1;
        }
    }
    public class ProwlerVanguard : Unidad
    {
        public ProwlerVanguard()
        {
            update = new Update();
            update.Evento += Efecto;
            Tipo = "Unidad";
            CostoDeMana = 4;
            Damage = 0;
            DamageActual = 0;
            DamageTotal = 0;
            Vida = 6;
            VidaActual = 6;
            VidaMaxima = 6;
            codigo = 194;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "Prowler Vanguard";
            Descripcion = "Los aliados cercanos a Prowler Vanguard obtienen +1 de armadura";
        }

        int posicion = 0;

        void Efecto()
        {
            if (!Muerta && LineaActual != null)
            {
                for (int i = 0; i < LineaActual.Unidades.Length; i++)
                {
                    ///////Descomentar lo de dentro del try En Caso de Implementar los Swap 
                    try
                    {
                        if (LineaActual.Unidades[i] == this) posicion = i;
                        //LineaActual.Unidades[i].Actualizar -= AumentarArmadura;
                        //LineaActual.Unidades[i].Actualizar(LineaActual.Unidades[i]);
                        //LineaActual.Unidades[i].EliminarEfecto(this);
                    }
                    catch { }
                }

                try
                {
                    if (!PoseeElEfecto(this))
                    {
                        Actualizar += AumentarArmadura;
                        Actualizar(this);
                        AgregarEfecto(this);
                    }
                }
                catch
                { }
                try
                {
                    if (!LineaActual.Unidades[posicion + 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion + 1].AgregarEfecto(this);
                        LineaActual.Unidades[posicion + 1].Actualizar += AumentarArmadura;
                        LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                    }
                }
                catch { }

                try
                {
                    if (!LineaActual.Unidades[posicion - 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion - 1].AgregarEfecto(this);
                        LineaActual.Unidades[posicion - 1].Actualizar += AumentarArmadura;
                        LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                    }
                }
                catch { }
            }
        }
        public override void Matar()
        {
            try
            {
                LineaActual.Unidades[posicion + 1].Actualizar -= AumentarArmadura;
                LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                LineaActual.Unidades[posicion + 1].EliminarEfecto(this);
            }
            catch { }
            try
            {
                LineaActual.Unidades[posicion - 1].Actualizar -= AumentarArmadura;
                LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                LineaActual.Unidades[posicion - 1].EliminarEfecto(this);
            }
            catch { }

            base.Matar();

        }

        void AumentarArmadura(Unidad unidad)
        {
            unidad.ArmaduraActual += 1;
        }
    }

}
