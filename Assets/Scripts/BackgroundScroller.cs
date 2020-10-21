using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;


    private void Awake()
    {
        DeletePlayerPrefs();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Moving background using a Quad
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }

    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
