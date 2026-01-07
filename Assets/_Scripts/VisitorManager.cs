using UnityEngine;

public class VisitorManager : MonoBehaviour
{
    public static VisitorManager Instance;
    float time;
    [SerializeField]
    GameObject visitorScreen;
    bool hourHappened;
    public void updateTime(float newTime)
    {
        this.time = newTime;
        // Debug.Log("Updating time!");
        if(IsNewHour(newTime))
        {
            // Debug.Log("new hour!");
            if(GetHour(time) == 1)
            {
                // Debug.Log("Is hour 1, showing visitor");
                visitorScreen.SetActive(true);
            }
        }

    }

    bool IsNewHour(float time)
    {
        int hour = (int) time /60;
        int minutes = (int) time - hour * 60;
        bool isNewHour = minutes == 0 && hour != 0;
        if(isNewHour)
        {
            if(hourHappened) return false;
            else
            {
                hourHappened = true;
                return true;
            }
        }
        else
        {
            hourHappened = false;
            return false;
            
        }
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
