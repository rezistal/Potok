using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    public List<GameObject> targetsArray;
    public Text gameText;
    public Text popUP;
    GameObject getGO;
    Rigidbody getTarget;
    //GameObject getTarget;
    bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;
    public int isGame;
    public int isPresentation;
    public Button StartButton;
    public GameObject StopButton;
    public GameObject nextItem;

    private void Start()
    {
        StartButton.GetComponentInChildren<Text>().text = TheGame.startBut;
        
        gameText = GameObject.FindWithTag("State").GetComponent<Text>();
        popUP = GameObject.FindWithTag("popUP").GetComponent<Text>();
        popUP.text = "";

        switch (TheGame.i)
        {
            case 0:
                targetsArray.Add(GameObject.FindWithTag("AirFilter"));
                targetsArray.Add(GameObject.FindWithTag("PL"));
                targetsArray.Add(GameObject.FindWithTag("Hose"));
                targetsArray.Add(GameObject.FindWithTag("Mask"));
                targetsArray.Add(GameObject.FindWithTag("Plug"));
                targetsArray.Add(GameObject.FindWithTag("Oxy"));
                gameText.text = "";
                StopButton.SetActive(false);
                isGame = TheGame.i;
                break;
            case 1:
                StopButton.SetActive(true);
                foreach (string s in TheGame.sA)
                {
                    targetsArray.Add(GameObject.FindWithTag(s));
                }
                gameText.text = "Сначала подключите предохранительный клапан";
                isGame = TheGame.i;
                break;
            case 2:
                isPresentation = 1;
                nextItem = GameObject.FindWithTag("PL");
                foreach (string s in TheGame.sA)
                {
                    targetsArray.Add(GameObject.FindWithTag(s));
                }
                gameText.text = "Сначала подключаем предохранительный клапан";
                nextItem.GetComponent<Animation>().Play(nextItem.tag);
                isGame = 1;
                break;
        }
    }

    void LateUpdate()
    {

        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            getGO = ReturnClickedObject(out hitInfo);

            //getTarget = ReturnClickedObject(out hitInfo);
            //if (getTarget != null && targetsArray.Contains(getTarget) && !getTarget.GetComponent<FixController>().isFixed)
            if (getGO != null && targetsArray.Contains(getGO) && !getGO.GetComponent<FixController>().isFixed)
            {
                getTarget = getGO.GetComponent<Rigidbody>();
                isMouseDragging = true;
                //Converting world position to screen position.
                positionOfScreen = Camera.main.WorldToScreenPoint(getTarget.transform.position);
                offsetValue = getTarget.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z));
            }
        }

        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        //Is mouse Moving
        if (isMouseDragging)
        {
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, positionOfScreen.z);

            //converting screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offsetValue;

            //It will update target gameobject's current postion.
            getTarget.MovePosition(currentPosition);
            //getTarget.transform.position = currentPosition;
        }


    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    public void RestartEvent()
    {
        TheGame.i = 0;
        TheGame.sA.Clear();
        TheGame.startBut = "Начать";
        SceneManager.LoadScene("whatisthis");
    }

    public void StartEvent()
    {
        TheGame.startBut = "Начать заново";
        TheGame.i = 1;
        TheGame.sA.Clear();
        TheGame.sA.Add("PL");
        SceneManager.LoadScene("whatisthis");
    }

    public void StopEvent()
    {
        TheGame.i = 0;
        isGame = TheGame.i;
        isPresentation = 0;
        targetsArray.Clear();
        targetsArray.Add(GameObject.FindWithTag("AirFilter"));
        targetsArray.Add(GameObject.FindWithTag("PL"));
        targetsArray.Add(GameObject.FindWithTag("Hose"));
        targetsArray.Add(GameObject.FindWithTag("Mask"));
        targetsArray.Add(GameObject.FindWithTag("Plug"));
        targetsArray.Add(GameObject.FindWithTag("Oxy"));
        GlowDragging[] myItems = FindObjectsOfType(typeof(GlowDragging)) as GlowDragging[];
        foreach (GlowDragging go in myItems)
        {
            go.UpdOnce();
        }
        gameText.text = "";
        TheGame.startBut = "Начать";
        StopButton.SetActive(false);
        StartButton.GetComponentInChildren<Text>().text = TheGame.startBut;
    }

    public void PresentationEvent()
    {
        TheGame.i = 2;
        TheGame.sA.Clear();
        TheGame.sA.Add("PL");
        SceneManager.LoadScene("whatisthis");
    }
}

// Храним переменные между вызовами LoadScene
public static class TheGame
{
    public static int i = 0;
    public static List<string> sA = new List<string>();
    //public static string[] sA = {"PL", "AirFilter", "Plug", "Hose", // "Oxy", // "Mask" };

    //Smooth camera reload
    public static float cX = -40.0f;
    public static float cY = 20.0f;
    public static float cD = 30.0f;

    public static string startBut = "Начать";

}