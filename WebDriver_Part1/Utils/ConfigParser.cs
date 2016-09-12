using System.Text;
using IniParser;
using IniParser.Model;


namespace WebDriver_Part1.Utils
{
    class ConfigParser
    {
        public static string[] ParseUser(string userAlias)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData data = parser.ReadFile(@"Conf\Logins.ini", Encoding.UTF8);
            KeyDataCollection user = data[userAlias];
            string[] values = new string[3];
            values[0] = user["login"];
            values[1] = user["password"];
            values[2] = user["domain"];
            return values;
        }

        public static string[] ParseLetter(string letterName)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData data = parser.ReadFile(@"Conf\Letters.ini", Encoding.UTF8);
            KeyDataCollection letter = data[letterName];
            string[] values = new string[3];
            values[0] = letter["to"];
            values[1] = letter["subject"];
            values[2] = letter["body"].Replace("\\n\\r", "\n\r");
            return values;
        }
    }
}
