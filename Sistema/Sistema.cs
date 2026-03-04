using System;
using IPC2_Proyecto1_2020XXXX.Estructuras;
using IPC2_Proyecto1_2020XXXX.Modelos;
using IPC2_Proyecto1_2020XXXX.Utilidades;

namespace IPC2_Proyecto1_2020XXXX.Sistema
{
    public class SistemaSistema
    {
        private ListaEnlazada<Paciente> pacientes = new ListaEnlazada<Paciente>();

        public void CargarPacientes(string rutaXml)
        {
            var lista = XMLManager.CargarPacientes(rutaXml);
            foreach (var p in lista)
            {
                pacientes.AddLast(p);
            }
            Console.WriteLine($"Cargados {lista.Count} pacientes.");
        }

        public void SimularPaciente(string nombre)
        {
            var p = ObtenerPaciente(nombre);
            if (p == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }
            Simulador.RunAll(p);
        }

        public Paciente? ObtenerPaciente(string nombre)
        {
            return pacientes.Find(x => x.DatosPersonales.Nombre == nombre);
        }

        public void SimularTodos()
        {
            foreach (var p in pacientes)
            {
                Simulador.RunAll(p);
            }
        }

        public void GenerarSalida(string rutaXml)
        {
            XMLWriter.EscribirSalida(pacientes, rutaXml);
        }

        public void Limpiar()
        {
            pacientes = new ListaEnlazada<Paciente>();
            Console.WriteLine("Memoria limpiada.");
        }
    }
}
