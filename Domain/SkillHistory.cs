namespace SkillSet.Domain
{
    public class SkillHistory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public long SkillId { get; set; }
        public DateTime ActualFromDate { get; set; }
    }
}
