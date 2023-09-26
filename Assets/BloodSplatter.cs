using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class BloodSplatter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExtendBlood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExtendBlood() => StartCoroutine(nameof(ExtendBloodCoroutine));
    private IEnumerator ExtendBloodCoroutine()
    {
        yield return null;
        //yield return new WaitForSeconds(1);
        var particleSystem = GetComponent<ParticleSystem>();
        var particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int count = particleSystem.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            //particle.position = Vector3.zero;
            particle.remainingLifetime = 10f;
            particle.startLifetime = 10f;
        }
        particleSystem.SetParticles(particles, count);

        /*yield return null;
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        var m = particleSystem.main;
        m.startLifetimeMultiplier = 1000f;*/

        /*var ps = GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(1);
        ps.Stop();
        var main = ps.main;
        main.startLifetimeMultiplier = Mathf.Infinity;
        ps.Play();*/
    }
}
