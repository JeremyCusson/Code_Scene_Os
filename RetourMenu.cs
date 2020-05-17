using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Gère les scènes lors de l'exécusion

public class RetourMenu : MonoBehaviour
{
    // Envoie l'utilisateur dans la scène Menu
    public void RetournerAuMenu()
    {
        // Charge la scène Menu
        SceneManager.LoadScene("Menu"); 
    }
}
