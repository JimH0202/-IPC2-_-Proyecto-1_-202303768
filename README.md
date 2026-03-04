# Documentación - Simulador de Rejillas Epidemiológicas

## Archivos Disponibles

### 1. [MANUAL_TECNICO.md](MANUAL_TECNICO.md)
**Dirigido a**: Desarrolladores, arquitectos de software, estudiantes de ingeniería en sistemas.

**Contenido**:
- Descripción técnica completa del sistema
- Arquitectura en capas
- Descripción detallada de cada clase y componente
- Algoritmos principales explicados en pseudocódigo
- Diagrama de clases UML
- Diagramas de actividad para procesos clave
- Notas técnicas de implementación

**Extensión**: 4-7 páginas de contenido + apéndices

### 2. [MANUAL_USUARIO.md](MANUAL_USUARIO.md)
**Dirigido a**: Usuarios finales, estudiantes de biología/epidemiología, no necesariamente técnicos.

**Contenido**:
- Requisitos del sistema
- Pasos de instalación (ejecutable y código fuente)
- Guía completa de uso paso a paso
- Formato esperado de archivos XML de entrada
- Interpretación de resultados
- Ejemplos de uso
- Solución de problemas (FAQ)
- Tips y buenas prácticas

**Extensión**: ~12 secciones, fácil de seguir

### 3. [DIAGRAMA_CLASES.md](DIAGRAMA_CLASES.md) **NUEVO**
**Dirigido a**: Todos, especialmente si los diagramas Mermaid no se ven en tu editor.

**Contenido**:
- Diagrama de clases en **ASCII Art** (visible en cualquier editor)
- Resumen detallado de relaciones (agregación, asociación, dependencia)
- Jerarquía de paquetes/namespaces
- Flujo de instanciación de objetos
- Explicación visual de métodos principales

**Ventaja**: Se ve siempre sin necesidad de renderizar Mermaid

### 4. [diagrama_clases.dot](diagrama_clases.dot) **NUEVO**
**Formato**: Graphviz (.dot)

**Cómo usar**:
```powershell
dot -Tpng diagrama_clases.dot -o diagrama_clases.png
```
Se generará `diagrama_clases.png` con visualización profesional.

---

## Estructura del Proyecto Documentado

```
IPC2_Proyecto1_2020XXXX/
├── documentacion/
│   ├── README.md                    ← Este archivo (índice)
│   ├── MANUAL_TECNICO.md            ← Descripción técnica + diagramas Mermaid
│   ├── MANUAL_USUARIO.md            ← Guía de uso para usuarios finales
│   ├── DIAGRAMA_CLASES.md           ← Diagrama de clases en ASCII Art 
│   └── diagrama_clases.dot          ← Diagrama en formato Graphviz 
├── Modelos/
├── Estructuras/
├── Sistema/
├── Utilidades/
├── ArchivosEntrada/
├── ArchivosSalida/
└── bin/
    └── Debug/
        └── net10.0/
            └── IPC2_Proyecto1_2020XXXX.exe
```

---

## Inicio Rápido

### Para Usuarios
1. Lee [MANUAL_USUARIO.md](MANUAL_USUARIO.md) → Sección 3 (Uso de la Aplicación)
2. Prepara un archivo XML en `ArchivosEntrada/`
3. Ejecuta `bin/Debug/net10.0/IPC2_Proyecto1_2020XXXX.exe`
4. Sigue el menú interactivo

### Para Desarrolladores
1. Lee [MANUAL_TECNICO.md](MANUAL_TECNICO.md) → Sección 2-5 (Arquitectura y Componentes)
2. Consulta los diagramas en el APÉNDICE A y B
3. Revisa el código fuente en `Modelos/`, `Sistema/`, `Utilidades/`

---

## Diagramas: Múltiples Formatos Disponibles

### Diagrama de Clases
Si no ves los diagramas Mermaid en tu editor, tienes varias opciones:

| Opción | Ubicación | Cómo Verlo |
|--------|-----------|-----------|
| **Mermaid (interactivo)** | MANUAL_TECNICO.md (APÉNDICE A) | GitHub, VS Code con ext. Markdown Preview Mermaid |
| **ASCII Art (simple)** | [DIAGRAMA_CLASES.md](DIAGRAMA_CLASES.md) | Cualquier editor de texto |
| **Graphviz (.dot)** | [diagrama_clases.dot](diagrama_clases.dot) | Convertir a PNG: `dot -Tpng diagrama_clases.dot -o diagrama_clases.png` |
| **PNG (compilado)** | `diagrama_clases.png` (genera tú) | Visor de imágenes |

### Diagramas de Actividad

Están en [MANUAL_TECNICO.md](MANUAL_TECNICO.md) - APÉNDICE B en formato Mermaid:
1. **Simulación Completa (RunAll)**
2. **Carga de Pacientes desde XML**
3. **Detección de Ciclos**

---

## Cómo Ver Diagramas Mermaid si no se Renderizan

### Opción 1: GitHub (Recomendado)
1. Sube los archivos `.md` a un repositorio GitHub
2. Los diagramas Mermaid se renderizan automáticamente

### Opción 2: VS Code
1. Instala: `Markdown Preview Mermaid Support` (ext. oficial)
2. Abre el archivo `.md`
3. Presiona `Ctrl+Shift+V` para preview

### Opción 3: Editor Online Mermaid
1. Ve a https://mermaid.live
2. Copia el contenido Mermaid del archivo
3. Pégalo en el editor

### Opción 4: Convertir con Graphviz
1. Instala Graphviz: https://graphviz.org/download/
2. En la carpeta `documentacion/`, ejecuta:
   ```powershell
   dot -Tpng diagrama_clases.dot -o diagrama_clases.png
   ```
3. Abre `diagrama_clases.png`

---

## Notas sobre Diagramas

### Diagrama de Clases (MANUAL_TECNICO.md - APÉNDICE A)
Muestra la relación entre todas las clases principales:
- Clases de dominio: `Paciente`, `Rejilla`, `Celda`
- Clases de simulación: `Simulador`, `SimulationSession`
- Clases de utilidad: `XMLManager`, `XMLWriter`, `GraphvizGenerator`
- Estructuras: `ListaEnlazada<T>`

**Herramienta**: Mermaid.js (compatible con GitHub, VS Code, etc.)

### Diagramas de Actividad (MANUAL_TECNICO.md - APÉNDICE B)
Tres diagramas clave:
1. **Simulación Completa (RunAll)**: Flujo de ejecución de la simulación
2. **Carga de Pacientes desde XML**: Procesamiento de entrada
3. **Detección de Ciclos**: Lógica principal del autómata celular

**Herramienta**: Mermaid.js (compatible con GitHub, VS Code, etc.)

---

## Información del Proyecto

| Aspecto | Detalle |
|--------|--------|
| **Lenguaje** | C# |
| **Framework** | .NET 10.0 |
| **Tipo de Aplicación** | Consola (CLI) |
| **Patrón Principal** | Autómata Celular (Juego de la Vida adaptado) |
| **Algoritmo Base** | Detección de ciclos + Simulación a escala |
| **Estructuras de Datos** | Matriz 2D, Lista Enlazada personalizada |
| **Formato de Datos** | XML |
| **Visualización** | Graphviz (.dot, .png) |
| **Compiled Size** | ~100 MB (con runtime) |

---

## Requisitos Cumplidos

Manual técnico entre 4-7 páginas  
Diagrama de clases UML (Mermaid + ASCII Art + Graphviz)  
Diagramas de actividad para algoritmos principales  
Manual de usuario completo  
Documentación en Markdown  
Arquitectura clara y modular  
**Múltiples formatos para visualización**  

---

## Próximas Mejoras (Opcionales)

- [ ] Generar diagrama de secuencias para flujo cliente-servidor
- [ ] Crear guía de instalación de Graphviz paso a paso
- [ ] Agregar ejemplos de casos de uso complejos
- [ ] Video tutorial en YouTube

---

**Documentación versión 1.1**  
Generada: Marzo 2026  
**Última actualización**: Agregados DIAGRAMA_CLASES.md y diagrama_clases.dot para múltiples formatos de visualización  
Generada: Marzo 2026
