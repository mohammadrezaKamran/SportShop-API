namespace Shop.Query.Products.DTOs;

public class FilterDetailsDto
{
	public List<string>? AllColors { get; set; }
	public List<string>? AllSizes { get; set; }
	public List<string>? AllBrands { get; set; }
}