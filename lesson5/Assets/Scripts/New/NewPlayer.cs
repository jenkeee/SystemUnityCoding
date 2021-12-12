using Mirror;
using System;
using UnityEngine;
using TMPro;

public class NewPlayer : NetworkBehaviour // для мультика вместо монобеха
{
    [SyncVar]
    public string playerName;


    public static event Action<NewPlayer, string> OnMessage; // ion<NewPlayer и this это одно. инвок this это action

    [Command]
    public void CmdSend(string message) 
    {
        if (message.Trim() != "")  // если мы ввели не пустую стрку
            RpcReceive(message.Trim()); // не совсем понимаю что трим делает со строкой, но она не особо меняется

    }

    [ClientRpc]
    public void RpcReceive(string message)
    {
        OnMessage?.Invoke(this, message);
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            transform.GetComponentInChildren<TextMeshPro>().text = playerName;
        }
        else
            transform.GetComponentInChildren<TextMeshPro>().text = playerName;
    }

}
