namespace SkillSet.Application.Models
{
    public class UpdatePersonDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<UpdateSkillDto> Skills { get; set; }
    }
}