using BetterTOS2;

//this class is only used inside ModStates.IsLoaded("curtis.tuba.better.tos2") checks
//which seems to avoid the error where it fails to load the type bc its not in the function itself (?)
public class BTOSChecker
{ 
    public static bool CheckBTOSIsModded()
    {
        return BTOSInfo.IS_MODDED;
    }
}
