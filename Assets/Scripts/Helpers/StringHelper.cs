using System.IO;
using System.Reflection;
using UnityEngine;

public static class StringHelper 
{
    public static string GetColoredString(this string s, Color color)
    {
        string colorName = "";
        PropertyInfo[] props = color.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);

        foreach (PropertyInfo prop in props)
        {
            if ((Color)prop.GetValue(null, null) == color) { colorName = prop.Name; }
        }
        if (colorName == "") colorName = color.ToString();

        string s1 = $"<color={colorName}> {s} </color>";
        return s1;
    }

   
        public static string GetSelfiePath()
        {
            string filename = $"photo{GetSelfieNum()}.png";
            return Path.Combine(Application.persistentDataPath, filename);
        }

        static string PrefName = "photoNum";

        private static int GetSelfieNum()
        {
            int n = PlayerPrefs.GetInt(PrefName, 0);
            return n;
        }

        public static void IncreaseSelfieNum ()
        {
            int n = PlayerPrefs.GetInt(PrefName, 0);
            if (++n > 10) n = 0;
            PlayerPrefs.SetInt(PrefName,n);
        }


}
