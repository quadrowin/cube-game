using UnityEngine;

public class ScrollObjects : MonoBehaviour {

    public float duration = 0.7f;

    private float startTime;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

	void Start () {
        if (0 == startTime)
        {
            // запускать необходимо через MoveToPosition()
            GetComponent<ScrollObjects>().enabled = false;
            return;
        }
    }
	
	void Update () {
        float timeDelta = Time.time - startTime;
        if (timeDelta >= duration)
        {
            startTime = 0;
            transform.localPosition = targetPosition;
            GetComponent<ScrollObjects>().enabled = false;
            return;
        }
        transform.localPosition = Vector3.Lerp(startPosition, targetPosition, timeDelta / duration);
	}

    public void MoveToPosition(Vector3 pos)
    {
        targetPosition = pos;
        startTime = Time.time;
        startPosition = transform.localPosition;
        enabled = true;
    }

}
