namespace Entities.ViewModel
{
    public class TreeViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; } = false;

        public static implicit operator TreeViewModel(Category c)
        {
            return new TreeViewModel
            {
               Id = c.Id,
               ParentId = c.ParentId,
               Name = c.Name
            };
        }
        public static implicit operator TreeViewModel(BlogCategory c)
        {
            return new TreeViewModel
            {
                Id = c.Id,
                ParentId = c.Parent.Id,
                Name = c.Name
            };
        }
    }
}
