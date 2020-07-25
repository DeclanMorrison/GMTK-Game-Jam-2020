using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : MonoBehaviour
{
    private List<ParticleSystem> explosionFX;
    private List<AudioSource> explosionSounds;

    private AudioSource rumbleSound;

    private ShipSubsystems shipSubsystems;
    private bool exploding;

    public float duration = 10;
    public float explosionSpacingMin = 1;
    public float explosionSpacingMax = 5;

    public GameObject thrusterFore;
    public GameObject thrusterAft;

    public GameObject firePrefab;

    // Start is called before the first frame update
    void Start()
    {
        explosionFX = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        explosionSounds = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        rumbleSound = GetComponent<AudioSource>();
        shipSubsystems = GetComponentInParent<ShipSubsystems>();
    }

    public void StartGameOver()
    {
        StartCoroutine(BlowUpShip());
        StartCoroutine(SpawnExplosions());
    }

    private IEnumerator SpawnExplosions()
    {
        while (exploding)
        {
            //Spawn new explosions
            float timeUntilExplosion = UnityEngine.Random.Range(explosionSpacingMin, explosionSpacingMax);
            yield return new WaitForSeconds(timeUntilExplosion);
            int explosionIndex = UnityEngine.Random.Range(0, explosionFX.Count);
            explosionFX[explosionIndex].Play();
            explosionSounds[explosionIndex].Play();
            shipSubsystems.DamageSubsystem();
        }
    }

    internal void SpawnVisualDamage(float health, ContactPoint2D[] contacts)
    {
        //Calculate Average Point
        Vector2 averagePoint = Vector2.zero;
        for (int i = 0; i < contacts.Length; i++)
        {
            averagePoint += contacts[i].point;
        }
        averagePoint /= contacts.Length;

        //Spawn FX there based on severity
        if (health < 50)
        {
            Instantiate(firePrefab, averagePoint, transform.rotation * Quaternion.Euler(0, 0, 180), transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BlowUpShip()
    {
        exploding = true;
        //Disable thrusters (TODO remove once thruster visual damage is finished)
        thrusterFore.SetActive(false);
        thrusterAft.SetActive(false);
        //Wait and then load end scene
        yield return new WaitForSeconds(duration);
        SceneManager.LoadSceneAsync(2);
    }
}
