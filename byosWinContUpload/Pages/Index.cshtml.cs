using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using byosWinContUpload.models;
namespace byosWinContUpload.Pages
{
    public class IndexModel : PageModel
    {

        private AppDbCtx _context;
        public List<ImgModel> imgModel { get; set; }

        public IndexModel(AppDbCtx context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            imgModel = await _context.Images.ToListAsync();
        }


    }
}
  

