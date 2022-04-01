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
        
        Vector3 ogPositon = Player.instance.transform.position;
        float elapsed = 0;

            while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            Player.instance.transform.position = new Vector3(Player.instance.transform.position.x + x, Player.instance.transform.position.y + y, Player.instance.transform.position.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        Player.instance.transform.position = ogPositon;
    }

    public void StartShake() 
    {
        StartCoroutine(CameraShake(5f, 0.1f));
    }


}
