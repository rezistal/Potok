using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onOff : MonoBehaviour {

    public Main main;
    public GameObject buttonOn;
    public GameObject buttonOff;
    public GameObject plug;
    public bool trigger;
    private bool stateTimerActive;
    public float stateDelay = 0.0f;

    // Use this for initialization
    void Start () {
        trigger = false;
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
    }
    void OnMouseEnter()
    {
        main.popUP.text = "Кнопка включения/выключения";
    }

    void OnMouseExit()
    {
        main.popUP.text = "";
    }

    void OnMouseDown()
    {
        if (main.isGame == 0)
        {
            if (plug.GetComponent<FixController>().isFixed == true)
            {
                if (trigger == false)
                {
                    trigger = true;
                }
                else
                {
                    trigger = false;
                }
            }
        }
        else
        {
            if (plug.GetComponent<FixController>().isFixed == true && trigger == false)
            {
                trigger = true;
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("Hose"));
                main.gameText.text = "Это было легко! Теперь присоедините дыхательный контур";
            }
        }
    }
    public void TurnOnce()
    {
        stateTimerActive = true;
    }
    public void NextStep()
    {
        main.gameText.text = "Теперь присоединияем дыхательный контур";
        stateTimerActive = false;
        main.nextItem = GameObject.FindWithTag("Hose");
        main.targetsArray.Clear();
        main.targetsArray.Add(GameObject.FindWithTag("Hose"));
        main.nextItem.GetComponent<Animation>().Play(main.nextItem.tag);
    }
    // Update is called once per frame
    void Update () {

        if (stateTimerActive)
        {
            stateDelay += Time.deltaTime;
        }

        if (stateDelay > 3.5 && stateTimerActive)
        {
            trigger = true;
            NextStep();
        }

        buttonOn.GetComponent<Renderer>().enabled = trigger;
        buttonOff.GetComponent<Renderer>().enabled = !trigger;
    }
}
