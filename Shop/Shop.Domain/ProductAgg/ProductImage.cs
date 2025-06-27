using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAgg
{
    public class ProductImage : BaseEntity
    {
		public ProductImage(string imageName, int sequence, string altText)
		{
			NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));

			ImageName = imageName;
			Sequence = sequence;
			AltText = altText;
		}
		public string AltText { get; private set; }
		public long ProductId { get; internal set; }
        public string ImageName { get; private set; }
        public int Sequence { get; private set; }
    }
}