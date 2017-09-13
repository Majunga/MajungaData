module Majunga.Data.Entity


open System.Data.Entity
open System.Linq
open Majunga.Data.Types
open System
open System.Linq.Expressions

let checkDbExists (conn:string) =
    System.Data.Entity.Database.Exists(conn)

let Save (db:DbContext) =
    db.SaveChanges() |> ignore

type Operations<'TEntity when 'TEntity : (new : unit -> 'TEntity) and 'TEntity : not struct and 'TEntity :> IEntity>
    (db:(DbContext)) = 

    member this.Create(model : 'TEntity)  = 
        db.Set<'TEntity>().Add(model) |> ignore
        Save db
        model.Id

    member this.Read(id)  = 
        db.Set<'TEntity>().Where(fun x -> x.Id = id).FirstOrDefault()

    member this.Update(model)  = 
        db.Set<'TEntity>().Attach(model)  |> ignore
        db.Entry(model).State <- EntityState.Modified
        Save db

    member this.Delete(model)  = 
        db.Set<'TEntity>().Remove(model) |> ignore
        Save db

    member this.ReadAll() = 
        db.Set<'TEntity>().ToList() |> List.ofSeq 

    member this.Attach(model) =
        db.Set<'TEntity>().Attach(model) |> ignore   
        
    member this.Query =
        db.Set<'TEntity>().AsQueryable()