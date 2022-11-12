using System;
using System.Collections.Generic;

namespace LogicoNuevo
{
    public delegate void Tiempo(Unidad unidad);
    public delegate void TiempoSimple();
    public class Temporizador
    {
        int contador = 0;
        int Frecuencia;
        bool Enabled;
        Unidad unidad;
        public Tiempo Evento;
        public bool AutoReset;
        public List<Unidad> unidades;
        public Artifact juego;

        public Temporizador(int frecuencia)
        {
            //Inicializa un temporizador que se activa a los frecuencia(n) turnos
            Frecuencia = frecuencia;
            unidades = new List<Unidad>();
        }
        void CrecerContador()
        {
            //Crece el contador Hasta que el mismo llega a la acantidad de turnos prefijada
            if (Enabled)
            {
                contador++;
                if (contador == Frecuencia)
                {
                    contador = 0;
                    LanzarEvento();
                    if (!AutoReset)
                        Desactivar();
                }
            }
        }
        public void AddUnidad(Unidad _unidad)
        {
            //Agrega una unidad al temporizador para que actue sobre la misma
            unidad = _unidad;
            unidades.Add(_unidad);
            _unidad.juego.EventoTiempo += CrecerContador;//en esta linea se agrega el evenento crecer contador al eventoTiempo del juego de 
            //la partida de esta manera actua mi Temporizador por turnos
        }
        public void Activar()
        {
            //activa el temporizador
            Enabled = true;
            contador = 0;
        }
        public void Desactivar()
        {
            Enabled = false;
        }
        void LanzarEvento()
        {
            Evento(unidad);
            //for (int i = 0; i < unidades.Count; i++)
            //{
            //    try { Evento(unidades[i]); } catch { }
            //}
        }
    }
    public class TemporizadorSimple
    {
        int contador = 0;
        int Frecuencia;
        bool Enabled;
        public TiempoSimple Evento;
        public TemporizadorSimple(int frecuencia)
        {
            Frecuencia = frecuencia;
            JuegoEstatico.juego.EventoTiempo += CrecerContador;
        }
        void CrecerContador()
        {
            if (Enabled)
            {
                contador++;
                if (contador == Frecuencia)
                {
                    contador = 0;
                    LanzarEvento();
                }
            }
        }
        public void Activar()
        {
            Enabled = true;
            contador = 0;
        }
        public void Desactivar()
        {
            Enabled = false;
        }
        void LanzarEvento()
        {
            Evento();
        }

    }
}