using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDonkeyKong()
    {
        SceneManager.LoadScene("DonkeyKong");
    }

    public void LoadPacMan()
    {
        SceneManager.LoadScene("Pacman-2.0");
    }

    public void LoadPokemon()
    {
        SceneManager.LoadScene("Pokemon");
    }
}
