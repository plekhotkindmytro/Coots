using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PuzzleGenerator : MonoBehaviour
{
    public GameObject prefab;
    public GameObject oddPrefab;
    public AudioSource popSound;
    public AudioSource angryCatSound;
    public GameObject handPointerPrefab;
    public int colorSchemeIndex;
    public bool tutorial = false;
    private int oddRow;
    private int oddColumn;
    GameObject handPointer;
    public GameController gameController;

    public int ROW_NUMBER = 6;
    public int COLUMN_NUMBER = 3;
    private readonly float OFFSET_Y = 3f;
    private int rowNumber;
    private int columnNumber;
    private GameObject[,] grid;


    private static readonly Color32 bgColor1 = new Color32(202, 240, 248, 255);
    private static readonly Color32 bgColor2 = new Color32(253, 252, 220, 255);
    private static readonly Color32 bgColor3 = new Color32(254, 250, 224, 255);
    private static readonly Color32 bgColor4 = new Color32(38, 70, 83, 255);
    
    private static readonly Color32[] backgroundColors = new Color32[]
    {
        bgColor1, bgColor2, bgColor3, bgColor4
    };


    private Camera cam;

    private static readonly Color32[] colors1 = new Color32[] { 
        new Color32(0, 119, 182, 255), 
        new Color32(0, 180, 216, 255), 
        new Color32(144, 224, 239, 255), 
        //new Color32(3, 4, 94, 255) 
    };

    private static readonly Color32[] colors2 = new Color32[] {
        new Color32(0, 129, 167, 255),
        new Color32(0, 175, 185, 255),
        new Color32(254, 217, 183, 255),
        new Color32(240, 113, 103, 255)
    };

    private static readonly Color32[] colors3 = new Color32[] {
        new Color32(204, 213, 174, 255),
        new Color32(233, 237, 201, 255),
        new Color32(250, 237, 205, 255),
        new Color32(212, 163, 115, 255)
    };

    private static readonly Color32[] colors4 = new Color32[] {
        new Color32(42, 157, 143, 255),
        new Color32(233, 196, 106, 255),
        new Color32(244, 162, 97, 255),
        new Color32(231, 111, 81, 255)
    };

    private static readonly Color32[][] colors = new Color32[][] { 
        colors1, colors2,colors3, colors4
    };

    public void GenerateGrid()
    {
        gameController.SetCanClick(false);
        if (tutorial)
        {
            angryCatSound.Play();
        }
        
        cam = Camera.main;
        SetCameraColor();
        oddRow = Random.Range(0, ROW_NUMBER);
        oddColumn = Random.Range(0, COLUMN_NUMBER);
        DestroyPreviousGrid();

        rowNumber = ROW_NUMBER;
        columnNumber = COLUMN_NUMBER;

        grid = new GameObject[rowNumber, columnNumber];

        GameObject exampleTile = null;


        for (int rowIndex = 0; rowIndex < rowNumber; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnNumber; columnIndex++)
            {

                GameObject tile;
                if (rowIndex == oddRow && columnIndex == oddColumn)
                {
                    tile = Instantiate(oddPrefab);
                }
                else
                {
                    tile = Instantiate(prefab);
                   
                }

                tile.transform.parent = transform;
                
                if(!tutorial)
                {
                    SetColor(tile);
                }

                ScaleTile(tile);
                MoveTile(tile, rowIndex, columnIndex);

                grid[rowIndex, columnIndex] = tile;
                

                if (exampleTile == null)
                {
                    exampleTile = tile;
                }

                if (tutorial)
                {
                    if (rowIndex == oddRow && columnIndex == oddColumn)
                    {
                     

                        handPointer = Instantiate(handPointerPrefab);
                        ScaleTile(handPointer);
                        float distance = -0.5f;
                        Vector3 pointerPosition = tile.transform.localPosition + tile.transform.localScale.x * new Vector3(0, distance, 0);
                        handPointer.transform.position = pointerPosition;

                        Sequence pointerSequence = DOTween.Sequence();
                        pointerSequence.Append(handPointer.transform.DOMoveY(handPointer.transform.position.y - distance, 0.5f));
                        pointerSequence.Append(handPointer.transform.DOMoveY(handPointer.transform.position.y, 0.5f));
                        pointerSequence.SetLoops(-1);
                        pointerSequence.Play();
                    }
                }
            }
        }
        MoveCamera(exampleTile);


        RunAnimation();

    }

    private void RunAnimation()
    {
        if (grid != null)
        {
            for (int rowIndex = 0; rowIndex < grid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < grid.GetLength(1); columnIndex++)
                {
                    GameObject tile = grid[rowIndex, columnIndex];
                    float targetScale = tile.transform.localScale.x;
                    tile.transform.localScale = Vector2.zero;
                    float delay = 0.15f * (rowIndex + columnIndex);
                    float popSoundDelay = 0.05f * (rowIndex + columnIndex);
                    popSound.PlayDelayed(popSoundDelay);

                    tile.transform.DOScale(targetScale*1.5f, 0.2f).SetDelay(delay).OnComplete(()=> {
                        popSound.Play();
                        tile.transform.DOScale(targetScale, 0.1f).OnComplete(() =>
                        {
                            if(tile.Equals(grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1]))
                            {
                                gameController.SetCanClick(true);
                            }
                        });
                    });
                }
            }
        }

                    
    }

    private void SetColor(GameObject tile)
    {
        int colorIndex = Random.Range(0, colors[colorSchemeIndex].Length);
        Debug.Log("colorSchemeIndex: " + colorSchemeIndex);
        Debug.Log("colorIndex: "+ colorIndex);
        tile.GetComponent<SpriteRenderer>().color = colors[colorSchemeIndex][colorIndex];
    }

    private void DestroyPreviousGrid()
    {
        DOTween.KillAll();
        
        if (grid != null)
        {
            for (int rowIndex = 0; rowIndex < grid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < grid.GetLength(1); columnIndex++)
                {
                    Destroy(grid[rowIndex, columnIndex]);
                }
            }

            grid = null;
        }
    }
   /* void Start()
    {
        
        GenerateGrid();
    }
*/
    private void SetCameraColor()
    {
        cam.backgroundColor = backgroundColors[colorSchemeIndex];
    }

    private void OnEnable()
    {
        GenerateGrid();
    }

    private void OnDisable()
    {
        Destroy(handPointer);
        DestroyPreviousGrid();
    }
    private void ScaleTile(GameObject tile)
    {


        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
        float tileWidth = spriteRenderer.sprite.bounds.size.x;
        float tileHeight = spriteRenderer.sprite.bounds.size.y;

        // float tileAspect = tileWidth / tileHeight;
        float tileAspect = 1;
        float newScaleX = (cam.orthographicSize * 2 * Camera.main.aspect) / tileAspect / columnNumber;
        float newScaleY = (Camera.main.orthographicSize * 2 - OFFSET_Y) / rowNumber;

        if (newScaleX > newScaleY)
        {
            newScaleX = newScaleY;
        }
        else
        {
            newScaleY = newScaleX;
        }

        tile.transform.localScale = new Vector3(newScaleX, newScaleY);
    }

    internal GameObject[,] GetGrid()
    {
        return grid;
    }

    private void MoveTile(GameObject tile, int rowIndex, int columnIndex)
    {
        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
        float tileWidth = spriteRenderer.sprite.bounds.size.x;
        float tileHeight = spriteRenderer.sprite.bounds.size.y;
       
        // float tileAspect = tileWidth / tileHeight;
        float tileAspect = 1;
        float xPos = tile.transform.localScale.x * tileAspect * columnIndex;
        float yPos = tile.transform.localScale.y * rowIndex + OFFSET_Y/2f;
        tile.transform.position = new Vector3(xPos, yPos);
    }


    private void MoveCamera(GameObject tile)
    {
        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
        float tileWidth = spriteRenderer.sprite.bounds.size.x;
        float tileHeight = spriteRenderer.sprite.bounds.size.y;

        // float tileAspect = tileWidth / tileHeight;
        float tileAspect = 1;
        float newX = (tile.transform.localScale.x * tileAspect) / 2 * (columnNumber - 1);
        float newY = (tile.transform.localScale.y) / 2 * (rowNumber - 1) + OFFSET_Y/2;
        Camera.main.transform.position = new Vector3(newX, newY, Camera.main.transform.position.z);
    }
}
