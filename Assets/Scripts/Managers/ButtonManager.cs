using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    public GameObject buttoncanvas;
    public GameObject arkaplan;
    public GameObject score;
    public GameObject overAgain;
    public GameObject skortext;
    public GameObject geributton;
    private void Start()
    {
        Time.timeScale = 0f;
        score.SetActive(false);
        skortext.SetActive(false);
    }
    public void Click()
    {
        Debug.Log("bastım");
        EditorApplication.isPlaying = false;
    }
    public void OverAgain()
    {
#pragma warning disable CS0618 // Tür veya üye eski
        Application.LoadLevel(0);
#pragma warning restore CS0618 // Tür veya üye eski
    }
    public void PlayClick()
    {
        Time.timeScale = 1.2f;
        buttoncanvas.SetActive(false);
        arkaplan.SetActive(false);
        score.SetActive(true);
    }
    public void skortusu()
    {
        buttoncanvas.SetActive(false);
        skortext.SetActive(true);
        geributton.SetActive(true);
    }
    public void geri()
    {

    }
}
