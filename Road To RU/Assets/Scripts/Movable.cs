using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Element
{

    public float speed;
    Vector3 direction;
    Vector3 scale;
    bool instanciate = false;
    bool shouldBeDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instanciate)
        {
            if (direction.x > 0 && this.transform.position.x > scale.x/2 + GetComponent<Collider>().bounds.size.x/2)
            {
                shouldBeDestroyed = true;
            }
            else if (direction.x < 0 && this.transform.position.x < scale.x/-2 - GetComponent<Collider>().bounds.size.x/2)
            {
                shouldBeDestroyed = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (instanciate && !shouldBeDestroyed)
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void setDirection(Vector3 new_direction, Vector3 position, Vector3 new_scale)
    {
        direction = new_direction;
        scale = new_scale;
        Vector3 new_position = new Vector3(position.x + (scale.x/2 + GetComponent<Collider>().bounds.size.x/2) * -direction.x, position.y, position.z);
        this.transform.position = new_position;
        instanciate = true;
    }

    public bool shouldBeDestroyedByParent()
    {
        return shouldBeDestroyed;
    }
}
