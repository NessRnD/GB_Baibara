using UnityEngine;

public class OnOffPlayerController : MonoBehaviour
{
    //private ViewControl viewControl;
    private MoveControl moveControl;
    private void Start()
    {
        //viewControl = GetComponent<ViewControl>();
        moveControl = GetComponent<MoveControl>();
    }

    public void ActivateMoveControl()
    {
        moveControl.enabled = true;
        //viewControl.enabled = true;
    }

    public void DeactivateMoveControl()
    {
        moveControl.enabled = false;
        //viewControl.enabled = false;
    }


}
