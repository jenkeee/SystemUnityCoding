using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// https://mirror-networking.gitbook.io/docs/guides/communications/remote-actions
public class Player : NetworkBehaviour
{
    public int Health;
    Transform Lives;

    Transform _camera;
    [SerializeField]
    private Vector3  cameraPositin;
    private Vector2 mouseTurn;

    int speed;
    [SerializeField]
    public GameObject _bullet;

    private void Start()
    {
        _camera = FindObjectOfType<CameraFolowScript>().transform;
        cameraPositin = new Vector3(0, 2, -5);
        speed = 15;
        Health = 4;
        Lives = transform.GetChild(2);
    }
    void SomeMovement()
    {
        if (isLocalPlayer)
        {
            _camera.position = transform.GetChild(0).transform.position;
            _camera.transform.LookAt(transform);

            mouseTurn.x += Input.GetAxis("Mouse X");
            mouseTurn.y += Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(-mouseTurn.y, mouseTurn.x, 0);


            float moveForward = Input.GetAxis("Horizontal");
            float moveSide = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveForward, 0, moveSide);
            /*зависимость от камеры */
            movement = movement.x * Camera.main.transform.right + movement.y * Camera.main.transform.forward + movement.z * Camera.main.transform.forward;
            movement.Set(movement.x, movement.y, movement.z);
            /*зависимость от камеры*/

            transform.position += movement * Time.deltaTime * speed;
        }
    }
    void Fire()
    {
        Transform gun = transform.GetChild(1);
        var mybullet = Instantiate(_bullet, gun.position + Camera.main.transform.forward*3+Camera.main.transform.right, Quaternion.identity);
       // mybullet.tag = connectionToClient.ToString();
       // NetworkConnection.LocalConnectionId;
       // Debug.Log(connectionToServer);
        var mybulletRB = mybullet.AddComponent<Rigidbody>();
        mybulletRB.rotation = Quaternion.Euler(0,0,90);
        mybulletRB.AddForce(Camera.main.transform.forward * 1000 + Camera.main.transform.up*1000, ForceMode.Force);
        Destroy(mybullet, 6f);
    }
    // Учим общаться клиент с сервером посредстовом жизни ---------------------------------
    [SyncVar(hook = nameof(SyncHealth))] //задаем метод, который будет выполняться при синхронизации переменной
    int _SyncHealth;

    //метод не выполнится, если старое значение равно новому
    void SyncHealth(int oldValue, int newValue) //обязательно делаем два значения - старое и новое. 
    {
        Health = newValue;
    }
    [Server] //обозначаем, что этот метод будет вызываться и выполняться только на сервере
    public void ChangeHealthValue(int newValue)
    {
        _SyncHealth = newValue;
    }
    //   [Command] //обозначаем, что этот метод должен будет выполняться на сервере по запросу клиента // уберем нужду в асорити это дырка // так нельзя
    [Command(requiresAuthority = false)]
     public void CmdChangeHealth(int newValue) //обязательно ставим Cmd в начале названия метода
    {        
        ChangeHealthValue(newValue); //переходим к непосредственному изменению переменной
    }
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
       
               for (int i=0; i < Lives.childCount; i++)
        {// крутая строчка 
            bool isAlive = !(Health - 1 < i);

                Lives.GetChild(i).gameObject.SetActive(isAlive); // бновление объектов-жизней в соответствии с количеством жизней:
        }
        

        if (Input.GetKeyDown(KeyCode.Mouse1) && isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && isLocalPlayer)
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        SomeMovement();

        if (hasAuthority) //проверяем, есть ли у нас права изменять этот объект
        {

            if (Input.GetKeyDown(KeyCode.H)) //отнимаем у себя жизнь по нажатию клавиши H
            {
                if (isServer) //если мы являемся сервером, то переходим к непосредственному изменению переменной
                {
                    ChangeHealthValue(Health - 1);
                    Debug.Log(Health +"");
                }
                else
                {
                    CmdChangeHealth(Health - 1); //в противном случае делаем на сервер запрос об изменении переменной
                    Debug.Log(Health +"");
                }
            }
        }
        
    }
    //[ClientRpc]
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null)
        {
            Debug.Log("haha");
            if (isServer) //если мы являемся сервером, то переходим к непосредственному изменению переменной
            {
                ChangeHealthValue(Health - 1);
                Debug.Log(Health);
            }
            else
            {
                CmdChangeHealth(Health - 1); //в противном случае делаем на сервер запрос об изменении переменной
                Debug.Log(Health);
            }
        }
    }


}
