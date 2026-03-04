using System;
using System.Xml;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Estructuras;

namespace IPC2_Proyecto1_2020XXXX.Utilidades
{
    public static class XMLManager
    {
        // Devuelve una lista enlazada construida por el alumno
        public static ListaEnlazada<Paciente> CargarPacientes(string ruta)
        {
            var pacientes = new ListaEnlazada<Paciente>();

            XmlDocument doc = new XmlDocument();
            try
            {
                // Si el usuario pegó el contenido XML en lugar de una ruta, cargar desde la cadena
                if (!string.IsNullOrWhiteSpace(ruta) && ruta.TrimStart().StartsWith("<"))
                {
                    doc.LoadXml(ruta);
                }
                else
                {
                    doc.Load(ruta);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error al cargar XML: {ex.Message}");
                Console.WriteLine("Asegúrese de introducir una ruta válida o pegar contenido XML completo cuando se le solicite la ruta.");
                return pacientes;
            }

            var nPacientes = doc.DocumentElement?.SelectNodes("/pacientes/paciente");
            if (nPacientes == null) return pacientes;
            foreach (XmlNode nodo in nPacientes)
            {
                Paciente p = new Paciente();
                p.DatosPersonales.Nombre = nodo.SelectSingleNode("datospersonales/nombre")?.InnerText;
                int.TryParse(nodo.SelectSingleNode("datospersonales/edad")?.InnerText, out int edad);
                p.DatosPersonales.Edad = edad;
                int.TryParse(nodo.SelectSingleNode("periodos")?.InnerText, out int per);
                p.Periodos = per;
                int.TryParse(nodo.SelectSingleNode("m")?.InnerText, out int m);
                p.M = m;

                // Si la rejilla o el número de periodos supera el umbral de generación, activar modo simulado
                bool simulado = (m > SimulationConfig.MaxGenerate) || (per > SimulationConfig.MaxGenerate);
                p.Simulado = simulado;

                int gridToCreate = simulado ? System.Math.Min(m, SimulationConfig.MaxGenerate) : m;
                p.Rejilla = new Rejilla(gridToCreate);

                var celdas = nodo.SelectNodes("rejilla/celda");
                int contadorInfectadas = 0;
                if (celdas != null)
                {
                    foreach (XmlNode cel in celdas)
                    {
                        if (int.TryParse(cel.Attributes?["f"]?.Value, out int f) &&
                            int.TryParse(cel.Attributes?["c"]?.Value, out int c))
                        {
                            if (f > 0 && f <= m && c > 0 && c <= m)
                            {
                                // Mapear posiciones originales a la rejilla creada (si es simulada, la rejilla creada es más pequeña)
                                int targetM = p.Rejilla.M;
                                int idxF = (int)System.Math.Floor((f - 1) * (double)targetM / m);
                                int idxC = (int)System.Math.Floor((c - 1) * (double)targetM / m);
                                if (idxF < 0) idxF = 0; if (idxF >= targetM) idxF = targetM - 1;
                                if (idxC < 0) idxC = 0; if (idxC >= targetM) idxC = targetM - 1;

                                contadorInfectadas++;
                                p.Rejilla!.Matriz[idxF, idxC].Infectada = true;
                            }
                        }
                    }
                }

                p.InicialInfectadas = contadorInfectadas;

                pacientes.AddLast(p);
            }

            return pacientes;
        }
    }
}
