namespace LogicoNuevo
{
    public class Lycan : Heroe
    {
        public Lycan()
        {
            update = new Update();
            update.Evento += Efecto;

            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Lycan";
            color = Color.Verde;
            codigo = 151;

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
            CartaAsignada = new SavageWolf();
            Descripcion = "Las unidades aliadas cercanas adquieren +2 de ataque.";

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
                        //LineaActual.Unidades[i].Actualizar -= AumentarDamage;
                        //LineaActual.Unidades[i].Actualizar(LineaActual.Unidades[i]);
                        //LineaActual.Unidades[i].EliminarEfecto(this);
                    }
                    catch { }
                }

                try
                {
                    if (!PoseeElEfecto(this))
                    {
                        AgregarEfecto(this);
                        Actualizar += AumentarDamage;
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
                        LineaActual.Unidades[posicion + 1].Actualizar += AumentarDamage;
                        LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                    }
                }
                catch { }

                try
                {
                    if (!LineaActual.Unidades[posicion - 1].PoseeElEfecto(this))
                    {
                        LineaActual.Unidades[posicion - 1].AgregarEfecto(this);
                        LineaActual.Unidades[posicion - 1].Actualizar += AumentarDamage;
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
                LineaActual.Unidades[posicion + 1].Actualizar -= AumentarDamage;
                LineaActual.Unidades[posicion + 1].Actualizar(LineaActual.Unidades[posicion + 1]);
                LineaActual.Unidades[posicion + 1].EliminarEfecto(this);
            }
            catch { }
            try
            {
                LineaActual.Unidades[posicion - 1].Actualizar -= AumentarDamage;
                LineaActual.Unidades[posicion - 1].Actualizar(LineaActual.Unidades[posicion - 1]);
                LineaActual.Unidades[posicion - 1].EliminarEfecto(this);
            }
            catch { }

            base.Matar();

        }

        void AumentarDamage(Unidad unidad)
        {
            unidad.DamageActual += 2;
        }
    }
    class SavageWolf : Unidad
    {
        public SavageWolf()
        {
            Tipo = "Unidad";
            CostoDeMana = 4;
            Damage = 2;
            DamageActual = 2;
            DamageTotal = 2;
            Vida = 3;
            VidaActual = 3;
            VidaMaxima = 3;
            codigo = 225;
            color = Color.Verde;
            Actualizar += Refrescar;
            Nombre = "SavageWolf";
            Descripcion = " Despues de la fase de combate modifica a Savage Wolf con +1 de ataque y +2 de vida";
        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(1);
            unidad.VidaMaxima += 2;
            unidad.VariarVida(2);
        }
        public override void Atacar(Unidad unidad)
        {
            base.Atacar(unidad);
            Actualizar += AumentarDamage;
            Actualizar(this);
            AgregarEfecto(this);
        }
        public override void Atacar(Estructura estructura)
        {
            base.Atacar(estructura);
            Actualizar += AumentarDamage;
            Actualizar(this);
            AgregarEfecto(this);
        }
    }
}



