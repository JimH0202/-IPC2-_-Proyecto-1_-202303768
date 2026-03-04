# Manual de Usuario - Simulador de Rejillas Epidemiológicas

## Introducción

Bienvenido al **Simulador de Rejillas Epidemiológicas**. Esta aplicación permite simular la propagación de enfermedades infecciosas en una población modelada como una matriz. Es una herramienta educativa para entender cómo evolucionan los patrones de infección a lo largo del tiempo.

---

## 1. Requisitos del Sistema

### Mínimos Requeridos
- **Sistema Operativo**: Windows 10 o posterior (o Linux/macOS con .NET instalado)
- **.NET Runtime**: .NET 10.0 o superior
- **Espacio en Disco**: 100 MB (aplicación) + espacio para salidas
- **Memoria RAM**: 512 MB (mínimo), 2+ GB recomendado para rejillas grandes

### Opcional
- **Graphviz**: Para generar visualizaciones PNG
  - Descargar desde: https://graphviz.org/download/
  - Instalar en ruta por defecto: `C:\Program Files\Graphviz\`

---

## 2. Instalación

### Opción A: Usando el Ejecutable (Recomendado)

1. Localiza el archivo ejecutable:
   ```
   bin\Debug\net10.0\IPC2_Proyecto1_2020XXXX.exe
   ```

2. Copia la carpeta completa del proyecto a tu ubicación deseada

3. Ejecuta el archivo `.exe` haciendo doble clic

### Opción B: Ejecutar desde Código Fuente

1. Abre PowerShell o CMD en la carpeta del proyecto

2. Ejecuta:
   ```powershell
   dotnet run --project IPC2_Proyecto1_2020XXXX.csproj
   ```

---

## 3. Uso de la Aplicación

### 3.1 Inicio

Al ejecutar la aplicación verás el menú principal:

```
=== Simulador de Rejillas Epidemiológicas ===

Menú:
1. Cargar archivo XML
2. Elegir paciente para análisis
3. Ejecutar un periodo de forma manual
4. Ejecutar automáticamente todos los periodos
5. Generar salida XML
6. Limpiar memoria
7. Salir
Opción:
```

### 3.2 Paso 1: Cargar Archivo XML

**Selecciona la opción 1**

Se te pedirá:
```
Ruta de entrada XML:
```

#### Opciones de Entrada

**A) Especificar una ruta relativa** (desde la carpeta del proyecto):
```
ArchivosEntrada/entrada_prueba.xml
```

**B) Especificar una ruta absoluta** (completa):
```
C:\Users\tu_usuario\Desktop\proyecto\ArchivosEntrada\entrada_prueba.xml
```

**C) Pegar contenido XML directamente** (multi-línea):
```xml
<?xml version="1.0" encoding="utf-8"?>
<pacientes>
  <paciente>
    <datospersonales>
      <nombre>Paciente1</nombre>
      <edad>30</edad>
    </datospersonales>
    <periodos>5</periodos>
    <m>10</m>
    <rejilla>
      <celda f="1" c="1"/>
      <celda f="1" c="2"/>
    </rejilla>
  </paciente>
</pacientes>
```

Luego presiona Enter dos veces para confirmar.

#### Formato de Archivo XML

```xml
<?xml version="1.0" encoding="utf-8"?>
<pacientes>
  <paciente>
    <!-- Datos del paciente -->
    <datospersonales>
      <nombre>Nombre Completo</nombre>
      <edad>30</edad>
    </datospersonales>
    
    <!-- Parámetros de simulación -->
    <periodos>100</periodos>              <!-- Máximo número de períodos -->
    <m>10</m>                              <!-- Dimensión de la rejilla (m x m) -->
    
    <!-- Celdas infectadas inicialmente -->
    <rejilla>
      <celda f="5" c="5"/>                <!-- Fila 5, Columna 5 -->
      <celda f="5" c="6"/>                <!-- Fila 5, Columna 6 -->
    </rejilla>
  </paciente>
  
  <!-- Puedes agregar más pacientes aquí -->
  <paciente>
    ...
  </paciente>
</pacientes>
```

**Notas Importantes:**
- Las coordenadas (f, c) van de 1 a m (no de 0)
- Coordenadas fuera del rango se ignoran
- El programa acepta múltiples pacientes en un archivo
- Si m > 100 o periodos > 100, automáticamente usa simulación escalada

---

### 3.3 Paso 2: Elegir Paciente para Análisis

**Selecciona la opción 2**

Se te pedirá:
```
Nombre del paciente a simular:
```

Ingresa el nombre exacto del paciente (sensible a mayúsculas/minúsculas) que deseas simular.

Ejemplo:
```
Nombre del paciente a simular: Paciente1
Paciente seleccionado. Puede ejecutar periodos de forma manual o automática.
```

---

### 3.4 Paso 3: Ejecutar Simulación

Tienes dos opciones:

#### Opción 3A: Ejecución Manual (Período por Período)

**Selecciona la opción 3**

Cada vez que selecciones esta opción, la simulación avanza un período y muestra:
```
Periodo 1: sanas=92 infectadas=8
```

Puedes revisar los archivos generados antes de continuar.

**Cuándo usar**: Para entender paso a paso cómo evoluciona la enfermedad.

#### Opción 3B: Ejecución Automática (Todos los Períodos)

**Selecciona la opción 4**

La simulación ejecutará automáticamente todos los períodos (hasta encontrar un patrón o alcanzar el límite) en cuestión de segundos o minutos.

Verás un resumen como:
```
Patrón inicial se repite en N=3. Resultado: mortal
```

**Cuándo usar**: Cuando quieras resultados rápidos sin ver cada paso.

---

### 3.5 Interpretación de Resultados

La simulación produce **un resultado por paciente**:

| Resultado | Significado |
|-----------|------------|
| **Mortal** | El patrón inicial se repite exactamente → El ciclo es muy corto |
| **Mortal_simulado** | Versión simulada: patrón inicial se repite en escala |
| **Grave** | El patrón entra en un ciclo predecible de longitud N1 |
| **Grave_simulado** | Versión simulada del resultado grave |
| **Leve** | Sin ciclo detectado → La enfermedad se controla o muere de hambre |
| **Simulado** | Simulación a escala completada (entradas > 100×100) |

---

### 3.6 Paso 4: Generar Salida XML

**Selecciona la opción 5**

Se te pedirá:
```
Ruta de salida XML:
```

Especifica dónde guardar los resultados:
```
ArchivosSalida/resultados.xml
```

El archivo generado contendrá todos los pacientes con sus resultados:

```xml
<?xml version="1.0" encoding="utf-8"?>
<pacientes>
  <paciente>
    <datospersonales>
      <nombre>Paciente1</nombre>
      <edad>30</edad>
    </datospersonales>
    <periodos>5</periodos>
    <m>10</m>
    <resultado>mortal</resultado>
    <n>1</n>
  </paciente>
</pacientes>
```

---

### 3.7 Limpiar Memoria

**Selecciona la opción 6**

Borra todos los pacientes y sesiones actuales de la memoria.

**Cuándo usar**: Antes de cargar un nuevo archivo de entrada.

---

### 3.8 Salir

**Selecciona la opción 7**

Cierra la aplicación.

---

## 4. Archivos Generados

### Archivos .dot (Graphviz)

Se guardan en:
```
ArchivosSalida/Periodos/periodo_1.dot
ArchivosSalida/Periodos/periodo_2.dot
...
```

**Formato**: Texto plano legible (no requiere Graphviz para verlo)

**Contenido**: Representación textual de la rejilla:
```dot
digraph G {
  node [shape=plaintext]
  tabla [label=<
    <TABLE BORDER='0' CELLBORDER='1' CELLSPACING='0'>
      <TR><TD BGCOLOR='white'>...</TD>...</TR>
    </TABLE>
  >];
}
```

### Archivos .png (Opcionales)

Se generan automáticamente si Graphviz está instalado:
```
ArchivosSalida/Periodos/periodo_1.png
ArchivosSalida/Periodos/periodo_2.png
```

**Visualización**: Matriz de colores
- **Blanco**: Celda sana
- **Rojo**: Celda infectada

---

## 5. Ejemplos de Uso

### Ejemplo 1: Simulación Simple (10×10)

1. **Cargar XML**:
   ```
   ArchivosEntrada/entrada_prueba.xml
   ```

2. **Elegir paciente**: `PacientePrueba`

3. **Ejecutar automáticamente**: Opción 4

4. **Resultado esperado**: `mortal` (en 1-3 períodos típicamente)

### Ejemplo 2: Simulación Grande Escalada (10000×10000)

1. **Cargar XML**:
   ```
   ArchivosEntrada/Entrada10000x10000.xml
   ```
   
   La app detecta automáticamente que 10000 > 100 y activa **modo simulado**

2. **Elegir paciente**: `Paciente10000`

3. **Ejecutar automáticamente**: Opción 4

4. **Resultado**: Simulación rápida en rejilla 100×100 equivalente

---

## 6. Troubleshooting

### Problema: "Error al cargar XML: Could not find file..."

**Solución**: 
- Verifica que la ruta sea correcta
- Usa ruta absoluta si no estás seguro
- Asegúrate de que el archivo existe

### Problema: "Paciente no encontrado"

**Solución**:
- El nombre debe coincidir exactamente (mayúsculas/minúsculas)
- Carga primero el archivo XML (opción 1)
- Verifica el nombre en el XML

### Problema: "Opción inválida" (bucle de menú)

**Solución**:
- Si pegaste XML multi-línea, presiona Enter después del cierre `</pacientes>`
- Asegúrate de escribir 1-7 para opciones válidas

### Problema: No se generan PNG

**Solución**:
- Instala Graphviz desde https://graphviz.org/download/
- O descarga los archivos .dot y úsalos con un visor online
- Los .dot contienen toda la información (los PNG son solo visualización)

### Problema: Aplicación se cierra inesperadamente

**Solución**:
- Revisa el último mensaje de error antes de cierre
- Intenta cargar un XML más pequeño (prueba con 10×10)
- Verifica memoria disponible (rejillas > 500×500 requieren varios GB)

---

## 7. Tips y Buenas Prácticas

1. **Empieza pequeño**: Prueba primero con rejillas 10×10 o 25×25
2. **Guarda tus inputs**: Mantén copias de los XML que generes
3. **Revisa los outputs**: Los .dot y .png son útiles para entender visualmente
4. **Prueba patrones conocidos**: Experimenta con configuraciones simples primero
5. **Documenta resultados**: Exporta a XML regularmente para comparar

---

## 8. Contacto y Soporte

Para más información sobre la lógica del autómata celular o sobre cómo interpretar los resultados, consulta el **Manual Técnico** incluido en la carpeta `documentacion/`.

---

**Manual de Usuario v1.0**  
**Marzo 2026**
