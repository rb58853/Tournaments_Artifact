namespace LogicoNuevo
{
    public class Enchantres : Heroe
    {
        // Update update = new Update();

        public Enchantres()
        {
            update = new Update();
            update.Evento += Efecto;
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Enchantres ";
            color = Color.Verde;
            codigo = 91;

            Damage = 4;
            DamageTotal = 4;
            DamageActual = 4;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 8;
            VidaMaxima = 8;
            VidaActual = 8;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new VerdantRefuge();
            Descripcion = "Las unidades aliadas cercanas adquieren +2 de regeneracion.";
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
                        //LineaActual.Unidades[i].Actualizar -= AumentarRegeneracion;
                        //LineaActual.Unidades[i].Actualizar(LineaActual.Unidades[i]);
                        //LineaActual.Unidades[i].EliminarEfecto(this);
                    }
                    catch { }
                }

                try
                {
                    if (!PoseeElEfecto(this))
                    {
                        Actualizar += AumentarRegeneracion;
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
                        LineaActual.Unidades[posicion + 1].Actualizar += AumentarRegeneracion;
                        LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                        LineaActual.Unidades[posicion + 1].AgregarEfecto(this);
                    }
                }
                catch { }

                try
                {
                    if (!LineaActual.Unidades[posicion - 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion - 1].Actualizar += AumentarRegeneracion;
                        LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                        LineaActual.Unidades[posicion - 1].AgregarEfecto(this);
                    }
                }
                catch { }
            }
        }
        public override void Matar()
        {
            try
            {
                LineaActual.Unidades[posicion + 1].Actualizar -= AumentarRegeneracion;
                LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                LineaActual.Unidades[posicion + 1].EliminarEfecto(this);
            }
            catch { }
            try
            {
                LineaActual.Unidades[posicion - 1].Actualizar -= AumentarRegeneracion;
                LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                LineaActual.Unidades[posicion - 1].EliminarEfecto(this);
            }
            catch { }

            base.Matar();

        }

        void AumentarRegeneracion(Unidad unidad)
        {
            unidad.Regeneracion += 2;
        }
    }
}



