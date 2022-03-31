using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    public static Utils instance { get; private set; }
    // Start is called before the first frame update
   public void Start()
    {
        instance = this;
    }

    // Update is called once per frame
   public void Update()
    {
        
    }

    public IEnumerator CameraShake(float duration, float magnitude) 
    {
        
        Vector3 ogPositon = Player.instance.cam.transform.position;
        float elapsed = 0;

            while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;


            Player.instance.cam.transform.position = new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return 0;


        }

        Player.instance.cam.transform.position = ogPositon;

    }

    public void StartShake() 
    {
        StartCoroutine(CameraShake(5f, 0.1f));
    }


}
