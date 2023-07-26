namespace FiniteStateMachine.Tests

module DomainTests =

    open Xunit
    open FiniteStateMachine

    [<Fact>]
    let ``Domain initial state is CLOSED`` () =
        // ARRANGE
        let expected = State.CLOSED

        // ACT
        let actual = Domain.InitialState

        // ASSERT
        Assert.Equivalent(actual, expected)

    [<Fact>]
    let ``When input is empty Event list then result is Ok with value CLOSED`` () =
        // ARRANGE
        let input = []
        let expected = State.CLOSED

        // ACT
        let result = Domain.CalculateFinalState input

        // ASSERT
        match result with
        | Ok resultValue -> Assert.Equivalent(expected, resultValue)
        | Error errorValue ->
            let error = $"Expected Ok but was Error ({errorValue})."
            Assert.True(false, error)

    [<Fact>]
    let ``When input starts not with APP_PASSIVE_OPEN or APP_ACTIVE_OPEN then result is Error`` () =
        // ARRANGE
        let input = [Event.APP_SEND]
        let expected = "ERROR"

        // ACT
        let result = Domain.CalculateFinalState input

        // ASSERT
        match result with
        | Error errorValue -> Assert.Equivalent(expected, errorValue)
        | Ok resultValue ->
            let error = $"Expected Error but was Ok ({resultValue})."
            Assert.True(false, error)

    [<Fact>]
    let ``When input is [APP_PASSIVE_OPEN, APP_SEND, RCV_SYN_ACK] then result is ESTABLISHED`` () =
        // ARRANGE
        let input = [Event.APP_PASSIVE_OPEN; Event.APP_SEND; Event.RCV_SYN_ACK]
        let expected = State.ESTABLISHED

        // ACT
        let result = Domain.CalculateFinalState input

        // ASSERT
        match result with
        | Ok resultValue -> Assert.Equivalent(expected, resultValue)
        | Error errorValue ->
            let error = $"Expected Ok but was Error ({errorValue})."
            Assert.True(false, error)