// Learn more about F# at http://fsharp.org

open System

let zoom num =
    num % 3 = 0 && num % 5 = 0

let zip num =
    not (zoom num) && num % 3 = 0

let zap num =
    not (zoom num) && num % 5 = 0

let zipZapZoom num = 
    match num with
    | x when zip x -> "Zip"
    | x when zap x -> "Zap"
    | x when zoom x -> "Zoom"
    | _ -> "Invalid"
    //if (num |> zip) then "Zip"
    //else if (num |> zap) then "Zap"
    //else if (num |> zoom) then "Zoom"
    //else "Invalid"

let playZipZapZoom numbers =
    match numbers with
    | [] -> printfn "Empty list. Can't play Zip Zap Zoom."
    | _ ->  for number in numbers do
                let result = zipZapZoom number
                printfn "%i %s" number result

let rec csvString numbers : string =
    match numbers with
    | head :: tail ->
        let str = csvString tail
        if str.Length > 0 then sprintf "%d,%s" head str
        else sprintf "%d" head
    | [] -> ""

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    playZipZapZoom [9; 10; 15; 19]
    playZipZapZoom []

    let result = [9; 10; 15; 19] |> csvString
    printfn "%s" result

    let result = [9] |> csvString
    printfn "%s" result

    let result = [] |> csvString
    printfn "%s" result

    0 // return an integer exit code
