using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsPool : MonoBehaviour
{
    //Все названия норм, поймешь
    public int lowSpawnRadius = 10;
    public int highSpawnRadius = 15;
    public int destroyPillRadius = 60;
    public int maxNumberOfPills = 10;

    public LayerMask obstLayer;

    public float refreshTime = 1;

    public GameObject pill;

    private float currentRefreshTime;
    private int currentNumberOfPills = 0;

    private System.Random rand = new System.Random();

    private List<GameObject> objectsPool = new List<GameObject>(20); // пул 



    void Start()
    {
        currentRefreshTime = refreshTime;
        for(int i = 0; i!= 20; i++)
        {
            GameObject obj = Instantiate(pill);
            obj.SetActive(false);
            objectsPool.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentRefreshTime -= Time.deltaTime;
        currentNumberOfPills = 0;
        foreach (GameObject obj in objectsPool)//Подсчет активных таблетОчек
        {
            
            if (obj.activeSelf)
            {
                if((obj.transform.position - transform.position).magnitude >= destroyPillRadius)
                {
                    obj.SetActive(false);
                }
                else
                {
                    currentNumberOfPills++;
                }
            }
        }




        if (currentNumberOfPills < maxNumberOfPills && currentRefreshTime<= 0) //стандартная операция по активации объекта из пула
        {
            currentRefreshTime = refreshTime;
            foreach (GameObject obj in objectsPool)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    while (true)
                    {
                        float angle = rand.Next(-180, 180) * Mathf.PI / 180;//угол относительно нуля, на котором будет лежать таблетОчка
                        int radius = rand.Next(lowSpawnRadius, highSpawnRadius);//Расстояние от игрока до таблетОчки
                        Collider2D overlap = Physics2D.OverlapCircle(new Vector2(transform.position.x + Mathf.Cos(angle) * radius, transform.position.y + Mathf.Sin(angle) * radius), 0.5f, obstLayer);
                        if (overlap == null)
                        {
                            obj.transform.position = new Vector3(transform.position.x + Mathf.Cos(angle) * radius, transform.position.y + Mathf.Sin(angle) * radius, -1);//активация таблетОчки в правильной позиции
                            return;
                        }
                    }
                }
            }

            GameObject objct = Instantiate(pill);
            objct.SetActive(false);
            objectsPool.Add(objct);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lowSpawnRadius);
        Gizmos.DrawWireSphere(transform.position, highSpawnRadius);
        Gizmos.DrawWireSphere(transform.position, destroyPillRadius);
    }
}
