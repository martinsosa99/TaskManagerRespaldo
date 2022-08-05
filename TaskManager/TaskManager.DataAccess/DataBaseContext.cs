using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManager.Domain;

namespace TaskManager.DataAccess;

public class DataBaseContext : DbContext
{

    public DbSet<TodoTask> TodoTasks { get; set; }

	public DbSet<User> Users { get; set; }

	public DbSet<Session> Sessions { get; set; }

	public DataBaseContext() { }

	public DataBaseContext(DbContextOptions options) : base(options) { }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	}*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=motty.db.elephantsql.com;Database=zwyqepqp;User Id=zwyqepqp;Password=Oled0x2x46vbzhNmB-7ywUz-ScHJm7wG;Port=5432");
        }
    }

}