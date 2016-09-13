using WebDriver_Part1.Utils;

namespace WebDriver_Part1.BusinessObjects
{
    class Letter
    {
        private const string _defaultMail = "default";
        private string _to;
        private string _subject;
        private string _body;

        public Letter(string letterName = _defaultMail)
        {
            string[] letterFields = ConfigParser.ParseLetter(letterName);
            Addressee = letterFields[0];
            Subject = letterFields[1];
            Body = letterFields[2];
        }

        public string Addressee
        {
            private set { _to = value; }
            get { return _to; }
        }

        public string Subject
        {
            private set { _subject = value; }
            get { return _subject; }
        }

        public string Body
        {
            private set { _body = value; }
            get { return _body; }
        }
    }
}
