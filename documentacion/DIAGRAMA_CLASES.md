# Diagrama de Clases - Simulador de Rejillas Epidemiológicas

## Visualización en ASCII Art

```
┌────────────────────────────────────────────────────────────────────────────────┐
│                         SIMULADOR DE REJILLAS                                  │
└────────────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────┐
│      PACIENTE            │
├──────────────────────────┤
│ + DatosPersonales        │────┐
│ + Periodos: int          │    │
│ + M: int                 │    │
│ + Rejilla: Rejilla       │────────┐
│ + Simulado: bool         │        │
│ + InicialInfectadas: int │        │
│ + Resultado: string      │        │
│ + N: int                 │        │
│ + N1: int                │        │
└──────────────────────────┘        │
         │                          │
         │ "contiene"               │ "usa"
         ▼                          ▼
┌──────────────────────────┐  ┌──────────────────────────┐
│  DATOSPERSONALES         │  │      REJILLA             │
├──────────────────────────┤  ├──────────────────────────┤
│ + Nombre: string         │  │ + M: int                 │
│ + Edad: int              │  │ + Matriz: Celda[,]       │
└──────────────────────────┘  │                          │
                              │ + ProximoPeriodo(): R... │
                              │ + ToString(): string     │
                              │ - ContarVecinos(i,j)     │
                              └──────────────────────────┘
                                      │
                                      │ "contiene"
                                      ▼
                              ┌──────────────────────────┐
                              │      CELDA               │
                              ├──────────────────────────┤
                              │ + Infectada: bool        │
                              └──────────────────────────┘

┌────────────────────────────────────────────┐
│      SIMULATION SESSION                    │
├────────────────────────────────────────────┤
│ + Paciente: Paciente                       │
│ + Actual: Rejilla                          │
│ + Periodo: int                             │
│ - historial: ListaEnlazada<string>         │
│ - inicial: string                          │
│                                            │
│ + Step(): bool                             │
│ + RunAll(): void                           │
│ - Cuenta(infectada: bool): int             │
└────────────────────────────────────────────┘
         │              │
         │ "usa"        │ "usa"
         ▼              ▼
    PACIENTE       REJILLA


┌────────────────────────────────────────────┐
│         SIMULADOR                          │
├────────────────────────────────────────────┤
│ + RunAll(paciente: Paciente): void         │
└────────────────────────────────────────────┘
         │
         │ "usa"
         ▼
    PACIENTE, REJILLA


┌────────────────────────────────────────────┐
│      XML MANAGER                           │
├────────────────────────────────────────────┤
│ + CargarPacientes(ruta): ListaEnlazada     │
└────────────────────────────────────────────┘
         │
         │ "crea"
         ▼
    PACIENTE, REJILLA


┌────────────────────────────────────────────┐
│      XML WRITER                            │
├────────────────────────────────────────────┤
│ + EscribirSalida(pacientes, ruta): void    │
└────────────────────────────────────────────┘
         │
         │ "lee"
         ▼
    PACIENTE


┌────────────────────────────────────────────┐
│   GRAPHVIZ GENERATOR                       │
├────────────────────────────────────────────┤
│ + Generar(rejilla, ruta): void             │
└────────────────────────────────────────────┘
         │
         │ "visualiza"
         ▼
    REJILLA


┌────────────────────────────────────────────┐
│  SIMULATION CONFIG                         │
├────────────────────────────────────────────┤
│ + MaxGenerate: int = 100                   │
└────────────────────────────────────────────┘


┌────────────────────────────────────────────┐
│   LISTA ENLAZADA<T>                        │
├────────────────────────────────────────────┤
│ + AddLast(item): void                      │
│ + Contains(item): bool                     │
│ + IndexOf(item): int                       │
│ + Count: int                               │
└────────────────────────────────────────────┘
         │
         │ "usado por"
         ▼
  SIMULATION SESSION
     SISTEMA


┌────────────────────────────────────────────┐
│         SISTEMA                            │
├────────────────────────────────────────────┤
│ - pacientes: ListaEnlazada<Paciente>       │
│                                            │
│ + CargarPacientes(ruta): void              │
│ + ObtenerPaciente(nombre): Paciente        │
│ + GenerarSalida(ruta): void                │
│ + Limpiar(): void                          │
└────────────────────────────────────────────┘
         │
         │ "contiene y maneja"
         ▼
    PACIENTE
```

---

## Resumen de Relaciones

### Agregación (Composición Fuerte)
- **Paciente** contiene **DatosPersonales** (siempre)
- **Paciente** contiene **Rejilla** (si no está en modo simulado puro)
- **Rejilla** contiene **Celda[,]** (matriz)

### Asociación (Uso)
- **SimulationSession** usa **Paciente** y **Rejilla**
- **Simulador** procesa **Paciente** y **Rejilla**
- **XMLManager** crea **Paciente** y **Rejilla** al leer
- **XMLWriter** lee **Paciente** para escribir salida
- **GraphvizGenerator** visualiza **Rejilla**
- **Sistema** contiene colección de **Paciente**

### Dependencia
- **XMLManager** depende de **SimulationConfig** (para MaxGenerate)
- **Simulador** depende de **SimulationConfig**

---

## Jerarquía de Paquetes

```
namespace IPC2_Proyecto1_2020XXXX
│
├── Modelos/
│   ├── Celda.cs
│   ├── Paciente.cs
│   └── Rejilla.cs
│
├── Estructuras/
│   ├── ListaEnlazada.cs
│   └── Nodo.cs
│
├── Sistema/
│   ├── Sistema.cs
│   ├── Simulador.cs
│   └── SimulationSession.cs
│
└── Utilidades/
    ├── SimulationConfig.cs
    ├── XMLManager.cs
    ├── XMLWriter.cs
    └── GraphvizGenerator.cs
```

---

## Flujo de Instanciación

```
Program.cs MAIN
    │
    ├─→ new Sistema()
    │     │
    │     └─→ ListaEnlazada<Paciente> privada
    │
    └─→ Menú (opción 1: Cargar XML)
          │
          └─→ XMLManager.CargarPacientes(ruta)
                │
                ├─→ Para cada <paciente> en XML
                │     └─→ new Paciente()
                │           ├─→ new DatosPersonales()
                │           └─→ new Rejilla(m)
                │                 └─→ Celda[m,m]
                │
                └─→ Mapeo escalado (si M > 100)
                      └─→ Reducción a max 100×100
```

---

## Vista Detallada de Métodos

### Clase REJILLA - Método ProximoPeriodo()

```
┌─ ProximoPeriodo() ─────────────────────────────┐
│                                                │
│  ENTRADA: Rejilla (this)                       │
│  SALIDA: Rejilla (siguiente generación)        │
│                                                │
│  PROCESO:                                      │
│  ┌──────────────────────────────────────────┐  │
│  │ 1. Crear nueva Rejilla(M)                │  │
│  │ 2. Para cada celda (i,j) en 0..M×M       │  │
│  │    │                                     │  │
│  │    ├─ Contar vecinos infectados          │  │
│  │    │  (máximo 8 adyacentes)              │  │
│  │    │                                     │  │
│  │    ├─ SI está infectada:                 │  │
│  │    │  └─ SI vecinos = 2 o 3:             │  │
│  │    │     └─ Sobrevive (nuevaRejilla=1)   │  │
│  │    │  └─ SINO:                           │  │
│  │    │     └─ Muere (nuevaRejilla=0)       │  │
│  │    │                                     │  │
│  │    └─ SI está sana:                      │  │
│  │       └─ SI vecinos = 3:                 │  │
│  │          └─ Se infecta (nuevaRejilla=1)  │  │
│  │       └─ SINO:                           │  │
│  │          └─ Permanece sana (nuevaRejilla │  │
│  │                                          │  │
│  │ 3. Retornar nuevaRejilla                 │  │
│  └──────────────────────────────────────────┘  │
│                                                │
│  Complejidad: O(M²) por período                │
└────────────────────────────────────────────────┘
```

---

**Diagrama de Clases v1.0**  
**Alternativa a Mermaid para mejor visualización**
