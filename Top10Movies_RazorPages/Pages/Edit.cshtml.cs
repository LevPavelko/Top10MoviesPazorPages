using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Top10Movies_RazorPages.Model;

namespace Top10Movies_RazorPages.Pages
{
    public class EditModel : PageModel
    {
        private readonly MovieContext _context;
        public IEnumerable<Model.Movie> filemodel { get; set; } = default!;
        IWebHostEnvironment _appEnvironment;
        public EditModel(MovieContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
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
            movie = movieId;
            return Page();
        }
        [BindProperty]
        public IFormFile uploadedFile { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string path = "/img/" + uploadedFile.FileName; // имя файла

          
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
            }
            movie.Poster = path;
            _context.Attach(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
