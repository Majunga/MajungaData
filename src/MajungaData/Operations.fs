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

let createEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (model:'TEntity) (db:DbContext) =
    db.Set<'TEntity>().Add(model) |> ignore
    Save db
    model.Id

let readEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (id:int) (db:DbContext) =
    db.Set<'TEntity>().Where(fun x -> x.Id = id).FirstOrDefault()

let updateEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (model:'TEntity) (db:DbContext) =
    db.Set<'TEntity>().Attach(model)  |> ignore
    db.Entry(model).State <- EntityState.Modified
    Save db

let deleteEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (model:'TEntity) (db:DbContext) =
    db.Set<'TEntity>().Remove(model) |> ignore
    Save db


let readAllEntities<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (db:DbContext) =
    db.Set<'TEntity>().ToList() |> List.ofSeq 

let attachEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (model:'TEntity) (db:DbContext) =
    db.Set<'TEntity>().Attach(model) |> ignore   

let queryEntities<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (db:DbContext) =
    db.Set<'TEntity>().AsQueryable()


type Operations<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity>
    (db:(DbContext)) = 

    member this.Create(model : 'TEntity)  = 
        db |> createEntity<'TEntity>  model

    member this.Read(id) : ('TEntity) = 
        db |> readEntity<'TEntity> id

    member this.Update(model)  = 
        db |> updateEntity<'TEntity> model

    member this.Delete(model)  = 
        db |> deleteEntity model


    member this.ReadAll : ('TEntity list) =
        db |> readAllEntities

    member this.Attach(model) =
        db |> attachEntity model 
        
    member this.Query : (IQueryable<'TEntity>) =
        queryEntities db