using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour
{
    public Text mostrador;
    public Text mostradorRecorde;
    private int placar;
    private int recorde;

    void Start()
    {
        placar = 0;
        recorde = PlayerPrefs.GetInt("recorde", 0);

        mostradorRecorde.text = "Recorde: " + recorde;

        // chama pontua() uma vez a cada 0,3s
        InvokeRepeating("pontua", 0.3f, 0.3f);

    }
    void pontua()
    {
        Debug.Log("Pontua chamado"); // Adicione esta linha
        placar += 1;
        if (placar > recorde)
        {
            recorde = placar;
            PlayerPrefs.SetInt("recorde", recorde);
        }

        mostrador.text = "Pontuação: " + placar;
        mostradorRecorde.text = "Recorde: " + recorde;
    }

    public void AdicionarPontos(int valor)
    {
        placar += valor;

        if (placar > recorde)
        {
            recorde = placar;
            PlayerPrefs.SetInt("recorde", recorde);
        }

        mostrador.text = "Pontuação: " + placar;
        mostradorRecorde.text = "Recorde: " + recorde;
    }

public int Pontos => placar;
}