using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Top10Movies_RazorPages.Model;

namespace Top10Movies_RazorPages.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Top10Movies_RazorPages.Model.MovieContext _context;

        public DetailsModel(Top10Movies_RazorPages.Model.MovieContext context)
        {
            _context = context;
        }

        public Movie movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movieId = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movieId == null)
            {
                return NotFound();
            }
            else
            {
                movie = movieId;
            }
            return Page();
        }
    }
}
