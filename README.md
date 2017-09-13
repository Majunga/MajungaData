# MajungaData
Data Library for making data access and handling easier

Currently this is an abstraction for Entity Framework to make it easier to get going with CRUD operations for Code First. It also includes a way for Reading All records, Querying records and Attaching other tables to the current context.

### Getting Started
*All code examples can be found in MajungaDataTests project*

Define a model and implement the IEntity Interface for the Id value and Key of the model.

``` fsharp 
module modelTypes

open Majunga.Data.Types

type Setting() = 
    member this.Id with get() = (this :> IEntity).Id and set v = (this :> IEntity).Id <- v
    interface IEntity with
        member val Id = 0 with get, set

    member val Key = "" with get, set
    member val Value = "" with get, set

```

Define the DbContext

```
module dal

open System.Data.Entity
open modelTypes

[<Literal>]
let connectionString = "Server=.\\SQLEXPRESS;Database=Test;Trusted_Connection=True;MultipleActiveResultSets=true"

type dataContext(conn:string) =
    inherit DbContext(conn)

    [<DefaultValue()>] val mutable settings : IDbSet<Setting>
    member x.Settings with get() = x.settings and set v = x.settings <- v

```
Lastly define the service that is used to access the database using the model that was define earlier.

```
module dataServices

open Majunga.Data.Entity
open dal
open modelTypes
let context = new dataContext(connectionString)
let settingsService = new Operations<Setting>(context)

```


Once that is done you can start to access the different CRUD operations for that table by including the Services module and access the Service function



### Road Map

* Create more tests to cover all Operations
* Add more Operation types
* Update Projects to Standard Libraries
* Create a better way to add custom queries to a data Service
* Add documentation to code
