using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Evidence : MonoBehaviour
{

    public int evidenceCount;
    public Text evidenceText;
    public GameObject door;
    private bool doorDestroyed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        evidenceText.text = "Evidence: " + evidenceCount.ToString();

        if (evidenceCount == 2 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }
    }
}
