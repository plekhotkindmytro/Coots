using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RedDot : MonoBehaviour
{
    private static readonly string CLICKABLE_OBJECT_NAME_PREFIX = "variant";
    private static readonly string SIZE_KEY = "size";
    private static readonly string BLURED_KEY = "blured";
    private static readonly string COLOR_KEY = "color";
    private static readonly float WAVE_MAX_SCALE = 1f;
    private static readonly float WAVE_MAX_SCALE_TIME = 1f;
    private static readonly float MAX_DOT_IDLE_TIME = 1f;
    private static readonly float MOVE_SPEED = 4f;

    private static readonly Color BLUE = new Color32(62, 120, 178, 255);
    private static readonly Color RED = new Color32(236, 11, 67, 255);
    private static readonly Color GREEN = new Color32(19, 138, 54, 255);


    public GameObject wavePrefab;
    public Sprite solidSprite;
    public Sprite bluredSprite;
    public AudioSource clickSound;


    private float size = 0.5f;
    private bool solid = true;
    private Color color = BLUE;
    private float timePassed = 0;
    private Tweener moveTweener = null;

    void Start()
    {
        Configure();
        Debug.Log("on start");
    }

    private void OnEnable()
    {
        Configure();
        Debug.Log("on enable");
    }

    private void Configure()
    {
        SetStartPosition();
        SetSize();
        SetForm();
        SetColor();
    }

    private void SetStartPosition()
    {
        transform.position = Vector3.zero;
    }
    private void SetSize()
    {
        if (PlayerPrefs.HasKey(SIZE_KEY))
        {
            size = PlayerPrefs.GetFloat(SIZE_KEY);
        }

        gameObject.transform.localScale = new Vector2(size, size);
    }

    private void SetForm()
    {
        solid = !PlayerPrefs.HasKey(BLURED_KEY);
        gameObject.GetComponent<SpriteRenderer>().sprite = solid ? solidSprite : bluredSprite;
    }

    private void SetColor()
    {
        if (PlayerPrefs.HasKey(COLOR_KEY))
        {
           string colorName = PlayerPrefs.GetString(COLOR_KEY);

            if ("red".Equals(colorName))
            {
                color = RED;
            } 
            else if ("blue".Equals(colorName))
            {
                color = BLUE;
            }
            else if ("green".Equals(colorName))
            {
                color = GREEN;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    void Update()
    {
        TouchHandler();
        TimeHandler();
    }

    private void TouchHandler()
    {
        if (Input.touches != null && Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Began");
                    if (gameObject.activeSelf)
                    {
                        Debug.Log("Active");
                        GameObject clickedObject = CheckCollision(touch.position, CLICKABLE_OBJECT_NAME_PREFIX);
                        
                        if (this.gameObject.Equals(clickedObject))
                        {
                            Debug.Log("This equals");
                            CollideAnimation(clickedObject);
                        }
                    }
                }
            }
        }
    }

    private void TimeHandler()
    {
        // Check time and move after 4 seconds;
        timePassed += Time.deltaTime;


        if (timePassed >= MAX_DOT_IDLE_TIME)
        {
            Vector3 newPos = GetNewRandomPosition(gameObject);
            moveTweener = transform.DOMove(newPos, MOVE_SPEED);
            timePassed = 0;
        }
    }

    private Vector3 GetNewRandomPosition(GameObject gameObject)
    {
        float maxX = Camera.main.orthographicSize * Camera.main.aspect - gameObject.transform.localScale.x / 2;
        float maxY = Camera.main.orthographicSize - gameObject.transform.localScale.y / 2;
        float randomX = UnityEngine.Random.Range(0f, maxX);
        float randomY = UnityEngine.Random.Range(0f, maxY);
        int signX = UnityEngine.Random.value > 0.5f ? 1 : -1;
        int signY = UnityEngine.Random.value > 0.5f ? 1 : -1;
        randomX *= signX;
        randomY *= signY;
        Vector3 newPos = new Vector3(randomX, randomY, gameObject.transform.position.z);
        return newPos;
    }

    private void CollideAnimation(GameObject clickedObject)
    {
        if (clickedObject == null)
        {
            return;
        }
        moveTweener.Kill();
        timePassed = 0;
        Vector3 newPos = GetNewRandomPosition(clickedObject);

        GameObject wave = Instantiate(wavePrefab);
        wave.transform.position = clickedObject.transform.position;
        wave.transform.DOScale(size + WAVE_MAX_SCALE, WAVE_MAX_SCALE_TIME);

        Ease ease = Ease.InSine;
        clickedObject.transform.DOScale(0f, 0.5f).SetEase(ease).OnComplete(() =>
        {
            timePassed = 0;
            clickedObject.transform.localPosition = newPos;
            clickedObject.transform.DOScale(size, 0.5f).SetEase(ease).OnComplete(() => timePassed = 0);
        });
        wave.transform.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete(() => Destroy(wave));

        clickSound.Play();
    }



    private GameObject CheckCollision(Vector2 position, string objectNamePrefix)
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
            GameObject gameObject = hit2D.collider.gameObject;
            if (gameObject.name.StartsWith(objectNamePrefix))
            {
                // if start get source object
                // if end get target object
                clickedObject = gameObject;
            }
        }

        return clickedObject;
    }


}
