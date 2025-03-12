
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayCard : MonoBehaviour
{
    public List<Card> displayCard = new List<Card>();

    public string name;
    public string type;

    // Update is called once per frame
    void Update()
    {
        name = displayCard[0].name;
        type = displayCard[0].type; 
    }
}
