using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;

    Enemy enemy;
    
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(PrintWaypointName());
    }
    void Start()
    {
        enemy = GetComponent<Enemy>();    
    }

    void FindPath()
    {
        path.Clear();
        
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

     IEnumerator PrintWaypointName()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 StartPosition = transform.position;
            Vector3 EndPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(EndPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(StartPosition, EndPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }


        }
        gameObject.SetActive(false);
        enemy.StealGold();
    }
    
    
}
