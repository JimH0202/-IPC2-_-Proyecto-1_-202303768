using System;

namespace IPC2_Proyecto1_2020XXXX.Modelos
{
    public class Paciente
    {
        public DatosPersonales DatosPersonales { get; set; }
        public int Periodos { get; set; }
        public int M { get; set; }
        public Rejilla? Rejilla { get; set; }

        // modo de carga rápida para entradas muy grandes
        public bool Simulado { get; set; } = false;
        // número de celdas infectadas iniciales (usado por el modo simulado)
        public int InicialInfectadas { get; set; } = 0;

        // resultados
        public string? Resultado { get; set; }
        public int N { get; set; }
        public int N1 { get; set; }

        public Paciente()
        {
            DatosPersonales = new DatosPersonales();
        }
    }

    public class DatosPersonales
    {
        public string? Nombre { get; set; }
        public int Edad { get; set; }
    }
}
