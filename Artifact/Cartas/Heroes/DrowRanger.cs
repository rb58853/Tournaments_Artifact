namespace LogicoNuevo
{
    public class DrowRanger : Heroe
    {
        ///   Update update = new Update();

        public DrowRanger()
        {
            update = new Update();
            update.Evento += Efecto;
            Tipo = "Heroe";
            EsHeroe = true;
            Nombre = "Drow Ranger ";
            Descripcion = "Todos los aliados en todas las lineas adquieren +1 de Ataque";
            color = Color.Verde;
            codigo = 84;

            Damage = 4;
            DamageTotal = 4;
            DamageActual = 4;

            Armadura = 0;
            ArmaduraTotal = 0;
            ArmaduraActual = 0;

            Vida = 7;
            VidaMaxima = 7;
            VidaActual = 7;

            PorcientoVida = 100;
            Actualizar += Refrescar;
            CartaAsignada = new Gust();

        }
        void Efecto()
        {
            if (!Muerta && LineaActual != null && !Silenciada)
                for (int i = 0; i < 3; i++)
                    for (int ii = 0; ii < 7; ii++)
                    {
                        try
                        {
                            if (!bando.lineas[i].Unidades[ii].PoseeElEfecto(this))
                            {
                                bando.lineas[i].Unidades[ii].Actualizar += AumentarDamage;
                                bando.lineas[i].Unidades[ii].AgregarEfecto(this);
                            }
                        }
                        catch { }
                        try { bando.lineas[i].Unidades[ii].Actualizar(bando.lineas[i].Unidades[ii]); } catch { }
                    }
            else
            {
                EliminarEfecto();
            }
        }

        void EliminarEfecto()
        {
            for (int i = 0; i < 3; i++)
                for (int ii = 0; ii < bando.lineas[i].Unidades.Length; ii++)
                {
                    try { bando.lineas[i].Unidades[ii].Actualizar -= AumentarDamage; } catch { }
                    try { bando.lineas[i].Unidades[ii].EliminarEfecto(this); } catch { }
                    try { bando.lineas[i].Unidades[ii].Actualizar(bando.lineas[i].Unidades[ii]); } catch { }
                }
        }
        public override void Matar()
        {
            base.Matar();
            EliminarEfecto();
        }
        void AumentarDamage(Unidad unidad)
        {
            unidad.VariarAtaque(1);
        }
    }
}



