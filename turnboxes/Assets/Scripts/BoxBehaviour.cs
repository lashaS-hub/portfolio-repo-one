using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    public SpriteRenderer frontPanel;
    public SpriteRenderer backPanel;
    public SpriteRenderer tierFrontSprite;
    public SpriteRenderer tierBackSprite;
    public ParticleSystem particle;

    public AudioSource source;

    internal Vector2 address;
    internal BoxValue stats;
    internal BoxValue lastStats;

    internal Animator animator;


    public static event System.Action OnFinishAnimation;
    // private Material material;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Start()
    {
        // yield return new WaitForSeconds((address.x + address.y)/10f);
        // FindMeshInChildren();
        // material = new Material(Shader.Find("Unlit/Color"));
        // GetComponent<MeshRenderer>().sharedMaterial = material;
        IdentifyBox(true);
        yield return new WaitForSeconds((address.x + address.y) / 10f);

        animator.SetTrigger("Rotate");

    }

    public bool Trigger(BoxBehaviour killer)
    {
        // Debug.Log("Trugger");
        return stats.Contact(killer, this);
    }

    public void IdentifyBox(bool onstart = false)
    {
        lastStats = stats;
        stats = getrandom(onstart);
        if(lastStats == null)
            lastStats = stats;
        backPanel.sprite = stats.image_Sprite;
        backPanel.color = stats.tierColor;
        tierBackSprite.sprite = stats.tier_Sprite;
    }

    public BoxValue getrandom(bool onstart)
    {
        int totalweight = 0;
        var boxes = LevelManager.Instance.boxIdentity.Where(x => x.minimumLevelAppear <= LevelManager.Instance.currentLevel && (onstart ? x.appearOnStart : true));
        // Debug.Log(LevelManager.Instance.currentLevel);
        foreach (BoxValue item in boxes)
        {
            totalweight += item.weight;
        }
        int num = Random.Range(0, totalweight);
        BoxValue a = null;
        foreach (BoxValue item in boxes)
        {
            if (num < item.weight)
            {
                a = item;
                break;
            }
            num -= item.weight;
        }
        return a;

    }

    public void OnRotate()
    {
        FinishAnimation();
        frontPanel.sprite = stats.image_Sprite;
        frontPanel.color = stats.tierColor;
        tierFrontSprite.sprite = stats.tier_Sprite;
        // material.color = Color.blue;
        // if(LevelManager.Instance.canClick)
        stats.Init(this);

    }

    public void FinishAnimation()
    {
        if (OnFinishAnimation != null)
            OnFinishAnimation();
    }

}