namespace BlazorSozluk.Common.Models.Queries;

public class BaseFooterRateViewModel
{
    public VoteTypes VoteTypes { get; set; }
}

public class BaseFooterFavoritedViewModel
{
    public bool IsFavorited { get; set; }
    public int FavoritedCount { get; set; }
}

public class BaseFooterRateFavoritedViewModel  : BaseFooterFavoritedViewModel
{
    public VoteTypes VoteTypes { get; set; }
}