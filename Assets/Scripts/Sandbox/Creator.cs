using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : Action
{
    public GameObject original;
    public FloatData speed;
    public FloatData damping;

    bool action { get; set; } = false;

    public override void StartAction()
    {
        action = true;
    }

    public override void StopAction()
    {
        action = false;
    }

    //public float speed = 100.0f;

    void Update()
    {
        if (action)//Input.GetMouseButton(0))//GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if(gameObject.TryGetComponent<Body>(out Body body))
            {
                Vector2 force = Random.insideUnitSphere.normalized * speed.value;

                body.AddForce(force);

                body.damping = damping.value;

                World.Instance.bodies.Add(body);
            }
        }
    }
}
