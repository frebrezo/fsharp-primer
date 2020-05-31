// Learn more about F# at http://fsharp.org

open System
open System.Collections.Generic

// C-like function declarations. Functions must be declared ABOVE usage.
// Using explicit typing. F# does not require explicit typing.
//let addSeries (numbers) =
let addSeries (numbers : int seq) : int =
    Seq.fold (fun state item -> state + item) 0 numbers // func, seed, seq
    // reduce throws an exception if seq is empty.
    //if Seq.isEmpty numbers then 0
    //else numbers |> Seq.reduce (fun x y -> x + y)
    // sum is a special case of reduce.
    //else numbers |> Seq.sum

let addImmutableSample() =
    // Array
    let numbers = [|10; 13; 1; 2; 4; 5|]
    printfn "%A" numbers

    let result = addSeries(numbers)
    printfn "%i" result

    // List
    let numbers = [10; 13; 1; 2; 4; 5]
    printfn "%A" numbers

    let result = addSeries(numbers)
    printfn "%i" result

    // Singleton
    let numbers = Seq.singleton 10
    printfn "%A" numbers

    let result = addSeries(numbers)
    printfn "%i" result

    // Empty
    let numbers = []
    printfn "%A" numbers

    let result = addSeries(numbers)
    printfn "%i" result

let addMutableSample() =
    // FYI, F# doesn't help you add open (using) statements.
    let numbers = new List<int>()
    numbers.AddRange([10; 13; 1; 2; 4; 5])
    printfn "%s" (String.Join(',', numbers)) // Very C#
    let result = addSeries(numbers)
    printfn "%i" result

    // Generally, it doesn't make sense to use .NET generic lists in F#.
    //      Simply join lists.
    let mutable numbers = [10; 13; 1; 2; 4; 5]
    numbers <- 6 :: numbers
    numbers <- numbers @ [3; 5; 7; 9; 11]
    printfn "%A" numbers

    let result = addSeries(numbers)
    printfn "%i" result

// Produces an infinite list of Fibonnaci numbers.
let generateFibonacciNumbers : int seq =
    (1, 1)
    |> Seq.unfold (fun state ->
        let next = fst state + snd state
        Some(next, (snd state, next)))

let add x y =
    x + y

let subtract x y =
    x - y

let mulitple x y =
    x * y

let divide x y =
    x / y

let calc op x y =
    op x y

let calculator() =
    let result = calc add 1.0 2.0
    printfn "%f" result

    let result = calc subtract 1.0 2.0
    printfn "%f" result

    let result = calc mulitple 1.0 2.0
    printfn "%f" result

    let result = calc divide 1.0 2.0
    printfn "%f" result

let convertToAlphabet (s : string) : string =
    let sarr = s.ToLowerInvariant().ToCharArray()
            |> Seq.filter (fun x -> Char.IsLetter(x))
            |> Seq.distinct
            |> Seq.sort
    String.Join(String.Empty, sarr)

let isPangram (s : string ) : bool =
    let result = convertToAlphabet s
    result = "abcdefghijklmnopqrstuvwxyz"

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    addImmutableSample()
    addMutableSample()

    printfn "%A" generateFibonacciNumbers
    let fibonnaciSeq = generateFibonacciNumbers |> Seq.take 10 |> Seq.toList
    printfn "%A" fibonnaciSeq

    calculator()

    printfn "Is pangram %b" (isPangram "The quick brown fox jumps over the lazy dog.")

    0 // return an integer exit code
