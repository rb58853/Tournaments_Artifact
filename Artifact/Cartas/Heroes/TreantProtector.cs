namespace LogicoNuevo
{
    public class TreantProtector : Heroe
    {
        public TreantProtector()
        {
            update = new Update();
            update.Evento += Efecto;

            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Treant Protector";
            color = Color.Verde;
            codigo = 276;

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
            CartaAsignada = new RoseleafDruid();
            Descripcion = "Las unidades aliadas cercanas adquieren +2 de armadura.";

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
                        LineaActual.Unidades[posicion + 1].Actualizar += AumentarArmadura;
                        LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                        LineaActual.Unidades[posicion + 1].AgregarEfecto(this);
                    }
                }
                catch { }

                try
                {
                    if (!LineaActual.Unidades[posicion - 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion - 1].Actualizar += AumentarArmadura;
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
            unidad.ArmaduraActual += 2;
        }
    }
    public class RoseleafDruid : Unidad
    {
        public RoseleafDruid()
        {
            Tipo = "Unidad";
            CostoDeMana = 4;
            Damage = 2;
            DamageActual = 2;
            DamageTotal = 2;
            Vida = 6;
            VidaActual = 6;
            VidaMaxima = 6;
            codigo = 219;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "RoseleafDruid";
            Descripcion = "Tu torre Adquiere +1 de mana";
        }
        public override void Jugado()
        {
            LineaActual.estructura.AumentarMana();
        }
        public override void Matar()
        {
            LineaActual.estructura.VariarManaMaximo(-1);
            base.Matar();
        }
    }
}
