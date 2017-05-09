using UnityEngine;

public class ScrollObjects : MonoBehaviour {

    public float duration = 0.7f;

    private float startTime;

    private bool movePosition = false;
    private Vector3 startPosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    private bool moveAnchorPosition = false;
    private Vector3 startAnchorPosition = Vector3.zero;
    private Vector3 targetAnchorPosition = Vector3.zero;

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
            if (movePosition)
            {
                movePosition = false;
                transform.localPosition = targetPosition;
            }
            if (moveAnchorPosition)
            {
                moveAnchorPosition = false;
                GetComponent<RectTransform>().anchoredPosition3D = targetAnchorPosition;
            }
            GetComponent<ScrollObjects>().enabled = false;
            return;
        }
        if (movePosition)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, timeDelta / duration);
        }
        if (moveAnchorPosition)
        {
            GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(startAnchorPosition, targetAnchorPosition, timeDelta / duration);
        }
	}

    public void MoveToPosition(Vector3 pos)
    {
//        print("MoveToPosition " + pos);
        movePosition = true;
        targetPosition = pos;
        startTime = Time.time;
        startPosition = transform.localPosition;
        enabled = true;
    }

    public void MoveToAnchorPosition(Vector3 pos)
    {
//        print("MoveToAnchorPosition " + pos);
        moveAnchorPosition = true;
        targetAnchorPosition = pos;
        startTime = Time.time;
        startAnchorPosition = GetComponent<RectTransform>().anchoredPosition3D;
        enabled = true;
    }

}
