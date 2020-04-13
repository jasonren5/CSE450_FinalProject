using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int score;
    public bool[] pokemon_collectables = new bool[8];

    void Awake()
    {
        instance = this;
    }
}
