using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    /*
     ≈сли вдруг мы обределим асинк методы как void
    await Task.WhenAll(task1, task2);
    Task.WaitAll(task1, task2);
    —ообщат нам что методы начали свою работу. дл€ того чтобы получить инфо о завершение
    void заменим на Task
     */
    void Start()
    {
        UnitTasksAsync();
    }

    async void UnitTasksAsync()
    {
        Task task1 = Task.Run(() => Unit1Async());
        Task task2 = Task.Run(() => Unit2Async());
      // Task task1 = Unit1Async();
       // Task task2 = Unit2Async();

        await Task.WhenAll(task1, task2);
        Debug.Log("All units have finish their tasks.");

        Task.WaitAll(task1, task2);
        Debug.Log("All units have finish their tasks.");
    }

    async Task Unit1Async()
    {
        Debug.Log("Unit1 starts chopping wood.");
        await Task.Delay(1000);
        Debug.Log("Unit1 finishes chopping wood.");
    }

   async Task  Unit2Async()
    {
        Debug.Log("Unit2 starts patrolling.");
        await Task.Delay(60000);
        Debug.Log("Unit2 finishes patrolling.");
    }



}
