using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legende : MonoBehaviour
{
    // Déclaration des objets
    // Le chiffre attribué à chaque carpe
    public GameObject Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8;
    // Le nom attribué à chaque doigt
    public GameObject NomDoigt1, NomDoigt2, NomDoigt3, NomDoigt4, NomDoigt5;
    // Canvas contenant tous les noms et toutes les informations des os de la main
    public GameObject LegendeInfo;
    // Les objets ou les os représentant les phalanges distales
    public GameObject Distale1, Distale2, Distale3, Distale4, Distale5;
    // Les objets ou les os représentant les phalanges médianes
    public GameObject Mediane1, Mediane2, Mediane3, Mediane4;
    // Les objets ou les os représentant les phalanges proximales
    public GameObject Proximale1, Proximale2, Proximale3, Proximale4, Proximale5;
    // Les objets ou les os représentant les métacarpes
    public GameObject Metacarpe1, Metacarpe2, Metacarpe3, Metacarpe4, Metacarpe5;
    // Les objets ou les os représentant les carpes
    public GameObject Carpe1, Carpe2, Carpe3, Carpe4, Carpe5, Carpe6, Carpe7, Carpe8;
    // Les objets ou les os représentant le radius et le cubitus
    public GameObject Radius, Cubitus;
    // Le matériel(couleur) originel des os de la main
    public Material OriginaleColor;
    // Le nouveau matériel(couleur) pour chacun des os de la main
    public Material NewColorDistale, NewColorMediane, NewColorProximale, NewColorMetacarpe, NewColorCarpe, NewColorRadius, NewColorCubitus;
    // Valeur qui sert à savoir si le Toggle est allumé ou éteint
    public bool ActDes;
    // Valeur qui sert à savoir si le Toggle est allumé ou éteint pour d'autres classes
    public bool Activ;

    // Sert à aller chercher les fonctions de la classe Mov_pouce
    public Mov_pouce Pouce;
    // Sert à aller chercher les fonctions de la classe Mov_Majeur
    public Mov_Majeur Majeur;
    // Sert à aller chercher les fonctions de la classe Mov_Index
    public Mov_Index Index;
    // Sert à aller chercher les fonctions de la classe Mov_Annulaire
    public Mov_Annulaire Annulaire;
    // Sert à aller chercher les fonctions de la classe Mov_Auriculaire
    public Mov_Auriculaire Auriculaire;

    // Fonction qui active ou désactive le canvas contenant tous les noms et toutes les informations des os de la main
    // Fonction qui retourne la main, l'avant-bras et leurs composantes à leur position initiale 
    // Fonction qui active ou désactive le chiffre attribué à chaque carpe et le nom attribué à chaque doigt
    // Fonction qui change le matériel(couleur) originel pour les nouveaux matériels(couleurs)
    public void ActiverDesactiver(bool AD)
    {
        // ActDes prend la valeur donnée à AD par le Toggle
        ActDes = AD;

        // Si le ActDes est vrai, le contenu, à l'intérieur, est accessible
        if (ActDes == true)
        {
            // Active et affiche le canvas contenant tous les noms et toutes les informations des os de la main
            LegendeInfo.SetActive(true);
            // Va chercher la fonction RetournePositionInitiale de la classe Mov_Index pour l'utiliser
            Index.RetournePostionInitiale();
            // Va chercher la fonction RetournePositionInitiale de la classe Mov_Majeur pour l'utiliser
            Majeur.RetournePostionInitiale();
            // Va chercher la fonction RetournePositionInitiale de la classe Mov_Annulaire pour l'utiliser
            Annulaire.RetournePostionInitiale();
            // Va chercher la fonction RetournePositionInitiale de la classe Mov_Auriculaire pour l'utiliser
            Auriculaire.RetournePostionInitiale();
            // Va chercher la fonction RetournePositionInitiale de la classe Mov_pouce pour l'utiliser
            Pouce.RetournePostionInitiale();

            // Active et affiche le chiffre attribué à chacun des carpes
            Num1.SetActive(true);
            Num2.SetActive(true);
            Num3.SetActive(true);
            Num4.SetActive(true);
            Num5.SetActive(true);
            Num6.SetActive(true);
            Num7.SetActive(true);
            Num8.SetActive(true);

            // Active et affiche le nom attribué à chacun des doigts
            NomDoigt1.SetActive(true);
            NomDoigt2.SetActive(true);
            NomDoigt3.SetActive(true);
            NomDoigt4.SetActive(true);
            NomDoigt5.SetActive(true);

            // Change le matériel(couleur) originel des Distales pour le nouveau matériel(couleur) 
            Distale1.GetComponent<Renderer>().material = NewColorDistale;
            Distale2.GetComponent<Renderer>().material = NewColorDistale;
            Distale3.GetComponent<Renderer>().material = NewColorDistale;
            Distale4.GetComponent<Renderer>().material = NewColorDistale;
            Distale5.GetComponent<Renderer>().material = NewColorDistale;

            // Change le matériel(couleur) originel des Medianes pour le nouveau matériel(couleur)
            Mediane1.GetComponent<Renderer>().material = NewColorMediane;
            Mediane2.GetComponent<Renderer>().material = NewColorMediane;
            Mediane3.GetComponent<Renderer>().material = NewColorMediane;
            Mediane4.GetComponent<Renderer>().material = NewColorMediane;

            // Change le matériel(couleur) originel des Proximales pour le nouveau matériel(couleur)
            Proximale1.GetComponent<Renderer>().material = NewColorProximale;
            Proximale2.GetComponent<Renderer>().material = NewColorProximale;
            Proximale3.GetComponent<Renderer>().material = NewColorProximale;
            Proximale4.GetComponent<Renderer>().material = NewColorProximale;
            Proximale5.GetComponent<Renderer>().material = NewColorProximale;

            // Change le matériel(couleur) originel des Metacarpes pour le nouveau matériel(couleur)
            Metacarpe1.GetComponent<Renderer>().material = NewColorMetacarpe;
            Metacarpe2.GetComponent<Renderer>().material = NewColorMetacarpe;
            Metacarpe3.GetComponent<Renderer>().material = NewColorMetacarpe;
            Metacarpe4.GetComponent<Renderer>().material = NewColorMetacarpe;
            Metacarpe5.GetComponent<Renderer>().material = NewColorMetacarpe;

            // Change le matériel(couleur) originel des Carpes pour le nouveau matériel(couleur)
            Carpe1.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe2.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe3.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe4.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe5.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe6.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe7.GetComponent<Renderer>().material = NewColorCarpe;
            Carpe8.GetComponent<Renderer>().material = NewColorCarpe;

            // Change le matériel(couleur) originel du Radius et du Cubitus pour le nouveau matériel(couleur)
            Radius.GetComponent<Renderer>().material = NewColorRadius;
            Cubitus.GetComponent<Renderer>().material = NewColorCubitus;
        }
        if (ActDes == false) // Si le ActDes est faux, le contenu, à l'intérieur, est accessible
        {
            // Désactive le canvas contenant tous les noms et toutes les informations des os de la main
            LegendeInfo.SetActive(false);

            // Désactive le chiffre attribué à chacun des carpes
            Num1.SetActive(false);
            Num2.SetActive(false);
            Num3.SetActive(false);
            Num4.SetActive(false);
            Num5.SetActive(false);
            Num6.SetActive(false);
            Num7.SetActive(false);
            Num8.SetActive(false);

            // Désactive le nom attribué à chacun des doigts
            NomDoigt1.SetActive(false);
            NomDoigt2.SetActive(false);
            NomDoigt3.SetActive(false);
            NomDoigt4.SetActive(false);
            NomDoigt5.SetActive(false);

            // Change le nouveau matériel(couleur) des Distales pour le matériel(couleur) originel
            Distale1.GetComponent<Renderer>().material = OriginaleColor;
            Distale2.GetComponent<Renderer>().material = OriginaleColor;
            Distale3.GetComponent<Renderer>().material = OriginaleColor;
            Distale4.GetComponent<Renderer>().material = OriginaleColor;
            Distale5.GetComponent<Renderer>().material = OriginaleColor;

            // Change le nouveau matériel(couleur) des Medianes pour le matériel(couleur) originel
            Mediane1.GetComponent<Renderer>().material = OriginaleColor;
            Mediane2.GetComponent<Renderer>().material = OriginaleColor;
            Mediane3.GetComponent<Renderer>().material = OriginaleColor;
            Mediane4.GetComponent<Renderer>().material = OriginaleColor;

            // Change le nouveau matériel(couleur) des Proximales pour le matériel(couleur) originel
            Proximale1.GetComponent<Renderer>().material = OriginaleColor;
            Proximale2.GetComponent<Renderer>().material = OriginaleColor;
            Proximale3.GetComponent<Renderer>().material = OriginaleColor;
            Proximale4.GetComponent<Renderer>().material = OriginaleColor;
            Proximale5.GetComponent<Renderer>().material = OriginaleColor;

            // Change le nouveau matériel(couleur) des Metacarpes pour le matériel(couleur) originel
            Metacarpe1.GetComponent<Renderer>().material = OriginaleColor;
            Metacarpe2.GetComponent<Renderer>().material = OriginaleColor;
            Metacarpe3.GetComponent<Renderer>().material = OriginaleColor;
            Metacarpe4.GetComponent<Renderer>().material = OriginaleColor;
            Metacarpe5.GetComponent<Renderer>().material = OriginaleColor;

            // Change le nouveau matériel(couleur) des Carpes pour le matériel(couleur) originel
            Carpe1.GetComponent<Renderer>().material = OriginaleColor;
            Carpe2.GetComponent<Renderer>().material = OriginaleColor;
            Carpe3.GetComponent<Renderer>().material = OriginaleColor;
            Carpe4.GetComponent<Renderer>().material = OriginaleColor;
            Carpe5.GetComponent<Renderer>().material = OriginaleColor;
            Carpe6.GetComponent<Renderer>().material = OriginaleColor;
            Carpe7.GetComponent<Renderer>().material = OriginaleColor;
            Carpe8.GetComponent<Renderer>().material = OriginaleColor;

            // Change le nouveau matériel(couleur) du Radius et du Cubitus pour le matériel(couleur) originel
            Radius.GetComponent<Renderer>().material = OriginaleColor;
            Cubitus.GetComponent<Renderer>().material = OriginaleColor;
        }
    }

    // Retourn la valeur fausse ou vraie pour savoir si le Toggle est activé ou non
    public bool RetourneActivation()
    {
        // Activ prend la valeur d'ActDes
        Activ = ActDes;
        // Retourne la valeur de Activ
        return Activ;
    }
}
