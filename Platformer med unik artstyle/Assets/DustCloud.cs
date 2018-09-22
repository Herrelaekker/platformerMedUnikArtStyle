using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dette kan kun virke på FeetPos, ellers skal noget i scriptet laves om

public class DustCloud : MonoBehaviour {

    public GameObject dustCloud;
    private ParticleSystem ps;
    private Color tempColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //Finder farve på jorden -> laver dens opacitet om til 50 %
            tempColor = other.GetComponent<SpriteRenderer>().color;
            tempColor.a = .5f;

            //finder particlesystemet som vi bruger som dustcloud;
            ps = dustCloud.GetComponent<ParticleSystem>();
            //Laver maincolor fra particlesystemet om til en variabel
            //Dette er nødvendigt da man ikke kan sige ps.main.startColor
            var main = ps.main;
            //laver startfarven på particlesystemet om til den farve vi valgte tidligere
            main.startColor = tempColor;

            //laver dustclouden på feetposition
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
        }
    }
}