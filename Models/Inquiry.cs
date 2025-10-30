using System.ComponentModel.DataAnnotations;

namespace CarDealerApp.Models
{
    public class Inquiry
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string Message { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }
    }
}
