﻿namespace DbLevel.Models
{
    public class ProductStorageDto
    {
        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
