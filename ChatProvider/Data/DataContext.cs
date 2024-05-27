using ChatProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatProvider.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserConnection> Connections { get; set; }
}
