using UnityEngine;
using UnityEngine.PlayerLoop;

public class DirtGenerator : MonoBehaviour
{
    [SerializeField] float timeBetweenDirt;
    [SerializeField] Collider2D spawnZone;
    [SerializeField] GameObject dirtPrefab;

    [SerializeField] AudioSource audioSource;
    float actualTime;
    bool start;

    public static System.Action CreateDirt;
    private void OnEnable()
    {
        CreateDirt += SpawnDirt;
    }

    private void Awake()
    {
        ResetTimer();
    }

    [ContextMenu("Enable")]
    public void EnableSpawner() => start = true;

    [ContextMenu("Disable")]
    public void DisableSpawner() => start = false;

    private void Update()
    {
        if (!start) return;

        if (actualTime > 0)
        {
            actualTime -= Time.deltaTime;
        }
        else
        {
            ResetTimer();
            SpawnDirt();
        }
    }

    private void ResetTimer()
    {
        actualTime = timeBetweenDirt;
    }

    private void SpawnDirt()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        GameObject newDirt = Instantiate(dirtPrefab, GetRandomPosition(), Quaternion.identity, transform);
    }

    private Vector2 GetRandomPosition()
    {
        Bounds bounds = spawnZone.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(randomX, randomY);
    }
}
