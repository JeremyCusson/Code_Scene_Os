using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_AvantBras : MonoBehaviour
{
    // Déclaration des objets
    // Valeur de l'axe y
    private float rotationy = 0;
    // L'objet qui contient la main et l'avant-bras
    public GameObject PositionDeTous;
    // Valeur de la rotation initiale de l'objet qui contient la main et l'avant-bras
    private Quaternion PositionDeTousD;

    // Start is called before the first frame update
    void Start()
    {
        // Prend la rotation(position) initiale de l'objet qui contient la main et l'avant-bras
        PositionDeTousD = PositionDeTous.transform.rotation;
    }

    // Fait tourner la main et l'avant-bras vers la gauche
    public void Gauche()
    {
            rotationy = 3; // prend la valeur 3
            PositionDeTous.transform.Rotate(0, rotationy, 0); // rotation de l'avant-bras et de la main vers la gauche
    }

    // Fait tourner la main et l'avant-bras vers la droite
    public void Droite()
    {
            rotationy = -3; // prend la valeur -3
            PositionDeTous.transform.Rotate(0, rotationy, 0); // rotation de l'avant-bras et de la main vers la droite
    }

    // Ramène la main et l'avant-bras à leur position initiale
    public void Retour()
    {
        // Redonne la valeur initiale de rotation à l'objet qui contient la main et l'avant-bras
        PositionDeTous.transform.rotation = PositionDeTousD;
    }
}
