module dal

open System.Data.Entity
open modelTypes

[<Literal>]
let connectionString = "Server=.\\SQLEXPRESS;Database=Test;Trusted_Connection=True;MultipleActiveResultSets=true"

type dataContext(conn:string) =
    inherit DbContext(conn)

    [<DefaultValue()>] val mutable settings : IDbSet<Setting>
    member x.Settings with get() = x.settings and set v = x.settings <- v