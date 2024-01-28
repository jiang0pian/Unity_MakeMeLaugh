using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class qiehuanchangjing : MonoBehaviour
{
    public string diyiguan;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(diyiguan);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
