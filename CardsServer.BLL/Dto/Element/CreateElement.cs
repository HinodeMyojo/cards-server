﻿namespace CardsServer.BLL.Dto.Element
{
    public class CreateElement
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
        public int? ImageString { get; set; }
    }
}
