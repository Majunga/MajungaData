module DataServices

open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open System

open DAL
open modelTypes
open Majunga.Data.Entity

let context = 
    let dataContext = new DataContext()
    dataContext.Database.EnsureCreated() |> ignore
    dataContext

let settingsService = new Operations<Setting>(context)

    
