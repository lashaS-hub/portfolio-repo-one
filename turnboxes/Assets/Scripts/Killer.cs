using UnityEngine;

public class Killer : MonoBehaviour
{
    public BoxBehaviour main;
    public BoxBehaviour target;
    private float speed;
    private Animator animator;
    private SpriteRenderer sprite;
    private void Update()
    {
        Vector2 vec = (target.transform.position - this.transform.position);
        // Debug.DrawLine(this.transform.position, target.transform.position, Color.red);
        // Debug.Log(vec.magnitude);
        if (vec.magnitude > .1)
        {
            this.transform.Translate(vec.normalized * Time.deltaTime * speed);
        }
        else
        {

            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            else if (!sprite.enabled)
            {
                target.Trigger(main);
                Destroy(this.gameObject);
            }
        }

    }


    public void Init(BoxBehaviour main, BoxBehaviour target)
    {
        this.transform.Translate(Vector3.back);
        this.main = main;
        this.target = target;
        this.speed = (target.transform.position - this.transform.position).magnitude * 1.5f;
        this.animator = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();

    }
}