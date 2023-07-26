namespace FiniteStateMachine

module Mapper =

    let private mapToEvents listStr =

        let rec mapToEvents' listStr acc =
            match listStr with
            | x::xs ->
                match Event.FromString x with
                | Ok event -> mapToEvents' xs (acc @ [event])
                | Error errorValue -> Error errorValue
            | [] -> Ok acc

        mapToEvents' listStr []

    let GetEventsFromString (str: string) : Result<Event list, string> = 
        str.Split(",")
        |> Array.map(fun x -> x.Trim())
        |> Array.where(fun x -> x.Length <> 0)
        |> Array.toList
        |> mapToEvents

