using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDropController : MonoBehaviour {

    public Color altColor = Color.black;
    public Renderer rend;

    public GameObject heart;
    public GameObject coin;
    public GameObject randomDrop;

    public ParticleSystem readyDrop;
    public GameObject particle;

    private bool heartDropped = false;

    private float dropPositionX;
    private float dropPositionZ;

    private int randomDropNumber;

    private int colorChanger = 1;

    //I do not know why you need this?
    void Example()
    {
        altColor.g = 0f;
        altColor.r = 0f;
        altColor.b = 1f;
        altColor.a = 1f;
    }

    void Start()
    {
        particle.SetActive(false);
        readyDrop.Stop();
        //Call Example to set all color values to zero.
        Example();
        //Get the renderer of the object so we can access the color
        rend = GetComponent<Renderer>();
        //Set the initial color (0f,0f,0f,0f)
        rend.material.color = altColor;
    }

    void FixedUpdate()
    {
        rend.material.color = altColor;
        if (altColor.g >= 1 && altColor.r >= 1 && altColor.b <= 0)
        {
            colorChanger = 1;
        }

        if (altColor.g <= 0 && altColor.r >= 1 && altColor.b <= 0)
        {
            colorChanger = 2;
        }

        if (altColor.g <= 0 && altColor.r >= 1 && altColor.b >= 1)
        {
            colorChanger = 3;
        }

        if (altColor.g <= 0 && altColor.r <= 0 && altColor.b >= 1)
        {
            colorChanger = 4;
        }

        if (altColor.g >= 1 && altColor.r <= 0 && altColor.b >= 1)
        {
            colorChanger = 5;
        }

        if (altColor.g >= 1 && altColor.r <= 0 && altColor.b <= 0)
        {
            colorChanger = 6;
        }

        switch (colorChanger)
        {
            case 1:
                altColor.g -= 0.03f;
                rend.material.color = altColor;
                break;
            case 2:
                altColor.b += 0.03f;
                rend.material.color = altColor;
                break;
            case 3:
                altColor.r -= 0.03f;
                rend.material.color = altColor;
                break;
            case 4:
                altColor.g += 0.03f;
                rend.material.color = altColor;
                break;
            case 5:
                altColor.b -= 0.03f;
                rend.material.color = altColor;
                break;
            case 6:
                altColor.r += 0.03f;
                rend.material.color = altColor;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                particle.SetActive(true);
                readyDrop.Play();
                for(int i = 1; i < 5; i++)
                {
                    switch(i)
                    {
                        case 1:
                            dropPositionX = -2.0f;
                            dropPositionZ = 0.0f;
                            break;
                        case 2:
                            dropPositionX = 2.0f;
                            dropPositionZ = 0.0f;
                            break;
                        case 3:
                            dropPositionX = 0.0f;
                            dropPositionZ = -2.0f;
                            break;
                        case 4:
                            dropPositionX = 0.0f;
                            dropPositionZ = 2.0f;
                            break;
                    }
                    randomDropNumber = Random.Range(1, 5);
                    switch(randomDropNumber)
                    {
                        case 1:
                            if(!heartDropped)
                            {
                                Instantiate(heart, transform.position + new Vector3(dropPositionX, 0.0f, dropPositionZ), Quaternion.Euler(0, 90 * i, 0));
                                heartDropped = true;
                            }
                            break;
                        case 2:
                            Instantiate(coin, transform.position + new Vector3(dropPositionX, 0.0f, dropPositionZ), Quaternion.Euler(0, 90 * i, 0));
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                }
                Destroy(randomDrop, 0.1f);
            }
        }
    }
}

