module DAL

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Infrastructure
open modelTypes

[<Literal>]
let ConnectionString = "Server=.\\SQLEXPRESS;Database=Test;Trusted_Connection=True;MultipleActiveResultSets=true"

type DataContext() =
    inherit DbContext()
    override this.OnConfiguring (options : DbContextOptionsBuilder) =
        options.UseSqlServer(ConnectionString) |> ignore
    [<DefaultValue()>] val mutable settings : DbSet<Setting>
    member x.Settings with get() = x.settings and set v = x.settings <- v