using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DirtGenerator : MonoBehaviour
{
    [SerializeField] float timeBetweenDirt;
    [SerializeField] Collider2D spawnZone;
    [SerializeField] GameObject dirtPrefab;
    [SerializeField] Animator catAnimator;
    SoundsSender soundsSender;
    float actualTime = 0;
    bool start;
    Coroutine DirtGeneratorCorutine;


    public static System.Action<bool> OnChangeStartState;
    private void OnEnable()
    {
        OnChangeStartState += SpawnDirt;
    }

    private void Awake()
    {
        soundsSender = GetComponent<SoundsSender>();
        // ResetTimer();
        // SpawnDirt();

    }
    private void Start()
    {
        start = true;
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
            SpawnDirt(true);
        }
    }

    private void ResetTimer()
    {
        actualTime = timeBetweenDirt;
    }

    private void SpawnDirt(bool startCorutine)
    {
        start = startCorutine;

        if (startCorutine)
        {
            if (DirtGeneratorCorutine == null) DirtGeneratorCorutine = StartCoroutine(CatScratch());
        }
        else
        {
            if (DirtGeneratorCorutine != null) StopCoroutine(DirtGeneratorCorutine);
            DirtGeneratorCorutine = null;
            Debug.Log("Stopping coroutine");
        }

    }
    IEnumerator CatScratch()
    {
        catAnimator.SetBool("scratch", true);
        yield return new WaitForSeconds(0.2f);
        PlaySpawnSound();
        GameObject newDirt = Instantiate(dirtPrefab, GetRandomPosition(), Quaternion.identity, transform);
        yield return new WaitForSeconds(0.7f);
        catAnimator.SetBool("scratch", false);
        DirtGeneratorCorutine = null;
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
