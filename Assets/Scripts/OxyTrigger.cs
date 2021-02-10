using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyTrigger : MonoBehaviour {

    public Main main;
    public GameObject oxy;
    public float vx;
    public float vy;
    public float vz;
    public float stateDelay = 0.0f;

    void Start()
    {
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
        vx = 180.0f;
        vy = 180.0f;
        vz = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        stateDelay = 0;
        oxy.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
    }
    private void OnTriggerExit(Collider other)
    {
        stateDelay = 0;
    }
    // Use this for initialization
    void OnTriggerStay(Collider other)
    {
        stateDelay += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && oxy.GetComponent("Collider") == other && oxy.GetComponent<FixController>().isFixed == false)
        {
            oxy.GetComponent<FixController>().isFixed = true;
            oxy.transform.position = new Vector3(-2.33f, -3.04f, -14.25f);
            oxy.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
            oxy.GetComponent<Rigidbody>().isKinematic = true;
            oxy.GetComponent<Rigidbody>().useGravity = false;

            if (main.isGame == 1)
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("OxyCap"));
                main.gameText.text = "И открыть подачу кислорода";
            }
        }
        if (stateDelay > 2)
        {
            if (main.isPresentation == 1 && main.nextItem.tag == "Oxy")
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("OxyCap"));
                main.nextItem = GameObject.FindWithTag("OxyCap");
                main.gameText.text = "И открываем подачу кислорода";
                main.nextItem.GetComponent<Rotate2>().RotateOnce();
            }
        }
    }
}
