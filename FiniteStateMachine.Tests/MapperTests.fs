namespace FiniteStateMachine.Tests

module MapperTests =

    open System
    open Xunit
    open FiniteStateMachine

    [<Fact>]
    let ``When input is empty string list result is empty Event list`` () =
        // ARRANGE
        let input = String.Empty

        //ACT
        let actualResult = Mapper.GetEventsFromString input

        //ASSERT
        match actualResult with
        | Ok resultValue -> Assert.True(resultValue.IsEmpty)
        | Error errorResult ->
            let error = $"Expected Ok but was Error ({errorResult})."
            Assert.True(false, error)

    [<Fact>]
    let ``When input is contains invalid event name then result is Error`` () =
        // ARRANGE
        let input = "APP_PASSIVE_OPEN, APP_SEND, RCV_SYN_ACK_ZZZ"
        let expected = "Invalid input. Can not match event 'RCV_SYN_ACK_ZZZ'"

        //ACT
        let actualResult = Mapper.GetEventsFromString input

        //ASSERT
        match actualResult with
        | Error errorResult -> Assert.Equal(expected, errorResult)
        | Ok resultValue ->
            let error = $"Expected Error but was Ok ({resultValue})."
            Assert.True(false, error)

    [<Fact>]
    let ``When input is random string then result is Error`` () =
        // ARRANGE
        let input = "Some random string"

        //ACT
        let actualResult = Mapper.GetEventsFromString input

        //ASSERT
        match actualResult with
        | Error _ -> Assert.True(true)
        | Ok resultValue ->
            let error = $"Expected Error but was Ok ({resultValue})."
            Assert.True(false, error)

    [<Fact>]
    let ``When input is valid event sequance then result is Ok`` () =
        // ARRANGE
        let input = @"APP_PASSIVE_OPEN, APP_SEND, RCV_SYN_ACK"
        let expected = [Event.APP_PASSIVE_OPEN; Event.APP_SEND; Event.RCV_SYN_ACK]

        //ACT
        let actualResult = Mapper.GetEventsFromString input

        //ASSERT
        match actualResult with
        | Ok resultValue -> Assert.Equivalent(expected, resultValue)
        | Error errorResult ->
            let error = $"Expected Ok but was Error ({errorResult})."
            Assert.True(false, error)

