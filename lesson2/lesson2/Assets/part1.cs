
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Burst;
using Unity.Jobs;

/*
Создайте задачу типа IJob, которая принимает данные в формате NativeArray<int> и в результате выполнения все значения более десяти делает равными нулю.
Вызовите выполнение этой задачи из внешнего метода и выведите в консоль результат.
*/

public class part1 : MonoBehaviour
{

    [SerializeField]
    private int CountObj;
    [SerializeField]
    private GameObject objPrefab;


    private NativeArray<int> NumInObj;

   // private TransformAccessArray transformAccessArray;

    void Start()
    {
        NumInObj = new NativeArray<int>(CountObj, Allocator.Persistent);

       // JobStruct myjob = new JobStruct(); // определяем стуркуту


      //  Transform[] transforms = new Transform[CountObj];
        for (int i = 0; i < CountObj; i++)
        {
            NumInObj[i] = Random.Range(4, 16);
            Instantiate(objPrefab, transform);
            transform.GetChild(i).name = "obg " + (i+1);
            Debug.Log("Объект под номером: " + (i + 1) + " получил число - " +  NumInObj[i]);
        }
        Debug.Log("----------------------------------------------------------------------------------------");

        JobStruct myjob = new JobStruct();
        myjob.Schedule();
        /*
        for (int i = 0; i < CountObj; i++)
        {
            Debug.Log("Объект под номером: " + (i + 1) + " получил число - " + NumInObj[i]);
        }*/

        //  transformAccessArray = new TransformAccessArray(transforms);       
    }

    // Update is called once per frame
    void Update()
    {/*
        JobStruct myjob = new JobStruct();
        myjob.s;*/
    }
    private void OnDestroy()
    {
        NumInObj.Dispose();
     //   transformAccessArray.Dispose();
    }
}
   