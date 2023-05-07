using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static LevelBoundary Instance; //Singleton

    [SerializeField] private Vector2 _screenResolution; //Размер экрана.


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        if (!Application.isEditor && Application.isPlaying)
        {
            _screenResolution.x = Screen.width;

            _screenResolution.y = Screen.height;
        }
    }

    //Левая граница экрана.
    public float LeftBorder
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        }
    }

    //Правая граница экрана.
    public float RightBorder
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(_screenResolution.x, 0, 0)).x;
        }
    }

#if UNITY_EDITOR
    //Отрисовкка границ на экране.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(new Vector3(LeftBorder, -10, 0), new Vector3(LeftBorder, 10, 0));

        Gizmos.DrawLine(new Vector3(RightBorder, -10, 0), new Vector3(RightBorder, 10, 0));
    }

#endif
}

