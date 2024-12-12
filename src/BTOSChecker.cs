using System;
using BetterTOS2;
using SML;

//this class is only used inside ModStates.IsLoaded("curtis.tuba.better.tos2") checks
//which seems to avoid the error where it fails to load the type bc its not in the function itself (?)
public class BTOSChecker
{
    private static Lazy<bool> BTOS_ACTIVE = new(() => BTOSInfo.IS_MODDED);
    public static bool CheckBTOSIsModded()
    {
        return ModStates.IsLoaded("curtis.tuba.better.tos2") && BTOS_ACTIVE.Value;
    }
}
