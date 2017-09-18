module modelTypes

open Majunga.Data.Types

type Setting() = 
    member this.Id with get() = (this :> IEntity).Id and set v = (this :> IEntity).Id <- v
    interface IEntity with
        member val Id = 0 with get, set

    member val Key = "" with get, set
    member val Value = "" with get, set

