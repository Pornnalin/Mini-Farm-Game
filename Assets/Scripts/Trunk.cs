using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Trunk : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rigi;
    public float speed;
    [SerializeField] private bool isHit = false;
    [SerializeField] private bool isMove = false;
    [SerializeField] private Transform target;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TrackTargetAndMove();
    }

    public void TrackTargetAndMove()
    {
        if (target == null || target.gameObject == null)
        {
            StopMove();
            FindTarget();
            return;
        }
        else
        {
            TrackingTarget();

            if (!isHit)
            {
                Move();


                if (target != null && target.gameObject != null)
                {
                    if (Vector3.Distance(transform.position, target.position) < .1f)
                    {
                        StopMove();
                        target = null;
                    }
                }
                else
                {
                    // หาก target เป็น null หรือถูกลบ
                    Debug.LogWarning("Target is null or destroyed. Cannot check distance.");
                    StopMove();
                    target = null;
                }
            }
           
        }



    }

    private void TrackingTarget()
    {
        PlantPot targetStatus = target.GetComponent<PlantPot>();
        if (!targetStatus.isCanHarvest)
        {
            Debug.Log("Target can't harvest");
            StopMove();
            target = null;
        }
    }
    private void StopMove()
    {
        isMove = false;
        rigi.velocity = Vector2.zero;
        animator.SetBool("IsMove", false);
    }
    private void Move()
    {
        if (target == null || target.gameObject == null)
        {
            StopMove();
            return;
        }

        isMove = true;
        Vector2 direction = (target.position - transform.position).normalized;
        rigi.velocity = direction * speed;

        animator.SetBool("IsMove", true);
    }

    private void FindTarget()
    {

        foreach (Transform item in GameManager.instance.plantManager.plantTrans)
        {
            if (item.GetComponent<PlantPot>().isCanHarvest)
            {
                target = item;
                break;
            }
        }
    }

    public void GotHit()
    {
        if (!isMove)
        {
            return;
        }
        StopMove();
        isHit = true;
        isMove = false;
        target = null;
        DropSeedPod();
        Invoke("WaitToWalkAgin", 5f);
        Debug.Log("Trunk got hit!");
    }

    private void DropSeedPod()
    {
        int random = Random.Range(0, 100);
        if (random % 2 == 0)
        {
            PlantData plantData = GameManager.instance.plantManager.plantData[Random.Range(0, GameManager.instance.plantManager.plantData.Count)];
            GameManager.instance.inventory.AddOrUpdateItem(plantData, false);
            GameManager.instance.notification.notiEvent.Invoke(plantData.seedPodName);
            GameManager.instance.soundManager.PlayeGetItem();
        }
        else
        {
            GameManager.instance.soundManager.PlayeFail();

        }
    }

    void WaitToWalkAgin()
    {
        isHit = false;
        Debug.Log("Trunk can walk again!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            var plantPot = collision.transform.parent.parent.GetComponent<PlantPot>();
            //Debug.Log("Trunk hit Plant!" + collision.transform.parent.parent);

            plantPot.Reset();
            StopMove();
            target = null;
            GameManager.instance.soundManager.PlayeHit();
        }


    }
}
