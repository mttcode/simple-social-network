using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Loners.Models
{
    public class ChatModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("Message")]
        public string Message { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("FromUser")]
        public string FromUser { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayName("Date time of message")]
        public string DateTimeOfMessage { get; set; }
    }
}
