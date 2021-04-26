using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public BoolData collision;
    public BoolData wrap;
    public StringData fpsText;

    public float timeAccumulator = 0.0f;

    static World instance;
    public static World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    public List<Body> bodies { get; set; } = new List<Body>();

    float fps = 0;
    float fpsAverage = 0;
    float smoothing = 0.975f;
    Vector2 size;

    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    void Update()
    {
        //Debug.Log(1.0f / Time.deltaTime);
        if (!simulate) return;

        //Another way to do fps
        float dt = Time.deltaTime;
        fps = (1.0f / dt);
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = "FPS: " + fpsAverage.ToString("F1");
        //Debug.Log(fps);

        timeAccumulator += Time.deltaTime;

        GravitationalForce.ApplyForce(bodies, gravitation.value);

        //bodies.ForEach(body => body.shape.color = Color.red);

        while (timeAccumulator >= fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.ImplicitEuler(body, fixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.cyan);

            if (collision)
            {
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
                ContactSolver.Resolve(contacts);
            }

            timeAccumulator = timeAccumulator - fixedDeltaTime;
        }

        if (wrap) { bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size)); }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
