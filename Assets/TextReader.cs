using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextReader : MonoBehaviour
{
    public GameObject[] bullet;
    public Transform bulletSpawn;

    float timeToGo;
    public float addedTime;
    string ln;

    private void Start()
    {
        timeToGo = Time.fixedTime + addedTime;
        ReadString();
    }
    private void FixedUpdate()
    {
        ReadString();
    }

    void ReadString()
    {
        string path = "Assets/pattern.txt";

        StreamReader reader = new StreamReader(path);

        //string line = reader.ReadLine();

        using(StreamReader file = new StreamReader(path))
        {
            int counter = 0;

            while((ln = file.ReadLine()) != null)
            {
                if (ln == "0" && Time.fixedTime >= timeToGo)
                {
                    Debug.Log(ln);
                    counter++;
                    SpawnBullet();
                    timeToGo = Time.fixedTime + addedTime;
                }
            }
        }

        

        reader.Close();
    }

    void SpawnBullet()
    {
        Instantiate(bullet[0], bulletSpawn);
    }
}
