module Asserts

open System
open Xunit

let greaterThan a b =
    Assert.True ((b > a))

let lessThan a b =
    Assert.True ((b < a))

let equal a b =
    Assert.True ((b = a))

let notEqual a b =
    Assert.True ((b <> a))