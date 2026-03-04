using System;
using System.Collections.Generic;
using System.Xml;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Estructuras;

namespace IPC2_Proyecto1_2020XXXX.Utilidades
{
    public static class XMLWriter
    {
        // acepta cualquier enumerable (incluida nuestra ListaEnlazada)
        public static void EscribirSalida(IEnumerable<Paciente> pacientes, string ruta)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("pacientes");
            doc.AppendChild(root);

            foreach (var p in pacientes)
            {
                XmlElement xp = doc.CreateElement("paciente");
                root.AppendChild(xp);

                XmlElement datos = doc.CreateElement("datospersonales");
                xp.AppendChild(datos);
                XmlElement nombre = doc.CreateElement("nombre");
                nombre.InnerText = p.DatosPersonales.Nombre ?? "";
                datos.AppendChild(nombre);
                XmlElement edad = doc.CreateElement("edad");
                edad.InnerText = p.DatosPersonales.Edad.ToString();
                datos.AppendChild(edad);

                XmlElement per = doc.CreateElement("periodos");
                per.InnerText = p.Periodos.ToString();
                xp.AppendChild(per);
                XmlElement m = doc.CreateElement("m");
                m.InnerText = p.M.ToString();
                xp.AppendChild(m);

                XmlElement res = doc.CreateElement("resultado");
                res.InnerText = p.Resultado ?? "";
                xp.AppendChild(res);

                if (p.N > 0)
                {
                    XmlElement n = doc.CreateElement("n");
                    n.InnerText = p.N.ToString();
                    xp.AppendChild(n);
                }
                if (p.N1 > 0)
                {
                    XmlElement n1 = doc.CreateElement("n1");
                    n1.InnerText = p.N1.ToString();
                    xp.AppendChild(n1);
                }
            }

            doc.Save(ruta);
            Console.WriteLine($"Archivo de salida generado en {ruta}");
        }
    }
}
