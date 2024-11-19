using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsperaAnimacion : MonoBehaviour
{
    [SerializeField] AnimacionesLogros logros;
    
    public void TerminarAnimacion()
    {
        logros.TerminarAnimacion();
    }
}
