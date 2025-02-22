using System;

namespace Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? ModifiedTimeStamp { get; set; }
}
