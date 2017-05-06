using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ReviewProj.Domain.Entities
{
    public class Resource
    {
        Resource()
        {
            Type = ResourceType.SecondaryImage;
        }

        Resource(Image image, bool isMain = false)
        {
            Type = isMain ? ResourceType.MainImage : ResourceType.SecondaryImage;
            // TODO: save image inside some local folder
            // and fill all other fields
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string DataPath { get; set; }

        [Required]
        public ResourceType Type { get; set; }

        [Required]
        public ResourceFormat Format { get; set; }
    }

    public enum ResourceType
    {
        MainImage,
        SecondaryImage
    }

    public enum ResourceFormat
    {
        PNG,
        JPEG,
        GIF,
        BMP,
        TIFF
    }
}
