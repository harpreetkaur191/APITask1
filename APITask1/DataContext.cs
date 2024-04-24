global using Microsoft.EntityFrameworkCore;
using APITask1.Models;

namespace APITask1
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users {  get; set; }
    }
}
