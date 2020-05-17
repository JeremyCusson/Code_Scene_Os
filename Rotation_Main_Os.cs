using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Main_Os : MonoBehaviour
{
    // Déclaration des objets
    // Valeur de l'axe y
    public float rotationy = 0;
    // L'objet qui contient la main et l'avant-bras
    public GameObject PositionDeTous;
    // Valeur de la rotation initiale de l'objet qui contient la main et l'avant-bras
    private Quaternion PositionDeTousD;
    // Sert à aller chercher les fonctions de la classe Legende
    public Legende Activation;

    // Start is called before the first frame update
    void Start()
    {
        // Prend la rotation(position) initiale de l'objet qui contient la main et l'avant-bras
        PositionDeTousD = PositionDeTous.transform.rotation;
    }

    // Fait tourner la main et l'avant-bras vers la gauche
    public void Gauche()
    {
        if (Activation.RetourneActivation() == false) // Si le Toggle des informations des os n'est pas activé
        {
            rotationy = 2; // prend la valeur 2
            PositionDeTous.transform.Rotate(0, rotationy, 0); // rotation de l'avant-bras et de la main vers la gauche
        }
    }

    // Fait tourner la main et l'avant-bras vers la droite
    public void Droite()
    {
        if (Activation.RetourneActivation() == false) // Si le Toggle des informations des os n'est pas activé
        {
            rotationy = -2; // prend la valeur -2
            PositionDeTous.transform.Rotate(0, rotationy, 0); // rotation de l'avant-bras et de la main vers la droite
        }
    }

    // Ramène la main et l'avant-bras à leur position initiale
    public void Retour()
    {
        rotationy = 0; // Prend la valeur 0
        PositionDeTous.transform.rotation = PositionDeTousD; // Redonne la valeur initiale de rotation à l'objet qui contient la main et l'avant-bras
    }

}
