module dataServices

open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open System

open dal
open modelTypes
open Majunga.Data.Entity

let context = new dataContext()

let settingsService = new Operations<Setting>(context)

    
