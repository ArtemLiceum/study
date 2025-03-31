RD #0x10; считываем адресс начала первого массива
WR R0; записываем его в R0

RD #5; считываем размер 1 массива
WR R1

RD #256
WR R7

CALL FUNC; вызов подпрограммы
RD R7
WR 0x40

RD #0x20
WR R0

RD #5
WR R1

RD #256
WR R7
CALL FUNC
RD R7
WR 0x41

RD #0x30
WR R0

RD #5
WR R1

RD #256
WR R7
CALL FUNC
RD R7
WR 0x42


RD 0x40
ADD 0x41
ADD 0x42
DIV #3
WR 0x0
HLT


FUNC:
    start:
    RD @R0+; считываем первый элемент массива
    WR 0x0; записываем в R2
    SBC 0x0, 7; если бит равен нулю, то пропускаем следующую команду
    
    JMP compair
    
    DJRNZ R1, start
    finish:
    RET; выход из под программы
    
compair:
    RD 0x0
    CMP R7
    JNN less
    JMP start

    less:
        RD 0x0 ; если меньше
        WR R7
               
        DJRNZ R1, start
    JMP finish

.c 2
.org 0x10
.db 23, 44, 200, 120, 129
.org 0x20
.db 250, 14, 222, 100, 130
.org 0x30
.db 155, 170, 183, 201, 20
