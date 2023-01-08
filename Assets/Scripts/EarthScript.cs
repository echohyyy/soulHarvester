using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EarthScript : MonoBehaviour
{

    public TextMeshProUGUI tutorialTMP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            tutorialTMP.text = "Press F";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            tutorialTMP.text = "";
        }
    }
}
