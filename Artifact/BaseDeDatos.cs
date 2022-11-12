using System.Collections.Generic;
using System.IO;

namespace LogicoNuevo
{
    public class BaseDeDatos
    {
        #region Graficos 
        public static int graficos = 2;
        #endregion
        public static bool Modo3raPersona;
        static string Start = "C:\\Users\\Raul\\Documents\\Artifact\\save\\";

        static string[] datos;

        public static void CargarSalva()
        {
            try
            {
                datos = File.ReadAllLines(Start + "data.text");
                string[] data = File.ReadAllLines(Start + "cartas.text");

                for (int i = 0; i < data.Length; i++)
                    try { ListaDeCantidades.Cantidad[i] = int.Parse(data[i]); } catch { }

                for (int i = 0; i < datos.Length; i++)
                {
                    try
                    {
                        string[] temp = File.ReadAllLines(Start + datos[i]);
                        ListaMazos.mazos.Add(new Mazo(datos[i]));
                        for (int j = 0; j < temp.Length; j++)
                        {
                            AddCarta(ListaMazos.mazos[ListaMazos.mazos.Count - 1], int.Parse(temp[j]));
                        }
                    }
                    catch { continue; }
                }
            }
            catch
            {
            }
        }

        static void AddCarta(Mazo mazo, int n)
        {
            if (ListaCartas.GetCarta(n).EsItem)
            {
                mazo.AddItems(ListaCartas.GetCarta(n) as Item);
                return;
            }
            try
            {
                if ((ListaCartas.GetCarta(n) as Unidad).EsHeroe)
                {
                    mazo.AddHeroe(ListaCartas.GetCarta(n) as Heroe);
                    return;
                }
            }
            catch { }
            mazo.AddCarta(ListaCartas.GetCarta(n));
        }

        public static void SalvarMazo(Mazo mazo)
        {
            string temp = mazo.Nombre;
            string basico = Start;
            string direccion = basico + temp;
            string direccionData = basico + "data.text";
            List<string> temporal = new List<string>();
            for (int i = 0; i < datos.Length; i++)
            {
                temporal.Add(datos[i]);
            }
            temporal.Add(mazo.Nombre);

            List<string> salva = new List<string>();

            for (int i = 0; i < 5; i++)
                salva.Add(mazo.heroes[i].codigo.ToString());
            for (int i = 0; i < mazo.items.Count; i++)
                salva.Add(mazo.items[i].codigo.ToString());
            for (int i = 0; i < mazo.cartas.Count; i++)
                salva.Add(mazo.cartas[i].codigo.ToString());

            File.WriteAllLines(direccion, salva.ToArray());
            File.WriteAllLines(direccionData, temporal.ToArray());
        }
        public static void SalvarCantidad()
        {
            string basico = Start;
            string direccion = basico + "cartas.text";

            List<string> salva = new List<string>();

            for (int i = 0; i < ListaDeCantidades.Cantidad.Count; i++)
                salva.Add(ListaDeCantidades.Cantidad[i].ToString());

            File.WriteAllLines(direccion, salva.ToArray());

        }
    }
}

