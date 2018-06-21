using UnityEngine;

public class TimeManager : MonoBehaviour {

    // Slow motion variables
    public float slowMotionFactor = 0.5f;
    public float slowMotionLength = 2.0f;

    // Function to call for slow motion
    public void SlowMotion ()
    {
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    // Function to call to return to normal motion
    public void NormalMotion ()
    {
        while (Time.timeScale < 1.0f)
        {
            Time.timeScale += (1 / slowMotionLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
        }
    }
}
