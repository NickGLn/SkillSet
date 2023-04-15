namespace SkillSet.Domain
{
    public class Skill
    {
        public long Id { get; set; }
        public string Name { get; set; }    
        public byte Level { get; set; }
        public Person Person { get; set; }
    }
}
