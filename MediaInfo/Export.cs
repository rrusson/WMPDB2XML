using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace MediaInfo
{
	public class Export : IDisposable
	{
		private readonly XmlWriter _writer;
		private readonly string _mediaType;
		private bool _disposed;

		/// <summary>
		/// Outputs the media library to an XML file
		/// </summary>
		/// <param name="outputFileName">File path/name</param>
		/// <param name="mediaType">Optional filter for media type (e.g. "audio")</param>
		public Export(string outputFileName, string mediaType = null)
		{
			_writer = XmlWriter.Create(outputFileName, new XmlWriterSettings { Async = true, NewLineChars = Environment.NewLine, Indent = true });
			_mediaType = mediaType;
			_disposed = false;
		}

		/// <summary>
		/// Writes all items from the media library to an XML file
		/// </summary>
		public async Task Write()
		{
			// Add linebreaks at the end of each element
			await _writer.WriteStringAsync(Environment.NewLine);
			await _writer.WriteStartElementAsync(null, "Collection", null);
			await _writer.WriteStringAsync(Environment.NewLine);

			foreach (var media in new Library())
			{
				try
				{
					await this.WriteMedia(media);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error writing {media.Name} entry: {ex.Message}");
				}
			}

			await _writer.WriteEndElementAsync();
			await _writer.WriteStringAsync(Environment.NewLine); // Add linebreak at the end of the element
		}

		/// <summary>
		/// Writes a media element to the XML file (omiting the element if the media type is specified and does not match)
		/// </summary>
		/// <param name="media">An individual media element</param>
		private async Task WriteMedia(Media media)
		{
			if (!string.IsNullOrWhiteSpace(_mediaType) && !media.Any(m => m.Key.Equals("MediaType") && m.Value.Equals(_mediaType)))
			{
				return;
			}

			await _writer.WriteStartElementAsync(null, "media", null);

			foreach (var attribute in media)
			{
				await this.WriteAttribute(attribute);
			}

			await _writer.WriteEndElementAsync();
			await _writer.WriteStringAsync(Environment.NewLine); // Add linebreak at the end of the element
		}

		/// <summary>
		/// Writes an individual attribute to the XML file
		/// </summary>
		/// <param name="kv"></param>
		/// <returns></returns>
		private async Task WriteAttribute(KeyValuePair<string, string> kv)
		{
			var value = Clean(kv.Value);

			if (!string.IsNullOrWhiteSpace(value))
			{
				await _writer.WriteAttributeStringAsync(null, XmlConvert.EncodeLocalName(kv.Key), null, value);
			}
		}

		/// <summary>
		/// Sanitizes the input string to ensure it is valid XML
		/// </summary>
		/// <param name="info">String value to clean</param>
		/// <returns>An XML-safe string</returns>
		private static string Clean(string info) => new string(info.Where(XmlConvert.IsXmlChar).ToArray());

		#region IDisposable implementation
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				_writer.Dispose();
			}

			_disposed = true;
		}
		#endregion
	}
}
