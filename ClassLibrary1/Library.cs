namespace ClassLibrary1
{
    using System.Collections;
    using System.Collections.Generic;
    using WMPLib;

    internal class Library : IEnumerable<Media>
    {
        IEnumerator<Media> IEnumerable<Media>.GetEnumerator()
        {
            var playlist = new WindowsMediaPlayer().mediaCollection.getAll();
            var playlistCount = playlist.count;

            for (int mediaNumber = 0; mediaNumber < playlistCount; mediaNumber++)
            {
                yield return new Media(playlist.Item[mediaNumber]);

                // Otherwise items above ~100 are empty (no attributes).
                playlist = new WindowsMediaPlayer().mediaCollection.getAll();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Media>)this).GetEnumerator();
    }
}
