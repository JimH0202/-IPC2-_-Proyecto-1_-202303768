using System;

namespace IPC2_Proyecto1_2020XXXX.Modelos
{
    public class Paciente
    {
        public DatosPersonales DatosPersonales { get; set; }
        public int Periodos { get; set; }
        public int M { get; set; }
        public Rejilla Rejilla { get; set; }

        // resultados
        public string Resultado { get; set; }
        public int N { get; set; }
        public int N1 { get; set; }

        public Paciente()
        {
            DatosPersonales = new DatosPersonales();
        }
    }

    public class DatosPersonales
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
}
