using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading; // ��������� ��� ������ ������


public class CubeMove : MonoBehaviour
{
    /*
     ���� ����� �� ��������� ����� ������ ��� void
    await Task.WhenAll(task1, task2);
    Task.WaitAll(task1, task2);
    ������� ��� ��� ������ ������ ���� ������. ��� ���� ����� �������� ���� � ����������
    void ������� �� Task
     */
    /*
     CancellationToken������������� ����������� ���������� ������ ��� �������, ������� ��������� ���� ������� ��� Task ��������. 
    ����� ������ ��������� ����� �������� ���������� CancellationTokenSource �������, ������� ��������� �������� ������, ����������� �� ��� CancellationTokenSource.
    Token ��������. ����� ����� ������ ���������� � ����� ���������� �������, ����� ��� ��������, ������� ������ �������� ����������� �� ������. 
    ����� ������ ������������ ��� ��������� ������. ����� ������-�������� �������� CancellationTokenSource.Cancel , IsCancellationRequested
    �������� ������ ����� ������ ������ ������������� �������� true . �������, ���������� �����������, ����� �������� ����� ����������� ��������. */


    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); // ������� ����� ��������� ������ 
    void Start()
    {
        UnitTasksAsync();

       // CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); // ������� ����� ��������� ������ // ������� �� ����� �� �� ������ ����������� � �� ������

    }

    async void UnitTasksAsync()
    {
        Task task1 = Task.Run(() => Unit1Async(cancellationTokenSource.Token));
        Task task2 = Task.Run(() => Unit2Async());
      // Task task1 = Unit1Async(cancellationTokenSource.Token); // ��� ���� ������ ����� �� �������� . ������?

        cancellationTokenSource.Cancel(); // ���� �������� ��������� � ������� ������������ ������
       // Task task2 = Unit2Async();

        await Task.WhenAll(task1, task2);
        Debug.Log("All units have finish their tasks.");

        Task.WaitAll(task1, task2);
        Debug.Log("All units have finish their tasks.");
    }

    async Task Unit1Async(CancellationToken cancellationTokenSource) // ��� ��� �� ���� ��������, � ������ ���� ����� CancellationToken
    {
        Debug.Log("Unit1 starts chopping wood.");

        cancellationTokenSource.ThrowIfCancellationRequested(); // ���� �� � ������ ������� ���� ��������� ������������. �� ������ ��� � ����� ���������. ���� ���������������

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
