START:
 RD #0
 CMP 0x0
 JC OUT_OF_LOW_RANGE  
 
 RD #18
 WR R0
 RD 0x0
 CMP R0 
 JC OUT_OF_HG_RANGE 
    
 RD 0x0 
 WR R0
 RD #4
 MUL R0
 MUL R0
 WR R0
 
 RD R7
 OR #0
 JNZ OVER_STACK
 
 RD #5
 MUL 0x0
 WR R1
 
 RD R0
 SUB R1
 
 ADD #3
 WR 0x1
      
 RD #0 
 WR 0x2
 JMP END
 
OUT_OF_LOW_RANGE: 
    RD #1
    WR 0x2
    JMP END
     
OUT_OF_HG_RANGE:  
    RD #2
    WR 0x2
    JMP END

OVER_STACK:
    RD #3
    WR 0x2
    JMP END
 
END:
    HLT
