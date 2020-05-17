using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Majeur : MonoBehaviour
{
    // Déclaration des objets
    // l'objet qui représent la phalange distale du majeur
    public GameObject BoutMajeur;
    // l'objet qui représent la phalange médiane du majeur
    public GameObject MoyenMajeur;
    // l'objet qui représent la phalange proximale du majeur
    public GameObject DebutMajeur;
    // Va prendre la position initiale des composantes du majeur
    public Quaternion BoutMajeurDRotation, MoyenMajeurDRotation, DebutMajeurDRotation;
    // Les angles d'Euler initials des composantes du majeur
    private float AngleDBout, AngleDMoyen, AngleDDebut;
    // Les angles d'Euler finals des composantes du majeur
    private float AngleFBout, AngleFMoyen, AngleFDebut;
    // Les angles D'Euler limites des composantes du majeur
    private float DifAngleBout = 90.0f, DifAngleMoyen = 40.0f, DifAngleDebut = 50.0f;
    // valeur des calculs entre les angles d'euler finals et les angles d'euler initials des composantes du majeur
    private float CalAngleBout, CalAngleMoyen, CalAngleDebut;
    // Les messages qui vont apparaitre en disant qu'il y a une erreur dans la flexion ou l'extension ou que le doigt a atteint sa limite
    // Un message guide qui va apparaitre pour aider à résoudre ces problèmes
    public GameObject MessageErreur, MessageErreur2, MessageGuide, MessageLimite;
    // le poignet de la main
    public GameObject PositionPoignet;
    // la position initiale du poignet de la main
    private float PositionPoignetD;
    // le GameObject représentant toute la main et l'avant-bras
    public GameObject ToutLaRotation;
    // la valeur de la rotation initiale de ToutLaRotation
    private Quaternion RotationInitiale;

    // Start is called before the first frame update
    void Start()
    {
        // Prend la valeur initiale de la rotation de la phalange distale du majeur
        BoutMajeurDRotation = BoutMajeur.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange médiane du majeur
        MoyenMajeurDRotation = MoyenMajeur.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange proximale du majeur
        DebutMajeurDRotation = DebutMajeur.transform.rotation;
        // Prend la valeur initiale de l'angle d'euler z de la phalange distale du majeur
        AngleDBout = BoutMajeur.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange médiane du majeur
        AngleDMoyen = MoyenMajeur.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange proximale du majeur
        AngleDDebut = DebutMajeur.transform.eulerAngles.z;
        // Prend la valeur initiale de la rotation x du poignet
        PositionPoignetD = PositionPoignet.transform.rotation.x;
        // Prend la valeur initiale de la rotation de l'ensemble de la main et de l'avant-bras
        RotationInitiale = ToutLaRotation.transform.rotation;
    }

    // Valeur des axes de rotation pour chacune des parties du majeur
    public float Majeurx1, Majeury1, Majeurz1 = 0;
    public float Majeurx2, Majeury2, Majeurz2 = 0;
    public float Majeurx3, Majeury3, Majeurz3 = 0;
    // le nombre de clique effectué sur chacune des partie du majeur
    private int nbrClickB = 0;
    private int nbrClickM = 0;
    private int nbrClickD = 0;

    // Update is called once per frame
    void Update()
    {
        // appelle la fonction CalculAngle
        CalculAngle();

        if (Input.GetMouseButtonDown(0)) // Regarde si la bouton droite de la souris a été appuyé
        {
            // Prend les valeurs du raycast
            RaycastHit hit;
            // Prend le rayon qui va de la caméra principale à un point d'écran 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) // Si le rayon croise un collisonneur(GameObject), il retourne vrai, sinon il retourne faux
            {
                 /* Si l'utilisateur clique sur la phalange distale du majeur, si les autres composantes du majeur n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoitMajeur" && nbrClickM == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    if (CalAngleBout <= DifAngleBout) // Si la valeur de la phalange distale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange distale
                    {
                            Majeurz1 = -3; // prend la valeur -3
                            BoutMajeur.transform.Rotate(Majeurx1, Majeury1, Majeurz1); // rotation de la phalange distale
                            nbrClickB++; // le nombre de clique pour la phalange distale augmente de 1
                    }
                    else
                    {
                         MessageLimite.SetActive(true); // Active le messageLimite 
                         // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                         StartCoroutine(DetMessageLimite());
                    }
                }
                // si l'utilisateur clique sur la phalange mediane ou proximale et que la phalange distale a subi une rotation et le poignet non
                if((hit.transform.tag == "MoyenDoitMajeur" || hit.transform.tag == "DebutDoitMajeur") && nbrClickB != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                /* Si l'utilisateur clique sur la phalange médiane du majeur, si les autres composantes du majeur n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoitMajeur" && nbrClickB == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange médiane du calcul de l'angle d'Euler z est plus grande ou égale à la limite de l'angle d'Euler z de la phalange médiane
                    if (CalAngleMoyen >= DifAngleMoyen) 
                    {
                            Majeurz2 = -3; // prend la valeur -3
                            MoyenMajeur.transform.Rotate(Majeurx2, Majeury2, Majeurz2); // rotation de la phalange médiane
                            nbrClickM++; // le nombre de clique pour la phalange médiane augmente de 1
                    }
                    else
                    {
                        MessageLimite.SetActive(true); // Active le messageLimite 
                        // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                        StartCoroutine(DetMessageLimite());
                    }
                }
                // si l'utilisateur clique sur la phalange distale ou proximale et que la phalange médiane a subi une rotation et le poignet non
                if((hit.transform.tag == "BoutDoitMajeur" || hit.transform.tag == "DebutDoitMajeur") && nbrClickM != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                       MessageErreur.SetActive(true); // Active le messageErreur
                       MessageGuide.SetActive(true); // Active le messageGuide
                       // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                       StartCoroutine(DisparaitreMessage());   
                }

                /* Si l'utilisateur clique sur la phalange proximale du majeur, si les autres composantes du majeur n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoitMajeur" && nbrClickB == 0 && nbrClickM == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange proximale du calcul de l'angle d'Euler z est plus grande ou égale à la limite de l'angle d'Euler z de la phalange proximale
                    // ou que la phalange proximale n'a pas encore subi de rotation
                    if (CalAngleDebut >= DifAngleDebut || nbrClickD == 0)
                    {
                            Majeurz3 = -3; // prend la valeur -3
                            DebutMajeur.transform.Rotate(Majeurx3, Majeury3, Majeurz3); // rotation de la phalange proximale
                            nbrClickD++; // le nombre de clique pour la phalange proximale augmente de 1
                    }
                    else
                    {
                        MessageLimite.SetActive(true); // Active le messageLimite 
                        // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                        StartCoroutine(DetMessageLimite());
                    }
                }
                // si l'utilisateur clique sur la phalange distale ou médiane et que la phalange proximale a subi une rotation et le poignet non
                if((hit.transform.tag == "BoutDoitMajeur" || hit.transform.tag == "MoyenDoitMajeur") && nbrClickD != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage()); 
                }

                 /* Si l'utilisateur clique sur la phalange distale extension du majeur, si le nombre de clique du distale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoigtMajeurD" && nbrClickB > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Majeurz1 = 3; // prend la valeur 3
                    BoutMajeur.transform.Rotate(Majeurx1, Majeury1, Majeurz1); // rotation de la phalange distale
                    nbrClickB--; // le nombre de clique pour la phalange distale diminue de 1
                }
                else
                if (hit.transform.tag == "BoutDoigtMajeurD" && nbrClickB <= 0) // Si l'utilisateur clique sur la phalange distale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange médiane extension du majeur, si le nombre de clique du médiane est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoigtMajeurD" && nbrClickM > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Majeurz2 = 3; // prend la valeur 3
                    MoyenMajeur.transform.Rotate(Majeurx2, Majeury2, Majeurz2); // rotation de la phalange médiane
                    nbrClickM--; // le nombre de clique pour la phalange médiane diminue de 1
                }
                else
                if (hit.transform.tag == "MoyenDoigtMajeurD" && nbrClickM <= 0) // Si l'utilisateur clique sur la phalange médiane extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange proximale extension du majeur, si le nombre de clique du proximale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoigtMajeurD" && nbrClickD > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Majeurz3 = 3; // prend la valeur 3
                    DebutMajeur.transform.Rotate(Majeurx3, Majeury3, Majeurz3); // rotation de la phalange proximale
                    nbrClickD--; // le nombre de clique pour la phalange proximale diminue de 1
                }
                else
                if ( hit.transform.tag == "DebutDoigtMajeurD" && nbrClickD <= 0) // Si l'utilisateur clique sur la phalange proximale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    StartCoroutine(DetMessageLimite()); // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                }

                // Si l'utilisateur clique sur n'importe quelle phalange flexion ou extension et si le poignet ou la main et l'avant-bras n'ont plus la même rotation
                if ((hit.transform.tag == "DebutDoigtMajeurD" || hit.transform.tag == "MoyenDoigtMajeurD" || hit.transform.tag == "BoutDoigtMajeurD" 
                || hit.transform.tag == "BoutDoitMajeur" || hit.transform.tag == "MoyenDoitMajeur" || hit.transform.tag == "DebutDoitMajeur") 
                && (PositionPoignetD != PositionPoignet.transform.rotation.x || ToutLaRotation.transform.rotation != RotationInitiale))
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                // Réinitialise les valeurs
                Majeurz1 = 0;
                Majeurz2 = 0;
                Majeurz3 = 0;
            }
        }
    }

    // Ramène le majeur et ses composantes à leur position initiale
    public void RetournePostionInitiale()
    {
        // Redonne la valeur initiale de rotation aux phalanges du majeur
        DebutMajeur.transform.rotation = DebutMajeurDRotation;
        MoyenMajeur.transform.rotation = MoyenMajeurDRotation;
        BoutMajeur.transform.rotation = BoutMajeurDRotation;
        // Réinitialise les valeurs des compteurs de clique
        nbrClickB = 0;
        nbrClickM = 0;
        nbrClickD = 0;
    }

    // Calcul l'angle d'Euler z entre l'angle d'Euler final et initial
    void CalculAngle()
    {
        // L'angle d'Euler z final prend la nouvelle valeur de l'angle d'Euler z des phalanges du majeur
        AngleFBout = BoutMajeur.transform.eulerAngles.z;
        AngleFMoyen = MoyenMajeur.transform.eulerAngles.z;
        AngleFDebut = DebutMajeur.transform.eulerAngles.z;

        CalAngleDebut = (-AngleFDebut) + (-AngleDDebut);
        if (CalAngleDebut <= -300.0f)
        {
           CalAngleDebut = CalAngleDebut + 200.0f;
           CalAngleDebut = - CalAngleDebut;
        }
        if (CalAngleDebut <= -200.0f)
        {
            CalAngleDebut = CalAngleDebut + 200.0f;
            CalAngleDebut = - CalAngleDebut;
        }
        CalAngleMoyen = -AngleDMoyen + (-AngleFMoyen);
        if (CalAngleMoyen >= 200.0f)
        {
            CalAngleMoyen = CalAngleMoyen - 200.0f;
        }
        if (CalAngleMoyen <= -200.0f)
        {
            CalAngleMoyen = CalAngleMoyen + 200.0f;
            CalAngleMoyen = -CalAngleMoyen;
        }
        if (CalAngleMoyen < 0)
        CalAngleMoyen = CalAngleMoyen + 100;

        CalAngleBout = (-AngleFBout) - (-AngleDBout);
    }

    // Fonction qui permet, selon le yield, d'avoir une certaine pause entre l'activation et la désactivation des textes 
    IEnumerator DisparaitreMessage() // IEnumerator est utiliser pour faire une pause
	{
		yield return new WaitForSeconds(4f); // Arrête l'exécution de la coroutine pendant 4 secondes
        MessageErreur.SetActive(false); // Désactive le MessageErreur
        MessageGuide.SetActive(false); // Désactive le MessageGuide
	}
    // Fonction qui permet, selon le yield, d'avoir une certaine pause entre l'activation et la désactivation des textes 
    IEnumerator DetMessageLimite() // IEnumerator est utiliser pour faire une pause
    {
        yield return new WaitForSeconds(2f); // Arrête l'exécution de la coroutine pendant 2 secondes 
        MessageLimite.SetActive(false); // Désactive le MessageLimite
        MessageErreur2.SetActive(false); // Désactive le MessageErreur2
    }
}
