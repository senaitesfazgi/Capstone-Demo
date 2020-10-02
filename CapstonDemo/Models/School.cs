using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstonDemo.Models
{
    [Table("school")]
    public partial class School
    {
        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]

        [Column(TypeName = "char(12)")]
        public string SchoolName { get; set; }

        [Column(TypeName = "char(12)")]
        public string SchoolAddress { get; set; }

      

        [Column("StudentID", TypeName = "int(10)")]
        public int StudentID { get; set; }
        [Required]

        // This attribute specifies which database field is the foreign key. Typically in the child (many side of the 1-many).
        [ForeignKey(nameof(StudentID))]

        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Student.Schools))]
        public virtual Student Student { get; set; }
    }
}
