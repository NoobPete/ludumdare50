using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    public float timeBeforeStopped = 1f;
    public float stopThreshold;
    public int side;

    public float timeSinceStop;
    public float maxSpeed;
    public float maxSpin;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddRelativeForce(Random.onUnitSphere * Random.Range(0f, maxSpeed));
        rb.AddRelativeTorque(Random.onUnitSphere * Random.Range(0f, maxSpin));
    }

    public bool Stopped()
    {
        return timeSinceStop > timeBeforeStopped || this.transform.position.y < -5;
    }

    // Update is called once per frame
    void Update()
    {
        side = WhichIsUp();
        timeSinceStop += Time.deltaTime;

        if (rb.velocity.magnitude > stopThreshold)
        {
            timeSinceStop = 0f;
        }
    }


    // sets the side definition variables.
    // (these have been changed to the literal vector
    // values for readability)
    private Vector3[] sides =
    {new Vector3(0, 1, 0),  // 1(+) or 6(-)
     new Vector3(0, 0, 1),  // 2(+) or 5(-)
     new Vector3(1, 0, 0)}; // 3(+) or 4(-) 
 
    public int WhichIsUp()
    {
        var maxY = float.NegativeInfinity;
        var result = -1;

        for (var i = 0; i < 3; i++)
        {
            // Transform the vector to world-space:
            var worldSpace = transform.TransformDirection(sides[i]);
            if (worldSpace.y > maxY)
            {
                result = i + 1; // index 0 is 1
                maxY = worldSpace.y;
            }
            if (-worldSpace.y > maxY)
            { // also check opposite side
                result = 6 - i; // sum of opposite sides = 7
                maxY = -worldSpace.y;
            }
        }
        return result;
    }

    public static void ChangeMoney(int amount)
    {

    }
}
