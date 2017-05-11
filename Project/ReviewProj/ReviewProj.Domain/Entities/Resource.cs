using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace ReviewProj.Domain.Entities
{
    public class Resource
    {
        public Resource()
        {
            Type = ResourceType.SecondaryImage;
        }

        public Resource(HttpPostedFileBase file, ResourceType type, string parentDir)
        {
            if (file == null)
                throw new ArgumentException("public Resource(HttpPostedFileBase file, ResourceType type)");


            string fmt = Path.GetExtension(file.FileName);
            fmt = fmt.ToLower();
            switch (fmt)
            {
                case "png":
                    this.Format = ResourceFormat.PNG;
                    break;
                case "tiff":
                    this.Format = ResourceFormat.TIFF;
                    break;
                case "bmp":
                    this.Format = ResourceFormat.BMP;
                    break;
                case "gif":
                    this.Format = ResourceFormat.GIF;
                    break;
                default:
                    this.Format = ResourceFormat.JPEG;
                    break;
            }

            this.DataPath = Guid.NewGuid().ToString() + fmt;
            string path = Path.Combine(parentDir, this.DataPath);
            file.SaveAs(path);

            this.Type = type;
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
