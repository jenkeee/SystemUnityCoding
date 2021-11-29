using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct JobStruct : IJob
{
    [ReadOnly]
    public NativeArray<int> NumInObj;

    public void Execute() 
    {
        for (int i = 0; i < NumInObj.Length; i++)
            if (NumInObj[i] > 10)
                NumInObj[i] = 0;
    }
}
