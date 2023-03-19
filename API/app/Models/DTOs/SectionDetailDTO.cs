namespace app.Models.DTOs
{
    public class SectionDetailDTO
    {
        public int Id { get;set; }
        public string Title { get;set; }
        public int OrderNumber { get; set; }
        public IEnumerable<ForumDetailDTO> Forums { get;set; }
    }
}