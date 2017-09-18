module DataAccessTests

open FsUnit
open NUnit.Framework
open dal
open modelTypes
open dataServices
open System.Linq
open System.Globalization
open Majunga.Data.Entity
open System

/// Setup Functions
let addNewSetting (i:int) =
    let setting = new Setting()
    setting.Key <- "Test Key" + i.ToString(CultureInfo.InvariantCulture)
    setting.Value <- "Test Value" + i.ToString(CultureInfo.InvariantCulture)
    setting

let createSettingsList =
    List.init 20 (fun index -> addNewSetting index)


/// CRUD Operation Tests
[<Test>]
let ``Database Exists`` () =
    checkDbExists connectionString
        |> should equal true

[<Test>]
let ``Create Record`` () = 
    let setting = new Setting()
    setting.Key <- "Test Key"
    setting.Value <- "Test Value"

    settingsService.Create setting
        |> should be (greaterThan 0)
    
[<Test>]
let ``Delete all Records`` () =
    settingsService.ReadAll 
        |> List.iter (fun (x:Setting) -> settingsService.Delete x)

    settingsService.ReadAll
        |> List.length 
        |> should equal 0

[<Test>]
let ``Create Multiple Records`` () = 
    createSettingsList 
        |> List.iter (fun x -> settingsService.Create x |> ignore)

    settingsService.ReadAll
        |> List.length 
        |> should equal 20

    ``Delete all Records``()

[<Test>]
let ``Read Record`` () = 
    addNewSetting 99 
        |> settingsService.Create 
        |> settingsService.Read 
        |> (fun (x:Setting) -> x.Key = "Test Key99") 
        |> should equal true

    ``Delete all Records``()

[<Test>]
let ``Custom Where Query`` () =
    ``Create Record``()

    (settingsService.Query).Where((fun x -> x.Key = "Test Key")).FirstOrDefault().Value 
        |> should equal "Test Value"

    ``Delete all Records``()


/// Edge case Tests

[<Test>]
let ``Create Record that exists`` () =
    let setting = new Setting()
     
    setting.Key <- "Test Key"
    setting.Value <- "Test Value"
    setting.Id <- settingsService.Create setting

    (fun () -> settingsService.Create setting |> ignore) |> should throw typeof<InvalidOperationException>
        
    settingsService.Delete setting

[<Test>]
let ``Update Record that doesn't exists`` () =
    let setting = new Setting()
    setting.Key <- "Test Key"
    setting.Value <- "Test Value"

    (fun () -> settingsService.Update setting) |> should throw typeof<InvalidOperationException>