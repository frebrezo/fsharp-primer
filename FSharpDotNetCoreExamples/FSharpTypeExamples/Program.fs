// Learn more about F# at http://fsharp.org

open System
open System

type Perimeter =
    | Square of double
    | Rectangle of double * double
    | Triangle of double * double * double
    | RightTriangle of double * double
    | Circle of double

let calc perimeter : double =
    match perimeter with
        | Square s -> 2.0 * s
        | Rectangle (l, w) -> 2.0 * l + 2.0 * w
        | Triangle (a, b, c) -> a + b + c
        | RightTriangle (a, b) -> a + b + sqrt(a ** 2.0 + b ** 2.0)
        | Circle (r) -> 2.0 * Math.PI * r

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    let perimeter = Square(5.0)
    let result = calc perimeter
    printfn "%f" result

    let perimeter = Rectangle(5.0, 2.0)
    let result = calc perimeter
    printfn "%f" result

    let perimeter = Triangle(5.0, 2.0, 10.0)
    let result = calc perimeter
    printfn "%f" result

    let perimeter = RightTriangle(5.0, 2.0)
    let result = calc perimeter
    printfn "%f" result

    let perimeter = Circle(5.0)
    let result = calc perimeter
    printfn "%f" result

    0 // return an integer exit code
