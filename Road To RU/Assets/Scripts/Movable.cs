using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Movable
/// </summary>
public class Movable : MonoBehaviour
{
    // Offset speed
    public float speed;

    // Offset direction
    Vector3 direction;

    // Scale of the gameObject
    Vector3 scale;

    // Boolean to indicate when execute really the script
    bool instanciate = false;

    // Boolean to indicate if the gameObject should be destroy
    bool shouldBeDestroyed = false;

    // Update is called once per frame
    void Update()
    {
        if (instanciate)
        {
            if (direction.x > 0 && this.transform.position.x > scale.x + GetComponent<Collider>().bounds.size.x)
            {
                shouldBeDestroyed = true;
            }
            else if (direction.x < 0 && this.transform.position.x < -scale.x - GetComponent<Collider>().bounds.size.x)
            {
                shouldBeDestroyed = true;
            }
        }
    }

    // Called each frame
    private void FixedUpdate()
    {
        if (instanciate && !shouldBeDestroyed)
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Set the offset direction, the scale of the gameObject and the initial position 
    /// </summary>
    /// <param name="new_direction">New offset direction</param>
    /// <param name="position">Initial position</param>
    /// <param name="new_scale">Scale</param>
    public void setDirection(Vector3 new_direction, Vector3 position, Vector3 new_scale)
    {
        direction = new_direction;
        scale = new_scale;
        Vector3 new_position = new Vector3(position.x + (scale.x + GetComponent<Collider>().bounds.size.x) * -direction.x, position.y, position.z);
        this.transform.position = new_position;
        instanciate = true;
    }

    /// <summary>
    /// Check if the gameObject should be destroyed by his parent
    /// </summary>
    /// <returns>Bollean to indicate if the gameObject should be destroyed</returns>
    public bool shouldBeDestroyedByParent()
    {
        return shouldBeDestroyed;
    }
}
