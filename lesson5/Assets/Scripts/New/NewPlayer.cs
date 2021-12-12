using Mirror;
using System;
using UnityEngine;
using TMPro;

public class NewPlayer : NetworkBehaviour // ��� �������� ������ ��������
{
    [SyncVar]
    public string playerName;


    public static event Action<NewPlayer, string> OnMessage; // ion<NewPlayer � this ��� ����. ����� this ��� action

    [Command]
    public void CmdSend(string message) 
    {
        if (message.Trim() != "")  // ���� �� ����� �� ������ �����
            RpcReceive(message.Trim()); // �� ������ ������� ��� ���� ������ �� �������, �� ��� �� ����� ��������

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
