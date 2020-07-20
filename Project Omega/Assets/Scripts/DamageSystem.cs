using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : MonoBehaviour
{
    private List<ParticleSystem> explosionFX;
    private ShipSubsystems shipSubsystems;
    private bool exploding;

    public float duration = 10;
    public float explosionSpacingMin = 1;
    public float explosionSpacingMax = 5;

    public GameObject thrusterFore;
    public GameObject thrusterAft;

    // Start is called before the first frame update
    void Start()
    {
        explosionFX = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
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
            shipSubsystems.DamageSubsystem();
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
