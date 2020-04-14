using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Text pokemonBest;
    public Text pacManBest;
    public Text donkeyKongBest;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        pokemonBest.text = PlayerPrefs.GetInt("pokemonBestScore").ToString() + "/8";
        pacManBest.text = PlayerPrefs.GetInt("pacManBestScore").ToString() + "/7";
        donkeyKongBest.text = PlayerPrefs.GetFloat("donkeyKongBestScore").ToString() + "/3";
    }

    // Reset high scores on button click
    public void ResetHighScores()
    {
        PlayerPrefs.SetInt("pokemonBestScore", 0);
        PlayerPrefs.SetInt("pacManBestScore", 0);
        PlayerPrefs.SetInt("donkeyKongBestScore", 0);
    }
}
