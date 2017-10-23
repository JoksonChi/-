using System;

namespace Club
{
    public static class ExtendMethod
    {
        public static int ToInt(this string sender,int defaultValue=0)
        {
            int result = 0;
            try
            {
                result = int.Parse(sender);
                return result;
            }
            catch (Exception ex)
            {
                return defaultValue;

            }
        }

        public static bool ToBit(this int sender)
        {
            var a = false;
            if (sender > 0)
            {
                a = true;
            }
            return a;
        }

    }
}