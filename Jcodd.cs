using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace YourOwnProject.EstreSharp
{
    /*

    MIT License

    Copyright (c) 2023 Esterkxz (Ester1 / 에스터1z) converted by Estre Soliette 2024 (since by v0.3)

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.



    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.

    */

    // JSON Characterized Object Data Definition //
    //
    // The JSON based lite code format
    //
    // v0.7 / release 2025.02.09
    //
    // Take to be liten from JSON code to smaller converted characters for like as BASE64.
    //
    //
    // :: Code regulations
    //
    // 1. null is n, true is t, false is f.
    // 2. No space and carriage return & line feed on code. Only allowed in data.
    // 3. Omit "" variable definition.


    public partial class Jcodd
    {
        /// <summary>
        /// Characterize JSON
        /// </summary>
        /// <param name="json">json</param>
        /// <returns>jcodd</returns>
        public static string ToCodd(string json)
        {
            string ex;

            string p1 = Regex.Replace(Regex.Replace(Regex.Replace(json, @"^[\b\t]+", ""), @"[\b\t\r\n]+$", ""), @"[\b\t]+", "");
            string p2 = Regex.Replace(Regex.Replace(p1, @"([\[\,\:])null([\]\,\}])", "$1n$2"), @"([\[\,\:])null([\]\,\}])", "$1n$2");
            string p3 = Regex.Replace(Regex.Replace(p1, @"([\[\,\:])true([\]\,\}])", "$1t$2"), @"([\[\,\:])true([\]\,\}])", "$1t$2");
            string p4 = Regex.Replace(Regex.Replace(p1, @"([\[\,\:])false([\]\,\}])", "$1f$2"), @"([\[\,\:])false([\]\,\}])", "$1f$2");
            string p5 = Regex.Replace(p2, @"([\{\,])\""([^\""]*)\""\:", "$1$2:");
            if (Regex.Match(p5, @"[\u0000-\u001F|\u0080-\uFFFF]").Length > 0)
            {
                string p6 = Escape(p5);
                ex = p6;
            }
            else ex = p5;

            return ex;
        }

        /// <summary>
        /// Convert object to JCODD directly
        /// </summary>
        /// <typeparam name="T">own type</typeparam>
        /// <param name="obj">own object</param>
        /// <returns>JCODD</returns>
        public static string Codify<T>(T obj)
        {
            string json = JsonSerializer.Serialize(obj);

            return ToCodd(json);
        }

        /// <summary>
        /// Parse JCODD to JSON
        /// </summary>
        /// <param name="codd">jcodd</param>
        /// <returns>json</returns>
        public static string ToJson(string codd)
        {
            string p1 = Unescape(codd);
            string p2 = Regex.Replace(p1, @"(\{|\}\,|\]\,|\""\,|[eE]?[+\-]?[\d.]+\,|[ntf]\,|true\,|false\,)([^\""\{\}\[\]\,\:]*)\:", "$1\"$2\":");
            string p3 = Regex.Replace(Regex.Replace(p2, @"([\[\,\:])n([\]\,\}])", "$1null$2"), @"([\[\,\:])n([\]\,\}])", "$1null$2");
            string p4 = Regex.Replace(Regex.Replace(p3, @"([\[\,\:])t([\]\,\}])", "$1true$2"), @"([\[\,\:])t([\]\,\}])", "$1true$2");
            string p5 = Regex.Replace(Regex.Replace(p4, @"([\[\,\:])f([\]\,\}])", "$1false$2"), @"([\[\,\:])f([\]\,\}])", "$1false$2");

            return p5;
        }

        /// <summary>
        /// Convert JCODD to object directly
        /// </summary>
        /// <typeparam name="T">own type</typeparam>
        /// <param name="codd">jcodd</param>
        /// <returns>own object</returns>
        public static T? Parse<T>(string codd)
        {
            string json = ToJson(codd);

            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Return to be escaped unicode character from char code
        /// </summary>
        /// <param name="cc">char code</param>
        /// <returns>escaped</returns>
        public static string Esc(int cc)
        {
            if (cc < 0x20 || cc > 0x7e)
            {
                string x16 = cc.ToString("X");
                string ex;
                if (x16.Length > 2) ex = "%u" + x16.PadLeft(4, '0').ToUpper();
                else ex = "%" + x16.PadLeft(2, '0').ToUpper();
                return ex;
            }
            else return Convert.ToChar(cc).ToString();
        }

        /// <summary>
        /// Return to be escaped unicode characters in string
        /// </summary>
        /// <param name="str">unescaped</param>
        /// <returns>escaped</returns>
        public static string Escape(string str)
        {
            char[] chars = str.ToCharArray();
            string escaped = "";
            for (int i = 0; i < chars.Length; i++)
            {
                escaped += Esc(chars[i]);
                Serilog.Log.Logger.Debug("escaped: {escaped}", escaped);
            }
            return escaped;
        }

        /// <summary>
        /// Return to be unescaped unicode characters in string
        /// </summary>
        /// <param name="str">escaped</param>
        /// <returns></returns>
        public static string Unescape(string str)
        {
            return Regex.Replace(str, @"%u([0-9A-F]{4})", new MatchEvaluator(match =>
                ((char)int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString())
            );
        }
    }

    public class Jcodd<T> : Jcodd {

        private T _obj;
        public T? Obj { get { return _obj; } }
        public string Json { get { return JsonSerializer.Serialize(_obj); } }
        public string Code { get { return Codify(_obj); } }
        public string Base64 { get { return Convert.ToBase64String(Encoding.UTF8.GetBytes(Code)); } }

        /// <summary>
        /// Set object
        /// </summary>
        /// <param name="obj"></param>
        public Jcodd(T obj) {
            _obj = obj;
        }

        /// <summary>
        /// Set object from any code types
        /// Need to be handled exception
        /// </summary>
        /// <param name="code">BASE64 or JCODD or JSON code</param>
        public Jcodd(string code)
        {
            _obj = TryParse(code) ?? default;
        }

        public static T? TryParse(string code)
        {
            try
            {
                return Parse<T>(code);
            }
            catch
            {
                // do nothing
            }
            try
            {
                return Parse<T>(Encoding.UTF8.GetString(Convert.FromBase64String(code)));
            }
            catch
            {
                // do nothing
            }
            return default;
        }

        /// <summary>
        /// Set object from any code types
        /// Exception safe method. returns null on failure
        /// </summary>
        /// <param name="code">BASE64 or JCODD or JSON code</param>
        public static Jcodd<T>? Parse(string code)
        {
            var parsed = TryParse(code);
            return parsed != null ? new Jcodd<T>(parsed) : null;
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
