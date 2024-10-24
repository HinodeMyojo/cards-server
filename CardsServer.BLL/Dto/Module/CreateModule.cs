﻿using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Dto.Module
{
    public class CreateModule
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Private { get; set; }
        public bool IsDraft { get; set; }
        public List<CreateElement> Elements { get; set; } = [];
    }
}