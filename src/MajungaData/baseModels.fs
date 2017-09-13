module Majunga.Data.Types
open System.ComponentModel.DataAnnotations

type IEntity =
      [<Key>] abstract member  Id : int with get, set
