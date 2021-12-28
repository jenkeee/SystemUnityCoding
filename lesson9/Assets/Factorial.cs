using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class Factorial : MonoBehaviour
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







    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    [SerializeField, Range(1, 8)] private int _depth = 2;
    int current_depth = 0;
    [SerializeField, Range(1, 360)] private int _rotationSpeed;
    private const float _positionOffset = 1.5f;
    private const float _scaleBias = .5f;

    private static readonly Vector3[] _directions = new Vector3[]
    {
        Vector3.up,
        Vector3.left,
        Vector3.right,
        Vector3.forward,
        Vector3.back,
    };
    private static readonly Quaternion[] _rotations = new Quaternion[]
{
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f),
};

    private void CreatePart(GameObject obgForParent, int numOfRepeat)
    {
     
        for (int i = 0; i < 5; i++)
        {
            var go = new GameObject($"Fractal Path L{numOfRepeat} C{i}");
            go.transform.SetParent(transform, false);
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = material;
            go.transform.localScale = obgForParent.transform.localScale * 0.5f;
            go.transform.localRotation = _rotations[i];
            go.transform.localPosition += obgForParent.transform.localPosition + obgForParent.transform.TransformDirection( _directions[i]) ;
        }        
    }

    private void Start()
    {
        GameObject _mainObj = new GameObject("Main");
        _mainObj.transform.SetParent(transform, false);
        _mainObj.AddComponent<MeshFilter>().mesh = mesh;
        _mainObj.AddComponent<MeshRenderer>().material = material;

        //   for (int i = 0; i < _depth; i++)
        CreatePart(_mainObj, 1);

        CreatePart(transform.GetChild(1).gameObject, 2);
        CreatePart(transform.GetChild(2).gameObject, 2);
        CreatePart(transform.GetChild(3).gameObject, 2);
        CreatePart(transform.GetChild(4).gameObject, 2);
        /*
                Transform[][] allItemsMass = new Transform[_depth][];




                positions = new NativeArray<Vector3>(numberOfEntities, Allocator.Persistent);
                Zaxis = new NativeArray<float>(numberOfEntities, Allocator.Persistent);
                // rotation = new NativeArray<Quaternion>(numberOfEntities, Allocator.Persistent);// создадим масив данных вращения , с алокатором безсрочным        

                Transform[][] transforms = new Transform[_depth][];
                for (int i = 0; i < transforms.Length; i++)
                {
                    positions[i] = Random.insideUnitSphere * Random.Range(0, startDistance);
                    velocities[i] = Random.insideUnitSphere * Random.Range(0, startVelocity);
                    Zaxis[i] = 10f;
                    accelerations[i] = Vector3.zero;
                    masses[i] = Random.Range(1, startMass);

                   // transforms[i] = Instantiate(celestialBodyPrefab, positions[i], Quaternion.identity, transform).transform;
                }
              //  transformAccessArray = new TransformAccessArray(transforms);

                // в старте не дал вращения*/
    }

    private void Update()
    {/*
        //Zaxis[0] = ZaxisUE;

        FirstJob gravitationJob = new FirstJob()
        {
            Positions = positions,
            Velocities = velocities,
            Accelerations = accelerations,
            Masses = masses,
            GravitationModifier = gravitationModifier,
            DeltaTime = Time.deltaTime
        };
        JobHandle gravitationHandle = gravitationJob.Schedule(numberOfEntities, 0);

        SecondJob moveJob = new SecondJob()
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
    {/*
        positions.Dispose();
        Zaxis.Dispose();
        //  rotation.Dispose();// высвободим память занятую этим вот 
        velocities.Dispose();
        accelerations.Dispose();
        masses.Dispose();
        transformAccessArray.Dispose();*/
    }
}
