using System.Xml;
using System.Xml.Linq;

namespace LinQTraining.LinqExtensions
{
    public static class LinQToXML
    {
        public static void Run()
        {
            var xml = new StringReader(@"
<notes>
<note>
<to>Tove 1</to>
<from>Jani 1</from>
<heading>Reminder 1</heading>
<body>1. Don't forget me this weekend!</body>
</note>

<note>
<to>Tove 2</to>
<from>Jani 2</from>
<heading>Reminder 2</heading>
<body>2. Don't forget me this weekend!</body>
</note>

<note>
<to>Tove 3</to>
<from>Jani 3</from>
<heading>Reminder 3</heading>
<body>3. Don't forget me this weekend!</body>
</note>
</notes>
");
            var xdocument = XDocument.Load(xml);

            Console.WriteLine("===============");
            var rootNote = xdocument.Root;
            Console.WriteLine(rootNote);

            Console.WriteLine("===============");
            var notes = rootNote.Elements("note");
            foreach (var note in notes) Console.WriteLine(note);

            Console.WriteLine("===============");
            var filtered = rootNote.Elements()
                .Where(note => note.Element("to")?.Value == "Tove 3")
                .FirstOrDefault();

            filtered = (from note in rootNote.Elements()
                        where note.Element("to")?.Value == "Tove 3"
                        select note).FirstOrDefault();

            Console.WriteLine(filtered);

            Console.WriteLine("===============");
            var newNotes = rootNote.Elements()
                .Select((ele, idx) =>
                {
                    var newEle = new XElement(ele);
                    newEle.SetElementValue("to", $"To {idx}");
                    return newEle;
                }).OrderByDescending(ele => ele.Element("to").Value);

            var stringWriter = new StringWriter();
            var newRootNote = new XElement("notes", newNotes);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                Indent = true
            }))
            {
                newRootNote.WriteTo(xmlWriter);
            }

            Console.WriteLine(stringWriter.ToString());
        }

        public class Note
        {
            public string To { get; set; }
            public string From { get; set; }
            public string Heading { get; set; }
            public string Body { get; set; }
        }
    }
}
