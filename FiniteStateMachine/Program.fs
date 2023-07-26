open System
open FiniteStateMachine

let instruction = 
    $"""
Initial FSM state: {Domain.InitialState}

Write events to get final state if it's possible.
Examples:
* ["APP_PASSIVE_OPEN", "APP_SEND", "RCV_SYN_ACK"]
* "APP_PASSIVE_OPEN", "APP_SEND", "RCV_SYN_ACK"
* ['APP_PASSIVE_OPEN', 'APP_SEND', 'RCV_SYN_ACK']
* 'APP_PASSIVE_OPEN', 'APP_SEND', 'RCV_SYN_ACK'
* APP_PASSIVE_OPEN, APP_SEND, RCV_SYN_ACK

Write "close" to exit program

"""

let printResult result =
    match result with
    | Ok state ->
        Console.ForegroundColor <- ConsoleColor.Green
        printfn "> State: %A" state
    | Error errorValue -> 
        Console.ForegroundColor <- ConsoleColor.Red
        printfn "> %s" errorValue
    Console.ForegroundColor <- ConsoleColor.White

let start input =
    input
    |> InputValidator.ValidateInput
    |> Result.bind Mapper.GetEventsFromString
    |> Result.bind Domain.CalculateFinalState

let rec loop () =
    printfn "%s" instruction
    match Console.ReadLine() with
    | "close" -> ()
    | x ->
        Console.WriteLine()
        x |> start |> printResult
        loop()

loop ()