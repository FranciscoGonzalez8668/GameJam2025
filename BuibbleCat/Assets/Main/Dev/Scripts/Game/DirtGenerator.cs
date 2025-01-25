using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DirtGenerator : MonoBehaviour
{
    // [SerializeField] float timeBetweenDirt;
    [SerializeField] Collider2D spawnZone;
    [SerializeField] GameObject dirtPrefab;
    [SerializeField] Animator catAnimator;
    SoundsSender soundsSender;
    float actualTime;
    bool start;

    public static System.Action CreateDirt;
    private void OnEnable()
    {
        CreateDirt += SpawnDirt;
    }

    private void Awake()
    {
        soundsSender = GetComponent<SoundsSender>();
        // ResetTimer();
        SpawnDirt();
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
            // ResetTimer();
            SpawnDirt();
        }
    }

    // private void ResetTimer()
    // {
    //     actualTime = timeBetweenDirt;
    // }

    private void SpawnDirt()
    {
        StartCoroutine(CatScratch());
    }

    IEnumerator CatScratch()
    {
        catAnimator.SetBool("scratch", true);
        yield return new WaitForSeconds(0.2f);
        PlaySpawnSound();
        GameObject newDirt = Instantiate(dirtPrefab, GetRandomPosition(), Quaternion.identity, transform);
        yield return new WaitForSeconds(0.7f);
        catAnimator.SetBool("scratch", false);
    }

    private Vector2 GetRandomPosition()
    {
        Bounds bounds = spawnZone.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(randomX, randomY);
    }

    void PlaySpawnSound() => soundsSender.Play("create");
}
