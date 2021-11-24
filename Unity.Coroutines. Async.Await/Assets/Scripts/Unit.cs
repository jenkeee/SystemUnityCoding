using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    int hp = 1;
    Vector2 hpRectSize;
    public Transform trHPText;
    Coroutine healingHoT;


    /*Задание 1. Применить корутины.
    Дано:
●	Класс Unit, у которого есть переменная health, отвечающая за текущее количество жизней.
●	Метод RecieveHealing(). 

Задача: реализовать корутину, которая будет вызываться из метода RecieveHealing, 
    чтобы юнит получал исцеление 5 жизней каждые полсекунды в течение 3 секунд или до тех пор, 
    пока количество жизней не станет равным 100. На юнит не может действовать более одного эффекта исцеления одновременно.
*/

    bool ActiveHoT;

    private void Start()
    {
        // hpRectSize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        trHPText.GetComponent<Text>().text = hp.ToString();       
    }


    public void HealingTouch()
    {
        if (healingHoT != null && ActiveHoT)
            StopCoroutine(healingHoT);
            healingHoT = StartCoroutine(HealingTouchCorutine());         
    }
    IEnumerator HealingTouchCorutine()
    {
        ActiveHoT = true;
        for (int i = 0; i < 3 / 0.5; i++)
            {
                yield return new WaitForSeconds(0.5f);
                if (hp < 100)
                {
                    hp += 5;
                if (hp >= 100) hp = 100;
                    trHPText.GetComponent<Text>().text = hp.ToString();
                }
           
        }           
        ActiveHoT = false;
    }
    void BarHPcurrent()
    {
        var get = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        get.x = hp * hpRectSize.x / 100;
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = get;
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = hp.ToString();
    }


}
