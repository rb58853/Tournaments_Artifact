namespace LogicoNuevo
{
    public class AphoteosisBalde : ItemDamage
    {

        public AphoteosisBalde()
        {
            color = Color.Vacio;
            Tipo = "Item";
            EsItem = true;
            codigo = 8;
            Nombre = "Aphoteosis Balde";
            Valor = 8;
            tipoItem = TipoItem.Damage;
        }

        public override void Efecto()
        {
            throw new System.NotImplementedException();
        }
    }

    public class AssassinVeil : ItemVida
    {
        public AssassinVeil()
        {
            color = Color.Vacio;
            Tipo = "Item";
            Precio = 4;
            EsItem = true;
            codigo = 14;
            Nombre = "Assassin Veil ";
            Valor = 4;
            tipoItem = TipoItem.Vida;
        }

        public override void Efecto()
        {
            throw new System.NotImplementedException();
        }
    }

    public class BarbedMail : ItemArmadura
    {

        public BarbedMail()
        {
            color = Color.Vacio;
            Tipo = "Item";
            Precio = 5;
            EsItem = true;
            codigo = 23;
            Nombre = "Barbed Mail";
            Valor = 1;
            tipoItem = TipoItem.Armadura;
        }

        public override void Variar(Unidad heroe)
        {
            base.Variar(heroe);
            heroe.Rebotar += 2;
        }
    }
    public class BladeOfTheVigil : ItemDamage
    {

        public BladeOfTheVigil()
        {
            color = Color.Vacio;
            Tipo = "Item";
            Precio = 7;
            EsItem = true;
            codigo = 31;
            Nombre = "Blade Of The Vigil";
            Valor = 2;
            tipoItem = TipoItem.Damage;
        }

        public override void Variar(Unidad heroe)
        {
            base.Variar(heroe);
            heroe.Hendidura += 4;
        }
    }

    public class Broadsword : ItemDamage
    {

        public Broadsword()
        {
            color = Color.Vacio;
            Tipo = "Item";
            Precio = 6;
            EsItem = true;
            codigo = 40;
            Nombre = "Broadsword";
            Valor = 4;
            tipoItem = TipoItem.Damage;
        }
    }
    public class Chainmail : ItemDamage
    {

        public Chainmail()
        {
            color = Color.Vacio;
            Tipo = "Item";
            Precio = 6;
            EsItem = true;
            codigo = 49;
            Nombre = "Chainmail";
            Valor = 2;
            tipoItem = TipoItem.Armadura;
        }

    }
}



