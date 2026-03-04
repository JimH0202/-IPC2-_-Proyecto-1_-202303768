using System;
using System.Collections.Generic;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Sistema;
using IPC2_Proyecto1_2020XXXX.Utilidades;

// Establecer el directorio de trabajo a la raíz del proyecto para que las rutas relativas funcionen correctamente
string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
while (!string.IsNullOrEmpty(projectRoot) && !System.IO.File.Exists(System.IO.Path.Combine(projectRoot, "IPC2_Proyecto1_2020XXXX.csproj")))
{
    projectRoot = System.IO.Path.GetDirectoryName(projectRoot);
}
if (!string.IsNullOrEmpty(projectRoot))
{
    Environment.CurrentDirectory = projectRoot;
}

Console.WriteLine("=== Simulador de Rejillas Epidemiológicas ===");

SistemaSistema sistema = new SistemaSistema();
SimulationSession? session = null;
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
    string option = Console.ReadLine() ?? "";

    switch (option)
    {
        case "1":
            Console.Write("Ruta de entrada XML: ");
            string inputPath = Console.ReadLine() ?? "";
            // Si el usuario pegó contenido XML multilínea, leer hasta el cierre del elemento raíz
            if (!string.IsNullOrWhiteSpace(inputPath) && inputPath.TrimStart().StartsWith("<") && !inputPath.Contains("</pacientes>"))
            {
                string buffer = inputPath;
                string? next;
                while ((next = Console.ReadLine()) != null)
                {
                    buffer += "\n" + next;
                    if (next.Contains("</pacientes>")) break;
                }
                inputPath = buffer;
            }

            sistema.CargarPacientes(inputPath);
            session = null;
            break;
        case "2":
            Console.Write("Nombre del paciente a simular: ");
            string nombre = Console.ReadLine() ?? "";
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
            string outputPath = Console.ReadLine() ?? "";
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
