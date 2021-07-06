using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CosmeticObjectsPool : MonoBehaviour
{
    //Все названия норм, поймешь
    public int heightOfSpawn = 5; 
    public int wigthOfSpawn = 5;
    public float spawnTime = 2; //кд на спавн
    float currentSpawnTime; //время с последнего спавна

    System.Random rand = new System.Random();


    public List<GameObject> allCosmeticObjects = new List<GameObject>(); //список объектов, которые можно заспавнить (все типы пузырок)
    List<GameObject> objectsPool = new List<GameObject>(); // пул пузырок



    void Start()
    {
        currentSpawnTime = spawnTime;


        for(int i = 0; i!= 10; i++) //мутим 10 пузырок для начала
        {
        
            GameObject obj = Instantiate(allCosmeticObjects[rand.Next(allCosmeticObjects.Count)]);
            obj.SetActive(false);
            objectsPool.Add(obj);
        
        }

    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime -= Time.deltaTime;


        if(currentSpawnTime<= 0)
        {
            
            foreach(GameObject obj in objectsPool) //проходим по всем пузыркам в пуле
            {
                
                if (!obj.activeSelf) //проверка на актива
                {
            
                    //активация пузырки и рандомные координаты
                    obj.SetActive(true);
                    obj.transform.position = new Vector3(rand.Next(-wigthOfSpawn, wigthOfSpawn)+gameObject.transform.position.x, rand.Next(-heightOfSpawn, heightOfSpawn) + gameObject.transform.position.y, -1);//Не бей, пжпжпжпжпжпжпж. тут просто координаты спавна
                    currentSpawnTime = spawnTime;
                
                    return;//выход, чтобы цикл не завершился
               
                }

            }

            //если цикл завершился, что все объекты используются и надо делать новый
            GameObject objct = Instantiate(allCosmeticObjects[rand.Next(allCosmeticObjects.Count)]);
            objct.SetActive(false);
            objectsPool.Add(objct);//добавляем неактивный объект в пул и идем на следующую итерацию, на которой он активируется, ибо таймер не откатился
        
        }
    }
}
