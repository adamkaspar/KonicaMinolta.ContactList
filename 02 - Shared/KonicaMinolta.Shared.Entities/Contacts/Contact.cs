using System.ComponentModel.DataAnnotations;

namespace KonicaMinolta.Shared.Entities.Contacts;

public class Contact : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string Surname { get; set; }

    public int Age { get; set; }

    public int Phone { get; set; }

    public bool IsActive{ get; set;}
}