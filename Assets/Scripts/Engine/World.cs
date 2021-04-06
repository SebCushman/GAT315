using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData fixedFPS;

    public float timeAccumulator = 0.0f;

    static World instance;
    public static World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Debug.Log(1.0f / Time.deltaTime);
        if (!simulate.value) return;

        //float dt = Time.deltaTime;
        timeAccumulator += Time.deltaTime;

        while(timeAccumulator > fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.ImplicitEuler(body, fixedDeltaTime));

            timeAccumulator = timeAccumulator - fixedDeltaTime;
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);

    }
}
