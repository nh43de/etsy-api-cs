using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EtsyApi.Models
{
    public class ListingImage
    {
        /// <summary>
        /// The numeric ID of the listing image.
        /// </summary>
        public int listing_image_id { get; set; }

        /// <summary>
        /// The image's average color, in webhex notation.
        /// </summary>
        public string hex_code { get; set; }

        /// <summary>
        /// The image's average red value, 0-255 (RGB color).
        /// </summary>
        public int red { get; set; }

        /// <summary>
        /// The image's average green value, 0-255 (RGB color).
        /// </summary>
        public int green { get; set; }

        /// <summary>
        /// The image's average blue value, 0-255 (RGB color).
        /// </summary>
        public int blue { get; set; }

        /// <summary>
        /// The image's average hue, 0-360 (HSV color).
        /// </summary>
        public int hue { get; set; }

        /// <summary>
        /// The image's average saturation, 0-100 (HSV color).
        /// </summary>
        public int saturation { get; set; }

        /// <summary>
        /// The image's average brightness, 0-100 (HSV color).
        /// </summary>
        public int brightness { get; set; }

        /// <summary>
        /// True if the image is in black & white.
        /// </summary>
        public bool is_black_and_white { get; set; }

        /// <summary>
        /// Creation time, in epoch seconds.
        /// </summary>
        public float creation_tsz { get; set; }

        /// <summary>
        /// The numeric value of the listing id the image belongs to.
        /// </summary>
        public int listing_id { get; set; }

        /// <summary>
        /// Display order.
        /// </summary>
        public int rank { get; set; }

        /// <summary>
        /// The url to a 75x75 thumbnail of the image.
        /// </summary>
        public string url_75x75 { get; set; }

        /// <summary>
        /// The url to a 170x135 thumbnail of the image.
        /// </summary>
        public string url_170x135 { get; set; }

        /// <summary>
        /// The url to a thumbnail of the image, no more than 570px width by variable height.
        /// </summary>
        public string url_570xN { get; set; }

        /// <summary>
        /// The url to the full-size image, up to 3000px in each dimension.
        /// </summary>
        public string url_fullxfull { get; set; }

        /// <summary>
        /// Height of the image returned by url_fullxfull.
        /// </summary>
        public int full_height { get; set; }

        /// <summary>
        /// Width of the image returned by url_fullxfull.
        /// </summary>
        public int full_width { get; set; }
    }
}