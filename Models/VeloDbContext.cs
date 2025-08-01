using Microsoft.EntityFrameworkCore;

namespace Velo.Models;

public class VeloDbContext(DbContextOptions<VeloDbContext> options) : DbContext(options)
{
    public DbSet<PdfFile> PdfFiles { get; set; }
}