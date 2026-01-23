using System;
using UnityEngine;

public class HitBumper : MonoBehaviour
{

    [SerializeField] private int bumperValue = 50;

    //maak een variabele voor je Particle System aan
    private ParticleSystem ps;

    //Pas het datatype aan die je meegeeft met je action event van string naar Transform
    //public static event Action<string,int> onHitBumper;
    public static event Action<Transform, int> onHitBumper;


    private void Start()
    {
        //Vraag het Particle System Component op als de game start en bewaar hem in je variabele, zodat je er later dingen mee kunt doen
        ps = GetComponent<ParticleSystem>();

        //zet je particle system stil! (? checkt of er wel een particle system is.)
        ps?.Stop();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            //geef in plaats van de tag nu de transform mee aan het event. Dit is noodzakelijk voor de screenshake!
            onHitBumper?.Invoke(gameObject.transform, bumperValue);

            //zet je Particle System hem eerst weer stil voor het geval hij nog niet klaar was met de vorige loop
            ps?.Stop();

            //speel hem nu af vanaf het begin.
            ps?.Play();
        }
    }
}
