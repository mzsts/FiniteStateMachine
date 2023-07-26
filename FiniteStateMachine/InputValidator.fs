namespace FiniteStateMachine

module InputValidator =

    let private validateRawInput input =
        match input with
        | null -> Error "The input data was irrelevant"
        | _ -> Ok input

    let private repairInput (inputStr: string) =
        inputStr
            .Replace("\"", "")
            .Replace("'", "")
            .Replace("[", "")
            .Replace("]", "")

    let ValidateInput (input: string): Result<string, string> = 
        validateRawInput input
        |> Result.bind (fun x -> x |> repairInput |> Ok)