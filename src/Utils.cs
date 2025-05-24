using SML;

namespace Aprilgen
{
    public static class Utils
    {
        public static bool BTOS2Exists()
        {
            return ModStates.IsEnabled("curtis.tuba.better.tos2");
        }
        public static bool IsBTOS2()
        {
            bool result;
            try
            {
                result = Utils.IsBTOS2Bypass();
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private static bool IsBTOS2Bypass()
        {
            return BTOS2Exists() && BetterTOS2.BTOSInfo.IS_MODDED;
        }
    }
}