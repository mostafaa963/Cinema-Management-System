using System.Numerics;

namespace Cinema_Management_System.Models
{
    public class Movie
    {
        //asp-area ="Customer" asp-controller="Cart" asp-action="AddTocCart" method="post"
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MainImg { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public int HomeId { get; set; }
        public Category Home { get; set; } = null!;
        public List<Actor>? Actors { get; set; }
        public int CinemaID { get; set; }
        public Cinema Cinema { get; set; } = null!;

    }
}
