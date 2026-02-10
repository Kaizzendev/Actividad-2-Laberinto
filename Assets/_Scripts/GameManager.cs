using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //hago este script accesible para otros en cualquier momento, no lo hago singleton porque no lo instancio ni cambio de escena
    public static GameManager Instance;

    public GameObject text;
    public PlayerController player;
    public Slider slider_vida;
    public Slider slider_puntos;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Play()
    {
        player.isPlaying = true;
    }

    public void Win()
    {
        text.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void Vida_puntos(float vida, float puntos)
    {
        slider_vida.value = vida;
        Debug.Log(puntos);
        slider_puntos.value = puntos;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
