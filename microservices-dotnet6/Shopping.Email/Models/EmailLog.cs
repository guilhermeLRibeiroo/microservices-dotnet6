using Shopping.Email.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Email.Models
{
    [Table("email_log")]
    public class EmailLog
        : BaseEntity
    {
        [Column("email")]
        public string Email { get; set; }

        [Column("log")]
        public string Log { get; set; }

        [Column("sending_date")]
        public DateTime SendingDate { get; set; }
    }
}
