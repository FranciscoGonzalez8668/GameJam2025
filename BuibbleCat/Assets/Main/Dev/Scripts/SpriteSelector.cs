using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] bool playOnAwake = true;
    SpriteRenderer spriteRenderer;
    [HideInInspector] public Sprite selectedSprite;
    [HideInInspector] public int selectedSpriteInt;
    private void Awake()
    {
        if (!playOnAwake) return;
        SetSprite();
    }

    public void SetSprite()
    {
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        selectedSpriteInt = Random.Range(0, sprites.Length);
        selectedSprite = sprites[selectedSpriteInt];
        spriteRenderer.sprite = selectedSprite;
    }

    public void SetSprite(int index)
    {
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        selectedSpriteInt = index;
        selectedSprite = sprites[index];
        spriteRenderer.sprite = selectedSprite;
    }

}
