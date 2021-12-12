using Mirror;
using UnityEngine;

public class RandomColorAndElse : NetworkBehaviour
{
        public override void OnStartServer() // эта команда типа Start но на сервере, выполниться при подключение нового клиента
        {
            base.OnStartServer();
            color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        // Color32 packs to 4 bytes
        [SyncVar(hook = nameof(SetColor))] // SyncVar обозначил что что переменная  Color32 color синхронизирует значения
                                           // методом SetColor. выполняет его каждый update?
                                           // каждый клиент получит одно и тоже значение с сервера.                                         
    public Color32 color = Color.black;

        // Unity clones the material when GetComponent<Renderer>().material is called
        // Cache it here and destroy it in OnDestroy to prevent a memory leak
        Material cachedMaterial;

        void SetColor(Color32 _, Color32 newColor)
        {
            if (cachedMaterial == null) cachedMaterial = GetComponentInChildren<Renderer>().material;
            cachedMaterial.color = newColor;
        }

        void OnDestroy()
        {
            Destroy(cachedMaterial);
        }
    }

