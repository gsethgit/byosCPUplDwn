using Microsoft.EntityFrameworkCore;

namespace byosLinuxImageWTM.models
{
    public class AppDbCtx:DbContext
    {
        public AppDbCtx(DbContextOptions<AppDbCtx> options) : base(options)
        {
        }
        public DbSet<ImgModel> Images { get; set; }
        
    }

}
