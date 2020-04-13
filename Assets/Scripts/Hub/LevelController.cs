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
        pokemonBest.text = PlayerPrefs.GetFloat("pokemonBestScore").ToString();
        pacManBest.text = PlayerPrefs.GetFloat("pacManBestScore").ToString();
        donkeyKongBest.text = PlayerPrefs.GetFloat("donkeyKongBestScore").ToString();
        //TODO: Add conditionals to check if time or score should be shown
        //TODO: Change "score" to be "time" and add measurements like "seconds" when switched to time
        //pokemonBest.text = PlayerPrefs.GetFloat("pokemonBestTime").ToString();
        //pacManBest.text = PlayerPrefs.GetFloat("pacManBestTime").ToString();
        //donkeyKongBest.text = PlayerPrefs.GetFloat("donkeyKongBestTime").ToString();
    }
}
