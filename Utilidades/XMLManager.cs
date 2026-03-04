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
            doc.Load(ruta);
            var nPacientes = doc.DocumentElement.SelectNodes("/pacientes/paciente");
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
                p.Rejilla = new Rejilla(m);

                var celdas = nodo.SelectNodes("rejilla/celda");
                foreach (XmlNode cel in celdas)
                {
                    if (int.TryParse(cel.Attributes["f"]?.Value, out int f) &&
                        int.TryParse(cel.Attributes["c"]?.Value, out int c))
                    {
                        if (f > 0 && f <= m && c > 0 && c <= m)
                            p.Rejilla.Matriz[f - 1, c - 1].Infectada = true;
                    }
                }

                pacientes.AddLast(p);
            }

            return pacientes;
        }
    }
}
