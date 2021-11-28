using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading; 


public class lesson1dz3 : MonoBehaviour
{
    /*
    Задание 3 (дополнительное).
Реализовать задачу WhatTaskFasterAsync, которая будет принимать в качестве параметров CancellationToken, 
    а также две задачи в виде переменных типа Task. Задача должна ожидать выполнения хотя бы одной из задач, 
    останавливать другую и возвращать результат. Если первая задача выполнена первой, вернуть true, если вторая — false. Если сработал CancellationToken, 
    также вернуть false. Проверить работоспособность с помощью задач из Задания 2. 
     */

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationTokenSource ct = new CancellationTokenSource(); 


    void Start()
    {
       UnitTasksAsync();
       // WhatTaskFasterAsync(ct.Token, Task.Run(() => Unit1Async()), Unit2Async());
    }

    async void UnitTasksAsync()
    {
        Task task1 = Task.Run(() => Unit1Async());
        Task task2 = Task.Run(() => Unit2Async(ct.Token));

       ct.Cancel();
        Task ComplitORNot = await Task.WhenAny(WhenCanceled(ct.Token),task2, task1); // when token

       // ct.Cancel();

        if (ComplitORNot == task1)
            Debug.Log("hello kitty task1 complited");
        else if (ComplitORNot == task2)
            Debug.Log("false or task2 complited");
        else
            Debug.Log("cancel token worked");


            await Task.WhenAll(task1, task2);
            Debug.Log("All units have finish their tasks.");
       Debug.Log("tsk1" + task1.Status+ "tsk2" + task2.Status); 
    }
    public static Task WhenCanceled(CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<bool>();
        cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
        return tcs.Task;
    }

    /*
    public Task<bool> WhatTaskFasterAsync(CancellationToken ct, Task task1, Task task2)
    {

       // await Task.WhenAny(task1, task2);

        return new Task<bool>(() => false);
    }*/

    async Task Unit1Async()
    { 
        Debug.Log("Unit1 starts chopping wood.");  
        await Task.Delay(10000);
        Debug.Log("Unit1 finishes chopping wood.");
    }

    async Task Unit2Async(CancellationToken cat)
    {
        Debug.Log("Unit2 starts patrolling.");
        try
        {
            cat.ThrowIfCancellationRequested();
        
            await Task.Delay(5000);
            Debug.Log("Unit2 finishes patrolling.");
        }
        catch { }
        finally { }
    }



}
