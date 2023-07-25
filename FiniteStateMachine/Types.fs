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