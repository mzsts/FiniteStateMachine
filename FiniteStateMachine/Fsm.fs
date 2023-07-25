namespace FiniteStateMachine

module Fsm =
    let private initialState: FsmState = CLOSED

    let getNextState (state: FsmState) (event: FsmEvent) =
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
        | _ -> Error "Error"

    let rec private calculateState (state: FsmState) (events: FsmEvent list) =
        match events with
        | x::xs -> getNextState state x |> Result.bind (fun s -> calculateState s xs)
        | [] -> Ok state

    let CalculateFinalState (events: FsmEvent list): Result<FsmState, string> =
        match events with
        | [] -> Ok initialState
        | _ -> calculateState initialState events

