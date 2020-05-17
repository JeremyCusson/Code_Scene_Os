using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    // Déclaration des objets
    public GameObject Text1, Text2, Text3, Text4, Text5; // Les textes d'information

    // Fonction qui fait apparaitre les messages
    public void Message()
    {
        // Active les textes d'information qui sont, à l'origine, désactivés
        Text1.SetActive(true); // Active le texte d'information qui est, à l'origine, désactivé
        Text2.SetActive(true); // Active le texte d'information qui est, à l'origine, désactivé
        Text3.SetActive(true); // Active le texte d'information qui est, à l'origine, désactivé
        Text4.SetActive(true); // Active le texte d'information qui est, à l'origine, désactivé
        Text5.SetActive(true); // Active le texte d'information qui est, à l'origine, désactivé

        // Fonction qui permet, selon le yield, d'avoir une certaine pause entre l'activation et la désactivation des textes 
        // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
        StartCoroutine(DisparaitreMessage()); 
    }

    // Désactive les textes 
    IEnumerator DisparaitreMessage() // IEnumerator est utiliser pour faire une pause
	{
        // Arrête l'exécution de la coroutine pendant 13 secondes
		yield return new WaitForSeconds(13f);

        // Désactive les textes d'information
        Text1.SetActive(false); // Désactive le texte d'information
        Text2.SetActive(false); // Désactive le texte d'information
        Text3.SetActive(false); // Désactive le texte d'information
        Text4.SetActive(false); // Désactive le texte d'information
        Text5.SetActive(false); // Désactive le texte d'information
	}
}
