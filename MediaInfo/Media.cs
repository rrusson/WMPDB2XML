using MediaInfo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WMPLib;

namespace MediaInfo
{
    internal class Media : IEnumerable<KeyValuePair<string, string>>
    {
		private readonly IWMPMedia _media;
		private readonly string[] _strings = { "Bitrate", "Duration", "FileSize", "FileType", "IsVBR", "MediaType", "SourceURL", "Title", "TrackingID" };

		internal Media(IWMPMedia media)
        {
			this._media = media;
        }

        internal string Name => this._media.name;

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            var attributeCount = this._media.attributeCount;

            for (int attributeNumber = 0; attributeNumber < attributeCount; attributeNumber++)
            {
                string key = this._media.getAttributeName(attributeNumber);

                if (_strings.Contains(key))
                {
					yield return KeyValuePair.Create(key, this._media.getItemInfo(key));
				}
			}
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<KeyValuePair<string, string>>)this).GetEnumerator();
    }
}
