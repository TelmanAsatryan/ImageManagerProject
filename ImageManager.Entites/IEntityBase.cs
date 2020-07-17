using System;

namespace ImageManager.Entities
{
    public interface IEntityBase
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
