using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public struct SecondJob : IJobParallelForTransform
{
    public NativeArray<Vector3> Positions;
    // public NativeArray<Quaternion> Rotation;// ��� ����� ������� ����������
    public NativeArray<Vector3> Velocities;
    public NativeArray<Vector3> Accelerations;
    [ReadOnly]
    public float DeltaTime;

    public NativeArray<float> ZaxisInJob;


    public void Execute(int index, TransformAccess transform)
    {
        int a = 0;
        Debug.Log(a += 1);
        Vector3 velocity = Velocities[index] + Accelerations[index];
        // Quaternion rotate = Quaternion.Euler(1, 2, ZaxisInJob[index]);
        ZaxisInJob[index] += 3f * DeltaTime;
        transform.position += velocity * DeltaTime;
        transform.rotation = Quaternion.identity * Quaternion.Euler(1, 2, ZaxisInJob[index]); // � ���������� ��� 
                                                                                              // ZaxisInJob[index] += 1;

        Positions[index] = transform.position;
        //ZaxisInJob[index] = transform.rotation;
        // Rotation[index] = transform.rotation;// � ������� ��������
        Velocities[index] = velocity;
        Accelerations[index] = Vector3.zero;
    }
}
