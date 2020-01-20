using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseFilter : MonoBehaviour {

    public Main main;
    public GameObject hoseFilter;
    public float vx;
    public float vy;
    public float vz;
    public float stateDelay = 0.0f;

    void Start()
    {
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
        vx = 0.0f;
        vy = 90.0f;
        vz = 90.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        stateDelay = 0;
        hoseFilter.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
    }
    private void OnTriggerExit(Collider other)
    {
        stateDelay = 0;
    }
    void OnTriggerStay(Collider other)
    {
        stateDelay += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) && hoseFilter.GetComponent("Collider") == other && hoseFilter.GetComponent<FixController>().isFixed == false)
        {
            hoseFilter.GetComponent<FixController>().isFixed = true;
            hoseFilter.transform.position = new Vector3(4.6f, -6.505564f, -8.085947f);
            hoseFilter.transform.rotation = Quaternion.Euler(new Vector3(vx, vy, vz));
            hoseFilter.GetComponent<Rigidbody>().isKinematic = true;
            hoseFilter.GetComponent<Rigidbody>().useGravity = false;

            if (main.isGame == 1)
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("PotokCap"));
                main.gameText.text = "Далее отрегулируйте давление потока";
            }
        }
        if (stateDelay > 2)
        {
            if (main.isPresentation == 1 && main.nextItem.tag == "Hose")
            {
                main.targetsArray.Clear();
                main.targetsArray.Add(GameObject.FindWithTag("PotokCap"));
                main.nextItem = GameObject.FindWithTag("PotokCap");
                main.gameText.text = "Регулируем давление потока";
                main.nextItem.GetComponent<Rotate2>().RotateOnce();
            }
        }
    }

}
