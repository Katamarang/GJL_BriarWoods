using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 savedPos;
    [SerializeField] bool placeOnWalkable;
    [SerializeField] float price;
    MoneyManager moneyManager;
    AudioManager audioManager;
    public bool bought { get; private set; }
    public bool held { get; private set; }

    [SerializeField] GameObject info;

    [Header("Sprites")]
    SpriteRenderer spriteRenderer;
    public Sprite StartingSprite;
    public Sprite GameplaySprite;

    [Header("Coins")]
    public bool coin;

    private void Awake()
    {
        moneyManager = FindFirstObjectByType<MoneyManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = StartingSprite; 
    }
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePos = Input.mousePosition - GetMousePos();
        spriteRenderer.sprite = StartingSprite;
        audioManager.Play("Select");

        savedPos = transform.position;
        held = true;
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos) + Vector3.up;
        info.SetActive(false);
    }

    private void OnMouseUp()
    {
        held = false;
        audioManager.Play("PopHigh");

        //stuff happens
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 6f);

        if (!hit.transform.CompareTag("Tile"))
        {
            transform.position = savedPos;
            return;
        }

        Tile t = hit.transform.GetComponent<Tile>();
        if (t.taken)
        {
            transform.position = savedPos;
            return;
        }

        // Determine if placement is valid
        bool canPlace = placeOnWalkable ? !t.blocked : t.blocked;
        if (!canPlace)
        {
            transform.position = savedPos;
            return;
        }

        if (!bought)
        {
            if (moneyManager.Money < price)
            {
                // can't afford
                print("Cant afford");
                transform.position = savedPos;
                return;
            }
            else 
            { 
                moneyManager.MoneyOut(price); 
                bought = true;                
            }
        }

        transform.parent = hit.transform;
        transform.position = new Vector3(hit.transform.position.x, 0.6f, hit.transform.position.z);

        spriteRenderer.sprite = GameplaySprite;

    }
    private void OnMouseEnter()
    {
        if (!bought)
        {
            info.SetActive(true);
            //audioManager.Play("PopLow");
        }
    }

    private void OnMouseExit()
    {
        info.SetActive(false);
    }

}
