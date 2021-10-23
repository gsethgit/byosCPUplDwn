using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using byosLinuxUpload.models;

namespace byosLinuxUpload.Pages
{
    public class imggalViewModel : PageModel
    {
        private AppDbCtx _context;
        public List<ImgModel> imgModel { get; set; }
        public imggalViewModel(AppDbCtx context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            
            imgModel = await _context.Images.ToListAsync();
        }
    }
}
