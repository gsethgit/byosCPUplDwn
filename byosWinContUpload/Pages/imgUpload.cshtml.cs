using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using byosWinContUpload.models;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace byosWinContUpload.Pages
{

    public class imgUploadModel : PageModel
    {

        private AppDbCtx _context;

        private readonly IConfiguration Configuration;

        public string uplPath = string.Empty;

        [BindProperty]
        public ImgModel imgModel { get; set; }

        [BindProperty]
        public AzFileUpload azfileUpload { get; set; }

        public imgUploadModel(IConfiguration configuration, AppDbCtx context)
        {
            _context = context;
            Configuration = configuration;
        }
        public void OnGet()
        {
            ViewData["Result"] = "";
        }

        public async Task<IActionResult> OnPostUpload(AzFileUpload fileUpload)
        {
            try
            {

                var uplFile = azfileUpload.uplFormFile;

                uplPath = Configuration["uploadPath"];

                var finalFilePath = Path.Combine(uplPath, uplFile.FileName);

                var formFileContent = await ProcessFormFile(azfileUpload.uplFormFile);

                using (var stream = System.IO.File.Create(finalFilePath))
                {
                    await stream.WriteAsync(formFileContent);
                    
                }

                imgModel.imgURL = finalFilePath;

                _context.Images.Add(imgModel);
                _context.SaveChanges();

                ViewData["Result"] = uplFile.FileName.ToString() + " Uploaded Successfully!!";

                return RedirectToPage("imggalView");
            }
            catch (Exception ex)
            {
                return RedirectToPage("Error");
            }
        }


        public static async Task<byte[]> ProcessFormFile(IFormFile formFile)
        {
            var fieldDisplayName = string.Empty;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    return memoryStream.ToArray();

                }
            }
            catch (Exception ex)
            {

            }

            return new byte[0];
        }
    }
    public class AzFileUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile uplFormFile { get; set; }

        public string Result { get; set; }

    }
}
