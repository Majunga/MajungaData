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
    let addNewSetting (i:int) =
        let setting = Setting()
        setting.Key <- "Test Key" + i.ToString(CultureInfo.InvariantCulture)
        setting.Value <- "Test Value" + i.ToString(CultureInfo.InvariantCulture)
        setting

    let createSettingsList =
        List.init 20 (addNewSetting)

    [<Fact>]
    let ``Create Record`` () = 
        let setting = Setting()
        setting.Key <- "Test Key"
        setting.Value <- "Test Value"

        settingsService.Create setting
            |> greaterThan 0
        
    [<Fact>]
    let ``Delete all Records`` () =
        settingsService.ReadAll 
            |> List.iter (settingsService.Delete)

        settingsService.ReadAll
            |> List.length 
            |> equal 0



    [<Fact>]
    let ``Read Record`` () = 
        addNewSetting 99 
            |> settingsService.Create 
            |> settingsService.Read 
            |> (fun (x:Setting) -> x.Key = "Test Key99") 
            |> equal true

        ``Delete all Records``()

    [<Fact>]
    let ``Custom Where Query`` () =
        ``Create Record``()

        (settingsService.Query).Where((fun x -> x.Key = "Test Key")).FirstOrDefault().Value 
            |> equal "Test Value"

        ``Delete all Records``()


    /// Edge case Tests
    let createExistingRecord () = 
        let setting = Setting()
        setting.Key <- "Test Key"
        setting.Value <- "Test Value"
        setting.Id <- settingsService.Create setting

        settingsService.Create setting |> ignore
            
        settingsService.Delete setting

    [<Fact>]
    let ``Create Record that exists`` () =
        Assert.ThrowsAny(createExistingRecord)

    let updateRecordThatDoesntExist () =
        let setting = Setting()
        setting.Key <- "Test Key"
        setting.Value <- "Test Value"
        settingsService.Update setting

    [<Fact>]
    let ``Update Record that doesn't exists`` () =

        Assert.ThrowsAny(updateRecordThatDoesntExist)

    [<Fact>]
    let ``Create Multiple Records`` () = 
        createSettingsList 
            |> List.iter (fun x -> settingsService.Create x |> ignore)

        settingsService.ReadAll
            |> List.length 
            |> equal 20

        ``Delete all Records``()