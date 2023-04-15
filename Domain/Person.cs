﻿using System.Collections.Generic;

namespace SkillSet.Domain
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public IEnumerable<Skill>? Skills { get; set; }
    }
}
