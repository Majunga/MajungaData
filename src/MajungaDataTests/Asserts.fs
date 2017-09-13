module Asserts

open NUnit.Framework

let equal a b =
    Assert.IsTrue((a = b))
let notEqual a b =
    Assert.IsTrue((a <> b))

let greaterThan a b =
    Assert.IsTrue((b > a))
let lessThan a b =
    Assert.IsTrue((b < a))

let should f a b =
    f a b