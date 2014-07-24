using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class ImagesEndpoint : BaseEndpoint
    {
        internal ImagesEndpoint(BeatsMusicManager beatsMusicManager)
            : base(beatsMusicManager)
        {
        }

        /// <summary>
        ///     Gets the Uri to the artist's image at the given size
        /// </summary>
        /// <param name="artistId">Id representing the artist with the desired image</param>
        /// <param name="size">Size of the image desired</param>
        /// <returns>Uri to the artist's image at the given size</returns>
        public async Task<Uri> GetArtistImageUri(string artistId, ImageSize size = ImageSize.Medium)
        {
            ValidateResourceId(artistId);

            string method = string.Format("artists/{0}/images/default",
                artistId);

            Uri response = await GetResourceImageUri(method, size);

            return response;
        }

        /// <summary>
        ///     Gets the Uri to the album's image at the given size
        /// </summary>
        /// <param name="albumId">Id representing the album with the desired image</param>
        /// <param name="size">Size of the image desired</param>
        /// <returns>Uri to the album's image at the given size</returns>
        public async Task<Uri> GetAlbumImageUri(string albumId, ImageSize size = ImageSize.Medium)
        {
            ValidateResourceId(albumId);

            string method = string.Format("albums/{0}/images/default", albumId);

            Uri response = await GetResourceImageUri(method, size);

            return response;
        }

        /// <summary>
        ///     Gets the Uri to the track's image at the given size
        /// </summary>
        /// <param name="trackId">Id representing the album with the desired image</param>
        /// <param name="size">Size of the image desired</param>
        /// <returns>Uri to the track's image at the given size</returns>
        public async Task<Uri> GetTrackImageUri(string trackId, ImageSize size = ImageSize.Medium)
        {
            ValidateResourceId(trackId);

            string method = string.Format("tracks/{0}/images/default", trackId);

            Uri response = await GetResourceImageUri(method, size);

            return response;
        }

        private static void ValidateResourceId(string resourceId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(resourceId),
                "resource id is null or empty.");
        }

        private async Task<Uri> GetResourceImageUri(string method, ImageSize size)
        {
            var dataParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("size",
                    ParamValueAttributeHelper.GetParamValueOfEnumAttribute<ImageSize>(size))
            };

            Uri response =
                await
                    BeatsMusicManager.GetDataUri(method, dataParams, false);
            return response;
        }
    }
}