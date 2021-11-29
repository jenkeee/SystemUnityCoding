using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public struct MoveJob : IJobParallelForTransform
{
    public NativeArray<Vector3> Positions;
    public NativeArray<Quaternion> Rotation;// тут также заведем переменную
    public NativeArray<Vector3> Velocities;
    public NativeArray<Vector3> Accelerations;
    [ReadOnly]
    public float DeltaTime;

    int x;
    public void Execute(int index, TransformAccess transform)
    {
      
        x = x+1;
        Quaternion target = Quaternion.Euler(120, 0, x);// += кватарнион не захотел брать, ну мы ему так тогда
       // Debug.Log(x);

        Vector3 velocity = Velocities[index] + Accelerations[index];
        transform.position += velocity * DeltaTime;
        transform.rotation = target; // и собственно вот 

        Positions[index] = transform.position;
        Rotation[index] = transform.rotation;// и каждому элементу
        Velocities[index] = velocity;
        Accelerations[index] = Vector3.zero;
    }
}
