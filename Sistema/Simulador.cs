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
            // Si está en modo simulado, ejecutar en escala sobre una rejilla limitada
            if (paciente.Simulado)
            {
                Console.WriteLine($"Simulación escalada para paciente {paciente.DatosPersonales.Nombre ?? ""}...");
                int limitPeriods = System.Math.Min(paciente.Periodos, Utilidades.SimulationConfig.MaxGenerate);
                var rejilla = paciente.Rejilla!;

                var historial = new ListaEnlazada<string>();
                string inicial = rejilla.ToString();
                historial.AddLast(inicial);

                for (int periodo = 1; periodo <= limitPeriods; periodo++)
                {
                    rejilla = rejilla.ProximoPeriodo();
                    string estado = rejilla.ToString();

                    GraphvizGenerator gen = new GraphvizGenerator();
                    gen.Generar(rejilla, $"./ArchivosSalida/Periodos/periodo_{periodo}");

                    if (estado == inicial)
                    {
                        paciente.Resultado = "mortal_simulado";
                        paciente.N = periodo;
                        Console.WriteLine($"Patrón inicial se repite en N={periodo}. Resultado: {paciente.Resultado}");
                        break;
                    }
                    else if (historial.Contains(estado))
                    {
                        paciente.Resultado = "grave_simulado";
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
                    if (paciente.Periodos > limitPeriods)
                    {
                        paciente.Resultado = "simulado";
                        Console.WriteLine($"Se simularon {limitPeriods} periodos en escala (rejilla {paciente.Rejilla.M}). Periodos reales: {paciente.Periodos}.");
                        Console.WriteLine("Resultado estimado: SIMULADO (escala)");
                    }
                    else
                    {
                        paciente.Resultado = "leve";
                        Console.WriteLine($"*** LÍMITE DE PERÍODOS ALCANZADO ({limitPeriods} periodos) ***");
                        Console.WriteLine($"No se detectó patrón repetido dentro del limite de {limitPeriods} periodos (simulado).");
                        Console.WriteLine("Resultado: LEVE (simulado)");
                    }
                }

                return;
            }

            Console.WriteLine($"Simulando paciente {paciente.DatosPersonales.Nombre ?? ""}...");
            int maxPeriodos = paciente.Periodos;
            var rej = paciente.Rejilla!;

            var hist = new ListaEnlazada<string>();
            string init = rej.ToString();
            hist.AddLast(init);

            for (int periodo = 1; periodo <= maxPeriodos; periodo++)
            {
                rej = rej.ProximoPeriodo();
                string estado = rej.ToString();

                GraphvizGenerator gen = new GraphvizGenerator();
                gen.Generar(rej, $"./ArchivosSalida/Periodos/periodo_{periodo}");

                if (estado == init)
                {
                    paciente.Resultado = "mortal";
                    paciente.N = periodo;
                    Console.WriteLine($"Patrón inicial se repite en N={periodo}. Resultado: {paciente.Resultado}");
                    break;
                }
                else if (hist.Contains(estado))
                {
                    paciente.Resultado = "grave";
                    paciente.N = periodo;
                    paciente.N1 = periodo - hist.IndexOf(estado);
                    Console.WriteLine($"Patrón detectado en periodo {periodo} con ciclo N1={paciente.N1}. Resultado: {paciente.Resultado}");
                    break;
                }
                else
                {
                    hist.AddLast(estado);
                }
            }

            if (string.IsNullOrEmpty(paciente.Resultado))
            {
                paciente.Resultado = "leve";
                Console.WriteLine($"*** LÍMITE DE PERÍODOS ALCANZADO ({maxPeriodos} periodos) ***");
                Console.WriteLine($"No se detectó patrón repetido dentro del limite de {maxPeriodos} periodos.");
                Console.WriteLine($"Resultado: LEVE (enfermedad controlable)");
            }
        }
    }
}
