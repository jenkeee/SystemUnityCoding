
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Burst;
using Unity.Jobs;

/*
�������� ������ ���� IJob, ������� ��������� ������ � ������� NativeArray<int> � � ���������� ���������� ��� �������� ����� ������ ������ ������� ����.
�������� ���������� ���� ������ �� �������� ������ � �������� � ������� ���������.
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

       // JobStruct myjob = new JobStruct(); // ���������� ��������


      //  Transform[] transforms = new Transform[CountObj];
        for (int i = 0; i < CountObj; i++)
        {
            NumInObj[i] = Random.Range(4, 16);
            Instantiate(objPrefab, transform);
            transform.GetChild(i).name = "obg " + (i+1);
            Debug.Log("������ ��� �������: " + (i + 1) + " ������� ����� - " +  NumInObj[i]);
        }
        Debug.Log("----------------------------------------------------------------------------------------");

        JobStruct myjob = new JobStruct();
        myjob.Schedule();
        /*
        for (int i = 0; i < CountObj; i++)
        {
            Debug.Log("������ ��� �������: " + (i + 1) + " ������� ����� - " + NumInObj[i]);
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
   