using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace APICRUDWithapiKeyAthentication.Models
{
    public class tbl_ApiKey
    {
        [Key]
        public int Id { get; set; }
        public  string ApiKey { get; set; }
        public DateTime? CreatedAt { get; set; }
      
        public  string UserId { get; set; }
        public bool IsRevoked { get; set; }
    }
}
