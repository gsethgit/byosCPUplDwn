using Microsoft.EntityFrameworkCore;

namespace byosWinContUpload.models
{
    public class AppDbCtx:DbContext
    {
        public AppDbCtx(DbContextOptions<AppDbCtx> options) : base(options)
        {
        }
        public DbSet<ImgModel> Images { get; set; }
        
    }

}
