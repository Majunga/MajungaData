﻿
module Majunga.Data.Entity


open Microsoft.EntityFrameworkCore
open System.Linq
open Majunga.Data.Types
open System
open System.Linq.Expressions
open Helpers

//let checkDbExists (conn:string) =
//    Database.Exists(conn)

let Save (db:DbContext) =
    db.SaveChanges() |> ignore

let readEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (id:int) (db:DbContext) =
    db.Set<'TEntity>().Where(fun x -> x.Id = id).FirstOrDefault()

let createEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (model:'TEntity) (db:DbContext) =
    let value = db |> readEntity<'TEntity> model.Id |> checkNull 

    if (not value) then raise (InvalidOperationException("Entity shouldn't already exist"))

    db.Set<'TEntity>().Add(model) |> ignore
    Save db
    model.Id

let updateEntity<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity> 
        (model:'TEntity) (db:DbContext) =
    if model.Id = 0 then raise (InvalidOperationException("Entity missing Id Value"))
    
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

/// Type for calling all Entity CRUD Operations and more
type Operations<'TEntity when 'TEntity : not struct and 'TEntity :> IEntity>
    (db:(DbContext)) = 
    /// Creates a record in the database using model
    member this.Create(model : 'TEntity)  = 
        db |> createEntity<'TEntity> model

    member this.Read(id) : ('TEntity) = 
        db |> readEntity<'TEntity> id

    member this.Update(model : 'TEntity)  = 

        db |> updateEntity<'TEntity> model

    member this.Delete(model)  = 
        db |> deleteEntity model


    member this.ReadAll : ('TEntity list) =
        db |> readAllEntities

    member this.Attach(model) =
        db |> attachEntity model 
        
    member this.Query : (IQueryable<'TEntity>) =
        queryEntities db