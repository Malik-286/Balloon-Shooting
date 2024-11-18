// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("IRnnLRptJpfok6co4pUPlHx9Mwpoa4uJ6EVQ/pJE/+zqhFek1mx69vNbeR7hyuGhpzGARGV3AJUmFtX3bd9cf21QW1R32xXbqlBcXFxYXV5JM0jL/ilgB7rAZURZ4O/234p7Wd9cUl1t31xXX99cXF38LCk8EMvfK1TIE1tQ+T/xn+Zcsg+E88w2eu/eKHtD7R4C3kNQdsWR+aHaSExtyjaMVLOLTJJUoGMtW5txVnLB3HS6mJk0vI8kS/5Tc5lMAu4F5NkuFdqt/da5cW72YqVU4UgDAzVndStFT16RukBhCy9snfcukp9ZMO9zh913olPh5573yJgUepBea3fnElokGI72+eLagyAYLmVpMyGs6MGXGtNkIszzHTvAgyj1TF9eXF1c");
        private static int[] order = new int[] { 11,8,3,11,13,8,11,13,8,11,13,12,13,13,14 };
        private static int key = 93;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
