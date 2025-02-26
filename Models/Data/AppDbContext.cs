using Microsoft.EntityFrameworkCore;
using TodoGrpcService.Models;

namespace TodoGrpc.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> TodoItems=> Set<ToDoItem>();
}
 