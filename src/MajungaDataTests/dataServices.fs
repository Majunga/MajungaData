module DataServices

open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Options
open Microsoft.Extensions.DependencyInjection
open System

open DAL
open modelTypes
open Majunga.Data.Entity

let optionsBuilder (name:string) : DbContextOptionsBuilder<DataContext> = 
    new DbContextOptionsBuilder<DataContext>() 
    |> (fun x -> x.UseInMemoryDatabase(name))
let CreateContext name = new DataContext(optionsBuilder(name).Options) 