using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugTrigger : MonoBehaviour {

    public Main main;
    public GameObject plug;
    public float vx;
    public float vy;
    public float vz;
    public float stateDelay = 0.0f;

    void Start()
    {
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
        vx = 0.0f;
        vy = 0.0f;
        vz = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        stateDelay = 0;
        plug.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
    }
    private void OnTriggerExit(Collider other)
    {
        stateDelay = 0;
    }

    // Use this for initialization
    void OnTriggerStay(Collider other)
    {
        stateDelay += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && plug.GetComponent("Collider") == other && plug.GetComponent<FixController>().isFixed == false)
        {
            plug.GetComponent<FixController>().isFixed = true;
            plug.transform.position = new Vector3(-8.34f, 3.4f, 20.48f);
            plug.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
            plug.GetComponent<Rigidbody>().isKinematic = true;
            plug.GetComponent<Rigidbody>().useGravity = false;

            if (main.isGame == 1)
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("OffButton")); // подсвечиваем меш, хотя пользователь вызывает срабатывание триггера другого компонента - onoffController
                main.gameText.text = "Далее нажмите кнопку включения";
            }
        }
        if (stateDelay > 2)
        {
            if (main.isPresentation == 1 && main.nextItem.tag == "Plug")
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("OffButton")); // подсвечивает меш кнопки
                main.nextItem = GameObject.FindWithTag("onoffController");  // вызывает переключение кнопки
                main.gameText.text = "И нажимаем кнопку включения";
                main.nextItem.GetComponent<onOff>().TurnOnce();
            }
        }
    }
}
