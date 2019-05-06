function [s]  = GetVariance( value )
	persistent cnt;
	persistent Var;
	persistent Esp;
    if isempty(cnt)||isempty(Var)||isempty(Esp)
        cnt = 0;
        Var = 0;
        Esp = 0;
    end
        
	TempValue = 0;
    
	cnt = cnt + 1;
	if cnt == 1
		Var = 0;
		Esp = value;
		s = Var;
    else 
        TempValue = value - Esp;
        Esp = (value + Esp*(cnt - 1))/cnt; 
        Var = Var + TempValue*(value - Esp);
        s = Var/(cnt-1);
    end