using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInteractions : MonoBehaviour
{
    UnitController controller;
    GridManager gridManager;
    Animator activeAnim;
    
    public float MaxSanity;
    [HideInInspector] public float sanity;
    public bool treacherous;
    bool finalTreach;

    public float Damage;
    [SerializeField] float timeBetweenAttacks;

    private void OnEnable() { UnitController.DestinationReached += AtDestination; }
    private void OnDisable() { UnitController.DestinationReached -= AtDestination; }

    private void Awake()
    {
        controller = GetComponent<UnitController>();
        sanity = MaxSanity;
    }

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        activeAnim = controller.activeUnit.GetComponentInChildren<Animator>();
    }

    void AtDestination(UnitController unit)
    {
        Vector2Int t = controller.FindCastle(treacherous);
        if (new Vector3Int(t.x, 0, t.y) == controller.activeUnit.transform.position)
        {
            finalTreach = treacherous;
            InvokeRepeating("AttackCastle", timeBetweenAttacks, timeBetweenAttacks);
            activeAnim.SetBool("isAttacking", true);
        }   
    }

    void AttackCastle()
    {
        Tile t = gridManager.GetTileFromCords(controller.FindCastle(finalTreach));
        t.GetComponent<Castle>().TakeDamge(Damage); 
    }
    
    public void TakeDamge(float damage)
    {
        sanity -= damage;
        if (sanity > MaxSanity)
        {
            sanity = MaxSanity;
        }

        
        if (sanity <= 0)
        {
            controller.DestroyActiveUnit();
            sanity = MaxSanity;
            Damage = 1;
            Castle[] calstle = FindObjectsByType<Castle>(FindObjectsSortMode.None);
            foreach (Castle cas in calstle)
            {
                cas.StopAttacking();
            }

            CancelInvoke("AttackCastle");
        }
    }

    public void UpdateUnit()
    {
        activeAnim = controller.activeUnit.GetComponentInChildren<Animator>();
        treacherous = false;
    }

    public void TreacherousColours()
    {
        controller.activeUnit.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.88f, 0.69f, 0.06f);
    }
}
