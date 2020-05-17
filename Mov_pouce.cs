using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_pouce : MonoBehaviour
{
    // Déclaration des objets
    // l'objet qui représent la phalange distale du pouce
    public GameObject BoutPouce;
    // l'objet qui représent la phalange proximale du pouce
    public GameObject MoyenPouce;
    // l'objet qui représent le métacarpe du pouce
    public GameObject DebutPouce;
    // Va prendre la position initiale des composantes du pouce
    private Quaternion BoutPouceDRotation, MoyenPouceDRotation, DebutPouceDRotation;
    // Les angles d'Euler initials des composantes du pouce
    private float AngleDBout, AngleDMoyen, AngleDDebut;
    // Les angles d'Euler finals des composantes du pouce
    private float AngleFBout, AngleFMoyen, AngleFDebut;
    // Les angles D'Euler limites des composantes du pouce
    private float DifAngleBout = 90.0f, DifAngleMoyen = 80.0f, DifAngleDebut = 20.0f;
    // valeur des calculs entre les angles d'euler finals et les angles d'euler initials des composantes du pouce
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
        // Prend la valeur initiale de la rotation des composantes du pouce
        BoutPouceDRotation = BoutPouce.transform.rotation;
        MoyenPouceDRotation = MoyenPouce.transform.rotation;
        DebutPouceDRotation = DebutPouce.transform.rotation;
        // Prend la valeur initiale de l'angle d'euler z des composantes du pouce
        AngleDBout = BoutPouce.transform.eulerAngles.z;
        AngleDMoyen = MoyenPouce.transform.eulerAngles.z;
        AngleDDebut = DebutPouce.transform.eulerAngles.z;
        // Prend la valeur initiale de la rotation x du poignet
        PositionPoignetD = PositionPoignet.transform.rotation.x;
        // Prend la valeur initiale de la rotation de l'ensemble de la main et de l'avant-bras
        RotationInitiale = ToutLaRotation.transform.rotation;
    }

    // Valeur des axes de rotation pour chacune des parties du pouce
    private float Poucex1 = 0, Poucey1 = 0, Poucez1 = 0;
    public float Poucex2 = 0, Poucey2 = 0, Poucez2 = 0;
    public float Poucex3 = 0, Poucey3 = 0, Poucez3 = 0;

    // le nombre de clique effectué sur chacune des partie du pouce
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
                /* Si l'utilisateur clique sur la phalange distale du pouce, si les autres composantes du pouce n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutPouce" && nbrClickM == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange distale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange distale
                    if (CalAngleBout <= DifAngleBout)
                    {
                        Poucez1 = -3; // prend la valeur -3
                        BoutPouce.transform.Rotate(Poucex1, Poucey1, Poucez1); // rotation de la phalange distale
                        nbrClickB++; // le nombre de clique pour la phalange distale augmente de 1
                    }
                    else
                    {
                         MessageLimite.SetActive(true); // Active le messageLimite 
                         // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                         StartCoroutine(DetMessageLimite());
                    }
                }
                // si l'utilisateur clique sur la phalange proximale ou le métacarpe et que la phalange distale a subi une rotation et le poignet non
                if((hit.transform.tag == "MoyenPouce" || hit.transform.tag == "DebutPouce") && nbrClickB != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                /* Si l'utilisateur clique sur la phalange proximale du pouce, si les autres composantes du pouce n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenPouce" && nbrClickB == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange proximale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange proximale
                    if (CalAngleMoyen <= DifAngleMoyen)
                    {
                            Poucey2 = -0.5f; // prend la valeur -0.5f
                            Poucez2 = -3; // prend la valeur -3
                            MoyenPouce.transform.Rotate(Poucex2, Poucey2, Poucez2); // rotation de la phalange proximale
                            nbrClickM++; // le nombre de clique pour la phalange proximale augmente de 1
                    }
                    else
                    {
                        MessageLimite.SetActive(true); // Active le messageLimite 
                        // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                        StartCoroutine(DetMessageLimite());
                    }
                }
                // si l'utilisateur clique sur la phalange distale ou le métacarpe et que la phalange proximale a subi une rotation et le poignet non
                if((hit.transform.tag == "BoutPouce" || hit.transform.tag == "DebutPouce") && nbrClickM != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                       MessageErreur.SetActive(true); // Active le messageErreur
                       MessageGuide.SetActive(true); // Active le messageGuide
                       StartCoroutine(DisparaitreMessage()); // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution    
                }

                /* Si l'utilisateur clique sur le métacarpe du pouce, si les autres composantes du pouce n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutPouce" && nbrClickB == 0 && nbrClickM == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur du métacarpe du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z du métacarpe
                    if (CalAngleDebut <= DifAngleDebut)
                    {
                            Poucey3 = -2; // prend la valeur -2
                            Poucez3 = -3; // prend la valeur -3
                            DebutPouce.transform.Rotate(Poucex3,Poucey3, Poucez3); // rotation du métacarpe
                            nbrClickD++; // le nombre de clique pour le métacarpe augmente de 1
                    }
                    else
                    {
                        MessageLimite.SetActive(true); // Active le messageLimite 
                        // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                        StartCoroutine(DetMessageLimite());
                    }
                }
                // si l'utilisateur clique sur la phalange distale ou proximale et que le métacarpe a subi une rotation et le poignet non
                if((hit.transform.tag == "BoutPouce" || hit.transform.tag == "MoyenPouce") && nbrClickD != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage()); 
                }

                /* Si l'utilisateur clique sur la phalange distale extension du pouce, si le nombre de clique du distale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutPouceD" && nbrClickB > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Poucez1 = 3; // prend la valeur 3
                    BoutPouce.transform.Rotate(Poucex1, Poucey1, Poucez1); // rotation de la phalange distale
                    nbrClickB--; // le nombre de clique pour la phalange distale diminue de 1
                }
                else
                if (hit.transform.tag == "BoutPouceD" && nbrClickB <= 0) // Si l'utilisateur clique sur la phalange distale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange proximale extension du pouce, si le nombre de clique du proximale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenPouceD" && nbrClickM > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Poucey2 = 0.5f; // prend la valeur 0.5f
                    Poucez2 = 3; // prend la valeur 3
                    MoyenPouce.transform.Rotate(Poucex2, Poucey2, Poucez2); // rotation de la phalange proximale
                    nbrClickM--; // le nombre de clique pour la phalange proximale diminue de 1
                }
                else
                if (hit.transform.tag == "MoyenPouceD" && nbrClickM <= 0) // Si l'utilisateur clique sur la phalange proximale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur le métacarpe extension du pouce, si le nombre de clique du métacarpe est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutPouceD" && nbrClickD > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Poucey3 = 2; // prend la valeur 2
                    Poucez3 = 3; // prend la valeur 3
                    DebutPouce.transform.Rotate(Poucex3,Poucey3, Poucez3); // rotation du métacarpe
                    nbrClickD--; // le nombre de clique pour du métacarpe diminue de 1
                }
                else
                if ( hit.transform.tag == "DebutPouceD" && nbrClickD <= 0) // Si l'utilisateur clique sur le métacarpe extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    StartCoroutine(DetMessageLimite()); // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                }

                // Si l'utilisateur clique sur n'importe quelle phalange flexion ou extension ou sur le métacarpe et si le poignet ou la main et l'avant-bras n'ont plus la même rotation
                if ((hit.transform.tag == "DebutPouceD" || hit.transform.tag == "MoyenPouceD" || hit.transform.tag == "BoutPouceD" ||
                hit.transform.tag == "BoutPouce" || hit.transform.tag == "MoyenPouce" || hit.transform.tag == "DebutPouce") && 
                (PositionPoignetD != PositionPoignet.transform.rotation.x || ToutLaRotation.transform.rotation != RotationInitiale))
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                // Réinitialise les valeurs
                Poucez1 = 0;
                Poucez2 = 0;
                Poucez3 = 0;
            }
        }
    }

    // Ramène le pouce et ses composantes à leur position initiale
    public void RetournePostionInitiale()
    {
        // Redonne la valeur initiale de rotation aux phalanges et au métacarpe du pouce
        DebutPouce.transform.rotation = DebutPouceDRotation;
        MoyenPouce.transform.rotation = MoyenPouceDRotation;
        BoutPouce.transform.rotation = BoutPouceDRotation;
        // Réinitialise les valeurs des compteurs de clique
        nbrClickB = 0;
        nbrClickM = 0;
        nbrClickD = 0;
    }

    // Calcul l'angle d'Euler z entre l'angle d'Euler final et initial
    void CalculAngle()
    {
        // L'angle d'Euler z final prend la nouvelle valeur de l'angle d'Euler z des phalanges et du métacarpe du pouce
        AngleFBout = BoutPouce.transform.eulerAngles.z;
        AngleFMoyen = MoyenPouce.transform.eulerAngles.z;
        AngleFDebut = DebutPouce.transform.eulerAngles.z;

        CalAngleDebut = (-AngleFDebut) - (-AngleDDebut);
        if (CalAngleDebut >= 300.0f)
        {
            CalAngleDebut = CalAngleDebut - 300.0f;
        }
        if (CalAngleDebut <= -300.0f)
        {
            CalAngleDebut = CalAngleDebut + 300.0f;
            CalAngleDebut = -CalAngleDebut;
        }
        CalAngleMoyen = (-AngleFMoyen) - (-AngleDMoyen);
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
