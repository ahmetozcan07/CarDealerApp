using System.ComponentModel.DataAnnotations;

namespace CarDealerApp.Models
{
    public class Car
    {
        public int Id { get; set; }

        public required string Make { get; set; }

        public required string Model { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        public string? Description { get; set; }
    }
}
