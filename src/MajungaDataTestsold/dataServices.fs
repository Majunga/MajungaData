module dataServices

open Majunga.Data.Entity
open dal
open modelTypes

let context = new dataContext(connectionString)

let settingsService = new Operations<Setting>(context)

    
