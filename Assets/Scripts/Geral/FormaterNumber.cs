using System;
using UnityEngine;

public class FormaterNumber : MonoBehaviour
{
    public static string FormatNumber(float number)
    {
        if (number < 1000)
        {
            return number.ToString("F0");
        }
        else if (number < 1000000)
        {
            float value = number / 1000f;
            int intValue = (int)value;
            int decimalValue = Mathf.FloorToInt((value - intValue) * 1000f);
            return string.Format("{0}K {1}", intValue, decimalValue);
        }
        else
        {
            string[] suffixes = { "M", "B", "T", "Q", "Qi" };
            int suffixIndex = 0;

            while (number >= 1000000f && suffixIndex < suffixes.Length)
            {
                suffixIndex++;
                number /= 1000000f;
            }

            string formattedNumber = number.ToString("N0");

            if (formattedNumber.EndsWith(".0"))
                formattedNumber = formattedNumber.Substring(0, formattedNumber.Length - 2);

            return formattedNumber + suffixes[suffixIndex - 1];
        }
    }

    public static string FormatDistance(float distanceInMeters)
    {
        if (distanceInMeters < 1000f)
        {
            return string.Format("{0}m", distanceInMeters.ToString("F0"));
        }
        else
        {
            float distanceInKilometers = distanceInMeters / 1000f;
            int kilometers = Mathf.FloorToInt(distanceInKilometers);
            float remainingMeters = distanceInMeters - kilometers * 1000f;

            if (remainingMeters < 1f)
            {
                return string.Format("{0}km", kilometers);
            }
            else
            {
                return string.Format("{0}km {1}m", kilometers, remainingMeters.ToString("F0"));
            }
        }
    }

}
