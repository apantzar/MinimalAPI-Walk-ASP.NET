using Microsoft.EntityFrameworkCore;

namespace MinimalShoppingListApi
{


    /*
     * 
     * 
     * Να θυμάμαι,
     *   1. Φτιάχνω το μοντέλο Grocery,
     *   2. Θέλω μια βάση δεδομένων, φτιάχνω μια κλάση πχ ApiDbContext που κάνει inherit απο την DbContext
     *      διότι θέλω να κάνω depedency injection
     *   3. Φτιαχνω constructor με ορίσματα (DbContextOptions<όνομαΚλάσηςCustomDbContex> name) 
     *      και κάνω επέκταση με :base (options) για να έχω τα βασικά options
     *   4. Φτιάχνω property DbSet<Grocery> Groceries => Set<Grocery>();
     *   5. Αφού κάνω MinimalAi στο Program.cs πρέπει να ορίσω:
     *      builder.Services.AddDbContext<όνομαΚλάσηςCustomDbContext>(opt => opt.UseInMemoryDatabase("ShoppingListApi"));
     *      και ως όρισμα έχει τα options.
     *  
     * 
     */


    public class ApiDbContext: DbContext
    {

        public DbSet<Grocery> Groceries => Set<Grocery>();
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }



    }
}
