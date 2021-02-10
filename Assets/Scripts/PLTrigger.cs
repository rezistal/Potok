using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLTrigger : MonoBehaviour {

    public Main main;
    public GameObject PL;
    public float vx;
    public float vy;
    public float vz;
    public float stateDelay = 0.0f;

    void Start()
    {
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
        vx = 0.0f;
        vy = 90.0f;
        vz = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        stateDelay = 0;
        PL.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
    }
    private void OnTriggerExit(Collider other)
    {
        stateDelay = 0;
    }
    // Use this for initialization
    void OnTriggerStay(Collider other)
    {
        stateDelay += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && PL.GetComponent("Collider") == other && PL.GetComponent<FixController>().isFixed == false)
        {
            PL.GetComponent<FixController>().isFixed = true;
            PL.transform.position = new Vector3(0, 0, 0);
            PL.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
            PL.GetComponent<Rigidbody>().isKinematic = true;
            PL.GetComponent<Rigidbody>().useGravity = false;

            if (main.isGame == 1)
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("PLCap"));
                main.gameText.text = "Хорошее начало! Теперь отрегулируйте клапан";
            }
        }
        if (stateDelay > 2)
        {
            if (main.isPresentation == 1 && main.nextItem.tag == "PL")
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("PLCap"));
                main.nextItem = GameObject.FindWithTag("PLCap");
                main.gameText.text = "И регулируем ограничитель";
                main.nextItem.GetComponent<Rotate2>().RotateOnce();
            }
        }
    }
}
