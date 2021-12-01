using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class Galaxy : MonoBehaviour
{
    [SerializeField]
    private int numberOfEntities;
    [SerializeField]
    private float startDistance;
    [SerializeField]
    private float startVelocity;
    [SerializeField]
    private float startMass;
    [SerializeField]
    private float gravitationModifier;

    [SerializeField]
    private GameObject celestialBodyPrefab;


    private NativeArray<Vector3> positions;

    [SerializeField] // в редактор не вывести
    public NativeArray<float> Zaxis; // dz3 для вращения
    [SerializeField]
    public float ZaxisUE;

    float Xaxis = 0;
    float Yaxis = 0;
   // float Zaxis = 0;

    private NativeArray<Vector3> velocities;
    private NativeArray<Vector3> accelerations;
    private NativeArray<float> masses;

    private TransformAccessArray transformAccessArray;


    private void Start()
    {
        positions = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
        Zaxis = new NativeArray<float>(numberOfEntities, Allocator.Persistent);
        // rotation = new NativeArray<Quaternion>(numberOfEntities, Allocator.Persistent);// создадим масив данных вращения , с алокатором безсрочным        
        velocities = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
        accelerations = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
        masses = new NativeArray<float>(numberOfEntities, Allocator.Persistent);

        Transform[] transforms = new Transform[numberOfEntities];
        for (int i = 0; i < numberOfEntities; i++)
        {
            positions[i] = Random.insideUnitSphere * Random.Range(0, startDistance);
            velocities[i] = Random.insideUnitSphere * Random.Range(0, startVelocity);
            Zaxis[i] = 10f;
            accelerations[i] = Vector3.zero;
            masses[i] = Random.Range(1, startMass);

            transforms[i] = Instantiate(celestialBodyPrefab, positions[i], Quaternion.identity, transform).transform;
        }
        transformAccessArray = new TransformAccessArray(transforms);

        // в старте не дал вращения
    }

    private void Update()
    {
        //Zaxis[0] = ZaxisUE;

        GravitationJob gravitationJob = new GravitationJob()
        {
            Positions = positions,
            Velocities = velocities,
            Accelerations = accelerations,
            Masses = masses,
            GravitationModifier = gravitationModifier,
            DeltaTime = Time.deltaTime
        };
        JobHandle gravitationHandle = gravitationJob.Schedule(numberOfEntities, 0);

        MoveJob moveJob = new MoveJob()
        {
            Positions = positions,
            Velocities = velocities,
            Accelerations = accelerations,
            ZaxisInJob = Zaxis,
           // Rotation = rotation, // вот куда нам без апдейта, скажем пусть обратит внимание на переменную которая и там и там

            DeltaTime = Time.deltaTime
        };


       JobHandle moveHandle = moveJob.Schedule(transformAccessArray, gravitationHandle);
       moveHandle.Complete();
        
        //Zaxis += 1;
       /* for (int i = 0; i < transform.childCount; i++)
        {
            int x = 1;
            x = x + 1;
            Quaternion target = Quaternion.Euler(0, 0, x);

            transform.GetChild(i).rotation = Quaternion.Euler(Xaxis, Yaxis, Zaxis);
        }*/
    }

    private void OnDestroy()
    {
        positions.Dispose();
        Zaxis.Dispose();
      //  rotation.Dispose();// высвободим память занятую этим вот 
        velocities.Dispose();
        accelerations.Dispose();
        masses.Dispose();
        transformAccessArray.Dispose();
    }
}
