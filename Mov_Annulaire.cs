using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mov_Annulaire : MonoBehaviour
{
    // Déclaration des objets
    // l'objet qui représent la phalange distale de l'annulaire
    public GameObject BoutAnn;
    // l'objet qui représent la phalange médiane de l'annulaire
    public GameObject MoyenAnn;
    // l'objet qui représent la phalange proximale de l'annulaire
    public GameObject DebutAnn;
    // Va prendre la position initiale des composantes de l'annulaire
    public Quaternion BoutAnnDRotation, MoyenAnnDRotation, DebutAnnDRotation;
    // Les angles d'Euler initials des composantes de l'annulaire
    private float AngleDBout, AngleDMoyen, AngleDDebut;
    // Les angles d'Euler finals des composantes de l'annulaire
    private float AngleFBout, AngleFMoyen, AngleFDebut;
    // Les angles D'Euler limites des composantes de l'annulaire
    private float DifAngleBout = 57.0f, DifAngleMoyen = 60.0f, DifAngleDebut = 80.0f;
    // valeur des calculs entre les angles d'euler finals et les angles d'euler initials des composantes de l'annulaire
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
        // Prend la valeur initiale de la rotation de la phalange distale de l'annulaire
        BoutAnnDRotation = BoutAnn.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange médiane de l'annulaire
        MoyenAnnDRotation = MoyenAnn.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange proximale de l'annulaire
        DebutAnnDRotation = DebutAnn.transform.rotation;
        // Prend la valeur initiale de l'angle d'euler z de la phalange distale de l'annulaire
        AngleDBout = BoutAnn.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange médiane de l'annulaire
        AngleDMoyen = MoyenAnn.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange proximale de l'annulaire
        AngleDDebut = DebutAnn.transform.eulerAngles.z;
        // Prend la valeur initiale de la rotation x du poignet
        PositionPoignetD = PositionPoignet.transform.rotation.x;
        // Prend la valeur initiale de la rotation de l'ensemble de la main et de l'avant-bras
        RotationInitiale = ToutLaRotation.transform.rotation;
    }

    // Valeur des axes de rotation pour chacune des parties de l'annulaire
    public float Annx1, Anny1, Annz1 = 0;
    public float Annx2, Anny2, Annz2 = 0;
    public float Annx3, Anny3, Annz3 = 0;

    // le nombre de clique effectué sur chacune des partie de l'annulaire
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
                /* Si l'utilisateur clique sur la phalange distale de l'annulaire, si les autres composantes de l'annulaire n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoitAnn" && nbrClickM == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange distale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange distale
                    if (CalAngleBout <= DifAngleBout)
                    {
                            Annz1 = -3; // Annz1 prend la valeur -3
                            BoutAnn.transform.Rotate(Annx1, Anny1, Annz1); // rotation de la phalange distale
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
                if((hit.transform.tag == "MoyenDoitAnn" || hit.transform.tag == "DebutDoitAnn") && nbrClickB != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                /* Si l'utilisateur clique sur la phalange médiane de l'annulaire, si les autres composantes de l'annulaire n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoitAnn" && nbrClickB == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange médiane du calcul de l'angle d'Euler z est plus grande ou égale à la limite de l'angle d'Euler z de la phalange médiane
                    // ou que la phalange médiane n'a pas encore subi de rotation
                    if (CalAngleMoyen >= DifAngleMoyen || nbrClickM == 0)
                    {
                            Annz2 = -3; // Annz2 prend la valeur -3
                            MoyenAnn.transform.Rotate(Annx2, Anny2, Annz2); // rotation de la phalange médiane
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
                if((hit.transform.tag == "BoutDoitAnn" || hit.transform.tag == "DebutDoitAnn") && nbrClickM != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                       MessageErreur.SetActive(true); // Active le messageErreur
                       MessageGuide.SetActive(true); // Active le messageGuide
                       // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                       StartCoroutine(DisparaitreMessage());   
                }

                /* Si l'utilisateur clique sur la phalange proximale de l'annulaire, si les autres composantes de l'annulaire n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoitAnn" && nbrClickB == 0 && nbrClickM == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange proximale du calcul de l'angle d'Euler z est plus grande ou égale à la limite de l'angle d'Euler z de la phalange proximale
                    // ou que la phalange proximale n'a pas encore subi de rotation
                    if (CalAngleDebut >= DifAngleDebut || CalAngleDebut == 0)
                    {
                            Annz3 = -3; // Annz3 prend la valeur -3
                            DebutAnn.transform.Rotate(Annx3,Anny3, Annz3); // rotation de la phalange proximale
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
                if((hit.transform.tag == "BoutDoitAnn" || hit.transform.tag == "MoyenDoitAnn") && nbrClickD != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage()); 
                }
                /* Si l'utilisateur clique sur la phalange distale extension de l'annulaire, si le nombre de clique du distale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoigtAnnD" && nbrClickB > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Annz1 = 3; // Annz1 prend la valeur 3
                    BoutAnn.transform.Rotate(Annx1, Anny1, Annz1); // rotation de la phalange distale
                    nbrClickB--; // le nombre de clique pour la phalange distale diminue de 1
                }
                else
                if (hit.transform.tag == "BoutDoigtAnnD" && nbrClickB <= 0) // Si l'utilisateur clique sur la phalange distale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange médiane extension de l'annulaire, si le nombre de clique du médiane est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoigtAnnD" && nbrClickM > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Annz2 = 3; // Annz2 prend la valeur 3
                    MoyenAnn.transform.Rotate(Annx2, Anny2, Annz2); // rotation de la phalange médiane
                    nbrClickM--; // le nombre de clique pour la phalange médiane diminue de 1
                }
                else
                if (hit.transform.tag == "MoyenDoigtAnnD" && nbrClickM <= 0) // Si l'utilisateur clique sur la phalange médiane extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange proximale extension de l'annulaire, si le nombre de clique du proximale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoigtAnnD" && nbrClickD > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Annz3 = 3; // Annz3 prend la valeur 3
                    DebutAnn.transform.Rotate(Annx3,Anny3, Annz3); // rotation de la phalange proximale
                    nbrClickD--; // le nombre de clique pour la phalange proximale diminue de 1
                }
                else
                if ( hit.transform.tag == "DebutDoigtAnnD" && nbrClickD <= 0) // Si l'utilisateur clique sur la phalange proximale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                // Si l'utilisateur clique sur n'importe quelle phalange flexion ou extension et si le poignet ou la main et l'avant-bras n'ont plus la même rotation
                if ((hit.transform.tag == "DebutDoigtAnnD" || hit.transform.tag == "MoyenDoigtAnnD" || hit.transform.tag == "BoutDoigtAnnD" ||
                hit.transform.tag == "BoutDoitAnn" || hit.transform.tag == "MoyenDoitAnn" || hit.transform.tag == "DebutDoitAnn") && (
                 PositionPoignetD != PositionPoignet.transform.rotation.x || ToutLaRotation.transform.rotation != RotationInitiale))
                 {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                 }

                // Réinitialise les valeurs
                Annz1 = 0;
                Annz2 = 0;
                Annz3 = 0;
            }
        }
    }

    // Ramène l'annulaire et ses composantes à leur position initiale
    public void RetournePostionInitiale()
    {
        // Redonne la valeur initiale de rotation aux phalanges de l'annulaire
        DebutAnn.transform.rotation = DebutAnnDRotation;
        MoyenAnn.transform.rotation = MoyenAnnDRotation;
        BoutAnn.transform.rotation = BoutAnnDRotation;
        // Réinitialise les valeurs des compteurs de clique
        nbrClickB = 0;
        nbrClickM = 0;
        nbrClickD = 0;
    }

    // Calcul l'angle d'Euler z entre l'angle d'Euler final et initial
    void CalculAngle()
    {
        // L'angle d'Euler z final prend la nouvelle valeur de l'angle d'Euler z des phalanges de l'annulaire
        AngleFBout = BoutAnn.transform.eulerAngles.z;
        AngleFMoyen = MoyenAnn.transform.eulerAngles.z;
        AngleFDebut = DebutAnn.transform.eulerAngles.z;

        CalAngleDebut = (-AngleDDebut) - (-AngleFDebut);
        if (CalAngleDebut >= 300.0f)
        {
           CalAngleDebut = CalAngleDebut - 200.0f;
        }
        if (CalAngleDebut >= 200.0f)
        {
            CalAngleDebut = CalAngleDebut - 200.0f;
        }
        CalAngleMoyen = (AngleFMoyen) + (AngleDMoyen);
        if (CalAngleMoyen >= 300.0f)
        {
           CalAngleMoyen = CalAngleMoyen - 200.0f;
        }
        if (CalAngleMoyen >= 200.0f)
        {
            CalAngleMoyen = CalAngleMoyen - 200.0f;
        }
        
        CalAngleBout = (-AngleFBout) + (-AngleDBout);
        if (CalAngleBout >= -300.0f)
        {
           CalAngleBout = CalAngleBout - 300.0f;
        }
        if (CalAngleBout <= -300.0f)
        {
            CalAngleBout = CalAngleBout + 300.0f;
            CalAngleBout = -CalAngleBout;
        }
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
