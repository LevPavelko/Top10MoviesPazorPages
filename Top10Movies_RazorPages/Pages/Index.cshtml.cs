using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Top10Movies_RazorPages.Model;

namespace Top10Movies_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MovieContext _context;

        public IndexModel(MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;
       

        public async Task OnGet()
        {
            if (_context.Movies != null)
            {
                Movie = await _context.Movies.ToListAsync();
            }
        }
    }
}
