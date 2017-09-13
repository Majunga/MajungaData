module dataServices

open Majunga.Data.Operations
open dal
open modelTypes
let context = new dataContext(connectionString)
let settingsService = new dataOps<Setting>(context)

    
