using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading; // неймспейс для токена отмены


public class CubeMove : MonoBehaviour
{
    /*
     Если вдруг мы обределим асинк методы как void
    await Task.WhenAll(task1, task2);
    Task.WaitAll(task1, task2);
    Сообщат нам что методы начали свою работу. для того чтобы получить инфо о завершение
    void заменим на Task
     */
    /*
     CancellationTokenПредоставляет возможность совместной отмены для потоков, рабочих элементов пула потоков или Task объектов. 
    Токен отмены создается путем создания экземпляра CancellationTokenSource объекта, который управляет токенами отмены, полученными из его CancellationTokenSource.
    Token Свойства. Затем токен отмены передается в любое количество потоков, задач или операций, которые должны получить уведомление об отмене. 
    Токен нельзя использовать для инициации отмены. Когда объект-владелец вызывает CancellationTokenSource.Cancel , IsCancellationRequested
    свойству каждой копии токена отмены присваивается значение true . Объекты, получающие уведомление, могут отвечать любым необходимым способом. */


    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); // создаем новый экземпляр класса 
    void Start()
    {
        UnitTasksAsync();

       // CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); // создаем новый экземпляр класса // вынесем за старт он же классу принадлежит а не методу

    }

    async void UnitTasksAsync()
    {
        Task task1 = Task.Run(() => Unit1Async(cancellationTokenSource.Token));
        Task task2 = Task.Run(() => Unit2Async());
      // Task task1 = Unit1Async(cancellationTokenSource.Token); // при этой записи токен не сработал . почему?

        cancellationTokenSource.Cancel(); // этой строчкой обратимся к заранее оставленному токену
       // Task task2 = Unit2Async();

        await Task.WhenAll(task1, task2);
        Debug.Log("All units have finish their tasks.");

        Task.WaitAll(task1, task2);
        Debug.Log("All units have finish their tasks.");
    }

    async Task Unit1Async(CancellationToken cancellationTokenSource) // тут уже не сорс источник, а просто даем токен CancellationToken
    {
        Debug.Log("Unit1 starts chopping wood.");

        cancellationTokenSource.ThrowIfCancellationRequested(); // если мы в другом участке кода запросили приостановку. он именно тут и будет проверять. всет последовательно

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
