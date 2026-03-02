using System;
using System.Collections.Generic;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Sistema;
using IPC2_Proyecto1_2020XXXX.Utilidades;

namespace IPC2_Proyecto1_2020XXXX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Simulador de Rejillas Epidemiológicas ===");

            SistemaSistema sistema = new SistemaSistema();
            SimulationSession session = null;
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMenú:");
                Console.WriteLine("1. Cargar archivo XML");
                Console.WriteLine("2. Elegir paciente para análisis");
                Console.WriteLine("3. Ejecutar un periodo de forma manual");
                Console.WriteLine("4. Ejecutar automáticamente todos los periodos");
                Console.WriteLine("5. Generar salida XML");
                Console.WriteLine("6. Limpiar memoria");
                Console.WriteLine("7. Salir");
                Console.Write("Opción: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Ruta de entrada XML: ");
                        string inputPath = Console.ReadLine();
                        sistema.CargarPacientes(inputPath);
                        session = null;
                        break;
                    case "2":
                        Console.Write("Nombre del paciente a simular: ");
                        string nombre = Console.ReadLine();
                        var p = sistema.ObtenerPaciente(nombre);
                        if (p == null)
                        {
                            Console.WriteLine("Paciente no encontrado.");
                        }
                        else
                        {
                            session = new SimulationSession(p);
                            Console.WriteLine("Paciente seleccionado. Puede ejecutar periodos de forma manual o automática.");
                        }
                        break;
                    case "3":
                        if (session == null)
                        {
                            Console.WriteLine("Primero debe elegir un paciente.");
                        }
                        else
                        {
                            bool finished = session.Step();
                            if (finished) session = null;
                        }
                        break;
                    case "4":
                        if (session == null)
                        {
                            Console.WriteLine("Primero debe elegir un paciente.");
                        }
                        else
                        {
                            session.RunAll();
                            session = null;
                        }
                        break;
                    case "5":
                        Console.Write("Ruta de salida XML: ");
                        string outputPath = Console.ReadLine();
                        sistema.GenerarSalida(outputPath);
                        break;
                    case "6":
                        sistema.Limpiar();
                        session = null;
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            }

            Console.WriteLine("Saliendo...");
        }
    }
}
