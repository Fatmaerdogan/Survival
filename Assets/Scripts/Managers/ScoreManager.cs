using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static int SubScore;
    public static int score;
    List<int> liste = new List<int>();
    Text text;
    public Text scoretext;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }/*
    private void Start()
    {
        foreach (int i in liste)
        {

            PlayerPrefs.GetInt("birinci", liste[i]);
        }
    }
    */
    void Update ()
    {
        text.text = "Score: " + score;
    }/*
    void Skor()
    {
        foreach(int i in liste)
        {
            if(score > i)
            {
                liste.Add(score);
            }
        }
    }
    void SkorYazdir(List<int> liste)
    {
        foreach (int i in liste) {
            
            PlayerPrefs.SetInt("birinci", liste[i]);
        }
    }*/
}
