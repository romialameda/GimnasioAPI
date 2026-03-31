using System.Data;

namespace GimnasioAPI.Models
{
    public class Gimnasio
    {
        public int id { get; set; }
        public string ciudad_gimnasio { get; set; }
        public int id_region { get; set; }
        public DateTime fecha_alta { get; set; }
        public string entrenador_lider { get; set; }
        public bool gimnasio_activo { get; set; }
        public string nombre_medalla { get; set; }

        //Constructor vacio para endopoints POST y PUT
        public Gimnasio()
        {
            this.id = -1;
            this.ciudad_gimnasio = "";
            this.id_region = -1;
            this.fecha_alta = DateTime.Now;
            this.entrenador_lider = "";
            this.gimnasio_activo = false;
            this.nombre_medalla = "";
        }

        // Busca el texto en la columna de SQL y lo guarda en la propiedad
        // Los "??" significan que si el dato viene nulo o vacío de SQL, le asigna un valor por defecto.
        public Gimnasio(DataRow fila) {
            this.id = int.Parse(fila["id"].ToString() ?? "0");
            this.ciudad_gimnasio = fila["ciudad_gimnasio"].ToString() ?? "ERROR";
            this.id_region = int.Parse(fila["id_region"].ToString() ?? "0");
            this.fecha_alta = DateTime.Parse(fila["fecha_alta"].ToString() ?? "1900-01-01");
            this.entrenador_lider = fila["entrenador_lider"].ToString() ?? "ERROR";
            this.gimnasio_activo = bool.Parse(fila["gimnasio_activo"].ToString() ?? "false");
            this.nombre_medalla = fila["nombre_medalla"].ToString() ?? "ERROR";
        }

        public Gimnasio(int id, string ciudad_gimnasio, int id_region, DateTime fecha_alta, string entrenador_lider, bool gimnasio_activo, string nombre_medalla)
        {
            this.id = id;
            this.ciudad_gimnasio = ciudad_gimnasio;
            this.id_region = id_region;
            this.fecha_alta = fecha_alta;
            this.entrenador_lider = entrenador_lider;
            this.gimnasio_activo = gimnasio_activo;
            this.nombre_medalla = nombre_medalla;
        }
    }
}
