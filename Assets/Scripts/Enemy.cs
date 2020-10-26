using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Waypoints[] navPoints;
  private Transform target;
  private Vector3 direction;
  public float amplify = 1;
  private int index = 0;
  private bool move = true;

  public int health;
  public int worth;
  public Purse purse;

  // Start is called before the first frame update
  void Start()
  {
    //Place our enemy at the start point
    transform.position = navPoints[index].transform.position;
    NextWaypoint();
    
    //Move towards the next waypoint
    //Retarget to the following waypoint when we reach our current waypoint
    //Repeat through all of the waypoints until you reach the end
  }

  // Update is called once per frame
  void Update()
  {
    if (move)
    {
      transform.Translate(direction.normalized * Time.deltaTime * amplify);

      if ((transform.position - target.position).magnitude < .1f)
      {
        NextWaypoint();
      }
    }
    this.HandleClick();
  }

  private void NextWaypoint()
  {
    if (index < navPoints.Length - 1)
    {
      index += 1;
      target = navPoints[index].transform;
      direction = target.position - transform.position;
    }
    else
    {
      move = false;
    }
  }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if(hit.transform.gameObject.name == this.name)
                {
                    this.Damage(10);
                    Debug.Log(this.health);
                }
            }
        }
    }

    public void Damage(int damage)
    {
        StartCoroutine(FlashColor());
        this.health -= damage;
        if (this.health < 1)
            Destroy();
    }

    public void Destroy()
    {
        this.purse.AddCoin(this.worth);
        Destroy(this.gameObject);
    }

    private IEnumerator FlashColor()
    {
        var renderer = this.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            var color = renderer.material.color;
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.25f);
            renderer.material.color = color;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
