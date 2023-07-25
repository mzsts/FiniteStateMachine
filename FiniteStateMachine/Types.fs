namespace FiniteStateMachine

type FsmEvent =
    | APP_PASSIVE_OPEN
    | APP_ACTIVE_OPEN
    | APP_SEND
    | APP_CLOSE
    | APP_TIMEOUT
    | ARCV_SYN
    | RCV_ACK
    | RCV_SYN_ACK
    | RCV_FIN
    | RCV_FIN_ACK
        
    static member FromString str =
        match str with
        | "APP_PASSIVE_OPEN" -> Ok APP_PASSIVE_OPEN
        | "APP_ACTIVE_OPEN" -> Ok APP_ACTIVE_OPEN
        | "APP_SEND" -> Ok APP_SEND
        | "APP_CLOSE" -> Ok APP_CLOSE
        | "APP_TIMEOUT" -> Ok APP_TIMEOUT
        | "ARCV_SYN" -> Ok ARCV_SYN
        | "RCV_ACK" -> Ok RCV_ACK
        | "RCV_SYN_ACK" -> Ok RCV_SYN_ACK
        | "RCV_FIN" -> Ok RCV_FIN
        | "RCV_FIN_ACK" -> Ok RCV_FIN_ACK
        | _ -> Error @$"Invalid input. Can not match event '{str}'"

type FsmState =
    | CLOSED
    | SYN_SENT
    | SYN_RCVD
    | ESTABLISHED
    | CLOSE_WAIT
    | LAST_ACK
    | FIN_WAIT_1
    | FIN_WAIT_2
    | CLOSING
    | TIME_WAIT