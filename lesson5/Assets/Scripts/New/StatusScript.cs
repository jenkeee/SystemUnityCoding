using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour
{
  

    void Awake()
    {
      
    }
   public void StopButtons()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            FindObjectOfType<CustomNetworkManager>().StopHost();            
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            FindObjectOfType<CustomNetworkManager>().StopClient();            
        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            FindObjectOfType<CustomNetworkManager>().StopServer();           
        }
    }

    void Update()
    {
        if (!NetworkClient.active)
        {
            transform.GetChild(1).GetComponent<Text>().text = $"Соединение false";
           // transform.GetChild(2).GetComponent<Text>().text = $"nothing"; 
        }
        else
        {
         //   transform.GetChild(1).GetComponent<Text>().text = $""; // для нестатического требуется ссылка
         //   transform.GetChild(2).GetComponent<Text>().text = $"nothing";
            // host mode
            // display separately because this always confused people:
            //   Server: ...
            //   Client: ...
            if (NetworkServer.active && NetworkClient.active)
            {
                transform.GetChild(1).GetComponent<Text>().text = $"<b>Host</b>: running via {Transport.activeTransport}";
            }
            // server only
            else if (NetworkServer.active) // апдейта не будет. если только сервер
            {
                transform.GetChild(1).GetComponent<Text>().text = $"<b>Server</b>: running via {Transport.activeTransport}";
            }
            // client only
            else if (NetworkClient.isConnected)
            {
                transform.GetChild(1).GetComponent<Text>().text = $"<b>Client</b>: connected to {FindObjectOfType<CustomNetworkManager>().networkAddress} via {Transport.activeTransport}";
            }          
        }
    }
}
