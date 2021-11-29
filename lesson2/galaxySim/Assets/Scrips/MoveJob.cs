using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public struct MoveJob : IJobParallelForTransform
{
    public NativeArray<Vector3> Positions;
    public NativeArray<Quaternion> Rotation;// ��� ����� ������� ����������
    public NativeArray<Vector3> Velocities;
    public NativeArray<Vector3> Accelerations;
    [ReadOnly]
    public float DeltaTime;

    int x;
    public void Execute(int index, TransformAccess transform)
    {
      
        x = x+1;
        Quaternion target = Quaternion.Euler(120, 0, x);// += ���������� �� ������� �����, �� �� ��� ��� �����
       // Debug.Log(x);

        Vector3 velocity = Velocities[index] + Accelerations[index];
        transform.position += velocity * DeltaTime;
        transform.rotation = target; // � ���������� ��� 

        Positions[index] = transform.position;
        Rotation[index] = transform.rotation;// � ������� ��������
        Velocities[index] = velocity;
        Accelerations[index] = Vector3.zero;
    }
}
