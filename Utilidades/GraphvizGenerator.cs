using System.IO;

namespace IPC2_Proyecto1_2020XXXX.Utilidades
{
    public class GraphvizGenerator
    {
        /// <summary>
        /// Crea un archivo .dot y opcionalmente ejecuta dot para generar png.
        /// </summary>
        /// <param name="rejilla">La rejilla a representar.</param>
        /// <param name="rutaArchivo">Ruta de salida sin extensión.</param>
        public void Generar(Modelos.Rejilla rejilla, string rutaArchivo)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo + ".dot"))
            {
                sw.WriteLine("digraph G {");
                sw.WriteLine("node [shape=plaintext]");
                sw.WriteLine("tabla [label=<");
                sw.WriteLine("<TABLE BORDER='0' CELLBORDER='1' CELLSPACING='0'>");

                for (int i = 0; i < rejilla.M; i++)
                {
                    sw.WriteLine("<TR>");

                    for (int j = 0; j < rejilla.M; j++)
                    {
                        string color = rejilla.Matriz[i, j].Infectada ? "red" : "white";
                        sw.WriteLine($"<TD BGCOLOR='{color}' WIDTH='20' HEIGHT='20'></TD>");
                    }

                    sw.WriteLine("</TR>");
                }

                sw.WriteLine("</TABLE>");
                sw.WriteLine(">];");
                sw.WriteLine("}");
            }

            // intentar generar PNG
            try
            {
                string dotPath = @"C:\Program Files\Graphviz\bin\dot.exe";
                System.Diagnostics.Process.Start(dotPath,
                    $"-Tpng \"{rutaArchivo}.dot\" -o \"{rutaArchivo}.png\"");
            }
            catch (Exception ex)
            {
                // dot no disponible, omitir y mostrar advertencia
                System.Console.WriteLine($"Advertencia: no se pudo generar PNG. Detalles: {ex.Message}");
            }
        }
    }
}
