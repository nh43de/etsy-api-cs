namespace EtsyApi.Models
{
    public class Taxonomy
    {
        public int id { get; set; }
        public int level { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public int parent_id { get; set; }
        public string path { get; set; }
        public int category_id { get; set; }
        public Taxonomy[] children { get; set; }
        public int[] children_ids { get; set; }
        public int[] full_path_taxonomy_ids { get; set; }
    }
}
