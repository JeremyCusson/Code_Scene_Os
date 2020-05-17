using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mov_Auriculaire : MonoBehaviour
{
    // Déclaration des objets
    // l'objet qui représent la phalange distale de l'auriculaire
    public GameObject BoutAuri;
    // l'objet qui représent la phalange médiane de l'auriculaire
    public GameObject MoyenAuri;
    // l'objet qui représent la phalange proximale de l'auriculaire
    public GameObject DebutAuri;
    // Va prendre la position initiale des composantes de l'auriculaire
    public Quaternion BoutAuriDRotation, MoyenAuriDRotation, DebutAuriDRotation;
    // Les angles d'Euler initials des composantes de l'auriculaire
    private float AngleDBout, AngleDMoyen, AngleDDebut;
    // Les angles d'Euler finals des composantes de l'auriculaire
    private float AngleFBout, AngleFMoyen, AngleFDebut;
    // Les angles D'Euler limites des composantes de l'auriculaire
    private float DifAngleBout = 90.0f, DifAngleMoyen = 135.0f, DifAngleDebut = 95.0f;
    // valeur des calculs entre les angles d'euler finals et les angles d'euler initials des composantes de l'auriculaire
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
        // Prend la valeur initiale de la rotation de la phalange distale de l'auriculaire
        BoutAuriDRotation = BoutAuri.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange médiane de l'auriculaire
        MoyenAuriDRotation = MoyenAuri.transform.rotation;
        // Prend la valeur initiale de la rotation de la phalange proximale de l'auriculaire
        DebutAuriDRotation = DebutAuri.transform.rotation;
        // Prend la valeur initiale de l'angle d'euler z de la phalange distale de l'auriculaire
        AngleDBout = BoutAuri.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange médiane de l'auriculaire
        AngleDMoyen = MoyenAuri.transform.eulerAngles.z;
        // Prend la valeur initiale de l'angle d'euler z de la phalange proximale de l'auriculaire
        AngleDDebut = DebutAuri.transform.eulerAngles.z;
        // Prend la valeur initiale de la rotation x du poignet
        PositionPoignetD = PositionPoignet.transform.rotation.x;
        // Prend la valeur initiale de la rotation de l'ensemble de la main et de l'avant-bras
        RotationInitiale = ToutLaRotation.transform.rotation;
    }

    // Valeur des axes de rotation pour chacune des parties de l'auriculaire
    public float Aurix1, Auriy1, Auriz1 = 0;
    public float Aurix2, Auriy2, Auriz2 = 0;
    public float Aurix3, Auriy3, Auriz3 = 0;

    // le nombre de clique effectué sur chacune des partie de l'auriculaire
    private int nbrClickB = 0;
    private int nbrClickM = 0;
    private int nbrClickD = 0;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // Regarde si la bouton droite de la souris a été appuyé
        {
            // Prend les valeurs du raycast
            RaycastHit hit;
            // Prend le rayon qui va de la caméra principale à un point d'écran 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) // Si le rayon croise un collisonneur(GameObject), il retourne vrai, sinon il retourne faux
            {
                // appelle la fonction CalculAngle
                CalculAngle();

                 /* Si l'utilisateur clique sur la phalange distale de l'auriculaire, si les autres composantes de l'auriculaire n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoigtAuri" && nbrClickM == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    if (CalAngleBout <= DifAngleBout) // Si la valeur de la phalange distale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange distale
                    {
                            Auriz1 = -3; // prend la valeur -3
                            BoutAuri.transform.Rotate(Aurix1, Auriy1, Auriz1); // rotation de la phalange distale
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
                if((hit.transform.tag == "MoyenDoigtAuri" || hit.transform.tag == "DebutDoigtAuri") && nbrClickB != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                /* Si l'utilisateur clique sur la phalange médiane de l'auriculaire, si les autres composantes de l'auriculaire n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoigtAuri" && nbrClickB == 0 && nbrClickD == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    // Si la valeur de la phalange médiane du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange médiane
                    if (CalAngleMoyen <= DifAngleMoyen) 
                    {
                            Auriz2 = -3; // prend la valeur -3
                            MoyenAuri.transform.Rotate(Aurix2, Auriy2, Auriz2); // rotation de la phalange médiane
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
                if((hit.transform.tag == "BoutDoigtAuri" || hit.transform.tag == "DebutDoigtAuri") && nbrClickM != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                       MessageErreur.SetActive(true); // Active le messageErreur
                       MessageGuide.SetActive(true); // Active le messageGuide
                       // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                       StartCoroutine(DisparaitreMessage());   
                }

                /* Si l'utilisateur clique sur la phalange proximale de l'auriculaire, si les autres composantes de l'auriculaire n'ont pas changées 
                de rotation, si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoigtAuri" && nbrClickB == 0 && nbrClickM == 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    if (CalAngleDebut <= DifAngleDebut) // Si la valeur de la phalange proximale du calcul de l'angle d'Euler z est plus petite ou égale à la limite de l'angle d'Euler z de la phalange proximale
                    {
                            Auriz3 = -3; // prend la valeur -3
                            DebutAuri.transform.Rotate(Aurix3,Auriy3, Auriz3); // rotation de la phalange proximale
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
                if((hit.transform.tag == "BoutDoigtAuri" || hit.transform.tag == "MoyenDoigtAuri") && nbrClickD != 0 && PositionPoignetD == PositionPoignet.transform.rotation.x)
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage()); 
                }

                /* Si l'utilisateur clique sur la phalange distale extension de l'auriculaire, si le nombre de clique du distale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "BoutDoigtAuriD" && nbrClickB > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Auriz1 = 3; // prend la valeur 3
                    BoutAuri.transform.Rotate(Aurix1, Auriy1, Auriz1); // rotation de la phalange distale
                    nbrClickB--; // le nombre de clique pour la phalange distale diminue de 1
                }
                else
                if (hit.transform.tag == "BoutDoigtAuriD" && nbrClickB <= 0) // Si l'utilisateur clique sur la phalange distale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }
                /* Si l'utilisateur clique sur la phalange médiane extension de l'auriculaire, si le nombre de clique du médiane est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "MoyenDoigtAuriD" && nbrClickM > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Auriz2 = 3; // prend la valeur 3
                    MoyenAuri.transform.Rotate(Aurix2, Auriy2, Auriz2); // rotation de la phalange médiane
                    nbrClickM--; // le nombre de clique pour la phalange médiane diminue de 1
                }
                else
                if (hit.transform.tag == "MoyenDoigtAuriD" && nbrClickM <= 0) // Si l'utilisateur clique sur la phalange médiane extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                    StartCoroutine(DetMessageLimite());
                }

                /* Si l'utilisateur clique sur la phalange proximale extension de l'auriculaire, si le nombre de clique du proximale est plus grand que 0,
                si la position initiale du poignet n'a pas changé et si la main et l'avant-bras n'ont pas subis de rotation*/
                if (hit.transform.tag == "DebutDoigtAuriD" && nbrClickD > 0 && PositionPoignetD == PositionPoignet.transform.rotation.x 
                && ToutLaRotation.transform.rotation == RotationInitiale)
                {
                    Auriz3 = 3; // prend la valeur 3
                    DebutAuri.transform.Rotate(Aurix3,Auriy3, Auriz3); // rotation de la phalange proximale
                    nbrClickD--; // le nombre de clique pour la phalange proximale diminue de 1
                }
                else
                if ( hit.transform.tag == "DebutDoigtAuriD" && nbrClickD <= 0) // Si l'utilisateur clique sur la phalange proximale extension et que le nombre de clique est plus petit ou égal à 0
                {
                    MessageErreur2.SetActive(true); // Active le messageErreur2
                    StartCoroutine(DetMessageLimite()); // Débute la fonction DetMessageLimite comme une coroutine qui peut arrêter son exécution
                }

                // Si l'utilisateur clique sur n'importe quelle phalange flexion ou extension et si le poignet ou la main et l'avant-bras n'ont plus la même rotation
                if ((hit.transform.tag == "DebutDoigtAuriD" || hit.transform.tag == "MoyenDoigtAuriD" || hit.transform.tag == "BoutDoigtAuriD"
                || hit.transform.tag == "BoutDoigtAuri" || hit.transform.tag == "MoyenDoigtAuri" || hit.transform.tag == "DebutDoigtAuri") && 
                (PositionPoignetD != PositionPoignet.transform.rotation.x || ToutLaRotation.transform.rotation != RotationInitiale))
                {
                   MessageErreur.SetActive(true); // Active le messageErreur
                   MessageGuide.SetActive(true); // Active le messageGuide
                   // Débute la fonction DisparaitreMessage comme une coroutine qui peut arrêter son exécution
                   StartCoroutine(DisparaitreMessage());
                }

                // Réinitialise les valeurs
                Auriz1 = 0;
                Auriz2 = 0;
                Auriz3 = 0;
            }
        }
    }

    // Ramène l'auriculaire et ses composantes à leur position initiale
    public void RetournePostionInitiale()
    {
        // Redonne la valeur initiale de rotation aux phalanges de l'auriculaire
        DebutAuri.transform.rotation = DebutAuriDRotation;
        MoyenAuri.transform.rotation = MoyenAuriDRotation;
        BoutAuri.transform.rotation = BoutAuriDRotation;
        // Réinitialise les valeurs des compteurs de clique
        nbrClickB = 0;
        nbrClickM = 0;
        nbrClickD = 0;
    }

    // Calcul l'angle d'Euler z entre l'angle d'Euler final et initial
    void CalculAngle()
    {
        // L'angle d'Euler z final prend la nouvelle valeur de l'angle d'Euler z des phalanges de l'auriculaire
        AngleFBout = BoutAuri.transform.eulerAngles.z;
        AngleFMoyen = MoyenAuri.transform.eulerAngles.z;
        AngleFDebut = DebutAuri.transform.eulerAngles.z;

        CalAngleDebut = (-AngleFDebut) - (-AngleDDebut);
        CalAngleMoyen = (-AngleFMoyen) - (-AngleDMoyen);
        CalAngleBout = (-AngleFBout) + (AngleDBout);
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
