using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

//GameManager is mostly used to switch between scenes, along with a bit of helping other scripts share data
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject textBox;
    public GameObject image;
    public GameObject canvas;
    public GameObject events;
    public GameObject startButton;
    public GameObject tutorialButton;
    public GameObject creditsButton;
    public GameObject helpButton;
    public GameObject creditsText;
    public GameObject backButton;

    // Audio
    public GameObject audioSource;
    public AudioMixer mixer;
    public GameObject volumeButton;
    public GameObject volumeSlider;

    private MoleculeValuesTable moleculeValuesTable = new MoleculeValuesTable();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
            DontDestroyOnLoad(audioSource);

        }
        else
        {
            if (canvas != null)
            {
                Destroy(canvas);
            }
            if (events != null)
            {
                Destroy(events);
            }
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        volumeButton.SetActive(false);
        textBox.SetActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        creditsButton.SetActive(false);
        helpButton.SetActive(false);
        canvas.SetActive(false);
        FadeOut("Play");
    }

    public void Tutorial()
    {
        volumeButton.SetActive(false);
        textBox.SetActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        creditsButton.SetActive(false);
        helpButton.SetActive(false);
        canvas.SetActive(false);
        FadeOut("Tutorial");
    }

    public void Help()
    {
        volumeButton.SetActive(false);
        textBox.SetActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        creditsButton.SetActive(false);
        helpButton.SetActive(false);
        canvas.SetActive(false);
        FadeOut("Help");
    }

    public void BackToMenu()
    {
        canvas.SetActive(true);
        volumeButton.SetActive(true);
        textBox.SetActive(true);
        startButton.SetActive(true);
        tutorialButton.SetActive(true);
        creditsButton.SetActive(true);
        helpButton.SetActive(true);

        FadeIn("MainMenu");
    }

    public void ShowCredits()
    {
        volumeButton.SetActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        creditsButton.SetActive(false);
        helpButton.SetActive(false);
        creditsText.SetActive(true);
        backButton.SetActive(true);
    }

    public void HideCredits()
    {
        volumeButton.SetActive(true);
        startButton.SetActive(true);
        tutorialButton.SetActive(true);
        creditsButton.SetActive(true);
        helpButton.SetActive(true);
        creditsText.SetActive(false);
        backButton.SetActive(false);
    }

    IEnumerator ColorLerp(Color endValue, float duration)
    {
        float time = 0;
        Image sprite = image.GetComponent<Image>();
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
    }
    
    IEnumerator LoadYourAsyncScene(string scene, Color finalColor)
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            StartCoroutine(ColorLerp(new Color(0, 0, 0, 1), 0.5f));
            while (!image.GetComponent<Image>().color.Equals(new Color(0, 0, 0, 1)))
            {
                yield return null;
            }
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        StartCoroutine(ColorLerp(finalColor, 0.5f));
    }
    

    public void FadeOut(string whichScene)
    {
        StartCoroutine(LoadYourAsyncScene(whichScene, new Color(0, 0, 0, 0)));
    }

    public void FadeIn(string whichScene)
    {
        StartCoroutine(LoadYourAsyncScene(whichScene, new Color(1, 1, 1, 1)));
    }

    public double getMoleculedH(string name) { return moleculeValuesTable.getMoleculedH(name); }
    public double getMoleculedS(string name) { return moleculeValuesTable.getMoleculedS(name); }
    public double getMoleculedG(string name) { return moleculeValuesTable.getMoleculedG(name); }

    public Dictionary<string, Molecule> getReactionTable() { return moleculeValuesTable.getReactionValues(); }

    // Volume Controls
    // Activates the volume slider by clicking the icon
    public void volumeOnClick()
    {
        if (volumeSlider.activeSelf == true)
        {
            volumeSlider.SetActive(false);
        }
        else
        {
            volumeSlider.SetActive(true);
        }
    }
    // Sets the volume using the slider
    public void setVolume(float sliderValue)
    {
        mixer.SetFloat("masterVol", (Mathf.Log10(sliderValue) * 20));
    }
}
