using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class Direction
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Навигация
        public List<Group> Groups { get; set; } = new();
        public List<DirectionTest> DirectionTests { get; set; } = new();
    }
}
