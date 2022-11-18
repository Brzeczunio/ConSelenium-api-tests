﻿namespace ConSelenium.Api.Client.Models.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
