using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [Header("Unity Handles")]
    public GameObject partcile;
    public GameObject cube;
    public GameObject prism;
    public GameObject Sphere;
    public ParticleSystem Part;
    public GameObject ShapeChangeText;

    [Header("Colour Input")]
    public Color[] color;
    public Color defaultColour;
    public static int ColourIndex = 0;

    [Header("Integers")]
    [SerializeField] int shapeIndex = 0;
    public int checker;
    
    //bool isPart;
    [Header("Booleans")]
    public bool[] hasColour = new bool[4] { true,false, false, false};
    public bool[] hasShapeColour = new bool[3] { false, false, false };

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            //Shape Change
            CheckShape();

            ShapeChangeText.SetActive(false);
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
           ;
            //Colour change 
            CheckColour();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            
           
            CheckColourDown();
        }

        if (ColourIndex < -0)
            ColourIndex = 3;
        else if (ColourIndex > 3)
            ColourIndex = 0;

    }


    void CheckShape()
	{
        if (shapeIndex == 5)
            shapeIndex = 0;

        if (shapeIndex == 0 && hasColour[0])
        {
            checker = 0;

            //Sound for morphing
            FindObjectOfType<MusicManager>().Play("Morphing");

            partcile.SetActive(true);
            cube.SetActive(false);
            prism.SetActive(false);
            Sphere.SetActive(false);
            GameManager.instance.SwitchImages(shapeIndex , new Color(255, 255, 255, 0.29f));

            if (hasColour[1])
                shapeIndex = 1;
            else if (hasColour[2])
                shapeIndex = 2;
            else if (hasColour[3])
                shapeIndex = 3;

        }
       else if (shapeIndex == 1 && hasColour[1])
        {
            checker = 1;

            //Sound for morphing
            FindObjectOfType<MusicManager>().Play("Morphing");

            partcile.SetActive(false);
            cube.SetActive(true);
            prism.SetActive(false);
            Sphere.SetActive(false);
            GameManager.instance.SwitchImages(shapeIndex, color[0]);


            if (hasColour[2])
                shapeIndex = 2;
            else if (hasColour[3])
                shapeIndex = 3;
            else if (hasColour[0])
                shapeIndex = 0;
        }
        else if (shapeIndex == 2 && hasColour[2])
        {
            checker = 2;

            //Sound for morphing
            FindObjectOfType<MusicManager>().Play("Morphing");

            partcile.SetActive(false);
            cube.SetActive(false);
            prism.SetActive(false);
            Sphere.SetActive(true);
            GameManager.instance.SwitchImages(shapeIndex, color[1]);

            if (hasColour[3])
                shapeIndex = 3;
            else if(hasColour[0])
                shapeIndex = 0;
        }
        else if (shapeIndex == 3 && hasColour[3])
        {
            checker = 4;

            //Sound for morphing
            FindObjectOfType<MusicManager>().Play("Morphing");

            partcile.SetActive(false);
            cube.SetActive(false);
            prism.SetActive(true);
            Sphere.SetActive(false);
            GameManager.instance.SwitchImages(shapeIndex, color[2]);
            shapeIndex++;
        }
        else
            shapeIndex = 0;
    }
    public void CheckColour()
    {
        if (ColourIndex == 0)
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, defaultColour);
           
            if (hasShapeColour[0])
                ColourIndex++;
        }
        else if (ColourIndex == 1 && hasShapeColour[0])
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color[0]);
            
            if(hasShapeColour[1])
                ColourIndex++;
            else
                ColourIndex = 0;
        }
        else if (ColourIndex == 2 && hasShapeColour[1])
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color[1]);
           
            if(hasShapeColour[2])
                ColourIndex++;
            else
                ColourIndex = 0;
        }
        else if (ColourIndex == 3 && hasShapeColour[2])
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color[2]);
            ColourIndex = 0;
        }
        else
            ColourIndex = 0;
    }
    public void CheckColourDown()
    {
        if (ColourIndex == 0)
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, defaultColour);

            if (hasShapeColour[2])
                ColourIndex = 3;
            else
                ColourIndex--;
        }
        else if (ColourIndex == 1 && hasShapeColour[0])
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color[0]);

            if (hasShapeColour[1])
                ColourIndex = 2;
            else
                ColourIndex--;
        }
        else if (ColourIndex == 2 && hasShapeColour[1])
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color[1]);

            if (hasShapeColour[0])
                ColourIndex--;
            else
                ColourIndex = 0;
        }
        else if (ColourIndex == 3 && hasShapeColour[2])
        {
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color[2]);
            ColourIndex = 0;
        }
        else
            ColourIndex = 0;
    }
    public void ReturnShape()
	{
        shapeIndex = 0;
        ColourIndex = 0;
        //DefaultColour
        var m = Part.main;
        m.startColor = new ParticleSystem.MinMaxGradient(Color.black, defaultColour);

        //DefaultShape
        partcile.SetActive(true);
        cube.SetActive(false);
        prism.SetActive(false);
        Sphere.SetActive(false);
        GameManager.instance.SwitchImages(shapeIndex, new Color(255, 255, 255, 0.29f));

    }

    public bool ReturnShapeIndex(int indexS)
	{

        if (shapeIndex == indexS)
            return true;
        else
            return false;
	}
}
