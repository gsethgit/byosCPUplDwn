using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using byosLinuxImageWTM.models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace byosLinuxImageWTM.Pages
{
    public class wtmPageModel : PageModel
    {
        private readonly ILogger<wtmPageModel> _logger;
        private readonly IConfiguration Config;
       
        private AppDbCtx _context;
        public List<ImgModel> imgModel { get; set; }

       
        public wtmPageModel(ILogger<wtmPageModel> logger, IConfiguration config,AppDbCtx context)
        {
            _logger = logger;
            _context = context;
            Config = config;
        }

        [BindProperty]
        public ImgModel image { get; set; }
        public async Task OnGet(int? id)
        {
            image = await _context.Images.FindAsync(id);
            imgModel = await _context.Images.ToListAsync();

        }

        public async Task<IActionResult> OnPost(int id)
        {
            try
            {
                _logger.LogInformation("entering watermark");
                var imageRectoUpdate = _context.Images.Find(id);
                string wtmtxt = image.imgTitle;
                string filename = Path.GetFileNameWithoutExtension(imageRectoUpdate.imgURL);
                string wtmFilename = filename + "_wtm.png";
                _logger.LogInformation(wtmtxt.ToString()+"::::: "+filename.ToString()+"::::: "+wtmFilename.ToString());
                using (var stream = new MemoryStream())
                {
                    using (FileStream file = new FileStream(imageRectoUpdate.imgURL, FileMode.Open, FileAccess.Read))
                    {
                        byte[] bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                        stream.Write(bytes, 0, (int)file.Length);
                    }


                    // Add watermark
                    var watermarkedStream = new MemoryStream();
                    using (var img = Image.FromStream(stream))
                    {
                        using (var graphic = Graphics.FromImage(img))
                        {
                            var font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold, GraphicsUnit.Pixel);
                            var color = Color.FromArgb(255, 0, 0);
                            var brush = new SolidBrush(color);
                            var point = new Point(img.Width - 120, img.Height - 30);

                            graphic.DrawString(wtmtxt, font, brush, point);
                            _logger.LogInformation(Config["uploadPath"].ToString());
                            img.Save(Config["uploadPath"] + "//" + wtmFilename, ImageFormat.Png);
                            imageRectoUpdate.wtimgURL = Config["uploadPath"] + "\\" + wtmFilename;
                            await _context.SaveChangesAsync();


                        }
                    }

                }
                _logger.LogInformation("Leaving Watermark");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Stack Trace from Logger: " + ex.StackTrace);
            }
          
                return RedirectToPage("./Index");

            }
        }
    }


