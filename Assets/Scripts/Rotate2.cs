using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2 : MonoBehaviour {

    public Main main;
    public GameObject parent;
    public bool rotationActive;
    public bool onceRotation;
    public bool stateTimerActive;
    public bool anotherTimer;
    public float j = 0.0f;
    public int i = 1;
    public float stateDelay = 0.0f;
    // Use this for initialization

    void Start()
    {
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
        rotationActive = false;
        onceRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationActive)
        {
            j++;
            if (j > 20)
            {
                rotationActive = false;
                j = 0.0f;
                i = i * -1;
            }
            // Debug.Log();
        }

        if (stateTimerActive || anotherTimer)
        {
            stateDelay += Time.deltaTime;
        }

        if(stateDelay > 2 && stateTimerActive)
        {
            rotationActive = true;
            preNextStep();
        }

        if(stateDelay > 2 && anotherTimer)
        {
            NextStep();
        }
    }

    void OnMouseDown()
    {
        if (main.isGame == 0 && parent.GetComponent<FixController>().isFixed)
        {
            rotationActive = true;
        }
        else
        {
            if (onceRotation == false && main.GetComponent<Main>().targetsArray.Contains(gameObject))
            {
                onceRotation = true;
                rotationActive = true;
                main.targetsArray.Clear();
                if(gameObject.tag == "PLCap")
                {
                    main.targetsArray.Add(GameObject.FindWithTag("AirFilter"));
                    main.gameText.text = "Отлично! Вставьте анти-бактериальный фильтр";
                }
                if (gameObject.tag == "OxyCap")
                {
                    main.targetsArray.Add(GameObject.FindWithTag("OxyCap2"));
                    main.gameText.text = "Осталось отрегулировать % кислорода";
                }
                if (gameObject.tag == "PotokCap")
                {
                    main.targetsArray.Add(GameObject.FindWithTag("Oxy"));
                    main.gameText.text = "Супер! Сейчас нужно подключить кислородный шланг";
                }
                if (gameObject.tag == "OxyCap2")
                {
                    main.StopEvent();
                    main.gameText.text = "Готово! Поток готов к эксплуатации!";
                }
            }
        }
    }

    public void RotateOnce()
    {
        stateTimerActive = true;
    }

    public void preNextStep()
    {
        stateTimerActive = false;
        stateDelay = 0;
        anotherTimer = true;
    }

    public void NextStep()
    {
        anotherTimer = false;

        main.targetsArray.Clear();
        switch (gameObject.tag)
        {
            case "PLCap":
                main.gameText.text = "Далее вставляем анти-бактериальный фильтр";
                main.nextItem = GameObject.FindWithTag("AirFilter");
                main.targetsArray.Add(GameObject.FindWithTag("AirFilter"));
                main.nextItem.GetComponent<Animation>().Play(main.nextItem.tag);
                break;
            case "PotokCap":
                main.gameText.text = "Подключаем кислородный шланг";
                main.nextItem = GameObject.FindWithTag("Oxy");
                main.targetsArray.Add(GameObject.FindWithTag("Oxy"));
                main.nextItem.GetComponent<Animation>().Play(main.nextItem.tag);
                break;
            case "OxyCap":
                main.gameText.text = "Регулируем % кислорода";
                main.nextItem = GameObject.FindWithTag("OxyCap2");
                main.targetsArray.Add(GameObject.FindWithTag("OxyCap2"));
                main.nextItem.GetComponent<Rotate2>().RotateOnce();
                break;
            case "OxyCap2":
                main.StopEvent();
                main.gameText.text = "Поток готов к эксплуатации!";
                break;
        }
    }

    void LateUpdate()
    {
        if (rotationActive)
        {
            if(gameObject.tag == "PLCap" || gameObject.tag == "OxyCap")
                transform.RotateAround(transform.GetComponent<Collider>().bounds.center, new Vector3(0, i, 0), j);
            if (gameObject.tag == "PotokCap" || gameObject.tag == "OxyCap2")
                transform.RotateAround(transform.GetComponent<Collider>().bounds.center, new Vector3(i, 0, 0), j);
        }
    }
}
