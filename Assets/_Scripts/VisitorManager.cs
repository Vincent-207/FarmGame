using UnityEngine;

public class VisitorManager : MonoBehaviour
{
    public static VisitorManager Instance;
    float time;
    [SerializeField]
    GameObject visitorScreen;
    public void updateTime(float newTIme)
    {
        this.time = newTIme;
        Debug.Log("Updating time!");
        if(IsNewHour(newTIme))
        {
            Debug.Log("new hour!");
            visitorScreen.SetActive(true);
        }

    }

    bool IsNewHour(float time)
    {
        int hour = (int) time /60;
        int minutes = (int) time - hour * 60;
        return minutes == 0 ? true : false;
    }

    int GetHour(float time)
    {
        int hour = (int) time/60;
        return hour;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
}
