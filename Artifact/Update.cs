using System;
using System.Timers;
namespace LogicoNuevo
{
    public delegate void Ciclo();
    public class Update
    {
        public Ciclo Evento;
        Timer timer;
        public bool Activo { get; private set; }

        public Update()
        {
            IniciarTimer();
        }
        void IniciarTimer()
        {
            timer = new Timer(100);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += timer_Tick;
            Activo = true;
        }

        void timer_Tick(System.Object source, ElapsedEventArgs e)
        {
            try { Evento(); } catch { }
        }
    }
}



