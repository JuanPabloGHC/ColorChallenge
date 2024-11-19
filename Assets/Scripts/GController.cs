using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GController : MonoBehaviour
{
    /*
     * PANEL NIVELES
     */
    [SerializeField] GameObject panelNiveles;
    // Botones niveles, informacion, flujo
    [SerializeField] GameObject[] buttonsPanelNiveles;
    [SerializeField] TextMeshProUGUI highScoreTextPanelNiveles;
    [SerializeField] GameObject RightButton;
    [SerializeField] GameObject LeftButton;
    [SerializeField] GameObject candadoImage;
    // Informacion adicional
    [SerializeField] GameObject panelInformacion;
    [SerializeField] TextMeshProUGUI indicacionText;
    [SerializeField] string[] indicacionesLista;
    // Variables
    private string indicacionActual;
    public int[] candados = { 0, 70, 90, 110 };
    private bool[] openLevels = { true, false, false, false, false };
    public bool[] OpenLevels { get { return openLevels; } }
    private int contadorNiveles;

    /*
     * PANEL OVER
     */
    [SerializeField] GameObject panelOver;
    [SerializeField] TextMeshProUGUI scoreOver;
    [SerializeField] GameObject highScoreText;

    /*
     * PANEL GAME
     */
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI scoreGame;
    [SerializeField] Enemy[] enemies;
    [SerializeField] Player player;
    
    /*
     * PANEL PRINCIPAL
     */
    [SerializeField] GameObject panelPrincipal;
    [SerializeField] GameObject corona;
    private bool ganador;
    public bool Ganador { get { return ganador; } }

    /*
     * PANEL APERTURA
     */
    [SerializeField] GameObject panelApertura;

    /*
     * AUDIO
     */
    // Sonidos de interaccion
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips; // 0: botones, 1: gameover, 2:logro
    // Musica de juego
    [SerializeField] AudioSource musicaAudio;
    [SerializeField] AudioClip[] musicaClips; // 0: principal, 1: seleccion niveles, 2: juego


    // Juego
    private int[] highScores = {0, 0, 0, 0};
    private int level;
    public int count;
    private float speed;
    public int[] HighScores { get { return highScores; } }
    public int Level { get { return level; } }
    public int Count { get { return count; } set { count = value; } }
    public float Speed { get { return speed; } }

    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = SaveManager.LoadData();
        if (gameData != null )
        {
            highScores = gameData.highScores;
            ganador = gameData.ganador;
            openLevels = gameData.openLevels;

        }
        // Panel Niveles
        contadorNiveles = 0;
        indicacionActual = indicacionesLista[0];
        indicacionText.text = indicacionActual;
        highScoreTextPanelNiveles.text = highScores[contadorNiveles].ToString();
        if (ganador) { Ganar(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (highScoreTextPanelNiveles.text != highScores[contadorNiveles].ToString())
        {
            highScoreTextPanelNiveles.text = highScores[contadorNiveles].ToString();
        }

        scoreGame.text = count.ToString();
    }
    
    public void NextLevel(bool next)
    {
        Sonido(0);
        // Esconder el actual
        buttonsPanelNiveles[contadorNiveles].SetActive(false);
        // Cambiar valor del contador, adelante o atras
        if (next)
        {
            contadorNiveles++;
        }
        else
        {
            contadorNiveles--;
        }

        //Botones de cambio de nivel
        if (contadorNiveles == 0)
        {
            LeftButton.SetActive(false);
        }
        else if (contadorNiveles > 2)
        {
            RightButton.SetActive(false);
        }
        else
        {
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
        }

        // Mostrar nuevos valores de boton, hs, indicaciones, candado
        if (openLevels[contadorNiveles] == true)
        {
            candadoImage.SetActive(false);
            buttonsPanelNiveles[contadorNiveles].SetActive(true);
            highScoreTextPanelNiveles.text = highScores[contadorNiveles].ToString();
        }
        else
        {
            candadoImage.SetActive(true);
        }
        indicacionActual = indicacionesLista[contadorNiveles];
        indicacionText.text = indicacionActual;
    }

    public void Informacion(bool abrir)
    {
        Sonido(0);
        if (abrir)
        {
            panelInformacion.SetActive(true);
        }
        else
        {
            panelInformacion.SetActive(false);
        }
    }

    public void StartGame(int level)
    {
        // Sonido boton
        Sonido(0);
        // Musica de juego
        musicaAudio.clip = musicaClips[2];
        musicaAudio.Play();
        this.level = level;
        panelNiveles.SetActive(false);
        panelGame.SetActive(true);
        Restart();
    }

    public void CheckCount()
    {
        count += 1;

        if (count % 3 == 0 && count > 0 && speed < 0.24f)
        {
            speed += 0.008f;
        }

        Debug.Log(count);
        Debug.Log(speed);
    }

    public void BackToPanelPrincipal()
    {
        // Sonido boton
        Sonido(0);
        // Musica inicial
        musicaAudio.clip = musicaClips[0];
        musicaAudio.Play();
        panelNiveles.SetActive(false);
        panelPrincipal.SetActive(true);
    }

    public void PanelNiveles()
    {
        // Sonido boton
        Sonido(0);
        // Musica de niveles
        musicaAudio.clip = musicaClips[1];
        musicaAudio.Play();
        panelPrincipal.SetActive(false);
        panelNiveles.SetActive(true);
    }

    public void GameOver()
    {
        // Musica de niveles cargar pero no reproducir
        musicaAudio.clip = musicaClips[1];
        // Pausar los objetos
        for (int j = 0; j < enemies.Length; j++)
        {
            enemies[j].enabled = false;
        }

        // Activar logro
        if (count >= candados[level] && openLevels[level] == false)
        {
            Sonido(1);
            panelApertura.SetActive(true);
            panelApertura.GetComponent<AnimacionesLogros>().Logro(level);
            openLevels[level] = true;
            if (level == 4) { Ganar(); }
        }

        // Activar panel de Game Over
        panelGame.SetActive(false);
        panelOver.SetActive(true);
        scoreOver.text = "SCORE: " + count.ToString();
        // Actualizar High Score
        if (count > highScores[level-1])
        {
            highScoreText.SetActive(true);
            highScores[level-1] = count;
        }

        //SaveManager.SaveData(this);
    }

    public void BackToPanelNiveles()
    {
        // Sonido boton
        Sonido(0);
        // Musica de niveles
        musicaAudio.Play();
        panelOver.SetActive(false);
        panelGame.SetActive(false);
        panelNiveles.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

        
    }

    public void Restart()
    {
        player.Restart();
        count = 0;
        speed = 0.05f;
        for (int j = 0; j < enemies.Length; j++)
        {
            enemies[j].Restart();
            enemies[j].enabled = true;
        }
        for (int i = 0; i < level + 2; i++)
        {
            buttons[i].SetActive(true);
        }
        highScoreText.SetActive(false);
    }

    public void Exit()
    {
        Sonido(0);
        Application.Quit();
    }

    public void Ganar()
    {
        ganador = true;
        corona.SetActive(true);
    }

    private void Sonido(int indice)
    {
        audioSource.PlayOneShot(audioClips[indice]);
    }

}
