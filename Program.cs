using System;
using System.Collections.Generic;
using System.Linq;


class TareaBasica
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaLimite { get; set; }
    public bool Completada { get; set; }

    public TareaBasica(string nombre, string descripcion, DateTime fechaLimite)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        FechaLimite = fechaLimite;
        Completada = false;
    }

    public override string ToString()
    {
        return $"{Nombre} - {Descripcion} - Fecha límite: {FechaLimite.ToString("dd/MM/yyyy")} - Completada: {(Completada ? "Sí" : "No")}";
    }
}


class TareaUrgente : TareaBasica
{
    public int NivelUrgencia { get; set; }

    public TareaUrgente(string nombre, string descripcion, DateTime fechaLimite, int nivelUrgencia)
        : base(nombre, descripcion, fechaLimite)
    {
        NivelUrgencia = nivelUrgencia;
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Nivel de urgencia: {NivelUrgencia}";
    }
}


class TareaConRecordatorio : TareaBasica
{
    public DateTime FechaRecordatorio { get; set; }

    public TareaConRecordatorio(string nombre, string descripcion, DateTime fechaLimite, DateTime fechaRecordatorio)
        : base(nombre, descripcion, fechaLimite)
    {
        FechaRecordatorio = fechaRecordatorio;
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Recordatorio: {FechaRecordatorio.ToString("dd/MM/yyyy HH:mm")}";
    }
}

class Program
{
    static List<TareaBasica> tareas = new List<TareaBasica>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Agregar tarea básica");
            Console.WriteLine("2. Agregar tarea urgente");
            Console.WriteLine("3. Agregar tarea con recordatorio");
            Console.WriteLine("4. Mostrar todas las tareas");
            Console.WriteLine("5. Filtrar y ordenar tareas por fecha límite");
            Console.WriteLine("6. Marcar tarea como completada");
            Console.WriteLine("7. Salir");
            Console.WriteLine("Seleccione una opción:");

            int opcion;
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    AgregarTareaBasica();
                    break;
                case 2:
                    AgregarTareaUrgente();
                    break;
                case 3:
                    AgregarTareaConRecordatorio();
                    break;
                case 4:
                    MostrarTodasLasTareas();
                    break;
                case 5:
                    FiltrarYOrdenarPorFechaLimite();
                    break;
                case 6:
                    MarcarTareaComoCompletada();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void AgregarTareaBasica()
    {
        Console.WriteLine("Ingrese nombre de la tarea:");
        string nombre = Console.ReadLine();
        Console.WriteLine("Ingrese descripción de la tarea:");
        string descripcion = Console.ReadLine();
        Console.WriteLine("Ingrese fecha límite (Formato: dd/mm/aaaa):");
        DateTime fechaLimite = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

        tareas.Add(new TareaBasica(nombre, descripcion, fechaLimite));
    }

    static void AgregarTareaUrgente()
    {
        Console.WriteLine("Ingrese nombre de la tarea:");
        string nombre = Console.ReadLine();
        Console.WriteLine("Ingrese descripción de la tarea:");
        string descripcion = Console.ReadLine();
        Console.WriteLine("Ingrese fecha límite (Formato: dd/mm/aaaa):");
        DateTime fechaLimite = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
        Console.WriteLine("Ingrese nivel de urgencia:");
        int nivelUrgencia = int.Parse(Console.ReadLine());

        tareas.Add(new TareaUrgente(nombre, descripcion, fechaLimite, nivelUrgencia));
    }

    static void AgregarTareaConRecordatorio()
    {
        Console.WriteLine("Ingrese nombre de la tarea:");
        string nombre = Console.ReadLine();
        Console.WriteLine("Ingrese descripción de la tarea:");
        string descripcion = Console.ReadLine();
        Console.WriteLine("Ingrese fecha límite (Formato: dd/mm/aaaa):");
        DateTime fechaLimite = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
        Console.WriteLine("Ingrese fecha y hora del recordatorio (Formato: dd/mm/aaaa hh:mm):");
        DateTime fechaRecordatorio = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null);

        tareas.Add(new TareaConRecordatorio(nombre, descripcion, fechaLimite, fechaRecordatorio));
    }

    static void MostrarTodasLasTareas()
    {
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas para mostrar.");
            return;
        }

        Console.WriteLine("Todas las tareas:");
        foreach (var tarea in tareas)
        {
            Console.WriteLine(tarea);
        }
    }

    static void FiltrarYOrdenarPorFechaLimite()
    {
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas para filtrar y ordenar.");
            return;
        }

        var tareasOrdenadas = tareas.OrderBy(t => t.FechaLimite);

        Console.WriteLine("Tareas filtradas y ordenadas por fecha límite:");
        foreach (var tarea in tareasOrdenadas)
        {
            Console.WriteLine(tarea);
        }
    }

    static void MarcarTareaComoCompletada()
    {
        Console.WriteLine("Ingrese el nombre de la tarea que desea marcar como completada:");
        string nombre = Console.ReadLine();

        var tarea = tareas.FirstOrDefault(t => t.Nombre == nombre);
        if (tarea != null)
        {
            tarea.Completada = true;
            Console.WriteLine($"La tarea '{nombre}' ha sido marcada como completada.");
        }
        else
        {
            Console.WriteLine($"No se encontró una tarea con el nombre '{nombre}'.");
        }
    }
}
