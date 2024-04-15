using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Top10Movies_RazorPages.Model;

namespace Top10Movies_RazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MovieContext _context;
        public CreateModel(MovieContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Movie movie { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Movies == null || movie == null)
            {
                return Page();
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
