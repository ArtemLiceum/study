RD #1
OUT 0x42
OUT 0x37
RD #0x0C
OUT 0x36

RD #0x40
WR R0

RD #0x21
WR R1

RD #0
WR R1

K: SBIS 0x43, 0
JMP K

PRESS:
    SBI 0x36, 7
    IN 0x41
    IN 0x34
    
    WR R2
    RD R1
    SUB 0x20
    WR @R0+
    
    RD R2
    WR R1
    CBI 0x36, 7


JMP K