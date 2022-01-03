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

   // [SerializeField] // в редактор не вывести
    //public NativeArray<float> Zaxis; // dz3 для вращения

    //public float ZaxisUE;

    float Xaxis = 1;
    float Yaxis = 0;
    float Zaxis = 0;

    private NativeArray<Vector3> velocities;
    private NativeArray<Vector3> accelerations;
    private NativeArray<float> masses;

    private TransformAccessArray transformAccessArray;







    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    [SerializeField, Range(1, 8)] private int _depth = 2;
    int current_depth = 0;
    [SerializeField, Range(1, 360)] private int _rotationSpeed =100;
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

    private void CreatePart(GameObject obgForParent)
    {     
        for (int i = 0; i < 5; i++)
        {
            var go = new GameObject($"Fractal Path L{obgForParent.gameObject.transform.localScale} C{i}");
            go.transform.SetParent(transform, false);
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = material;
            go.transform.localScale = obgForParent.transform.localScale * 0.5f;
            go.transform.localRotation = _rotations[i];
            go.transform.localPosition += obgForParent.transform.localPosition + obgForParent.transform.TransformDirection( _directions[i]) * obgForParent.transform.localScale.x;
        }        
    }
    private void DoRotationAroundParrent(GameObject obgForRotation,GameObject objForParrent,int dir)
    {/*
        for (int i = 0; i < 5; i++)
        {*/
        //obgForRotation.transform.RotateAround(objForParrent.transform.position, Vector3.up, 20 * Time.deltaTime);

        obgForRotation.transform.RotateAround(objForParrent.transform.position, _directions[dir], _rotationSpeed * Time.deltaTime*10);

    }

    private void Start()
    {
        GameObject _mainObj = new GameObject("Main 0 0");
        _mainObj.transform.SetParent(transform, false);
        _mainObj.AddComponent<MeshFilter>().mesh = mesh;
        _mainObj.AddComponent<MeshRenderer>().material = material;


        int x=0; // в данном случае это количество детей которые наплодит факториал
        for (int currentDepth = 0; currentDepth < _depth ; currentDepth++ )
        {
            x += Mathf.RoundToInt(Mathf.Pow(5, currentDepth));
        }

        for (int li = 0; li < x - Mathf.Pow(5, _depth-1); li++)
        {
            CreatePart(transform.GetChild(li).gameObject);
        }
        Debug.Log($"объектов должно быть: " + x);
        Debug.Log($"детей есть: " +transform.childCount);

        /*
        CreatePart(_mainObj, 1);
        CreatePart(transform.GetChild(1).gameObject, 2);
        CreatePart(transform.GetChild(2).gameObject, 2);
        CreatePart(transform.GetChild(3).gameObject, 2);
        CreatePart(transform.GetChild(4).gameObject, 2);
        CreatePart(transform.GetChild(5).gameObject, 2);
        CreatePart(transform.GetChild(6).gameObject, 3);*/
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
    public float test; 
    private void Update()
    {/*
        int currentObg = 1;
        int x = 0;
        for (int li = 0; li < 6; li++)
        {            
            for (int i = 0; i < 5; i++)
            {
                DoRotationAroundParrent(transform.GetChild(currentObg).gameObject, transform.GetChild(li).gameObject);
                currentObg += 1;
            }
            x++;
        }*//*
        DoRotationAroundParrent(transform.GetChild(1).gameObject, transform.GetChild(0).gameObject, 0);
        DoRotationAroundParrent(transform.GetChild(2).gameObject, transform.GetChild(0).gameObject,0 );
        DoRotationAroundParrent(transform.GetChild(3).gameObject, transform.GetChild(0).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(4).gameObject, transform.GetChild(0).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(5).gameObject, transform.GetChild(0).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(6).gameObject, transform.GetChild(1).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(7).gameObject, transform.GetChild(1).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(8).gameObject, transform.GetChild(1).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(9).gameObject, transform.GetChild(1).gameObject,0);
        DoRotationAroundParrent(transform.GetChild(10).gameObject, transform.GetChild(1).gameObject,0);*/
    /*    var deltaRotation = Quaternion.Euler(0f, _rotationSpeed * Time.deltaTime, 0f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation *= deltaRotation;
        }*/

        transform.GetChild(1).localPosition = transform.GetChild(0).localPosition + transform.GetChild(0).TransformDirection(_directions[0] * transform.GetChild(0).transform.localScale.x);


       // transform.GetChild(2).rotation = Quaternion.Euler(transform.GetChild(2).eulerAngles.x, transform.GetChild(0).eulerAngles.y, transform.GetChild(2).eulerAngles.z).normalized;
        transform.GetChild(2).localPosition = transform.GetChild(0).localPosition + transform.GetChild(0).TransformDirection(_directions[1] * transform.GetChild(0).transform.localScale.x);       
        transform.GetChild(3).localPosition = transform.GetChild(0).localPosition + transform.GetChild(0).TransformDirection(_directions[2] * transform.GetChild(0).transform.localScale.x);
        transform.GetChild(4).localPosition = transform.GetChild(0).localPosition + transform.GetChild(0).TransformDirection(_directions[3] * transform.GetChild(0).transform.localScale.x);
        transform.GetChild(5).localPosition = transform.GetChild(0).localPosition + transform.GetChild(0).TransformDirection(_directions[4] * transform.GetChild(0).transform.localScale.x);


        transform.GetChild(6).localPosition = transform.GetChild(1).localPosition + transform.GetChild(1).TransformDirection(_directions[0] * transform.GetChild(1).transform.localScale.x);
        transform.GetChild(7).localPosition = transform.GetChild(1).localPosition + transform.GetChild(1).TransformDirection(_directions[1] * transform.GetChild(1).transform.localScale.x);
        transform.GetChild(8).localPosition = transform.GetChild(1).localPosition + transform.GetChild(1).TransformDirection(_directions[2] * transform.GetChild(1).transform.localScale.x);
        transform.GetChild(9).localPosition = transform.GetChild(1).localPosition + transform.GetChild(1).TransformDirection(_directions[3] * transform.GetChild(1).transform.localScale.x);
        transform.GetChild(10).localPosition = transform.GetChild(1).localPosition + transform.GetChild(1).TransformDirection(_directions[4] * transform.GetChild(1).transform.localScale.x);
        
        
        transform.GetChild(11).localPosition = transform.GetChild(2).localPosition + transform.GetChild(2).TransformDirection(_directions[0] * transform.GetChild(2).transform.localScale.x);
        transform.GetChild(12).localPosition = transform.GetChild(2).localPosition + transform.GetChild(2).TransformDirection(_directions[1] * transform.GetChild(2).transform.localScale.x);
        transform.GetChild(13).localPosition = transform.GetChild(2).localPosition + transform.GetChild(2).TransformDirection(_directions[2] * transform.GetChild(2).transform.localScale.x);
        transform.GetChild(14).localPosition = transform.GetChild(2).localPosition + transform.GetChild(2).TransformDirection(_directions[3] * transform.GetChild(2).transform.localScale.x);
        transform.GetChild(15).localPosition = transform.GetChild(2).localPosition + transform.GetChild(2).TransformDirection(_directions[4] * transform.GetChild(2).transform.localScale.x);
        
        
        transform.GetChild(16).localPosition = transform.GetChild(3).localPosition + transform.GetChild(2).TransformDirection(_directions[0] * transform.GetChild(3).transform.localScale.x);
        transform.GetChild(17).localPosition = transform.GetChild(3).localPosition + transform.GetChild(2).TransformDirection(_directions[1] * transform.GetChild(3).transform.localScale.x);
        transform.GetChild(18).localPosition = transform.GetChild(3).localPosition + transform.GetChild(2).TransformDirection(_directions[2] * transform.GetChild(3).transform.localScale.x);
        transform.GetChild(19).localPosition = transform.GetChild(3).localPosition + transform.GetChild(2).TransformDirection(_directions[3] * transform.GetChild(3).transform.localScale.x);
        transform.GetChild(20).localPosition = transform.GetChild(3).localPosition + transform.GetChild(2).TransformDirection(_directions[4] * transform.GetChild(3).transform.localScale.x);

        transform.GetChild(21).localPosition = transform.GetChild(4).localPosition + transform.GetChild(2).TransformDirection(_directions[0] * transform.GetChild(4).transform.localScale.x);
        transform.GetChild(22).localPosition = transform.GetChild(4).localPosition + transform.GetChild(2).TransformDirection(_directions[1] * transform.GetChild(4).transform.localScale.x);
        transform.GetChild(23).localPosition = transform.GetChild(4).localPosition + transform.GetChild(2).TransformDirection(_directions[2] * transform.GetChild(4).transform.localScale.x);
        transform.GetChild(24).localPosition = transform.GetChild(4).localPosition + transform.GetChild(2).TransformDirection(_directions[3] * transform.GetChild(4).transform.localScale.x);
        transform.GetChild(25).localPosition = transform.GetChild(4).localPosition + transform.GetChild(2).TransformDirection(_directions[4] * transform.GetChild(4).transform.localScale.x);

        transform.GetChild(26).localPosition = transform.GetChild(5).localPosition + transform.GetChild(2).TransformDirection(_directions[0] * transform.GetChild(5).transform.localScale.x);
        transform.GetChild(27).localPosition = transform.GetChild(5).localPosition + transform.GetChild(2).TransformDirection(_directions[1] * transform.GetChild(5).transform.localScale.x);
        transform.GetChild(28).localPosition = transform.GetChild(5).localPosition + transform.GetChild(2).TransformDirection(_directions[2] * transform.GetChild(5).transform.localScale.x);
        transform.GetChild(29).localPosition = transform.GetChild(5).localPosition + transform.GetChild(2).TransformDirection(_directions[3] * transform.GetChild(5).transform.localScale.x);
        transform.GetChild(30).localPosition = transform.GetChild(5).localPosition + transform.GetChild(2).TransformDirection(_directions[4] * transform.GetChild(5).transform.localScale.x);



        // transform.GetChild(12).localPosition - transform.GetChild(2).localPosition;
        //  Quaternion.LookRotation(transform.GetChild(12).position, transform.GetChild(3).position);
        /*  transform.GetChild(12).rotation = Quaternion.Slerp(transform.GetChild(12).rotation, Quaternion.LookRotation(transform.GetChild(3).position - transform.GetChild(12).position), _rotationSpeed * Time.deltaTime);
          transform.GetChild(12).Translate(new Vector3(0, 0, Time.deltaTime * _rotationSpeed));*
        //  transform.GetChild(12).rotation *= Quaternion.Euler(0f,_rotationSpeed*Time.deltaTime,0f);
        //transform.GetChild(12).localRotation = transform.GetChild(2).localRotation * transform.GetChild(12).rotation;
        //  transform.GetChild(12).localPosition = transform.GetChild(2).localPosition;
       GameObject rotat = transform.GetChild(12).gameObject;
        GameObject rotatParent = transform.GetChild(2).gameObject;
        rotat.transform.LookAt(rotatParent.transform);
        rotat.transform.TransformPoint(rotatParent.transform.position);
//        rotat.transform.localPosition = rotatParent.transform.position + rotatParent.transform.eulerAngles;*/

        // obgForParent.transform.localPosition + obgForParent.transform.TransformDirection(_directions[i]) * obgForParent.transform.localScale.x;
        //    transform.GetChild(12).RotateAround(transform.GetChild(2).transform.localPosition, _directions[1], _rotationSpeed * Time.deltaTime * 10);


        /*  
          DoRotationAroundParrent(transform.GetChild(11).gameObject, transform.GetChild(3).gameObject,1); // _directions right [1]
          DoRotationAroundParrent(transform.GetChild(12).gameObject, transform.GetChild(3).gameObject,1);
          DoRotationAroundParrent(transform.GetChild(13).gameObject, transform.GetChild(3).gameObject,1);
          DoRotationAroundParrent(transform.GetChild(14).gameObject, transform.GetChild(3).gameObject,1);
          DoRotationAroundParrent(transform.GetChild(15).gameObject, transform.GetChild(3).gameObject,1);
          DoRotationAroundParrent(transform.GetChild(16).gameObject, transform.GetChild(3).gameObject,2);
          DoRotationAroundParrent(transform.GetChild(17).gameObject, transform.GetChild(3).gameObject,2);
          DoRotationAroundParrent(transform.GetChild(18).gameObject, transform.GetChild(3).gameObject,2);
          DoRotationAroundParrent(transform.GetChild(19).gameObject, transform.GetChild(3).gameObject,2);
          DoRotationAroundParrent(transform.GetChild(20).gameObject, transform.GetChild(3).gameObject,2);
          DoRotationAroundParrent(transform.GetChild(21).gameObject, transform.GetChild(4).gameObject,3);
          DoRotationAroundParrent(transform.GetChild(22).gameObject, transform.GetChild(4).gameObject,3);
          DoRotationAroundParrent(transform.GetChild(23).gameObject, transform.GetChild(4).gameObject,3);
          DoRotationAroundParrent(transform.GetChild(24).gameObject, transform.GetChild(4).gameObject,3);
          DoRotationAroundParrent(transform.GetChild(25).gameObject, transform.GetChild(4).gameObject,3);
          DoRotationAroundParrent(transform.GetChild(26).gameObject, transform.GetChild(5).gameObject,4);
          DoRotationAroundParrent(transform.GetChild(27).gameObject, transform.GetChild(5).gameObject,4);
          DoRotationAroundParrent(transform.GetChild(28).gameObject, transform.GetChild(5).gameObject,4);
          DoRotationAroundParrent(transform.GetChild(29).gameObject, transform.GetChild(5).gameObject,4);
          DoRotationAroundParrent(transform.GetChild(30).gameObject, transform.GetChild(5).gameObject,4);*/




        /*
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
