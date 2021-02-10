using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowDragging : MonoBehaviour {

    public Main main;
    public Material[] m;
    private Color mouseOverColor = Color.green;
    private Color highlightColor = Color.yellow;
    private Color originalColor;
    //private Material origMat;

    // Use this for initialization
    void Start () {
        main = GameObject.FindWithTag("Main").GetComponent<Main>();
        //origMat = GetComponent<Renderer>().material;
        originalColor = GetComponent<Renderer>().material.color;
        m = GetComponent<Renderer>().materials;
    }

    void OnMouseEnter()
    {
        if (!GetComponent<FixController>().isFixed && main.targetsArray.Contains(gameObject))
        {
            if(gameObject.tag != "Mask")
            {
                foreach (Material vM in m)
                {
                    try { vM.color = mouseOverColor; }
                    catch { vM.mainTexture = Resources.Load("Materials / HLMat") as Texture; }
                }
            }
        }
        switch (gameObject.tag)
        {
            case "PL":
                main.popUP.text = "Предохранительный клапан";
                break;
            case "AirFilter":
                main.popUP.text = "Анти-бактериальный фильтр";
                break;
            case "Hose":
                main.popUP.text = "Коннектор дыхательного контура";
                break;
            case "Mask":
                main.popUP.text = "Дыхательная маска";
                break;
            case "Plug":
                main.popUP.text = "Шнур питания";
                break;
            case "Oxy":
                main.popUP.text = "Кислородный шланг";
                break;
            case "PotokCap":
                main.popUP.text = "Регулятор давления потока";
                break;
            case "PLCap":
                main.popUP.text = "Регулятор предохранительного клапана";
                break;
            case "OxyCap":
                main.popUP.text = "Кислородный вентиль";
                break;
            case "OxyCap2":
                main.popUP.text = "Регулятор концентрации кислорода";
                break;
        }
    }

    void OnMouseExit()
    {
        if (gameObject.tag != "Mask")
        {
            foreach (Material vM in m)
            {
                try { vM.color = originalColor; }
                catch { vM.mainTexture = Resources.Load("Materials/Materials/plcap2text") as Texture; }
            }
        }
        main.popUP.text = "";
    }

    private void Update()
    {
        if (main.isGame == 1 && (gameObject.tag != "Mask"))
        {
            if (main.targetsArray.Contains(gameObject))
            {
                foreach (Material vM in m)
                {
                    try { vM.color = highlightColor; }
                    catch { vM.mainTexture = Resources.Load("Materials/HLMat") as Texture; }
                }
            }
            else
            {
                foreach (Material vM in m)
                {
                    try { vM.color = originalColor; }
                    catch { vM.mainTexture = Resources.Load("Materials/Materials/plcap2text") as Texture; }
                }
            }
        }
        /* //Другой цвет для презентации
        if (main.isPresentation == 1)
        {
            if (main.targetsArray.Contains(gameObject))
            {
                foreach (Material vM in m)
                {
                    vM.color = highlightColor;
                }
            }
        }
        */
    }

    public void UpdOnce()
    {
        if (gameObject.tag != "Mask")
        {
            foreach (Material vM in m)
            {
                try { vM.color = originalColor; }
                catch { vM.mainTexture = Resources.Load("Materials/Materials/plcap2text") as Texture; }
            }
        }
    }
}
