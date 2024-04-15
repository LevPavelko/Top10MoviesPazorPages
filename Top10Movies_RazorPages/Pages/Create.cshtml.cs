using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Top10Movies_RazorPages.Model;

namespace Top10Movies_RazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MovieContext _context;
        public IEnumerable<Model.Movie> filemodel { get; set; } = default!;
        IWebHostEnvironment _appEnvironment;
        public CreateModel(MovieContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Movie movie { get; set; } = default!;

        [BindProperty]
        public IFormFile poster { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Movies == null || movie == null)
            {
                return Page();
            }
            string path = "/img/" + poster.FileName; // имя файла


            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await poster.CopyToAsync(fileStream); // копируем файл в поток
            }
            movie.Poster = path;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
