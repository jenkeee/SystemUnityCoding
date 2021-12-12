using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    //������ ������� � ������� HUD � ��������� UI ����� � NetworkManager

    public string PlayerName { get; set; } // ���� �� ������� �������� �� ����� field

    [Header("Chat GUI")] // �������� ����� � �������
    public ChatWindow chatWindow;

    [SerializeField]
    Transform spawnList;


    public void SetHostname(string hostname)
    {
        networkAddress = hostname;
    }


    public struct CreatePlayerMessage : NetworkMessage // ������� ���������. ����� ������ ��� �� ����������
    {
        public string name;
    }

    public override void OnStartServer() // ������������� ����� OnStartServer
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect(NetworkConnection conn) // ������������� ����� OnClientConnect
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
       // playergo.GetComponentInChildren<TextMeshPro>().text = createPlayerMessage.name; //��������� ������ ����


        // set it as the player
       NetworkServer.AddPlayerForConnection(connection, playergo); // ������� ����� AddPlayerForConnection

        chatWindow.gameObject.SetActive(true);
    }
}
