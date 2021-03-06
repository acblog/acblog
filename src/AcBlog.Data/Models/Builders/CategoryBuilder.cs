﻿using System;
using System.Collections.Generic;

namespace AcBlog.Data.Models.Builders
{
    public class CategoryBuilder
    {
        List<string> Inner { get; set; } = new List<string>();

        public CategoryBuilder AddSubCategory(params string[] names)
        {
            foreach (var name in names)
            {
                if (!name.Contains('/'))
                {
                    Inner.Add(name);
                }
                else
                {
                    throw new Exception($"Invalid category name: {name}.");
                }
            }
            return this;
        }

        public CategoryBuilder RemoveSubCategory()
        {
            if (Inner.Count > 0)
            {
                Inner.RemoveAt(Inner.Count - 1);
                return this;
            }
            else
            {
                throw new Exception($"Empty category.");
            }
        }

        public bool IsEmpty => Inner.Count > 0;

        public Category Build() => new Category { Items = Inner.ToArray() };

        public static Category FromString(string source)
        {
            return new Category { Items = source.Split('/', StringSplitOptions.RemoveEmptyEntries) };
        }
    }
}
