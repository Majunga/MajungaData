module DAL

open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.EntityFrameworkCore.Infrastructure
open modelTypes


type DataContext =
    inherit DbContext
    new(buildOptions : DbContextOptions<DataContext>) = { 
        inherit DbContext(buildOptions) 
    }
    [<DefaultValue()>] val mutable settings : DbSet<Setting>
    member x.Settings with get() = x.settings and set v = x.settings <- v