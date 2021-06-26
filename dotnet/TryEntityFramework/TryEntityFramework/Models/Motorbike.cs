namespace TryEntityFramework.Models
{
    public class Motorbike : Entity
    {
        public string Brand { get; set; }
        public int ReleaseYear { get; set; }

        public virtual Person Owner { get; set; }
    }
}
