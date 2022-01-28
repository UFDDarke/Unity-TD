using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuManager : MonoBehaviour
{
    public GameObject menu;
    public int numSegments;
    public List<GameObject> segments = new List<GameObject>();
    public GameObject prefab; // Prefab for segments
    public float angleBetween;
    // TODO: Float variable to control radius
    // TODO: Shader has a little animation to signify when segment is hovered

    public Vector2 moveInput;
    void activate(int numSegments_)
	{
        // Populates segments array, then activates menu
        numSegments = numSegments_;
        populate();

        menu.SetActive(true);

	}

    void populate()
	{
        // Instantiates segment prefabs
        for(int i = 0; i < numSegments; i++)
		{
            GameObject newObj = Instantiate(prefab);
            //newObj.transform.parent = this.gameObject.transform;
            newObj.transform.SetParent(this.gameObject.transform, false);
            newObj.transform.position = this.gameObject.transform.position;

            angleBetween = (360f / numSegments);

            newObj.transform.rotation = Quaternion.identity;
            newObj.transform.rotation = Quaternion.Euler(0, 0, -i * angleBetween);
            newObj.GetComponent<Image>().fillAmount = ((1f / numSegments) + 0.00001f);


            segments.Add(newObj);
		}
	}

    void Update()
    {
        // TODO: Update radius of shader of segments

        // Find mouse distance from center
        float distance = Vector3.Distance(new Vector3(Input.mousePosition.x, Input.mousePosition.y), new Vector3(Screen.width / 2f, Screen.height / 2f));
        Debug.Log(distance);


        if(menu.activeInHierarchy)
		{
            moveInput.x = Input.mousePosition.x - Screen.width / 2f;
            moveInput.y = Input.mousePosition.y - Screen.height / 2f;
            moveInput.Normalize();

            //Debug.Log(moveInput);
            

            if(moveInput != Vector2.zero)
			{
                // calculate angle
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) / Mathf.PI;
                angle *= 180;
                angle += 90f;
                if(angle < 0)
				{
                    angle += 360;
				}

                for(int i = 0; i < numSegments; i++)
				{
                    if(angle > i * angleBetween && angle < (i + 1) * angleBetween && distance > 210f)
					{
                        // wacky block that finds what segment we're aiming at
                        //Debug.Log("Segment " + i);
                        segments[i].GetComponent<RadialSegment>().setActive();
					} else
					{
                        // deactivate all others
                        segments[i].GetComponent<RadialSegment>().setInactive();
                    }
				}



			}
		}
    }

	private void Awake()
	{
        activate(7);
	}
}
