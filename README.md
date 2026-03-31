# API_GIMNASIOS
Trabajo Practico del Curso de C# - PROFESOR: ● Genaro Rafael Bergesio COORDINADOR: ● Octavio Felix Cavalleris Malanca SECRETARIO SAE: ● Exequiel Carranza

### 📚 Descripcion 
Desarrollar un controlador el cual permita dar de alta los diferentes gimnasios que existan en
las regiones. Los mismos deben tener los datos: id, nombre de la ciudad donde está ubicado, id
region, fecha de creación del gimnasio, nombre del líder, gimnasio activo y el nombre de la medalla.
También debe ser posible modificarlos, consultarlos y darlos de baja lógicamente (atributo gimnasio
activo).

### 🗂️ Estructura de la tabla

| Nombre Atributo  | Tipo                |
| ---------------- | ------------------- |
| id (PK)          | INT (IDENTITY(0,1)) |
| ciudad_gimnasio  | VARCHAR(255)        |
| id_region (FK)   | INT                 |
| fecha_alta       | DATE                |
| entrenador_lider | VARCHAR(255)        |
| gimnasio_activo  | BIT                 |

---

## 🚀 Endpoints

### 🔹 Obtener todos los gimnasios

**GET** `Gimnasios/api/ObtenerGimnasiosCompleto`

* Recupera todos los gimnasios (activos e inactivos)
* Códigos de respuesta: `200`, `204`, `400`, `409`, `500`

---

### 🔹 Obtener gimnasios activos

**GET** `Gimnasios/api/ObtenerGimnasiosActivo`

* Recupera solo los gimnasios activos
* Códigos de respuesta: `200`, `204`, `400`, `409`, `500`

---

### 🔹 Obtener gimnasio por ID

**GET** `Gimnasios/api/ObtenerGimnasioXid/{id_gimnasio}`
* Recupera un gimnasio por su ID
* Códigos de respuesta: `200`, `204`, `400`, `409`, `500`

---

### 🔹 Crear gimnasio

**POST** `Gimnasios/api/CargarGimnasio`

* Recibe en el body todos los datos del gimnasio
* Devuelve el gimnasio creado si es exitoso
* Códigos de respuesta: `201`, `400`, `409`, `500`

--- 
### 🔹 Modificar gimnasio

**PUT** `Gimnasios/api/ModificarGimnasio`

* Recibe en el body todos los datos del gimnasio
* Devuelve el gimnasio modificado
* Si no existe → `404`
* Códigos de respuesta: `200`, `400`, `404`, `409`, `500`

---

### 🔹 Desactivar gimnasio

**PATCH / DELETE** `Gimnasios/api/DesactivarGimnasio/{id_gimnasio}`
* Desactiva un gimnasio mediante su ID
* Códigos de respuesta: `200`, `400`, `404`, `409`, `500`

---

## 🖥️ Aplicación de Escritorio

La aplicación debe incluir un **User Control** similar al utilizado en el proyecto de Pokémon, que permita:

* 📋 Visualizar listado de gimnasios activos
* ➕ Cargar un nuevo gimnasio
* ✏️ Modificar un gimnasio existente
* ❌ Desactivar un gimnasio

---

## 🎨 Diseño

* Respetar colores, fuentes y estilos actuales de la aplicación
* Se permite extender el diseño manteniendo coherencia con la paleta existente

---

## ⚠️ Notas

* La desactivación de gimnasios no elimina el registro, solo cambia su estado (`gimnasio_activo = 0`)
* Se recomienda validar datos antes de enviar requests a la API
