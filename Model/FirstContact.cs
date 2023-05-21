using System.ComponentModel.DataAnnotations.Schema;

namespace FirstContactAPI.Model
{
    public class FirstContact
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

    }
}
