using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ClickScript : MonoBehaviour
{

    private static readonly float WAVE_MAX_SCALE = 40f;
    private static readonly float WAVE_MAX_SCALE_TIME = 1f;
    public GameObject wavePrefab;
    public AudioSource cootsSound;
    public AudioSource angryCatSound;
    private AudioSource clickSound;
    public GameController gameController;
    private bool isOdd;

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetCanClick())
        {
            TouchHandler();
        }
       
    }

    private void TouchHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.activeSelf)
            {
                GameObject clickedObject = CheckCollision(Input.mousePosition);

                if (clickedObject != null)
                {
                    CollideAnimation(clickedObject);
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
            if (clickedObject != null)
            {
                if (clickedObject.tag.Equals("coots"))
                {
                    clickSound = cootsSound;
                    isOdd = true;
                }
                else if (clickedObject.tag.Equals("cat"))
                {
                    clickSound = angryCatSound;
                    isOdd = false;
                }
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

        if (!gameController.GetCanClick())
        {
            return;
        }
        gameController.SetCanClick(false);
        Transform shadow = clickedObject.transform.GetChild(0);
        Vector3 shadowLocalPos = shadow.localPosition;
        Vector3 movePosition = clickedObject.transform.localPosition + clickedObject.transform.localScale.x * shadowLocalPos;
        shadow.parent = null;

        if (isOdd)
        {
            GameObject wave = Instantiate(wavePrefab);
            wave.transform.position = movePosition;
           
            Color32 waveColor = Color.black;
            waveColor.a = 200;
            SpriteRenderer waveSpriteRenderer = wave.GetComponent<SpriteRenderer>();
            waveSpriteRenderer.color = waveColor;
            float size = clickedObject.transform.localScale.x;

            wave.transform.DOScale(size + WAVE_MAX_SCALE, WAVE_MAX_SCALE_TIME).OnComplete(() =>
            {
                Destroy(wave);

                gameController.SetCanClick(true);
                LoadNextLevel();

            });
        }


        Sequence sequence = DOTween.Sequence();
        sequence.Append(clickedObject.transform.DOLocalMove(movePosition, 0.1f).OnComplete(() =>
        {
            clickSound.Play();
        }));
        sequence.Append(clickedObject.transform.DOLocalMove(clickedObject.transform.localPosition, 0.1f));
        sequence.OnComplete(() =>
        {
            shadow.parent = clickedObject.transform;
            if (!isOdd)
            {
                 gameController.SetCanClick(true);
            }
        });
        sequence.Play();
    }

    private void LoadNextLevel()
    {
        gameController.LoadNextLevel();
    }
}
