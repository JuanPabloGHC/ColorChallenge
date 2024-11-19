using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimacionesLogros : MonoBehaviour
{
    // Animaciones nuevo nivel
    [SerializeField] Color[] colores;
    [SerializeField] Image nivelColor;
    
    [SerializeField] TextMeshProUGUI nivelTexto;
    
    [SerializeField] GameObject candado;
    [SerializeField] GameObject nivel;
    [SerializeField] GameObject texto;

    // Animacion victoria
    [SerializeField] GameObject corona;

    private bool _wait;

    public void Logro(int level)
    {
        if (level < 4)
        {
            nivelColor.color = colores[level-1];
            nivelTexto.text = (level+1).ToString();
            nivel.SetActive(true);
            candado.SetActive(true);
            texto.SetActive(true);
        }
        else
        {
            corona.SetActive(true);
        }
    }

    public void TerminarAnimacion()
    {
        nivel.SetActive(false);
        candado.SetActive(false);
        corona.SetActive(false);
        texto.SetActive(false);
        this.gameObject.SetActive(false);
    }
    
}
