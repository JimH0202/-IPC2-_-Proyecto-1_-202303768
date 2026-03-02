using System;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Utilidades;
using IPC2_Proyecto1_2020XXXX.Estructuras;

namespace IPC2_Proyecto1_2020XXXX.Sistema
{
    public static class Simulador
    {
        /// <summary>
        /// Ejecuta la simulación completa de un paciente (usa para "simular todos" o cuando se desee el cálculo inmediato).
        /// </summary>
        public static void RunAll(Paciente paciente)
        {
            Console.WriteLine($"Simulando paciente {paciente.DatosPersonales.Nombre}...");
            int maxPeriodos = paciente.Periodos;
            var rejilla = paciente.Rejilla;

            var historial = new ListaEnlazada<string>();
            string inicial = rejilla.ToString();
            historial.AddLast(inicial);

            for (int periodo = 1; periodo <= maxPeriodos; periodo++)
            {
                rejilla = rejilla.ProximoPeriodo();
                string estado = rejilla.ToString();

                GraphvizGenerator gen = new GraphvizGenerator();
                gen.Generar(rejilla, $"./ArchivosSalida/Periodos/periodo_{periodo}");

                if (estado == inicial)
                {
                    paciente.Resultado = "mortal";
                    paciente.N = periodo;
                    Console.WriteLine($"Patrón inicial se repite en N={periodo}. Resultado: {paciente.Resultado}");
                    break;
                }
                else if (historial.Contains(estado))
                {
                    paciente.Resultado = "grave";
                    paciente.N = periodo;
                    paciente.N1 = periodo - historial.IndexOf(estado);
                    Console.WriteLine($"Patrón detectado en periodo {periodo} con ciclo N1={paciente.N1}. Resultado: {paciente.Resultado}");
                    break;
                }
                else
                {
                    historial.AddLast(estado);
                }
            }

            if (string.IsNullOrEmpty(paciente.Resultado))
            {
                paciente.Resultado = "leve";
                Console.WriteLine("No se detectó patrón repetido dentro del límite, resultado leve.");
            }
        }
    }
}
