//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Loners.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChatBoard
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string FromUser { get; set; }
        public string DateTimeOfMessage { get; set; }
    }
}
