using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Loners.Models
{
    public class EditModel
    {
        public int UserID { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Day")]
        public int DateDay { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Month")]
        public int DateMonth { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Year")]
        public int DateYear { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("About Me")]
        public string AboutMe { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("MBTI")]
        public string MBTI { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("Biorytmus")]
        public string Biorytmus { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("InterestsHobbies")]
        public string InterestsHobbies { get; set; }
    }
}
