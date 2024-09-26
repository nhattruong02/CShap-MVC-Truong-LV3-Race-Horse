using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] float maxSpeedRD;
    [SerializeField] float minSpeedRD;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] float speedDown;
    private Vector3 movement;
    private float speed;

    public string Name { get => name; private set => name = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger(Common.Run);
        movement = Vector3.forward;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0,0, movement.z);
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.RemainDistance % 100 == 0 && GameManager.Instance.RemainDistance >= 0)
        {

            speed = Random.Range(minSpeedRD, maxSpeedRD);
        }
        rb.velocity = movement * speed;
    }
    private void OnTriggerExit(Collider horse)
    {
        if (horse.CompareTag(Common.Finish))
        {
            speed = 0;
            animator.SetTrigger(Common.Run);
        }
    }
}
