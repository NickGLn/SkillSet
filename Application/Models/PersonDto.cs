namespace SkillSet.Application.Models
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public List<SkillDto> Skills { get; set; }
    }
}