using System;

namespace SQLLibrary
{
    static public class SQLConvert
    {
        static public string ToString(object obj)
        {
            string result = string.Empty;
            try
            {
                result = Convert.ToString(obj);
            }
            catch { }

            return result;
        }

        static public int ToInt(object obj)
        {
            int result = -1;
            try
            {
                result = Convert.ToInt32(obj);
            }
            catch { }

            return result;
        }

        static public float ToFloat(object obj)
        {
            float result = -0.0001F;
            try
            {
                result = (float)Convert.ToDouble(obj);
            }
            catch { }

            return result;
        }

        static public double ToDouble(object obj)
        {
            double result = -0.0001;
            try
            {
                result = Convert.ToDouble(obj);
            }
            catch { }

            return result;
        }

        static public DateTime ToDateTime(object obj)
        {
            DateTime result = DateTime.MinValue;
            try
            {
                result = Convert.ToDateTime(obj);
            }
            catch { }

            return result;
        }
    }
}
