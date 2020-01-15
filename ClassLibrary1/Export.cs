namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    public class Export : IDisposable
    {
        private readonly XmlWriter writer;

        public Export(string outputFileName)
        {
            this.writer = XmlWriter.Create(outputFileName, new XmlWriterSettings { Async = true });
        }

        public async Task Write()
        {
            await this.writer.WriteStartElementAsync(null, "collection", null);

            foreach (var media in new Library())
            {
                await this.WriteMedia(media);
            }

            await this.writer.WriteEndElementAsync();
        }

        private async Task WriteMedia(Media media)
        {
            await this.writer.WriteStartElementAsync(null, "media", null);

            foreach (var attribute in media)
            {
                await this.WriteAttribute(attribute);
            }

            await this.writer.WriteEndElementAsync();
        }

        private async Task WriteAttribute(KeyValuePair<string, string> kv)
        {
            var value = Clean(kv.Value);

            if (!string.IsNullOrWhiteSpace(value))
            {
                await this.writer.WriteAttributeStringAsync(null, XmlConvert.EncodeLocalName(kv.Key), null, value);
            }
        }

        private static string Clean(string info) => new string(info.Where(XmlConvert.IsXmlChar).ToArray());

        void IDisposable.Dispose() => this.writer.Dispose();
    }
}
