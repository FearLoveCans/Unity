using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardButton : MonoBehaviour
{
    public Toggle toggle;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle == null) return;
        if(toggle.group == null)
        {
            toggle.group = transform.parent.gameObject.GetComponent<ToggleGroup>();
        }
    }

    public void Sei_toggle(bool Selete)
    {
        if(Selete)
        {

        }
        else
        {
            gameObject.transform.DOPlayBackwards();
        }
    }
}
