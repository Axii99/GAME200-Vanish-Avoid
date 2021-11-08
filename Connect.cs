using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour
{
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private Vector3 Offset = new Vector3(0f, 0.2f, 0f);
    public LayerMask CheckLayer;
    public LineRenderer Line;
    // Start is called before the first frame update
    private PlayerMovement playermovement;

    void Start()
    {
        Line.useWorldSpace = true;
        playermovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playermovement.isvanish) {
            Line.SetPosition(0, StartPosition);
            Line.SetPosition(1, this.transform.Find("LinePosition").position);
            Line.enabled = true;
            Color sColor = Line.startColor;
            sColor.a = 0.3f;
            Color eColor = Line.endColor;
            eColor.a = 0.3f;
            Line.startColor = sColor;
            Line.endColor = eColor;
        }
        else {
            Line.enabled = false;
        }
    }

    public Vector3 GetStartPosition() {
        return StartPosition;
    }

    public IEnumerator ShootRay() {

        //Debug.Log("ShootRay is called");
   

        RaycastHit2D[] RayHits = Physics2D.RaycastAll(StartPosition + Offset,EndPosition-StartPosition + Offset, Vector3.Distance(StartPosition,EndPosition), CheckLayer);
        if (RayHits.Length > 0) {
            foreach (RaycastHit2D Hit in RayHits){
                Debug.Log(Hit.transform.name);
                Enemy enemy =  Hit.transform.gameObject.GetComponent<Enemy>();
                if (enemy) {
                    StartCoroutine(enemy.Kill());
                }
                TrackEnemy trackenemy = Hit.transform.gameObject.GetComponent<TrackEnemy>();
                if (trackenemy) {
                    StartCoroutine(trackenemy.Kill());
                }
                //Destroy(Hit.transform.gameObject);
            }
           
        }

        RaycastHit2D[] RayHits2 = Physics2D.RaycastAll(StartPosition - Offset, EndPosition - StartPosition - Offset, Vector3.Distance(StartPosition, EndPosition), CheckLayer);
        if (RayHits2.Length > 0) {
            foreach (RaycastHit2D Hit in RayHits2) {
                Debug.Log(Hit.transform.name);
                Enemy enemy = Hit.transform.gameObject.GetComponent<Enemy>();
                if (enemy) {
                    StartCoroutine(enemy.Kill());
                }

                TrackEnemy trackenemy = Hit.transform.gameObject.GetComponent<TrackEnemy>();
                if (trackenemy) {
                    StartCoroutine(trackenemy.Kill());
                }
                //Destroy(Hit.transform.gameObject);
            }

        }

        if (RayHits.Length == 0 && RayHits2.Length == 0) {
            this.transform.position = StartPosition;
        }

        Line.SetPosition(0, StartPosition);
        Line.SetPosition(1, EndPosition);
        Line.enabled = true;
        Color sColor = Line.startColor;
        sColor.a = 1.0f;
        Color eColor = Line.endColor;
        eColor.a = 1.0f;
        Line.startColor = sColor;
        Line.endColor = eColor;
        AudioManager.Instance.PlaySound("Connect");
        //Debug.Log(StartPosition);
        //Debug.Log(EndPosition);

        
        yield return new WaitForSeconds(0.15f);
        Line.enabled = false;
    }

    public void SetStartPosition(Vector3 startPos) {
        StartPosition = startPos;
    }

    public void SetEndPosition(Vector3 EndPos) {
        EndPosition = EndPos;
    }
}
