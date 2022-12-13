using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiGestion : MonoBehaviour
{
    float speed = 200;

    bool boing = false;
    bool aRebondi = false;

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rt.localPosition.y > 0 && !aRebondi)
            rt.localPosition += Vector3.down * speed * Time.deltaTime;
        else if (rt.localPosition.y < 100 && !boing)
        {
            rt.localPosition += Vector3.up * speed * Time.deltaTime;
            aRebondi = true;
            speed = 150;
        }
        else if (rt.localPosition.y > 0)
        {
            boing = true;
            rt.localPosition += Vector3.down * speed * Time.deltaTime;
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("World");
    }
}
