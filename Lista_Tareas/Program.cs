using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Lista_Contactos
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            string contraseña_nueva = "", contraseña_ingresada = "";
            List<Tarea> listaTareas = new List<Tarea>();

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Escribe una contraseña para guardar.");
            Console.ResetColor();
            contraseña_nueva = Console.ReadLine();
            Console.WriteLine("");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contraseña guardada correctamente.");
            Console.ResetColor();

            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Escribe la contraseña para acceder a tu lista de tareas.");
            Console.ResetColor();
            contraseña_ingresada = Console.ReadLine();

            while (contraseña_ingresada != contraseña_nueva)
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Contraseña incorrecta. Intenta de nuevo.");
                Console.ResetColor();
                Console.WriteLine("");
                contraseña_ingresada = Console.ReadLine();
            }

            Console.WriteLine("");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Contraseña correcta. Bienvenido a tu lista de tareas.");
            Console.ResetColor();

            do
            {
                Console.WriteLine("==== MENÚ DE OPCIONES ====");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Mostrar tareas");
                Console.WriteLine("3. Marcar tarea como completada");
                Console.WriteLine("4. Eliminar tarea");
                Console.WriteLine("5. Mostrar Tareas pendientes");
                Console.WriteLine("6. Mostrar Estadísticas");
                Console.WriteLine("7. Salir");
                Console.WriteLine("==========================");
                Console.Write("Selecciona una opción: ");
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();


                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Opción 1: Agregar tarea");
                        Console.WriteLine("");
                        Console.WriteLine("Por favor escribe el nombre de la tarea.");
                        string nom_Tarea = Console.ReadLine();

                        Console.WriteLine("");
                        while (string.IsNullOrEmpty(nom_Tarea) || string.IsNullOrWhiteSpace(nom_Tarea))
                        {
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("El nombre de la tarea es incorrecta. Intenta de nuevo.");
                            nom_Tarea = Console.ReadLine();
                            Console.WriteLine("");
                        }

                        Console.WriteLine("Escribe una breve descripción para la tarea.");
                        string descripcion = Console.ReadLine();
                        Console.WriteLine("");

                        while (string.IsNullOrEmpty(descripcion) || string.IsNullOrWhiteSpace(descripcion))
                        {

                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La descripción de la tarea es incorrecta. Intenta de nuevo.");
                            descripcion = Console.ReadLine();
                            Console.WriteLine("");
                        }

                        Console.WriteLine("");
                        Console.WriteLine("Escribe la fecha de vencimiento para la tarea (dd/mm/yyyy).");
                        DateTime fechaVencimiento;
                        string fechaVencimientoStr = Console.ReadLine();

                        while (!DateTime.TryParse(fechaVencimientoStr, out fechaVencimiento) || fechaVencimiento < DateTime.Today)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La fecha de vencimiento es incorrecta. Debe ser una fecha válida (dd/mm/yyyy) y no puede ser anterior a hoy.");
                            Console.ResetColor();
                            Console.Write("Intenta de nuevo: ");
                            fechaVencimientoStr = Console.ReadLine();
                        }

                        listaTareas.Add(new Tarea(nom_Tarea, descripcion, DateTime.Parse(fechaVencimientoStr)));
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tarea agregada correctamente. No olvides completarla a tiempo.");
                        Console.ResetColor();
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    case 2:
                        Console.WriteLine("Opción 2: Mostrar todas las tareas");
                        if (listaTareas.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("No hay tareas para mostrar. Agrega una tarea para empezar a organizarte.");
                            Console.ResetColor();
                        }

                        foreach (Tarea t in listaTareas)
                        {
                            t.MostrarInformacion();
                        }
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 3:
                        Console.WriteLine("Opción 3: Marcar tarea como completada");

                        Console.WriteLine("Por favor, escribe el título de la tarea que deseas marcar como completada:");
                        string titulo = Console.ReadLine();
                        var buscar_Tarea = listaTareas.Find(t => t.titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
                        if (buscar_Tarea != null)
                        {
                            buscar_Tarea.completada = true;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("La tarea se ha marcado como completada.");
                            Console.Clear();
                            foreach (Tarea t in listaTareas)
                            {
                                t.MostrarInformacion();
                            }
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La tarea no fue encontrada.");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 4:
                        Console.WriteLine("Opción 4: Eliminar tarea");
                        Console.WriteLine("Por favor escribe el título de la tarea a eliminar.");
                        titulo = Console.ReadLine();
                        var tareaAEliminar = listaTareas.Find(t => t.titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
                        if (tareaAEliminar != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("La tarea se ha eliminado correctamente.");
                            listaTareas.Remove(tareaAEliminar);
                            Console.Clear();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La tarea no ha sido creada.");
                            Console.Clear();
                        }
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 5:
                        Console.WriteLine("Opción 5: Mostrar Tareas pendientes");
                        int contadorTareasPendientes = listaTareas.Count(t => !t.completada);
                        if (contadorTareasPendientes == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("No tienes tareas pendientes. ¡Muy Bien!");
                            Console.ResetColor();

                        }
                        Console.WriteLine($"Número de tareas pendientes: {contadorTareasPendientes}");
                        Console.WriteLine("");
                        foreach (Tarea t in listaTareas) { t.MostrarInformacion(); }

                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 6:
                        Console.WriteLine("Opción 6: Mostrar Estadísticas");
                        Console.WriteLine("===================================");
                        Console.WriteLine("Total de tareas: {0}", listaTareas.Count);
                        Console.WriteLine("Tareas completadas: {0}", listaTareas.Count(t => t.completada));
                        Console.WriteLine("Tareas pendientes: {0}", listaTareas.Count(t => !t.completada));
                        Console.WriteLine("Tareas vencidas: {0}", listaTareas.Count(t => t.FechaVencimiento < DateTime.Now && !t.completada));
                        Console.WriteLine("===================================");

                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 7:
                        Console.WriteLine("Saliendo del programa...");
                        Console.WriteLine("");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intenta de nuevo.");
                        break;
                }
            }
            while (opcion != 7);
            Console.WriteLine("Muchas gracias por utilizar el programa. ¡Hasta luego!");

        }
    }

    class Tarea
    {
        public string titulo { get; set; }
        private string descripcion { get; set; }
        private DateTime fechaVencimiento { get; set; }
        public bool completada { get; set; }

        public Tarea(string titulo, string descripcion, DateTime fechaVencimiento)
        {
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fechaVencimiento = fechaVencimiento;
            this.completada = false;
        }

        public DateTime FechaVencimiento => fechaVencimiento;
        public void MostrarInformacion()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("INFORMACIÓN DE LA TAREA");
            Console.WriteLine("");
            Console.WriteLine($"Título: {titulo}");
            Console.WriteLine($"Descripción: {descripcion}");
            Console.WriteLine($"Fecha de Vencimiento: {fechaVencimiento.ToShortDateString()}");
            Console.WriteLine($"Completada: {(completada ? "Sí" : "No")}");
            Console.WriteLine("=========================");
        }

    }
}
