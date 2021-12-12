using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    //методы которые я вызываю HUD и кастомным UI лежат в NetworkManager

    public string PlayerName { get; set; } // сюда мы положем значение из инпат field

    [Header("Chat GUI")] // создадим пункт в эдиторе
    public ChatWindow chatWindow;

    [SerializeField]
    Transform spawnList;


    public void SetHostname(string hostname)
    {
        networkAddress = hostname;
    }


    public struct CreatePlayerMessage : NetworkMessage // создаем структуру. чтобы задать имя до соеденения
    {
        public string name;
    }

    public override void OnStartServer() // переопределим метод OnStartServer
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect(NetworkConnection conn) // переопределим метод OnClientConnect
    {
        base.OnClientConnect(conn);

        // tell the server to create a player with this name
        conn.Send(new CreatePlayerMessage { name = PlayerName });
    }

    void GetNonUsedpoint()
    {
        
    }

    void OnCreatePlayer(NetworkConnection connection, CreatePlayerMessage createPlayerMessage)
    {
        // create a gameobject using the name supplied by client
        GameObject playergo = Instantiate(playerPrefab, spawnList.GetChild(Random.Range(0,3)).transform.position, Quaternion.identity ); // 
        playergo.transform.LookAt(spawnList);       
        playergo.name = $"player:{createPlayerMessage.name}withID:{connection}";
        playergo.GetComponent<NewPlayer>().playerName = createPlayerMessage.name;
       // playergo.GetComponentInChildren<TextMeshPro>().text = createPlayerMessage.name; //компонент должен быть


        // set it as the player
       NetworkServer.AddPlayerForConnection(connection, playergo); // заменил метод AddPlayerForConnection

        chatWindow.gameObject.SetActive(true);
    }
}
