using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton

    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Isn't this a single player game");
            return;
        }
        instance = this;
    }

    #endregion


    public delegate void OnNotHoverOverInteractabele();
    public OnNotHoverOverInteractabele OnNotHoverOverInteractabeleCallback;

    public float interactableDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        #region Picking Up Items

        // Mouse checking for interactable
        Vector3 mousePos = Input.mousePosition;

        RaycastHit2D rayCastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero, Color.yellow);
        if (rayCastHit.collider != null)
        {
            if (rayCastHit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                Interactable interactable = rayCastHit.collider.gameObject.GetComponent<Interactable>();
                float distance = Vector2.Distance(transform.position, interactable.transform.position);
                Color lineColor = Color.red;


                if (distance <= interactableDistance)
                {
                    lineColor = Color.green;

                    interactable.Hovering();

                    // Checking if 'E' is pressed, if so then try to interact with something
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Interact with something
                        interactable.Interact();
                    }


                }


                if (Input.GetKeyDown(KeyCode.E) && distance > interactableDistance)
                {
                    Debug.Log("Too Far Away!");
                }

                Debug.DrawLine(transform.position, interactable.transform.position, lineColor);

            }
            else
            {
                if (OnNotHoverOverInteractabeleCallback != null)
                    OnNotHoverOverInteractabeleCallback.Invoke();
            }
        }
        else
        {
            if (OnNotHoverOverInteractabeleCallback != null)
                OnNotHoverOverInteractabeleCallback.Invoke();
        }

        #endregion

        if (Input.GetButton("Drop"))
        {
            EquipmentManager.instance.DropItem();
        }

    }
}
