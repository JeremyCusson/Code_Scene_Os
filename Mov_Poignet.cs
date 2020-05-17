using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Poignet : MonoBehaviour
{
    // Déclaration des objets
    // l'objet qui représente le poignet
    public GameObject Poignet;
    // les positions du poignet de la main
    private Quaternion PositionDebut, NewPosition;
    // Les messages qui vont apparaitre en disant que le poignet a atteint sa limite
    public GameObject MessageLimite1, MessageLimite2;
    // valeur de l'axe x
    private float variablex;
    // Valeur de l'angle initial du poignet, de l'angle de flexion et de l'angle d'extension 
    private float AngleDebut, CalAngle1, CalAngle2;
    // Valeur de l'angle max de la flexion et de l'angle max de l'extension
    private float AngleMin = -70.0f, AngleMax = 37.0f;
    // Sert à aller chercher les fonctions de la classe Legende
    public Legende Activation;
    // le GameObject représentant toute la main et l'avant-bras
    public GameObject ToutLaRotation;
    // la valeur de la rotation initiale de ToutLaRotation
    private Quaternion RotationInitiale;

    // Start is called before the first frame update
    void Start()
    {
        // Prend la valeur initiale de la rotation x du poignet
        PositionDebut = Poignet.transform.rotation;
        // Prend la valeur initiale de la rotation du poignet
        AngleDebut = Poignet.transform.rotation.x;
        // Prend la valeur initiale de la rotation de l'ensemble de la main et de l'avant-bras
        RotationInitiale = ToutLaRotation.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // Regarde si la bouton droite de la souris a été appuyé
        {
            RaycastHit hit; // Prend les valeurs du raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Prend le rayon qui va de la caméra principale à un point d'écran 

            if (Physics.Raycast(ray, out hit)) // Si le rayon croise un collisonneur(GameObject), il retourne vrai, sinon il retourne faux
            {
                // Si l'utilisateur clique sur le poignet flexion, si le Toggle d'information n'est pas activer et si la main et l'avant-bras n'ont pas subis de rotation
                if (hit.transform.tag == "PoignetPlie" && Activation.RetourneActivation() == false && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    if (CalAngle1 >= AngleMin) // Si l'angle de flexion calculé est plus grand ou égal à l'angle max de la flexion
                    {
                        variablex = -3; // prend la valeur de -3
                        Poignet.transform.Rotate(variablex, 0, 0); // rotation du poignet
                    }
                    else
                    {
                        MessageLimite1.SetActive(true); // Active le MessageLimite1
                        StartCoroutine(DetMessageLimite()); // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    }
                }
                // Si l'utilisateur clique sur le poignet extension, si le Toggle d'information n'est pas activer et si la main et l'avant-bras n'ont pas subis de rotation
                if (hit.transform.tag == "PoignetDeplie" && Activation.RetourneActivation() == false && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    if (CalAngle2 <= AngleMax) // Si l'angle d'extension calculé est plus petit ou égal à l'angle max de l'extension
                    {
                        variablex = 3; // prend la valeur de 3
                        Poignet.transform.Rotate(variablex, 0, 0); // rotation du poignet
                    }
                    else
                    {
                        MessageLimite2.SetActive(true); // Active le MessageLimite2
                        StartCoroutine(DetMessageLimite()); // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    }
                }
            }
        }
        // Calcul la valeur de l'angle de flexion et d'extension
        CalAngle1 = (Poignet.transform.rotation.x) * 100;
        CalAngle2 = (Poignet.transform.rotation.x) * 100;
    }

    // Fonction qui permet, selon le yield, d'avoir une certaine pause entre l'activation et la désactivation des textes 
    IEnumerator DetMessageLimite() // IEnumerator est utiliser pour faire une pause
    {
        yield return new WaitForSeconds(2f); // Arrête l'exécution de la coroutine pendant 2 secondes   
        MessageLimite1.SetActive(false); // Désactive le MessageLimite1
        MessageLimite2.SetActive(false); // Désactive le MessageLimite2
    }
}
