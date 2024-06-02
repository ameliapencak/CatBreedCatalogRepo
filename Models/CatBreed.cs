namespace CatBreedCatalog.Models
{
    public class CatBreed
    {
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Origin { get; set; }
		public string? Temperament { get; set; }
		public byte[]? ImageData { get; set; }

		public CatBreed() { }

		public CatBreed(string name, string description, string origin, string temperament, byte[] imageData)
		{
			Name = name;
			Description = description;
			Origin = origin;
			Temperament = temperament;
			ImageData = imageData;
		}
	}



}
