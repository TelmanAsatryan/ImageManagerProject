﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Models.ImageStorageModels
{
    public class ImageStorageViewModel : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}