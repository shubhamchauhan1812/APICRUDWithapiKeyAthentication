using System.ComponentModel.DataAnnotations;

namespace APICRUDWithapiKeyAthentication.Models
{
    public class tblErrorDTO<T>
    {
        [Key]

        public int Id { get; set; }

        public bool Succeed { get; set; }

        public string ErrorMessage { get; set; }

        public T Result { get; set; }

        public int? ErrorCode { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
