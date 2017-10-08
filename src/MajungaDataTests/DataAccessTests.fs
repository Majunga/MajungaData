namespace MajungaDataTests



module DataAccessTests =
    open Xunit
    open DAL
    open modelTypes
    open DataServices
    open System.Linq
    open System.Globalization
    open Microsoft.EntityFrameworkCore
    open System
    open Majunga.Data.Entity
    open Asserts
    /// Setup Functions
    let settingsService name = new Operations<Setting>(CreateContext name)

    let addNewSetting (i:int) =
        Setting(Key = "Test Key" + i.ToString(CultureInfo.InvariantCulture),Value = "Test Value" + i.ToString(CultureInfo.InvariantCulture))

    let createSettingsList =
        List.init 20 (addNewSetting)

    [<Fact>]
    let ``Create Record`` () = 
        let service = settingsService "Create"
        let setting = Setting()
        setting.Key <- "Test Key"
        setting.Value <- "Test Value"

        service.Create  setting
            |> greaterThan 0

    [<Fact>]
    let ``Read Record`` () = 
        let service = settingsService "Delete"
        
        addNewSetting 99 
            |> service.Create 
            |> service.Read 
            |> (fun (x:Setting) -> x.Key = "Test Key99") 
            |> equal true

    [<Fact>]
    let ``Custom Where Query`` () =
        let service = settingsService "Query"

        let setting = Setting()
        setting.Key <- "Test Key"
        setting.Value <- "Test Value"

        service.Create  setting |> ignore

        (service.Query).Where((fun x -> x.Key = "Test Key")).FirstOrDefault().Value 
            |> equal "Test Value"

    /// Edge case Tests
    // let createExistingRecord () = 
    //     let service = settingsService "CreateExisting"
    
    //     let setting = Setting()
    //     setting.Key <- "Test Key"
    //     setting.Value <- "Test Value"
    //     setting.Id <- service.Create setting

    //     service.Create setting |> ignore
            

    // [<Fact>]
    // let ``Create Record that exists`` () =
    
    //     Assert.ThrowsAny(createExistingRecord)

    // let updateRecordThatDoesntExist () =
    //     let service = settingsService "CreateExisting"
    
    //     let setting = Setting()
    //     setting.Key <- "Test Key"
    //     setting.Value <- "Test Value"
    //     service.Update setting

    // [<Fact>]
    // let ``Update Record that doesn't exists`` () =
    //     Assert.ThrowsAny(updateRecordThatDoesntExist)

    // [<Fact>]
    // let ``Create Multiple Records`` () = 
    //     let service = settingsService "CreateMultiple"
    
    //     createSettingsList 
    //         |> List.map service.Create |> ignore
    //         // |>  List.iter 

    //     service.ReadAll
    //         |> List.length 
    //         |> equal 20

                    
    // [<Fact>]
    // let ``Delete all Records`` () =
    //     let service = settingsService "Delete"

    //     createSettingsList 
    //         |> List.iter (fun x -> service.Create x |> ignore)
    //     service.ReadAll 
    //         |> List.iter (service.Delete)

    //     service.ReadAll
    //         |> List.length 
    //         |> equal 0