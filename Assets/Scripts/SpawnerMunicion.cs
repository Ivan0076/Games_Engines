using UnityEngine;

public class SpawnerMunicion : MonoBehaviour
{
    public GameObject prefabMunicion;
    public float intervaloSpawn = 10f;
    public Vector3 areaMin;
    public Vector3 areaMax;

    void Start()
    {
        InvokeRepeating(nameof(SpawnMunicion), 2f, intervaloSpawn);
    }

    void SpawnMunicion()
    {
        Vector3 posicionAleatoria = new Vector3(
            Random.Range(areaMin.x, areaMax.x),
            Random.Range(areaMin.y, areaMax.y),
            Random.Range(areaMin.z, areaMax.z)
        );

        Instantiate(prefabMunicion, posicionAleatoria, Quaternion.identity);
    }
}
