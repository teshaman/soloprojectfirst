using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TicketSystem : MonoBehaviour
{
    [SerializeField]
    TMP_Text szTicketCount;
    public int nTicketCount;

    private void Start()
    {
        //szTicketCount.text = nTicketCount.ToString(); // TODO: UI
    }
    public void IncrementTicketCount(int amount)
    {
        nTicketCount = nTicketCount + amount;
        //szTicketCount.text = nTicketCount.ToString(); // TODO: UI
    }
    public void DecrementTicketCount(int amount)
    {
        nTicketCount = nTicketCount - amount;
        if(nTicketCount <= 0)
        {
            nTicketCount = 0;
        }
        szTicketCount.text = nTicketCount.ToString();
    }
}
