using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    public UFO ufoPrefab;

    public int spawncooldown;
    public float spawnDistance = 12.0f;
    public float spawnRate = 1.0f;

    public int amountPerSpawn = 1;

    [Range(0.0f, 45.0f)]
    public float trajectoryVariance = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    public void Spawn()
    {
        
        for (int i = 0; i < this.amountPerSpawn; i++)
        {

            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * this.spawnDistance;
            spawnPoint += this.transform.position;
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            UFO ufo = Instantiate(this.ufoPrefab, spawnPoint, rotation);


        }
    }
}
