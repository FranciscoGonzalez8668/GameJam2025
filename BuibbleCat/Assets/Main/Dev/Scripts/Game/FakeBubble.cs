using UnityEngine;

public class FakeBubble : MonoBehaviour
{
    [SerializeField] GameObject fakeBubble, fakeDirt;
    public void GenerateFakeBubble(Vector3 position, int bubbleSprite, int dirtSprite)
    {
        GameObject outBubble = Instantiate(fakeBubble, position, Quaternion.identity);
        SpriteSelector renderer = outBubble.GetComponentInChildren<SpriteSelector>();
        renderer.SetSprite(bubbleSprite);
        GameObject outDirt = Instantiate(fakeDirt, outBubble.transform);
        SpriteSelector dirtRenderer = outDirt.GetComponentInChildren<SpriteSelector>();
        dirtRenderer.SetSprite(dirtSprite);
        Destroy(outBubble, 3);
    }
}
