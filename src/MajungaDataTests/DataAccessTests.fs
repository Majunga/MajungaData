module DataAccessTests

open Asserts
open NUnit.Framework
open dal
open modelTypes
open dataServices
open System.Linq
open System.Data.Entity

[<Test>]
let ``Database Exists`` () =
    (new dal.dataContext(connectionString)).Database.Exists() |> should equal true

[<Test>]
let ``Create Record`` () = 
    let setting = new Setting()
    setting.Key <- "Test Key"
    setting.Value <- "Test Value"

    settingsService.addRecord(setting) |> greaterThan 0

    
[<Test>]
let ``Delete all Records`` () =
    settingsService.getAllRecords() |> List.iter (fun (x:Setting) -> settingsService.deleteRecord x)

    settingsService.getAllRecords() |> List.length |> equal 0

[<Test>]
let ``Custom Where Query`` () =
    ``Create Record``()

    (settingsService.Query).Where((fun x -> x.Key = "Test Key")).FirstOrDefault().Value |> should equal "Test Value"

    ``Delete all Records``()