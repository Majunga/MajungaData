module Helpers

let checkNull (x:obj) =
    match box x with
    | null -> true
    | _ -> false
