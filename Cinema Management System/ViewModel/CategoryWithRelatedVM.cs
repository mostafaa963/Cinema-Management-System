using Cinema_Management_System.Models;

namespace Cinema_Management_System.ViewModel
{
    public class CategoryWithRelatedVM
    {
        public IEnumerable<Category> categories { get; set; }
        public int totalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
