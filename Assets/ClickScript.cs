using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickScript : MonoBehaviour
{

    private static readonly float WAVE_MAX_SCALE = 40f;
    private static readonly float WAVE_MAX_SCALE_TIME = 1f;
    public GameObject wavePrefab;
    public AudioSource touchSound;

    public bool isOdd;

    private bool clicked = false;
    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            return;
        }
        TouchHandler();
    }

    private void TouchHandler()
    {
        if (Input.touches != null && Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                if (gameObject.activeSelf)
                {
                    GameObject clickedObject = CheckCollision(touch.position);

                    if (this.gameObject.Equals(clickedObject))
                    {
                        CollideAnimation(clickedObject);
                    }
                }
            }
        }
    }

    private GameObject CheckCollision(Vector2 position)
    {
        GameObject clickedObject = null;
        Camera cam = Camera.main;
        Vector2 origin = Vector2.zero;
        Vector2 direction = Vector2.zero;

        if (cam.orthographic)
        {
            origin = cam.ScreenToWorldPoint(position);
        }
        else
        {
            Ray ray = cam.ScreenPointToRay(position);
            origin = ray.origin;
            direction = ray.direction;
        }

        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction);

        if (hit2D.collider && hit2D.collider.gameObject)
        {
            clickedObject = hit2D.collider.gameObject;
            if (this.gameObject.Equals(clickedObject))
            {
                clickedObject = this.gameObject;
            }
        }

        return clickedObject;
    }

    private void CollideAnimation(GameObject clickedObject)
    {
        if (clickedObject == null)
        {
            return;
        }

        if (clicked)
        {
            return;
        }
        clicked = true;
        Transform shadow = this.gameObject.transform.GetChild(0);
        Vector3 shadowLocalPos = shadow.localPosition;
        Vector3 movePosition = clickedObject.transform.localPosition + clickedObject.transform.localScale.x * shadowLocalPos;
        shadow.parent = null;

        if (isOdd)
        {
            GameObject wave = Instantiate(wavePrefab);
            wave.transform.position = movePosition;
            Color32 parentColor = gameObject.GetComponent<SpriteRenderer>().color;
            Color32 waveColor = Color.black;
            waveColor.a = 200;
            SpriteRenderer waveSpriteRenderer = wave.GetComponent<SpriteRenderer>();
            waveSpriteRenderer.color = waveColor;
            float size = this.gameObject.transform.localScale.x;
            Debug.Log("size: " + size);
            wave.transform.DOScale(size + WAVE_MAX_SCALE, WAVE_MAX_SCALE_TIME).OnComplete(() =>
            {
                Destroy(wave);

            });
        }


        Sequence sequence = DOTween.Sequence();
        sequence.Append(clickedObject.transform.DOLocalMove(movePosition, 0.1f).OnComplete(() =>
        {
            

            touchSound.Play();
        }));
        sequence.Append(clickedObject.transform.DOLocalMove(clickedObject.transform.localPosition, 0.1f));
        sequence.OnComplete(() =>
        {
            shadow.parent = clickedObject.transform;
            clicked = false; 
        });
        sequence.Play();
    }
}
