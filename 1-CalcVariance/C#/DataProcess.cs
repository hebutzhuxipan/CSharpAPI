/* 一、类调用，用于多帧二维数组单点的方差计算*/
public class DataProcess
{
    static int[,] cnt;
    static float[,] Var;
    static float[,] Esp;

    public DataProcess(int row,int col)
    {
        cnt = new int[row,col];
        Var = new float[row,col];
        Esp = new float[row,col];
    }

    public float GetVariance(Int16 value,int curR,int curC)
    {
        float TempValue = 0;

        cnt[curR,curC] = cnt[curR,curC] + 1;
        if(cnt[curR,curC] == 1)
        {
	        Var[curR,curC] = 0;
	        Esp[curR,curC] = value;
	        return Var[curR,curC];
        }
        TempValue = value - Esp[curR,curC];
        Esp[curR,curC] = (value + Esp[curR,curC]*(cnt[curR,curC] - 1))/cnt[curR,curC]; 
        Var[curR,curC] = Var[curR,curC] + TempValue*(value - Esp[curR,curC]);
        return (Var[curR,curC]/(cnt[curR,curC]-1));
    }
}

void Main()
{
	DataProcess dataProcess = null;
	dataProcess = new DataProcess(debugFrame.chip.GetChipPhysicalRowNum(), debugFrame.chip.GetChipPhysicalColNum());
	for (int i = 0; i < iPhysicalRowNum; i++)
	{
	    for (int j = 0; j < iPhysicalColNum; j++)
	    {
	        m_SampleVar[i, j] = dataProcess.GetVariance(rawData[i, j], i, j);
	    }
	}
}

/*二、C语言版本*/
/*https://blog.csdn.net/chenwei2002/article/details/51086317*/
double GetVariance(uint64_t value)
{
	static uint8_t cnt = 0;
	static double Var = 0;
	static double Esp = 0;
	double TempValue = 0;
	
	cnt = cnt + 1;
	if(cnt == 1)
	{
		Var = 0;
		Esp = value;
		return Var;
	}
	TempValue = value - Esp;
	Esp = (value + Esp*(cnt - 1))/cnt; 
	Var = Var + TempValue*(value - Esp);
	return (Var/cnt);
}