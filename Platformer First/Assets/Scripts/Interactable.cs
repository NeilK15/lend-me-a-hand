using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Interactable : MonoBehaviour
{

    public static Color hoveringOver = Color.yellow;
    public Color defaultColor;

    private SpriteRenderer spriteRenderer;
    private Material material;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
            Debug.LogError("Cmon, No Renderer, how is this thing on the screen???");
        else
            material = rend.material;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        PlayerController.instance.OnNotHoverOverInteractabeleCallback += NotHovering;
    }

    public virtual void Interact()
    {
        // THIS METHOD IS MEANT TO BE OVERRIDEN
        Debug.Log("Interacting with " + gameObject.name);
    }

    private void NotHovering()
    {
        material.SetFloat("_Highlighted", 0f);
    }

    public void Hovering()
    {
        material.SetFloat("_Highlighted", 1f);
    }

}
