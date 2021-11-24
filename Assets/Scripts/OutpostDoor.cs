using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutpostDoor : MonoBehaviour
{
    private Animation anim;
    public Text hintText;
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            int points = other.GetComponent<Player>().points;
            if(points >= 8 && !isOpen)
            {
                anim.Play("Opening");
                isOpen = true;
            }
            else
            {
                hintText.gameObject.SetActive(true);
                hintText.text = "Voce possui "+points+"/8 livros. Colete todos para abrir a porta.";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            hintText.gameObject.SetActive(false);
        }

    }
}
