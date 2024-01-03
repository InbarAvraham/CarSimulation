using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public GameManger gm;
    public float speed;
    public TMP_Text distanceLabel;
    public Image north;
    public TMP_Text speedLabel;
    public RectTransform arrow;
    public float maxSpeed = 260.0f; // The maximum speed of the target ** IN KM/H **
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;
    public Transform target;
    Vector3 lastPosition = Vector3.zero;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -5, 0);
        // Get the game manager from previos scene
        gm = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        //get the first distention
        target = gm.GetDistenation();
        StartCoroutine(Speed());
    }

    public int GetDistanceToNextTarget(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return (int)distance;
    }
    IEnumerator Speed()
    {
        int labelspeed = ((int)(Vector3.Distance(transform.position, lastPosition) / Time.deltaTime) / 36);
        lastPosition = transform.position;

        if (speedLabel != null)
            speedLabel.text = labelspeed.ToString() + "km/h";

        if (arrow != null)
            arrow.transform.Rotate(new Vector3(0, 0, (labelspeed * -1) / 1.45f));
        yield return new WaitForSeconds(1);
    }
    void Update()
    {
        // Car moving and looking at next distention

        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        distanceLabel.text = GetDistanceToNextTarget(target.transform).ToString() + "m";
    }

    private void OnTriggerEnter(Collider other)
    {
        // On collision with "taget" object (the distention) move to next distention.
        if (other.tag == "Target")
        {
            Debug.Log(other.name);
            gm.IncreseCurrenttarget();
            target = gm.GetDistenation();
        }
    }
}
