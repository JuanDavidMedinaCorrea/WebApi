namespace WebApi.Models
{
    public class CategoriaLog
    {
        public int Id { get; set; }
        public int CategoriaIdEliminada { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public string? Usuario { get; set; } 
    }

}
