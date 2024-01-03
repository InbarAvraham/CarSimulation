using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{

    [SerializeField] public List<Transform> targets;
    int currentTarget = 0;
    public Transform Prefab;

    // Move to main Scene
    public void NextScene()
    {
        if (targets.Count > 0)
        {
            SceneManager.LoadScene("Main");
        }
        
    }
    void Update()
    {
        // On click on terrian place the prefab (the distentions).
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
              Vector3 pos = hit.point;
              pos.y += 2;
              Transform newprefab = Instantiate(Prefab, pos, Quaternion.identity);
              newprefab.Rotate(-90, 0, 0);
              targets.Add(newprefab); 
            }
            
        }
    }
    public Transform GetDistenation()
    {
        SceneManager.MoveGameObjectToScene(targets[currentTarget].gameObject, SceneManager.GetActiveScene());
        return targets[currentTarget];
    }

    // Increse current targer by 1 (move to next distention). If no more tagrets end simulation.
    public void IncreseCurrenttarget()
    {
        currentTarget++;
        if (currentTarget == targets.Count)
        {
            SceneManager.LoadScene("Game over");
        }
    }

}
