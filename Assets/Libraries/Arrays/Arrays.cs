using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFraese
{
    /// <summary>
    /// Utilities for packing and unpacking arrays. Main usage is packing
    /// serialized command arrays into a serialized move array, and cleanly
    /// packing an array.
    /// </summary>
    public class Arrays
    {
		/// <summary>
		/// Converts a 2D jagged array to a human readable string for
        /// easier debugging.
		/// </summary>
		public static string ToString<T>(T[][]array)
        {
			if (array == null) { return "null"; }
			if (array.Length == 0) return "{[]}";
			if (array.Length == 1) return $"[{ToString(array[0])}]";

			string arrayString = "{\n";
			for (int i = 0; i < array.Length; i++)
			{
                string term = i < array.Length - 1 ? "\n" : "\n}";
				arrayString += ToString(array[i]) + term;
			}
            return arrayString;
		}
        /// <summary>
        /// Converts array to a human readable string for easier debugging.
        /// </summary>
        public static string ToString<T>(T[] array)
        {
            if (array == null) { return "null"; }
            if (array.Length == 0) return "[]";
            if (array.Length == 1) return $"[{array[0]}]";

            string arrayString = "[";
            for (int i = 0; i < array.Length; i++)
            {
                string term = i < array.Length - 1 ? ", " : "]";
                arrayString += array[i].ToString() + term;
            }
            return arrayString;
        }
    }
}
