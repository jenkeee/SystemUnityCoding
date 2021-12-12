using Mirror;
using UnityEngine;

public class MyPlayerController : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Camera.main.orthographic = false;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0f, 4f, -8f);
        Camera.main.transform.localEulerAngles = new Vector3(20f, 0f, 0f);
    }

    void OnDisable()
    {
        if (isLocalPlayer && Camera.main != null)
        {
            Camera.main.orthographic = true;
            Camera.main.transform.SetParent(null);
            Camera.main.transform.localPosition = new Vector3(0f, 70f, 0f);
            Camera.main.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
    }

    private void Update()
    {
        
    }
}
