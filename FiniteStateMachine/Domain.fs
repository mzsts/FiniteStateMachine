namespace FiniteStateMachine

module Domain =
    let InitialState: State = CLOSED

    let private getNextState (state: State) (event: Event) =
        match (state, event) with
        | CLOSED, APP_PASSIVE_OPEN -> Ok LISTEN
        | CLOSED, APP_ACTIVE_OPEN -> Ok SYN_SENT
        | LISTEN, APP_SEND -> Ok SYN_SENT
        | LISTEN, RCV_SYN -> Ok SYN_RCVD
        | LISTEN, APP_CLOSE -> Ok CLOSED
        | SYN_RCVD, APP_CLOSE -> Ok FIN_WAIT_1
        | SYN_RCVD, RCV_ACK -> Ok ESTABLISHED
        | SYN_SENT, RCV_SYN -> Ok SYN_RCVD
        | SYN_SENT, RCV_SYN_ACK -> Ok ESTABLISHED
        | SYN_SENT, APP_CLOSE -> Ok CLOSED
        | ESTABLISHED, APP_CLOSE -> Ok FIN_WAIT_1
        | ESTABLISHED, RCV_FIN -> Ok CLOSE_WAIT
        | FIN_WAIT_1, RCV_FIN -> Ok CLOSING
        | FIN_WAIT_1, RCV_FIN_ACK -> Ok TIME_WAIT
        | FIN_WAIT_1, RCV_ACK -> Ok FIN_WAIT_2
        | CLOSING, RCV_ACK -> Ok TIME_WAIT
        | FIN_WAIT_2, RCV_FIN -> Ok TIME_WAIT
        | TIME_WAIT, APP_TIMEOUT -> Ok CLOSED
        | CLOSE_WAIT, APP_CLOSE -> Ok LAST_ACK
        | LAST_ACK, RCV_ACK -> Ok CLOSED
        | _ -> Error "ERROR"

    let rec private calculateState (state: State) (events: Event list) =
        match events with
        | x::xs -> getNextState state x |> Result.bind (fun s -> calculateState s xs)
        | [] -> Ok state

    let CalculateFinalState (events: Event list): Result<State, string> =
        match events with
        | [] -> Ok InitialState
        | _ -> calculateState InitialState events

