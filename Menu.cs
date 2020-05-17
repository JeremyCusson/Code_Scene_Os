using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Gère les scènes lors de l'exécusion

public class Menu : MonoBehaviour
{
    // Envoie l'utilisateur dans la scène Os
   public void os()
   {
       SceneManager.LoadScene("Os"); // Charge la scène Os
   }
   // Envoie l'utilisateur dans la scène Viande
   public void viande()
   {
       SceneManager.LoadScene("Viande"); // Charge la scène Viande
   }
   // Envoie l'utilisateur dans la scène Force
   public void force()
   {
       SceneManager.LoadScene("Force"); // Charge la scène Force
   }
   // Fait sortir l'utilisateur du programme
   public void quitter()
   {
       #if UNITY_EDITOR // Appelle les scriptes Unity Editor avec mes codes de l'application
        UnityEditor.EditorApplication.isPlaying = false; // Fait que l'éditeur n'est pas en ce moment en mode lecture


        #else
        Application.Quit(); // Quitte l'application
           
        #endif
   }
}
