﻿namespace Gemma.Basket.API.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }

    }
}
