using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementPoing : MonoBehaviour
{
    // Déclaration des objets
    // Nom des Animators
    public string NameAnimation;
    public string NameAnimation2;
    public string NameAnimation3;
    // Les animations
    Animation pointf;
    Animation ecarterdoigts;
    Animation pointer;
    // Start is called before the first frame update
    void Start()
    {
        // Commence par incrémenter le composant Animation du GameObject où elle se trouve aux Animations
        pointf = GetComponent<Animation>();
        ecarterdoigts = GetComponent<Animation>();
        pointer = GetComponent<Animation>();    
    }

    // Fonction qui fait jouer l'animation pointf
    public void fermepoing()
    {
        // Fait jouer l'animator de l'animation pointf
         pointf.Play(NameAnimation);
    }
    // Fonction qui fait jouer l'animation ecarterdoigts
    public void EcarterLesDoigts()
    {
        // Fait jouer l'animator de l'animation ecarterdoigts
        ecarterdoigts.Play(NameAnimation2);
    }
    // Fonction qui fait jouer l'animation pointer
    public void PointerAvecDoigts()
    {
        // Fait jouer l'animator de l'animation pointer
        pointer.Play(NameAnimation3);
    }
}
