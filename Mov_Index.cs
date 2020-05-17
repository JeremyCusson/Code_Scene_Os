using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Index : MonoBehaviour
{
    // Déclaration des objets
    // l'objet qui représent la phalange distale de l'index
    public GameObject BoutIndex;
    // l'objet qui représent la phalange médiane de l'index
    public GameObject MoyenIndex;
    // l'objet qui représent la phalange proximale de l'index
    public GameObject DebutIndex;
    // Va prendre la position initiale des composantes de l'index
    public Quaternion BoutIndexDR, MoyenIndexDR, DebutIndexDR;
    // Tout les GameObjects qui ont la main, l'avant-bras et les deux
    public GameObject PositionInitialR, PositionInitialR2, PositionInitialR3;
    // Va prendre la position initiale des GameObjects qui ont la main, l'avant-bras et les deux
    private Quaternion PositionI, PositionI2, PositionI3;
    // Les angles d'Euler initials des composantes de l'index
    private float AngleDBout, AngleDMoyen, AngleDDebut;
    // Les angles d'Euler finals des composantes de l'index
    private float AngleFBout, AngleFMoyen, AngleFDebut;
    // Les angles D'Euler limites des composantes de l'index
    private float DifAngleBout = 80.0f, DifAngleMoyen = 125.0f, DifAngleDebut = 90.0f;
    // valeur des calculs entre les angles d'euler finals et les angles d'euler initials des composantes de l'index
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
        // Prend la valeur initiale de la rotation de la phalange distale de l'index
        BoutIndexDR = BoutIndex.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange médiane de l'index
        MoyenIndexDR = MoyenIndex.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange proximale de l'index
        DebutIndexDR = DebutIndex.transform.rotation;
        // Prend la valeur initiale de l'angle d'euler z de la phalange distale de l'index
        AngleDBout = BoutIndex.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange médiane de l'index
        AngleDMoyen = MoyenIndex.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange proximale de l'index
        AngleDDebut = DebutIndex.transform.eulerAngles.z;
        // Prend la valeur de la rotation initiale des GameObjects qui ont la main, l'avant-bras et les deux
        PositionI = PositionInitialR.transform.rotation;
        PositionI2 = PositionInitialR2.transform.rotation;
        PositionI3 = PositionInitialR3.transform.rotation;

        // Prend la valeur initiale de la rotation x du poignet
        PositionPoignetD = PositionPoignet.transform.rotation.x;
        // Prend la valeur initiale de la rotation de l'ensemble de la main et de l'avant-bras
        RotationInitiale = ToutLaRotation.transform.rotation;
    }

    // Valeur des axes de rotation pour chacune des parties de l'index
    public float Indexx1, Indexy1, Indexz1 = 0;
    public float Indexx2, Indexy2, Indexz2 = 0;
    public float Indexx3, Indexy3, Indexz3 = 0;
    // le nombre de clique effectué sur chacune des partie de l'index
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
                 /* Si l'utilisateur clique sur la phalange distale de l'index, si les autres composantes de l'index n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoitIndex" && nbrClickM == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    if (CalAngleBout <= DifAngleBout) // Si la valeur de la phalange distale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange distale
                    {
                            Indexz1 = -3; // prend la valeur -3
                            BoutIndex.transform.Rotate(Indexx1, Indexy1, Indexz1); // rotation de la phalange distale
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
                if((hit.transform.tag == "MoyenDoitIndex" || hit.transform.tag == "DebutDoitIndex") && nbrClickB != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                /* Si l'utilisateur clique sur la phalange médiane de l'index, si les autres composantes de l'index n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoitIndex" && nbrClickB == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange médiane du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange médiane
                    if (CalAngleMoyen <= DifAngleMoyen) 
                    {
                            Indexz2 = -3; // prend la valeur -3
                            MoyenIndex.transform.Rotate(Indexx2, Indexy2, Indexz2); // rotation de la phalange médiane
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
                if((hit.transform.tag == "BoutDoitIndex" || hit.transform.tag == "DebutDoitIndex") && nbrClickM != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                       MessageErreur.SetActive(true); // Active le messageErreur
                       MessageGuide.SetActive(true); // Active le messageGuide
                       // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                       StartCoroutine(DisparaitreMessage());   
                }

                /* Si l'utilisateur clique sur la phalange proximale de l'index, si les autres composantes de l'index n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoitIndex" && nbrClickB == 0 && nbrClickM == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange proximale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange proximale
                    if (CalAngleDebut <= DifAngleDebut) 
                    {
                            Indexz3 = -3; // prend la valeur -3
                            DebutIndex.transform.Rotate(Indexx3, Indexy3, Indexz3); // rotation de la phalange proximale
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
                if((hit.transform.tag == "BoutDoitIndex" || hit.transform.tag == "MoyenDoitIndex") && nbrClickD != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage()); 
                }

                /* Si l'utilisateur clique sur la phalange distale extension de l'index, si le nombre de clique du distale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoitIndexD" && nbrClickB > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Indexz1 = 3; // prend la valeur 3
                    BoutIndex.transform.Rotate(Indexx1, Indexy1, Indexz1); // rotation de la phalange distale
                    nbrClickB--; // le nombre de clique pour la phalange distale diminue de 1
                }
                else
                if (hit.transform.tag == "BoutDoitIndexD" && nbrClickB <= 0) // Si l'utilisateur clique sur la phalange distale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange médiane extension de l'index, si le nombre de clique du médiane est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoitIndexD" && nbrClickM > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Indexz2 = 3; // prend la valeur 3
                    MoyenIndex.transform.Rotate(Indexx2, Indexy2, Indexz2); // rotation de la phalange médiane
                    nbrClickM--; // le nombre de clique pour la phalange médiane diminue de 1
                }
                else
                if (hit.transform.tag == "MoyenDoitIndexD" && nbrClickM <= 0) // Si l'utilisateur clique sur la phalange médiane extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange proximale extension de l'index, si le nombre de clique du proximale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoitIndexD" && nbrClickD > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Indexz3 = 3; // prend la valeur 3
                    DebutIndex.transform.Rotate(Indexx3, Indexy3, Indexz3); // rotation de la phalange proximale
                    nbrClickD--; // le nombre de clique pour la phalange proximale diminue de 1
                }
                else
                if (hit.transform.tag == "DebutDoitIndexD" && nbrClickM <= 0) // Si l'utilisateur clique sur la phalange proximale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    StartCoroutine(DetMessageLimite()); // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                }

                // Si l'utilisateur clique sur n'importe quelle phalange flexion ou extension et si le poignet ou la main et l'avant-bras n'ont plus la même rotation
                if ((hit.transform.tag == "DebutDoitIndexD" || hit.transform.tag == "MoyenDoitIndexD" || 
                hit.transform.tag == "BoutDoitIndexD" || hit.transform.tag == "BoutDoitIndex" || hit.transform.tag == "MoyenDoitIndex" 
                || hit.transform.tag == "DebutDoitIndex") && (PositionPoignetD != PositionPoignet.transform.rotation.x || ToutLaRotation.transform.rotation != RotationInitiale))
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                // Réinitialise les valeurs
                Indexz1 = 0;
                Indexz2 = 0;
                Indexz3 = 0;
            }
        }
    }

    // Ramène l'index et ses composantes à leur position initiale
    // Ramène la main et l'avant-bras à leur position initiale
    public void RetournePostionInitiale()
    {
        // Redonne la valeur initiale de rotation aux GameObjects qui ont la main, l'avant-bras et les deux
        PositionInitialR3.transform.rotation = PositionI3;
        PositionInitialR2.transform.rotation = PositionI2;
        PositionInitialR.transform.rotation = PositionI;
        // Redonne la valeur initiale de rotation aux phalanges de l'index
        DebutIndex.transform.rotation = DebutIndexDR;
        MoyenIndex.transform.rotation = MoyenIndexDR;
        BoutIndex.transform.rotation = BoutIndexDR;
        // Réinitialise les valeurs des compteurs de clique
        nbrClickB = 0;
        nbrClickM = 0;
        nbrClickD = 0;
    }

    // Calcul l'angle d'Euler z entre l'angle d'Euler final et initial
    void CalculAngle()
    {
        // L'angle d'Euler z final prend la nouvelle valeur de l'angle d'Euler z des phalanges de l'index
        AngleFBout = BoutIndex.transform.eulerAngles.z;
        AngleFMoyen = MoyenIndex.transform.eulerAngles.z;
        AngleFDebut = DebutIndex.transform.eulerAngles.z;

        CalAngleDebut = (-AngleFDebut) - (-AngleDDebut);
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
        yield return new WaitForSeconds(3f); // Arrête l'exécution de la coroutine pendant 3 secondes 
        MessageLimite.SetActive(false); // Désactive le MessageLimite
        MessageErreur2.SetActive(false); // Désactive le MessageErreur2
    }
}
