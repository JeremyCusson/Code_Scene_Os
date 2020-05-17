using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Description : MonoBehaviour
{
    // Tous les objets qui représentent tous les muscles de la main et de l'avant-bras
    public GameObject AdducteurDuPouce, CourtFlechisseurDuPouce, ExtenseurDoigts, ExtenseurRadialDuCarpe, ExtenseurUlnaireDuCarpe, 
    FlechisseurRadialDuCarpe, FlechisseurSuperficiel, LombricauxAnulaire, LombricauxIMajeur, LombricauxIndex, LombricauxOriculaire,
    LongExtenseurDuPousse, LongFlechisseurDuPouce, ReticulumDesMuscles;
    // les Valeurs qui définissent si le Toggle est activé ou non pour chacun des muscles de la main et de l'avant-bras
    private bool Activer, Activer2, Activer3, Activer4, Activer5, Activer6, Activer7, Activer8, Activer9, Activer10, Activer11, Activer12, Activer13, Activer14;

    // Active ou désactive l'objet du muscle adducteur du pouce
    public void ActivationAdducteurDuPouce(bool IsOnOrFalse)
    {
        Activer = IsOnOrFalse; // Activer prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer == false)
        {
            AdducteurDuPouce.SetActive(false); // Désactive l'objet de l'adducteur du pouce
        }
        if (Activer == true)
        {
            AdducteurDuPouce.SetActive(true); // Active et affiche l'objet de l'adducteur du pouce
        }
    }

    // Active ou désactive l'objet du muscle fléchisseur du pouce
    public void ActivationCourtFlechisseurDuPouce(bool IsOnOrFalse)
    {
        Activer2 = IsOnOrFalse; // Activer2 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer2 == false)
        {
            CourtFlechisseurDuPouce.SetActive(false); // Désactive l'objet du fléchisseur du pouce
        }
        if (Activer2 == true)
        {
            CourtFlechisseurDuPouce.SetActive(true); // Active et affiche l'objet du fléchisseur du pouce
        }
    }

    // Active ou désactive l'objet du muscle extenseur des doigts
    public void ActivationExtenseurDoigts(bool IsOnOrFalse)
    {
        Activer3 = IsOnOrFalse; // Activer3 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer3 == false)
        {
            ExtenseurDoigts.SetActive(false); // Désactive l'objet de l'extenseur des doigts
        }
        if (Activer3 == true)
        {
            ExtenseurDoigts.SetActive(true); // Active et affiche l'objet de l'extenseur des doigts
        }
    }

    // Active ou désactive l'objet du muscle extenseur radial du carpe
    public void ActivationExtenseurRadialDuCarpe(bool IsOnOrFalse)
    {
        Activer4 = IsOnOrFalse; // Activer4 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer4 == false)
        {
            ExtenseurRadialDuCarpe.SetActive(false); // Désactive l'objet de l'extenseur radial du carpe
        }
        if (Activer4 == true)
        {
            ExtenseurRadialDuCarpe.SetActive(true); // Active et affiche l'objet de l'extenseur radial du carpe
        }
    }

    // Active ou désactive l'objet du muscle extenseur ulnaire du carpe
    public void ActivationExtenseurUlnaireDuCarpe(bool IsOnOrFalse)
    {
        Activer5 = IsOnOrFalse; // Activer5 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer5 == false)
        {
            ExtenseurUlnaireDuCarpe.SetActive(false); // Désactive l'objet de l'extenseur ulnaire du carpe
        }
        if (Activer5 == true)
        {
            ExtenseurUlnaireDuCarpe.SetActive(true); // Active et affiche l'objet de l'extenseur ulnaire du carpe
        }
    }

    // Active ou désactive l'objet du muscle fléchisseur radial du carpe
    public void ActivationFlechisseurRadialDuCarpe(bool IsOnOrFalse)
    {
        Activer6 = IsOnOrFalse; // Activer6 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer6 == false)
        {
            FlechisseurRadialDuCarpe.SetActive(false); // Désactive l'objet de fléchisseur radial du carpe
        }
        if (Activer6 == true)
        {
            FlechisseurRadialDuCarpe.SetActive(true); // Active et affiche l'objet du fléchisseur radial du carpe
        }
    }

    // Active ou désactive l'objet du muscle fléchisseur superficiel
    public void ActivationFlechisseurSuperficiel(bool IsOnOrFalse)
    {
        Activer7 = IsOnOrFalse; // Activer7 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer7 == false)
        {
            FlechisseurSuperficiel.SetActive(false); // Désactive l'objet du fléchisseur superficiel
        }
        if (Activer7 == true)
        {
            FlechisseurSuperficiel.SetActive(true); // Active et affiche l'objet du fléchisseur superficiel
        }
    }

    // Active ou désactive l'objet du muscle lombricaux de l'annulaire
    public void ActivationLombricauxAnulaire(bool IsOnOrFalse)
    {
        Activer8 = IsOnOrFalse; // Activer8 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer8 == false)
        {
            LombricauxAnulaire.SetActive(false); // Désactive l'objet des lombricaux de l'annulaire
        }
        if (Activer8 == true)
        {
            LombricauxAnulaire.SetActive(true); // Active et affiche l'objet des lombricaux de l'annulaire
        }

    }

    // Active ou désactive l'objet du muscle lombricaux du majeur
    public void ActivationLombricauxIMajeur(bool IsOnOrFalse)
    {
        Activer9 = IsOnOrFalse; // Activer9 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer9 == false)
        {
            LombricauxIMajeur.SetActive(false); // Désactive l'objet des lombricaux du majeur
        }
        if (Activer9 == true)
        {
            LombricauxIMajeur.SetActive(true); // Active et affiche l'objet des lombricaux du majeur
        }
    }

    // Active ou désactive l'objet du muscle lombricaux de l'index
    public void ActivationLombricauxIndex(bool IsOnOrFalse)
    {
        Activer10 = IsOnOrFalse; // Activer10 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer10 == false)
        {
            LombricauxIndex.SetActive(false); // Désactive l'objet des lombricaux de l'index
        }
        if (Activer10 == true)
        {
            LombricauxIndex.SetActive(true); // Active et affiche l'objet des lombricaux de l'index
        }
    }

    // Active ou désactive l'objet du muscle lombricaux de l'auriculaire
    public void ActivationLombricauxOriculaire(bool IsOnOrFalse)
    {
        Activer11 = IsOnOrFalse; // Activer11 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer11 == false)
        {
            LombricauxOriculaire.SetActive(false); // Désactive l'objet des lombricaux de l'auriculaire
        }
        if (Activer11 == true)
        {
            LombricauxOriculaire.SetActive(true); // Active et affiche l'objet des lombricaux de l'auriculaire
        }
    }

    // Active ou désactive l'objet du muscle du long extenseur du pouce
    public void ActivationLongExtenseurDuPousse(bool IsOnOrFalse)
    {
        Activer12 = IsOnOrFalse; // Activer12 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer12 == false)
        {
            LongExtenseurDuPousse.SetActive(false); // Désactive l'objet du long extenseur du pouce
        }
        if (Activer12 == true)
        {
            LongExtenseurDuPousse.SetActive(true); // Active et affiche l'objet du long extenseur du pouce
        }
    }

    // Active ou désactive l'objet du muscle du long fléchisseur du pouce 
    public void ActivationLongFlechisseurDuPouce(bool IsOnOrFalse)
    { 
        Activer13 = IsOnOrFalse; // Activer13 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer13 == false)
        {
            LongFlechisseurDuPouce.SetActive(false); // Désactive l'objet du long fléchisseur du pouce 
        }
        if (Activer13 == true)
        {
            LongFlechisseurDuPouce.SetActive(true); // Active et affiche l'objet du long fléchisseur du pouce 
        }
    }

    // Active ou désactive l'objet du réticulum des muscles
    public void ActivationReticulumDesMuscles(bool IsOnOrFalse)
    {
        Activer14 = IsOnOrFalse; // Activer14 prend la valeur de IsOnOrFalse envoyée par le Toogle
        if (Activer14 == false)
        {
            ReticulumDesMuscles.SetActive(false); // Désactive l'objet du réticulum des muscles
        }
        if (Activer14 == true)
        {
            ReticulumDesMuscles.SetActive(true); // Active et affiche l'objet du réticulum des muscles
        }
    }
}
