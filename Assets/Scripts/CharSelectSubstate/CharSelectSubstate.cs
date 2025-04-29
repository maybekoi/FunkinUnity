using UnityEngine;

public class CharSelectSubstate : MonoBehaviour
{
    [Header("Main CharSelect Shit")]
    public GameObject[] objectsToToggle;
    public int currentCase = 0;
	public GameObject[] CharSelectIcons;
	public GameObject awyeahcanvas;
    public GameObject CSSubstateCanvas;
    [Header("Shit to MAKE SURE the CharSelection Shit works!!")]
    public GameObject[] MCanvasIcons;
    public GameObject[] MainGameChars;

    public void OpenSubstate()
    {
		awyeahcanvas.SetActive(false);
        CSSubstateCanvas.SetActive(true);
	}

    public void BACK()
    {
		awyeahcanvas.SetActive(true);
        CSSubstateCanvas.SetActive(false);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchCase();
        }
		if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // I'll do this shii later, I gave myself a damn headache.
            //UnSwitchCase();
        }
    }

    void SelectCurChar()
    {
        currentCase = (currentCase + 1) % 3;

        switch (currentCase)
        {
            case 0:
                // CHAR SELECT CANVAS
				CharSelectIcons[0].SetActive(true);
				CharSelectIcons[1].SetActive(false);
				CharSelectIcons[2].SetActive(false);
                objectsToToggle[0].SetActive(true);
                objectsToToggle[1].SetActive(false);
				objectsToToggle[2].SetActive(false);
                // MAIN GAME & MAIN CANVAS:
                MCanvasIcons[0].SetActive(true);
                MainGameChars[0].SetActive(true);
                MCanvasIcons[1].SetActive(false);
                MainGameChars[0].SetActive(false);    
                MCanvasIcons[1].SetActive(false);
                MainGameChars[2].SetActive(false);                          
                break;
            case 1:
                // CHAR SEL ICONS
				CharSelectIcons[0].SetActive(false);
				CharSelectIcons[1].SetActive(true);
				CharSelectIcons[2].SetActive(false);			
                objectsToToggle[0].SetActive(false);
                objectsToToggle[1].SetActive(true);
				objectsToToggle[2].SetActive(false);
                // MAIN GAME & MAIN CANVAS:
                MCanvasIcons[0].SetActive(false);
                MainGameChars[0].SetActive(false);
                MCanvasIcons[1].SetActive(true);
                MainGameChars[0].SetActive(true);    
                MCanvasIcons[1].SetActive(false);
                MainGameChars[2].SetActive(false);  
                break;
            case 2:
                // CHAR SEL
				CharSelectIcons[0].SetActive(false);
				CharSelectIcons[1].SetActive(false);
				CharSelectIcons[2].SetActive(true);			
			    objectsToToggle[0].SetActive(false);
                objectsToToggle[1].SetActive(false);
				objectsToToggle[2].SetActive(true);
                // MAIN GAME & MAIN CANVAS:
                MCanvasIcons[0].SetActive(false);
                MainGameChars[0].SetActive(false);
                MCanvasIcons[1].SetActive(false);
                MainGameChars[0].SetActive(false);    
                MCanvasIcons[1].SetActive(true);
                MainGameChars[2].SetActive(true);  
				break;
        }
    }

    /*
    void UnSwitchCase()
    {
        currentCase = (currentCase + 1) % 3;

        switch (currentCase)
        {
            case 0:
				CharSelectIcons[0].SetActive(true);
				CharSelectIcons[1].SetActive(false);
				CharSelectIcons[2].SetActive(false);
                objectsToToggle[0].SetActive(true);
                objectsToToggle[1].SetActive(false);
				objectsToToggle[2].SetActive(false);
                // CANSIJSHB
                MCanvasIcons[0].SetActive(true);
                MainGameChars[0].SetActive(false);
                MCanvasIcons[1].SetActive(false);
                MainGameChars[0].SetActive(true);    
                MCanvasIcons[1].SetActive(false);
                MainGameChars[2].SetActive(true);  
                break;
            case 1:
				CharSelectIcons[0].SetActive(false);
				CharSelectIcons[1].SetActive(true);
				CharSelectIcons[2].SetActive(false);			
                objectsToToggle[0].SetActive(false);
                objectsToToggle[1].SetActive(true);
				objectsToToggle[2].SetActive(false);
                // CANSIJSHB
                MCanvasIcons[0].SetActive(false);
                MainGameChars[0].SetActive(false);
                MCanvasIcons[1].SetActive(false);
                MainGameChars[0].SetActive(false);    
                MCanvasIcons[1].SetActive(false);
                MainGameChars[2].SetActive(true);                 
                break;
            case 2:
				CharSelectIcons[0].SetActive(false);
				CharSelectIcons[1].SetActive(false);
				CharSelectIcons[2].SetActive(true);			
			    objectsToToggle[0].SetActive(false);
                objectsToToggle[1].SetActive(false);
				objectsToToggle[2].SetActive(true);
				break;
        }
    }
    */

    void SwitchCase()
    {
        currentCase = (currentCase + 1) % 3;

        switch (currentCase)
        {
            case 0:
				CharSelectIcons[0].SetActive(false); // BF ICON
				CharSelectIcons[1].SetActive(false); // DD ICON
				CharSelectIcons[2].SetActive(true); // PICO ICON
                objectsToToggle[0].SetActive(false); // BF 
                objectsToToggle[1].SetActive(false); // DD
				objectsToToggle[2].SetActive(true);  // PICO
                break;
            case 1:
				CharSelectIcons[0].SetActive(false);
				CharSelectIcons[1].SetActive(true);
				CharSelectIcons[2].SetActive(false);
                objectsToToggle[0].SetActive(false);
                objectsToToggle[1].SetActive(true);
				objectsToToggle[2].SetActive(false);
                break;
            case 2:
				CharSelectIcons[0].SetActive(true);
				CharSelectIcons[1].SetActive(false);
				CharSelectIcons[2].SetActive(false);
			    objectsToToggle[0].SetActive(true);
                objectsToToggle[1].SetActive(false);
				objectsToToggle[2].SetActive(false);
				break;
        }
    }
}
