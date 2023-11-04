using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BigStar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigStar.Pages.Forms
{
  [BindProperties]
  public class PriceYourCardsModel : PageModel
  {
    public TradeCard Card { get; set; }
    public string Message { get; set; } = "No card found!";
    public List<SelectListItem> CardItems { get; set; }
    public String CardName { get; set; }
    public List<SelectListItem> Conditions { get; set; }
    public Condition SelectedCondition { get; set; }

    public void SetupLists()
    {
      var orderedCards = new CardSource().CardsOrderedByName;
      CardItems = orderedCards.Select(x => new SelectListItem(text: x.CardName, value: x.CardName)).ToList();

      var conditionNames = Enum.GetNames(typeof(Condition));
      Conditions = conditionNames.Select(c => new SelectListItem(text: c, value: c)).ToList();
    }
    public void OnGet()
    {
      SetupLists();
    }
    public void OnPost()
    {
      SetupLists();

      // var offerPrice = 12.50 * Card.CardCount;

      var offerPrice = CardSource.GetOfferPrice(CardName, SelectedCondition);
      Message = $"Our offer for {Card.CardCount} {CardName} card(s) in {Card.Condition} condition = {(offerPrice):C} USD ";
    }
  }
}
