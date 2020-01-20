using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTrigger : MonoBehaviour {

    public Main main;
    public GameObject airFilter;
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
        airFilter.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
    }
    private void OnTriggerExit(Collider other)
    {
        stateDelay = 0;
    }
    void OnTriggerStay (Collider other) {
        stateDelay += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && airFilter.GetComponent("Collider") == other && airFilter.GetComponent<FixController>().isFixed == false)
        {
            airFilter.GetComponent<FixController>().isFixed = true;
            airFilter.transform.position = new Vector3(-6.1f, 4.4f, -7.43f);
            airFilter.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
            airFilter.GetComponent<Rigidbody>().isKinematic = true;
            airFilter.GetComponent<Rigidbody>().useGravity = false;

            if (main.isGame == 1)
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("Plug"));
                main.gameText.text = "Хорошо. Теперь нужно подключить шнур питания";
            }
        }
        if (stateDelay > 2)
        {
            if (main.isPresentation == 1 && main.nextItem.tag == "AirFilter")
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("Plug"));
                main.nextItem = GameObject.FindWithTag("Plug");
                main.gameText.text = "Затем подключаем шнур питания";
                main.nextItem.GetComponent<Animation>().Play(main.nextItem.tag);
            }
        }
    }
}
