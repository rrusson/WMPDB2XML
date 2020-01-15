namespace ClassLibrary1
{
    using System.Collections;
    using System.Collections.Generic;
    using WMPLib;

    internal class Media : IEnumerable<KeyValuePair<string, string>>
    {
        private IWMPMedia media;

        internal Media(IWMPMedia media)
        {
            this.media = media;
        }

        internal string Name => this.media.name;

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            var attributeCount = this.media.attributeCount;
            for (int attributeNumber = 0; attributeNumber < attributeCount; attributeNumber++)
            {
                yield return KeyValuePair.Create(this.media.getAttributeName(attributeNumber), this.media.getItemInfo(this.media.getAttributeName(attributeNumber)));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<KeyValuePair<string, string>>)this).GetEnumerator();
    }
}
