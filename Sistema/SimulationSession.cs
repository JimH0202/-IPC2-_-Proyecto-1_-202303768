using System;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Utilidades;
using IPC2_Proyecto1_2020XXXX.Estructuras;

namespace IPC2_Proyecto1_2020XXXX.Sistema
{
    public class SimulationSession
    {
        public Paciente Paciente { get; }
        public Rejilla Actual { get; private set; }
        public int Periodo { get; private set; }
        private ListaEnlazada<string> historial;
        private string inicial;

        public SimulationSession(Paciente paciente)
        {
            Paciente = paciente;
            Actual = paciente.Rejilla!;
            Periodo = 0;
            historial = new ListaEnlazada<string>();
            inicial = Actual.ToString();
            historial.AddLast(inicial);
        }

        /// <summary>
        /// Avanza un periodo; devuelve true si la simulación ha terminado (resultado determinado o límite alcanzado).
        /// </summary>
        public bool Step()
        {
            Periodo++;
            Actual = Actual.ProximoPeriodo();
            string estado = Actual.ToString();

            GraphvizGenerator gen = new GraphvizGenerator();
            gen.Generar(Actual, $"./ArchivosSalida/Periodos/periodo_{Periodo}");

            Console.WriteLine($"Periodo {Periodo}: sanas={Cuenta(false)} infectadas={Cuenta(true)}");

            if (estado == inicial)
            {
                Paciente.Resultado = "mortal";
                Paciente.N = Periodo;
                Console.WriteLine($"Patrón inicial se repite en N={Periodo}. Resultado: {Paciente.Resultado}");
                return true;
            }
            else if (historial.Contains(estado))
            {
                Paciente.Resultado = "grave";
                Paciente.N = Periodo;
                Paciente.N1 = Periodo - historial.IndexOf(estado);
                Console.WriteLine($"Patrón detectado en periodo {Periodo} con ciclo N1={Paciente.N1}. Resultado: {Paciente.Resultado}");
                return true;
            }
            else
            {
                historial.AddLast(estado);
            }

            int limitPeriods = Paciente.Simulado ? System.Math.Min(Paciente.Periodos, Utilidades.SimulationConfig.MaxGenerate) : Paciente.Periodos;
            if (Periodo >= limitPeriods)
            {
                if (Paciente.Simulado && Paciente.Periodos > limitPeriods)
                {
                    Paciente.Resultado = "simulado";
                    Console.WriteLine($"Se han ejecutado {limitPeriods} periodos en escala (rejilla {Actual.M}). Periodos reales: {Paciente.Periodos}. Resultado estimado: SIMULADO");
                }
                else
                {
                    Paciente.Resultado = "leve";
                    Console.WriteLine($"*** LÍMITE DE PERÍODOS ALCANZADO ({limitPeriods} periodos) ***");
                    Console.WriteLine($"No se detectó patrón repetido dentro del limite de {limitPeriods} periodos.");
                    Console.WriteLine($"Resultado: LEVE (enfermedad controlable)");
                }
                return true;
            }

            return false;
        }

        public void RunAll()
        {
            while (!Step()) { }
        }

        private int Cuenta(bool infectada)
        {
            int c = 0;
            for (int i = 0; i < Actual.M; i++)
                for (int j = 0; j < Actual.M; j++)
                    if (Actual.Matriz[i, j].Infectada == infectada) c++;
            return c;
        }
    }
}
