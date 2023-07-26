namespace FiniteStateMachine.Tests

module InputValidatorTests =
    open System
    open Xunit
    open FiniteStateMachine

    [<Fact>]
    let ``When input is null result is Error`` () =
        // ARRANGE
        let input = null
        let expected = "The input data was irrelevant"
        
        // ACT
        let actualResult = InputValidator.ValidateInput input
        
        // ASSERT
        match actualResult with
        | Error errorValue ->
            Assert.Same(expected, errorValue)
        | Ok _ ->
            let error = "Expected Error but was Ok"
            printfn "%s" error
            Assert.True(false, error)

    [<Fact>]
    let ``When input is empty string result is Ok`` () =
        // ARRANGE
        let input = String.Empty
        let expected = input

        // ACT
        let actualResult = InputValidator.ValidateInput input
        
        // ASSERT
        match actualResult with
        | Ok resultValue ->
            Assert.Same(expected, resultValue)
        | Error _ ->
            let error = "Expected Ok but was Error"
            printfn "%s" error
            Assert.True(false, error)

    [<Fact>]
    let ``When input is not empty or whitespace and not null result of Ok`` () =
        // ARRANGE
        let input = "Some input string"
        let expected = input
        
        // ACT
        let actualResult = InputValidator.ValidateInput input
        
        // ASSERT
        match actualResult with
        | Ok resultValue ->
            Assert.Same(expected, resultValue)
        | Error _ ->
            let error = "Expected Ok but was Error"
            printfn "%s" error
            Assert.True(false, error)

    [<Fact>]
    let ``When input is valid string with events then result Ok with resultValue as input but without square brackets and quotes`` () =
        // ARRANGE
        let input = @"[""APP_PASSIVE_OPEN"", ""APP_SEND"", ""RCV_SYN_ACK""]"
        let expected = 
            input
                .Replace("\"", "")
                .Replace("'", "")
                .Replace("[", "")
                .Replace("]", "")
        
        // ACT
        let actualResult = InputValidator.ValidateInput input
        
        // ASSERT
        match actualResult with
        | Ok resultValue ->
            Assert.Same(expected, resultValue)
        | Error _ ->
            let error = "Expected Ok but was Error"
            printfn "%s" error
            Assert.True(false, error)
