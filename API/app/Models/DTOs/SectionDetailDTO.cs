namespace app.Models.DTOs
{
    public class SectionDetailDTO : SectionDTO
    {
        public IEnumerable<ForumDetailDTO> Forums { get;set; }
    }
}