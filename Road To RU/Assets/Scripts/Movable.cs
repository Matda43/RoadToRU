using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Element
{

    public float speed;
    Vector3 direction = Vector3.zero;
    Vector3 spawn;
    float plateformSize = 30;
    bool instanciate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Reset()
    {
        this.transform.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (instanciate)
        {
            if (direction.x == 1 && this.transform.position.x - transform.localScale.x >= 15)
            {
                this.transform.position = spawn;
            }
            else if (direction.x == -1 && this.transform.position.x + transform.localScale.x <= -15)
            {
                this.transform.position = spawn;
            }
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    public void setDirection(Vector3 new_direction)
    {
        direction = new_direction;
        spawn = new Vector3(plateformSize / 2 + transform.localScale.x, 0, 0) * -direction.x;
        this.transform.position = spawn;
        instanciate = true;
    }
}
