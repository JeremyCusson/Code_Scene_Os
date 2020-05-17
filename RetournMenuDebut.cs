using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Gère les scènes lors de l'exécusion

public class RetournMenuDebut : MonoBehaviour
{
    // Envoie l'utilisateur dans la scène Menu
    public void RetournerAuMenu()
    {
        SceneManager.LoadScene("Menu"); // Charge la scène Menu
    } 
}
